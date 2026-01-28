using Finisar.SQLite;
using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CapsuleDaysImporter
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        #region TextBox DragDrop
        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (FileList[0].ToLower().EndsWith(".txt"))
                textBox1.Text = FileList[0];
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;

        }
        #endregion

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (!File.Exists(textBox1.Text))
            {
                MessageBox.Show("File doesnt exist!");
                return;
            }

            string lastDate = string.Empty;
            string[] lines = File.ReadAllLines(textBox1.Text);

            SQLiteCommand CMD;
            SQLiteParameterCollection Params;
            
            string insertSQL = "INSERT INTO [logs] (date,modified,guid,user,color,html,text,image_count,attach_count,property) VALUES (@date,@modified,@guid,@user,@color,@html,@text,@image_count,@attach_count,@property)";

            int count = 0;

            foreach (string line in lines)
            {
                string item = line.Trim();

                if (string.IsNullOrEmpty(item))
                    continue;

                if (item.StartsWith("~"))
                { lastDate = item.Substring(1); continue; }

                Record x = ModifyString(item);

                CMD = new SQLiteCommand();
                Params = CMD.Parameters;

                Params.Add("@date", string.Format("{0} {1}:00", lastDate,x.time ));
                Params.Add("@modified", string.Format("{0} {1}:00", lastDate, x.time));
                Params.Add("@guid", Guid.NewGuid().ToString());
                Params.Add("@user", "New User"); 
                Params.Add("@color", "#808080");
                Params.Add("@html", x.html);
                Params.Add("@text", x.text);
                Params.Add("@image_count", "0"); 
                Params.Add("@attach_count", "0");
                Params.Add("@property", "[]");

               
                CMD.CommandText = insertSQL;
                CMD.CommandType = CommandType.Text;
                CMD.Connection = Program.objConn;
                CMD.Prepare();
                CMD.ExecuteScalar();

                if (CMD != null)
                {
                    CMD.Dispose();
                }

                count++;
            }

            MessageBox.Show("imported " + count + " records");
        }

        public Record ModifyString(string input)
        {
            Record rec = new Record();
            rec.html = rec.text = GetArticleWOtime(input);

            string timePattern = @"^(\d{1,2}:\d{2})";
            string pattern = @"(#[\w]+(?:\s#[\w]+)*)$";

            var timeMatch = Regex.Match(input, timePattern);
            var match = Regex.Match(rec.text, pattern);

            if (timeMatch.Success)
            {
                string[] timeParts = timeMatch.Value.Split(':');
                string hours = timeParts[0].PadLeft(2, '0');
                string minutes = timeParts[1];
                rec.time = string.Format("{0}:{1}", hours, minutes);
            }
            else 
                return null;

            if (match.Success)
            {
                string hashtags = match.Value;
                rec.html = rec.text.Replace(hashtags, "<div><br></div><div>" + hashtags + "</div>");
            }

            return rec;
        }


        public static string GetArticleWOtime(string input)
        {
            int spaceIndex = input.IndexOf(' ');

            if (spaceIndex == -1)
                return input;
            else 
                return input.Substring(spaceIndex + 1);

        }

        public class Record
        {
            public string html { get; set; }
            public string text { get; set; }
            public string time { get; set; }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((Program.objConn != null))
            {
                Program.objConn.Close();
                Program.objConn.Dispose();
            }
        }
    }
}


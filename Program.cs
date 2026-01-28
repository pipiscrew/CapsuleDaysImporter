using Finisar.SQLite;
using System;
using System.IO;
using System.Windows.Forms;

namespace CapsuleDaysImporter
{
    static class Program
    {
        public static SQLiteConnection objConn;
        private static string dbaseFile = Application.StartupPath + "\\data.db";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!File.Exists(dbaseFile))
            {
                MessageBox.Show("Please move this executable near CapsuleDays dbase");
                return;
            } else { 
                File.Copy(dbaseFile,dbaseFile + ".bak",true); //backup
            }

            Connect();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

         private static void Connect()
        {
            try
            {
                objConn = new SQLiteConnection("Data Source=" + dbaseFile + ";Version=3;");
                objConn.Open();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

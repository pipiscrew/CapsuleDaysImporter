using Finisar.SQLite;
using System;
using System.Diagnostics;
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
                Mes("Please move this executable near CapsuleDays dbase.");
                return;
            }
            else if (IsProcessRunning("CapsuleDays"))
            {
                Mes("cannot use this application while CapsuleDays is running, please close CapsuleDays.", MessageBoxIcon.Exclamation);
                return;
            }
            else
            {

                File.Copy(dbaseFile, dbaseFile + ".bak", true); //backup
            }

            ConnectDB();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static void ConnectDB()
        {
            try
            {
                objConn = new SQLiteConnection("Data Source=" + dbaseFile + ";Version=3;");
                objConn.Open();
            }
            catch (SQLiteException ex)
            {
                Mes(ex.Message, MessageBoxIcon.Error);
            }
        }

        private static bool IsProcessRunning(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length > 0;
        }

        public static DialogResult Mes(string descr, MessageBoxIcon icon = MessageBoxIcon.Information, MessageBoxButtons butt = MessageBoxButtons.OK)
        {
            if (descr.Length > 0)
                return MessageBox.Show(descr, Application.ProductName, butt, icon);
            else
                return DialogResult.OK;

        }
    }
}

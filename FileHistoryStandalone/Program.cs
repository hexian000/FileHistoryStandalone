using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileHistoryStandalone
{
    static class Program
    {
        internal const string Win32PathPrefix = @"\\?\";
        internal static Repository Repo = null;
        internal static DocLibrary DocLib = null;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.Run(new FrmManager());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            StringBuilder log = new StringBuilder();
            Exception ex = e.ExceptionObject as Exception;
            string msg;
            if (ex != null)
            {
                msg = ex.Message;
                log.AppendLine("Message: " + msg);
                log.AppendLine("Source: " + ex.Source);
                log.AppendLine("StackTrace: " + ex.StackTrace);
                Clipboard.SetText(log.ToString());
            }
            else msg = e.ExceptionObject.ToString();
            MessageBox.Show("发生严重错误：" + msg + Environment.NewLine + "详细信息已复制到剪贴板", "File History Standalone", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal static string GetIdByName(string fullName)
        {
            string name = fullName.Trim().ToLowerInvariant();
            if (name.StartsWith(Win32PathPrefix)) name = name.Substring(4);
            return name;
        }
    }
}

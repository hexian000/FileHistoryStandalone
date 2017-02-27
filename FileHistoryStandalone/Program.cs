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
            Application.ThreadException += Application_ThreadException;
            Application.Run(new FrmManager());
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            StringBuilder msg = new StringBuilder();
            msg.AppendLine("Message: " + e.Exception.Message);
            msg.AppendLine("Source: " + e.Exception.Source);
            msg.AppendLine("StackTrace: " + e.Exception.StackTrace);
            Clipboard.SetText(msg.ToString());
            MessageBox.Show("发生严重错误：" + e.Exception.Message + Environment.NewLine + "详细信息已复制到剪贴板", "File History Standalone", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }
    }
}

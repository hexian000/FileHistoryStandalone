using System;
using System.Collections.Generic;
using System.IO;
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
        internal static string[] CommandLine;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            CommandLine = args;
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
            MessageBox.Show("发生严重错误：" + msg + Environment.NewLine + "详细信息已复制到剪贴板", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal static string GetIdByName(string fullName)
        {
            string name = fullName.Trim().ToLowerInvariant();
            if (name.StartsWith(Win32PathPrefix)) name = name.Substring(4);
            return name;
        }

        internal static IEnumerable<FileInfo> EnumerateFiles(string path)
        {
            IEnumerable<string> dirs;
            try
            {
                dirs = Directory.EnumerateDirectories(path);
            }
            catch { dirs = new List<string>(0); }
            foreach (var dir in dirs)
                foreach (var file in EnumerateFiles(dir))
                    yield return file;
            IEnumerable<FileInfo> files;
            try
            {
                DirectoryInfo thisdir = new DirectoryInfo(path);
                files = thisdir.EnumerateFiles();
            }
            catch { files = new List<FileInfo>(0); }
            foreach (var file in files)
            {
                yield return file;
            }
        }

        internal static void TrimEmptyDirs(string path)
        {
            try
            {
                if (Directory.EnumerateFileSystemEntries(path).Count() == 0)
                    Directory.Delete(path);
                else
                    foreach (var dir in Directory.EnumerateDirectories(path))
                        TrimEmptyDirs(dir);
            }
            catch { }
        }
    }
}

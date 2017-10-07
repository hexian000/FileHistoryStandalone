using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FileHistoryStandalone
{
	static class Program
	{
		internal const string Win32PathPrefix = @"\\?\";
		internal static Repository Repo = null;
		internal static DocLibrary DocLib = null;
		internal static string[] CommandLine;

		private static object logLock = new object();
		private static StreamWriter log = null;
		internal static void WriteDebugLog(string tag, object message)
		{
			lock (logLock)
			{
				if (log == null) return;
				log.Write($"{DateTime.Now.ToString("yyyyMMddHHmmss")}\t{tag}\t");
				if (message is Exception ex)
					log.WriteLine($"{ex.GetType().Name}: {ex.Message} {ex.StackTrace}");
				else if (message is string str)
					log.WriteLine($"{str}");
				else if (message != null)
					log.WriteLine($"{message.GetType().Name}: {message.ToString()}");
				else
					log.WriteLine($"null");
				log.Flush();
			}
		}

		internal static void InitDebugLog(string path)
		{
			lock (logLock)
			{
				log?.Dispose();
				log = new StreamWriter(new FileStream(path, FileMode.Create), Encoding.UTF8);
			}
		}

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			CommandLine = args;
			var single = new Mutex(true, "FileHistoryStandalone", out bool created);
			if (!created)
			{
				MessageBox.Show("FileHistoryStandalone已经在运行", "FileHistoryStandalone", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			//Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
			//Application.ThreadException += Application_ThreadException;
			//AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.BelowNormal;
			Application.Run(new FrmManager());
			lock (logLock)
			{
				log?.Dispose();
				log = null;
			}
		}

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			DoUnhandledException(e.Exception);
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			DoUnhandledException(e.ExceptionObject);
		}

		internal static void DoUnhandledException(object exception)
		{
			StringBuilder log = new StringBuilder();
			Exception ex = exception as Exception;
			string msg;
			if (ex != null)
			{
				msg = ex.Message;
				log.AppendLine("Message: " + msg);
				log.AppendLine("Source: " + ex.Source);
				log.AppendLine("StackTrace: " + ex.StackTrace);
				var th = new Thread((o) =>
				{
					Clipboard.SetText(o.ToString());
				});
				th.SetApartmentState(ApartmentState.STA);
				th.Start(log.ToString());
				th.Join();
				th = null;
			}
			else msg = exception.ToString();
			WriteDebugLog("BUGCHK", msg);
			Program.log?.Close();
			MessageBox.Show("发生严重错误：" + msg + Environment.NewLine + "详细信息已复制到剪贴板", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			Environment.Exit(1);
		}

		internal static string NtPath(string Win32Path)
		{
			if (Win32Path.StartsWith(Win32PathPrefix)) return Win32Path.Substring(Win32PathPrefix.Length);
			else return Win32Path;
		}

		internal static string Win32Path(string NtPath)
		{
			if (!NtPath.StartsWith(Win32PathPrefix)) return Win32PathPrefix + NtPath;
			else return NtPath;
		}

		internal static IEnumerable<FileInfo> EnumerateTreeFiles(string path)
		{
			IEnumerable<string> dirs;
			try
			{
				dirs = Directory.EnumerateDirectories(path);
			}
			catch (Exception ex)
			{
				WriteDebugLog("WARNING", ex);
				dirs = new List<string>(0);
			}
			foreach (var dir in dirs)
				foreach (var file in EnumerateTreeFiles(dir))
					yield return file;
			IEnumerable<FileInfo> files;
			try
			{
				DirectoryInfo thisdir = new DirectoryInfo(path);
				files = thisdir.EnumerateFiles();
			}
			catch (Exception ex)
			{
				WriteDebugLog("WARNING", ex);
				files = new List<FileInfo>(0);
			}
			foreach (var file in files)
			{
				yield return file;
			}
		}

		internal static int TrimEmptyDirs(string path)
		{
			int count = 0;
			try
			{
				foreach (var dir in Directory.EnumerateFileSystemEntries(path))
				{
					if (Directory.Exists(dir))
						count += TrimEmptyDirs(dir);
					else count++;
				}
				if (count == 0)
					Directory.Delete(path);
			}
			catch (Exception ex) { WriteDebugLog("ERROR", ex); }
			return count;
		}

		internal static string FormatSize(long size)
		{
			if (size < 1024) return size + " 字节";
			else if (size < 1048576) return (size / 1024.0).ToString("G3") + " KB";
			else if (size < 1073741824) return (size / 1048576.0).ToString("G3") + " MB";
			else return (size / 1073741824.0).ToString("G3") + " GB";
		}

		public static Version GetProgramVersion()
		{
			return new Version(2, 2, 0, 2);
		}
	}
}

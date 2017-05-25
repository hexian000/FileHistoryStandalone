using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace FileHistoryStandalone {
	class DocLibrary : IDisposable {
		private class OperBufItem {
			public OperBufItem(FileSystemEventArgs args) { Args = args; }
			public FileSystemEventArgs Args;
			public int Countdown = 5;
		}

		private List<string> DocPath;
		private List<FileSystemWatcher> DocWatcher;
		private List<OperBufItem> OperBuff;
		private Timer BufSync;
		private Repository Repo;

		public DocLibrary(Repository repo) {
			DocPath = new List<string>();
			DocWatcher = new List<FileSystemWatcher>();
			OperBuff = new List<OperBufItem>();
			BufSync = new Timer((o) => Sync(), null, 10000, 2000);
			Repo = repo;
		}

		public string Paths {
			get { return string.Join(Environment.NewLine, DocPath); }
			set {
				if (DocWatcher != null)
					foreach (var i in DocWatcher) i.Dispose();
				var libs = new List<string>(value.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
				var ids = new List<string>(libs.Select((s) => s.ToLowerInvariant()));
				for (var i = 0; i < ids.Count; i++)
					for (var j = 0; j < i; j++) {
						if (ids[i].StartsWith(ids[j]) || ids[j].StartsWith(ids[i]))
							throw new Exception("路径存在重复或相互包含");
					}
				foreach (var path in libs) {
					FileSystemWatcher watcher = new FileSystemWatcher(path);
					watcher.Changed += Watcher_Changed;
					watcher.Created += Watcher_Changed;
					watcher.Renamed += Watcher_Renamed;
					watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite;
					watcher.IncludeSubdirectories = true;
					watcher.EnableRaisingEvents = true;
					DocWatcher.Add(watcher);
				}
				DocPath = libs;
			}
		}

		public void ScanLibrary(CancellationToken cancel) {
			foreach (var i in DocPath) {
				Program.WriteDebugLog("INFO", $"ScannLib start at {i}");
				ScanLibraryInternal(cancel, i);
				if (cancel.IsCancellationRequested) {
					Program.WriteDebugLog("INFO", "ScannLib cancelling");
					return;
				}
			}
			Program.WriteDebugLog("INFO", "ScannLib complete");
		}

		private void ScanLibraryInternal(CancellationToken cancel, string path) {
			try {
				Repo.Synchronize(path, cancel);
				foreach (var dir in Directory.EnumerateDirectories(path)) {
					if (cancel.IsCancellationRequested) return;
					ScanLibraryInternal(cancel, Program.Win32Path(dir));
				}
			} catch (Exception ex) {
				Program.WriteDebugLog("WARNING", ex);
			}
		}

		private void Watcher_Changed(object sender, FileSystemEventArgs e) {
			Program.WriteDebugLog("INFO", $"Watcher {e.ChangeType}: {e.FullPath}");
			if (Program.Repo.IsExcluded(e.FullPath)) return;
			lock (OperBuff) {
				bool found = false;
				for (int j = 0; j < OperBuff.Count; j++) {
					var i = OperBuff[j];
					if (i.Args.FullPath.ToLowerInvariant() == e.FullPath.ToLowerInvariant()) {
						if (e.ChangeType == WatcherChangeTypes.Created
							|| e.ChangeType == WatcherChangeTypes.Changed) {
							if (i.Args.ChangeType == WatcherChangeTypes.Created
							|| i.Args.ChangeType == WatcherChangeTypes.Changed) {
								i.Countdown = 5;
								found = true;
							} else if (i.Args.ChangeType == WatcherChangeTypes.Deleted) {
								OperBuff.RemoveAt(j);
							}
						} else if (e.ChangeType == WatcherChangeTypes.Deleted) {
							OperBuff.RemoveAt(j);
						}
					}
				}
				if (!found) OperBuff.Add(new OperBufItem(e));
			}
		}

		private void Watcher_Renamed(object sender, RenamedEventArgs e) {
			if (e.ChangeType != WatcherChangeTypes.Renamed) return;
			if (Program.Repo.IsExcluded(e.FullPath)) return;
			lock (OperBuff) {
				bool found = false;
				for (int j = 0; j < OperBuff.Count; j++) {
					var i = OperBuff[j];
					if (i.Args.FullPath.ToLowerInvariant() == e.OldFullPath.ToLowerInvariant()) {
						if (i.Args.ChangeType == WatcherChangeTypes.Created
						|| i.Args.ChangeType == WatcherChangeTypes.Changed) {
							i.Countdown = 5;
							i.Args = new FileSystemEventArgs(i.Args.ChangeType,
								Path.GetDirectoryName(e.FullPath), e.Name);
							found = true;
						} else if (i.Args.ChangeType == WatcherChangeTypes.Renamed) {
							i.Args = new RenamedEventArgs(e.ChangeType,
								Path.GetDirectoryName(e.FullPath),
								e.Name, ((RenamedEventArgs)i.Args).OldName);
							i.Countdown = 5;
							found = true;
						}
					}
				}
				if (!found) OperBuff.Add(new OperBufItem(e));
			}

		}

		private void Sync(bool flush = false) {
			if (flush) Program.WriteDebugLog("INFO", "Flush begin");
			lock (OperBuff) {
				List<OperBufItem> alive = new List<OperBufItem>();
				foreach (var i in OperBuff) {
					i.Countdown--;
					if (flush || i.Countdown <= 0) {
						var e = i.Args;
						if (e.ChangeType == WatcherChangeTypes.Created
						|| e.ChangeType == WatcherChangeTypes.Changed) {
							Program.WriteDebugLog("INFO", $"Sync Copy {e.ChangeType}: {e.FullPath}");
							if (File.Exists(e.FullPath))
								try { Repo.MakeCopy(e.FullPath); } catch (Exception ex) { Program.WriteDebugLog("ERROR", ex); }
						} else if (e.ChangeType == WatcherChangeTypes.Renamed) {
							var e2 = (RenamedEventArgs)e;
							Program.WriteDebugLog("INFO", $"Sync Rename {e2.ChangeType}: {e2.OldFullPath} => {e2.FullPath}");
							if (File.Exists(e2.FullPath)) Repo.Rename(e2.OldFullPath, e2.FullPath);
							else if (Directory.Exists(e2.FullPath)) Repo.RenameDir(e2.OldFullPath, e2.FullPath);
						}
					} else alive.Add(i);
				}
				OperBuff.Clear();
				OperBuff.AddRange(alive);
			}
		}

		#region IDisposable Support
		private bool disposedValue = false; // 要检测冗余调用

		protected virtual void Dispose(bool disposing) {
			if (!disposedValue) {
				Program.WriteDebugLog("INFO", "DocLibrary disposing");
				if (disposing) {
					if (DocWatcher != null)
						foreach (var i in DocWatcher) i.Dispose();
					BufSync.Dispose();
					if (OperBuff != null)
						Sync(true);
				}

				DocPath = null;
				DocWatcher = null;
				BufSync = null;
				Repo = null;
				OperBuff = null;

				disposedValue = true;
			}
		}

		// TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
		// ~DocLibrary() {
		//   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
		//   Dispose(false);
		// }

		// 添加此代码以正确实现可处置模式。
		public void Dispose() {
			// 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
			Dispose(true);
			// TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}

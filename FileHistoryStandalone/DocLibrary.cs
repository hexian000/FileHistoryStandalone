using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileHistoryStandalone
{
    class DocLibrary : IDisposable
    {
        private List<string> DocPath;
        private List<FileSystemWatcher> DocWatcher;
        private Repository Repo;

        public DocLibrary(Repository repo)
        {
            DocPath = new List<string>();
            DocWatcher = new List<FileSystemWatcher>();
            Repo = repo;
        }

        public string Paths
        {
            get { return string.Join(Environment.NewLine, DocPath); }
            set
            {
                if (DocWatcher != null)
                    foreach (var i in DocWatcher) i.Dispose();
                var libs = new List<string>(value.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
                var ids = new List<string>(libs.Select(Program.GetIdByName));
                for (var i = 0; i < ids.Count; i++)
                    for (var j = 0; j < i; j++)
                    {
                        if (ids[i].StartsWith(ids[j]) || ids[j].StartsWith(ids[i]))
                            throw new Exception("路径存在重复或相互包含");
                    }
                foreach (var path in libs)
                {
                    string id = Program.GetIdByName(path);
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

        public void ScanLibrary()
        {
            foreach (var i in DocPath)
            {
                foreach (var doc in Program.EnumerateFiles(i))
                {
                    string id = doc.FullName.ToLowerInvariant();
                    if (Repo.HasCopy(id))
                    {
                        if (Repo.GetLatestCopyTimeUtc(id) < doc.LastWriteTimeUtc) Repo.MakeCopy(doc.FullName);
                    }
                    else Repo.MakeCopy(doc.FullName);
                }
            }
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created
                || e.ChangeType == WatcherChangeTypes.Changed)
            {
                new Task(() =>
                {
                    Thread.Sleep(10000);
                    if (File.Exists(e.FullPath)) Repo.MakeCopy(e.FullPath);
                });
            }
        }

        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Renamed)
            {
                if (File.Exists(e.FullPath)) Repo.Rename(e.OldFullPath, e.FullPath);
                else if (Directory.Exists(e.FullPath)) Repo.RenameDir(e.OldFullPath, e.FullPath);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (DocWatcher != null)
                        foreach (var i in DocWatcher) i.Dispose();
                }

                DocPath = null;
                DocWatcher = null;
                Repo = null;

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~DocLibrary() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}

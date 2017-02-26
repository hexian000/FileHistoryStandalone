using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHistoryStandalone
{
    class DocLibrary : IDisposable
    {
        private static IEnumerable<FileInfo> EnumerateFiles(string path)
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
                DocPath = new List<string>(value.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
                foreach (var path in DocPath)
                {
                    FileSystemWatcher watcher = new FileSystemWatcher(path);
                    watcher.Changed += Watcher_Changed;
                    watcher.Created += Watcher_Changed;
                    DocWatcher.Add(watcher);
                }
            }
        }

        public void ScanLibrary()
        {
            foreach (var i in DocPath)
            {
                foreach (var doc in EnumerateFiles(i))
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
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                Repo.MakeCopy(e.FullPath);
            }
            else if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                Repo.MakeCopy(e.FullPath);
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

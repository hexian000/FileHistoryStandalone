using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHistoryStandalone
{
    class Repository
    {
        private class RepoFile
        {
            public RepoFile(FileInfo file)
            {
                FullName = file.FullName;
                string name = Path.GetFileNameWithoutExtension(file.FullName), ext = file.Extension;
                int pos = name.LastIndexOf('_');
                Source = name.Substring(0, pos) + ext;
                LastModifiedTimeUtc = DateTime.FromFileTimeUtc(long.Parse(name.Substring(pos + 1), System.Globalization.NumberStyles.AllowHexSpecifier));
                Length = file.Length;
            }

            public readonly string FullName;
            public readonly string Source;
            public readonly DateTime LastModifiedTimeUtc;
            public readonly long Length;
        }

        private static IEnumerable<RepoFile> EnumerateRepoFiles(string path)
        {
            IEnumerable<string> dirs;
            try
            {
                dirs = Directory.EnumerateDirectories(path);
            }
            catch { dirs = new List<string>(0); }
            foreach (var dir in dirs)
                foreach (var file in EnumerateRepoFiles(dir))
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
                RepoFile f = null;
                try
                {
                    f = new RepoFile(file);
                }
                catch { }
                if (f != null) yield return f;
            }
        }

        private string RepoPath;
        private Dictionary<string, List<RepoFile>> Files;

        public event EventHandler<string> CopyMade;

        private Repository(string repoPath)
        {
            RepoPath = repoPath;
            Files = new Dictionary<string, List<RepoFile>>();
        }

        public static Repository Create(string path)
        {
            Directory.CreateDirectory(path);
            return new Repository(path);
        }

        public static Repository Open(string path)
        {
            Repository ret = new Repository(path);
            foreach (var i in EnumerateRepoFiles(path))
                ret.AddRepoFile(i);
            return ret;
        }

        private void AddRepoFile(RepoFile file)
        {
            string src = file.Source.ToLowerInvariant();
            if (Files.ContainsKey(src))
                Files[src].Add(file);
            else
                Files.Add(src, new List<RepoFile>(new RepoFile[] { file }));
        }

        public int FileCount => Files.Count;

        public void MakeCopy(string source)
        {
            lock (Files)
            {
                string dir = Path.Combine(RepoPath, Path.GetDirectoryName(source).Replace(":", ""));
                Directory.CreateDirectory(dir);
                string newPath = Path.Combine(dir, Path.GetFileNameWithoutExtension(source) + "_" + new FileInfo(source).LastWriteTimeUtc.ToFileTimeUtc().ToString("X") + Path.GetExtension(source));
                File.Copy(source, newPath);
                AddRepoFile(new RepoFile(new FileInfo(newPath)));
            }
            CopyMade?.Invoke(this, source);
        }

        public bool HasCopy(string source) => Files.ContainsKey(source.ToLowerInvariant());
        public DateTime GetLatestCopyTimeUtc(string source)
        {
            DateTime? Latest = null;
            foreach (var i in Files[source])
            {
                if (Latest == null) Latest = i.LastModifiedTimeUtc;
                else if (Latest.Value < i.LastModifiedTimeUtc) Latest = i.LastModifiedTimeUtc;
            }
            return Latest.Value;
        }

        public IEnumerable<DateTime> EnumrateCopies(string source)
        {
            foreach (var i in Files[source])
                yield return i.LastModifiedTimeUtc;
        }

        public void Trim(long spaceNeeded)
        {
            Trim((file) => true, spaceNeeded);
        }

        public void Trim(TimeSpan howOld)
        {
            DateTime deadline = DateTime.UtcNow - howOld;
            Trim((file) => file.LastModifiedTimeUtc < deadline);
        }

        private void Trim(Func<RepoFile, bool> condition, long spaceNeeded = -1)
        {
            lock (Files)
            {
                List<RepoFile> dupFiles = new List<RepoFile>();
                foreach (var i in Files)
                {
                    DateTime? Latest = null;
                    foreach (var j in i.Value)
                    {
                        if (Latest == null) Latest = j.LastModifiedTimeUtc;
                        else if (Latest.Value < j.LastModifiedTimeUtc) Latest = j.LastModifiedTimeUtc;
                    }
                    foreach (var j in i.Value)
                        if (Latest.Value != j.LastModifiedTimeUtc) dupFiles.Add(j);
                }
                dupFiles.Sort((x, y) => Math.Sign((x.LastModifiedTimeUtc - y.LastModifiedTimeUtc).Ticks));
                List<RepoFile> survivor = new List<RepoFile>();
                long lenTotal = 0;
                foreach (var i in dupFiles)
                {
                    if (condition(i))
                    {
                        lenTotal += i.Length;
                        File.Delete(i.FullName);
                        Files[i.Source.ToLowerInvariant()].Remove(i);
                    }
                    else survivor.Add(i);
                    if (spaceNeeded > 0)
                        if (lenTotal >= spaceNeeded) break;
                }
                dupFiles = null;
            }
        }
    }
}

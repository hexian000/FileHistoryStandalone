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
        public class RepoFile
        {
            internal RepoFile(FileInfo file, int repoPathLen)
            {
                FullName = file.FullName;
                string name = Path.GetFileNameWithoutExtension(file.FullName), ext = file.Extension;
                int pos = name.LastIndexOf('_');
                // Restore drive letter
                string srcDir = Path.GetDirectoryName(FullName).Substring(repoPathLen + 1);
                if (srcDir.StartsWith("_")) srcDir = @"\" + srcDir.Substring(1);
                else srcDir = srcDir[0] + @":" + srcDir.Substring(1);
                Source = Path.Combine(srcDir, name.Substring(0, pos) + ext);
                LastModifiedTimeUtc = DateTime.FromFileTimeUtc(long.Parse(name.Substring(pos + 1), System.Globalization.NumberStyles.AllowHexSpecifier));
                Length = file.Length;
            }

            public readonly string FullName;
            public readonly string Source;
            public readonly DateTime LastModifiedTimeUtc;
            public readonly long Length;
        }

        private string RepoPath;
        private long RepoSize, RepoMaxSize;
        private Dictionary<string, List<RepoFile>> Files;

        public long Size { get { return RepoSize; } }
        public long MaxSize
        {
            get { return RepoMaxSize; }
            set
            {
                if (value < RepoSize) Trim(RepoSize - value);
                RepoMaxSize = value;
            }
        }

        public event EventHandler<string> CopyMade;

        private IEnumerable<RepoFile> EnumerateRepoFiles(string path)
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
                    f = new RepoFile(file, RepoPath.Length);
                }
                catch { }
                if (f != null) yield return f;
            }
        }

        private Repository(string repoPath)
        {
            RepoPath = repoPath;
            RepoSize = 0; RepoMaxSize = long.MaxValue;
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
            foreach (var i in ret.EnumerateRepoFiles(path))
                ret.AddRepoFile(i);
            return ret;
        }

        private void AddRepoFile(RepoFile file)
        {
            string src = file.Source.ToLowerInvariant();
            lock (Files)
            {
                if (Files.ContainsKey(src))
                    Files[src].Add(file);
                else
                    Files.Add(src, new List<RepoFile>(new RepoFile[] { file }));
            }
            RepoSize += file.Length;
        }

        public int FileCount => Files.Count;

        public void MakeCopy(string source)
        {
            string dir;
            if (source[1] == ':') dir = Path.Combine(RepoPath, source[0] + Path.GetDirectoryName(source).Substring(2));
            else if (source.StartsWith(@"\\")) dir = Path.Combine(RepoPath, '_' + Path.GetDirectoryName(source).Substring(1));
            else throw new ArgumentException("不支持的路径格式", nameof(source));
            long sizeOverflow = RepoSize + new FileInfo(source).Length - RepoMaxSize;
            if (sizeOverflow > 0) Trim(sizeOverflow);
            Directory.CreateDirectory(dir);
            string newPath = Path.Combine(dir, Path.GetFileNameWithoutExtension(source) + "_" + new FileInfo(source).LastWriteTimeUtc.ToFileTimeUtc().ToString("X") + Path.GetExtension(source));
            File.Copy(@"\\?\" + source, @"\\?\" + newPath);
            AddRepoFile(new RepoFile(new FileInfo(@"\\?\" + newPath), RepoPath.Length));
            CopyMade?.Invoke(this, source);
        }

        public bool HasCopy(string source) => Files.ContainsKey(source.ToLowerInvariant());
        public DateTime GetLatestCopyTimeUtc(string source)
        {
            DateTime? Latest = null;
            lock (Files)
                foreach (var i in Files[source])
                {
                    if (Latest == null) Latest = i.LastModifiedTimeUtc;
                    else if (Latest.Value < i.LastModifiedTimeUtc) Latest = i.LastModifiedTimeUtc;
                }
            return Latest.Value;
        }

        public IEnumerable<RepoFile> FindVersions(string docPath) => Files[docPath.ToLowerInvariant()];

        public void DeleteVersion(RepoFile version)
        {
            File.Delete(@"\\?\" + version.FullName);
            string id = version.Source.ToLowerInvariant();
            lock (Files)
            {
                List<RepoFile> vers = Files[id];
                if (vers.Count == 1)
                    Files.Remove(id);
                else
                    vers.Remove(version);
            }
            RepoSize -= version.Length;
        }
        public void SaveAs(RepoFile file, string to, bool overwrite = false) => File.Copy(file.FullName, to, overwrite);

        public IEnumerable<DateTime> EnumrateCopies(string source)
        {
            foreach (var i in Files[source])
                yield return i.LastModifiedTimeUtc;
        }

        public void Trim()
        {
            Trim((file) => true);
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
            List<RepoFile> dupFiles = new List<RepoFile>();
            lock (Files)
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
            long lenTotal = 0;
            foreach (var i in dupFiles)
            {
                if (condition(i))
                {
                    lenTotal += i.Length;
                    try { DeleteVersion(i); }
                    catch { }
                }
                if (spaceNeeded > 0)
                    if (lenTotal >= spaceNeeded) break;
            }
            dupFiles = null;
        }
    }
}

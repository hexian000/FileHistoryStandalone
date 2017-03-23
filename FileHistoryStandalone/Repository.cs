﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static FileHistoryStandalone.Program;

namespace FileHistoryStandalone
{
    class Repository
    {
        private string RepoPath;
        private long RepoSize, RepoMaxSize;

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
        public event EventHandler<string> Renamed;

        private Repository(string repoPath)
        {
            RepoPath = Win32Path(repoPath);
            RepoSize = 0; RepoMaxSize = long.MaxValue;
        }

        public static Repository Create(string path)
        {
            Directory.CreateDirectory(path);
            try
            {
                string name = NtPath(path.Trim());
                ProcessStartInfo psi = new ProcessStartInfo("compact.exe", $"/c /s:\"{name}\" /q")
                {
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                using (var proc = Process.Start(psi)) proc.WaitForExit(2000);
            }
            catch { }
            return new Repository(path);
        }

        public static Repository Open(string path)
        {
            return new Repository(path);
        }

        public void MakeCopy(string source)
        {
            try
            {
                source = NtPath(source);
                long sizeOverflow = RepoSize + new FileInfo(source).Length - RepoMaxSize;
                if (sizeOverflow > 0) Trim(sizeOverflow);
                string newPath = NameDoc2Repo(source, new FileInfo(source).LastWriteTimeUtc);
                WriteDebugLog("INFO", $"Copy {source} => {newPath}");
                Directory.CreateDirectory(Win32Path(Path.GetDirectoryName(newPath)));
                File.Copy(Win32Path(source), Win32Path(newPath), true);
                var srcAttr = new FileInfo(Win32Path(source));
                srcAttr.Attributes = srcAttr.Attributes & ~FileAttributes.Archive;
                var attr = new FileInfo(Win32Path(newPath));
                attr.Attributes = attr.Attributes & ~FileAttributes.Archive | FileAttributes.ReadOnly;
                CopyMade?.Invoke(this, source);
            }
            catch (Exception ex)
            {
                WriteDebugLog("FATAL", ex);
                DoUnhandledException(ex);
            }
        }

        public void Synchronize(string srcDir)
        {
            Dictionary<string, DateTime> repo = new Dictionary<string, DateTime>();
            string repoDir = PathDoc2Repo(srcDir);
            if (Directory.Exists(Win32Path(repoDir)))
                foreach (var file in Directory.EnumerateFiles(Win32Path(repoDir)))
                {
                    string name = Path.GetFileName(file);
                    string doc = NameRepo2Doc(name).ToLowerInvariant();
                    DateTime date = NameRepo2Time(name);
                    if (repo.ContainsKey(doc))
                    {
                        if (repo[doc] < date) repo[doc] = date;
                    }
                    else repo[doc] = date;
                }
            foreach (var doc in new DirectoryInfo(Win32Path(srcDir)).EnumerateFiles())
            {
                string id = doc.Name.ToLowerInvariant();
                if (repo.ContainsKey(id))
                {
                    if (repo[id] < doc.LastWriteTimeUtc) MakeCopy(doc.FullName);
                }
                else MakeCopy(doc.FullName);
            }
        }

        public void Rename(string source, string newSource)
        {
            WriteDebugLog("INFO", $"Rename {source} => {newSource}");
            var newName = Path.GetFileNameWithoutExtension(newSource);
            foreach (var ver in FindVersions(source))
            {
                try
                {
                    string verName = Path.GetFileName(ver);
                    string newPath = Win32Path(
                        Path.Combine(Path.GetDirectoryName(ver),
                        newName + verName.Substring(verName.LastIndexOf('_'))));
                    if (File.Exists(newPath))
                    {
                        new FileInfo(newPath).Attributes &= ~FileAttributes.ReadOnly;
                        File.Delete(newPath);
                    }
                    File.Move(Win32Path(ver), newPath);
                }
                catch (Exception ex) { WriteDebugLog("ERROR", ex); }
            }
            Renamed?.Invoke(this, newSource);
        }

        public void RenameDir(string source, string newSource)
        {
            WriteDebugLog("INFO", $"RenameDir {source} => {newSource}");
            try
            {
                Directory.Move(Win32Path(PathDoc2Repo(source)), Win32Path(PathDoc2Repo(newSource)));
            }
            catch (Exception ex) { WriteDebugLog("ERROR", ex); }
            Renamed?.Invoke(this, newSource);
        }

        //public bool HasCopy(string source)
        //{
        //    foreach (var i in FindVersions(source))
        //        return true;
        //    return false;
        //}

        public DateTime GetLatestCopyTimeUtc(string source, List<string> RepoDir)
        {
            DateTime? Latest = null;
            foreach (var i in FindVersions(source, RepoDir))
            {
                DateTime time = NameRepo2Time(i);
                if (Latest == null) Latest = time;
                else if (Latest.Value < time) Latest = time;
            }
            if (Latest == null) throw new FileNotFoundException("未找到已备份副本", source);
            return Latest.Value;
        }

        public IEnumerable<string> FindVersions(string source) => FindVersions(source, GetRepoDir(Path.GetDirectoryName(source)));

        public IEnumerable<string> FindVersions(string source, List<string> repoDir)
        {
            string prefix = Path.GetFileNameWithoutExtension(source).ToLowerInvariant() + '_';
            string suffix = Path.GetExtension(source).ToLowerInvariant();
            foreach (var file in repoDir)
            {
                string name = Path.GetFileName(file).ToLowerInvariant();
                if (name.StartsWith(prefix) && name.EndsWith(suffix))
                {
                    string hex = name.Substring(prefix.Length, name.Length - prefix.Length - suffix.Length);
                    if (hex.All((c) => char.IsDigit(c) || (c >= 'a' && c <= 'f')))
                        yield return file;
                }
            }
        }

        public List<string> GetRepoDir(string sourceDir)
        {
            var ret = new List<string>();
            string RepoDir = Win32Path(PathDoc2Repo(sourceDir));
            if (Directory.Exists(RepoDir))
                foreach (var file in Directory.EnumerateFiles(RepoDir))
                    ret.Add(file);
            return ret;
        }

        public void DeleteVersion(string version)
        {
            var attr = new FileInfo(Win32Path(version));
            attr.Attributes = attr.Attributes & ~FileAttributes.ReadOnly;
            RepoSize -= attr.Length;
            File.Delete(Win32Path(version));
        }

        public void SaveAs(string file, string to, bool overwrite = false)
        {
            File.Copy(Win32Path(file), Win32Path(to), overwrite);
            var attr = new FileInfo(Win32Path(file));
            attr.Attributes = attr.Attributes & ~FileAttributes.Archive & ~FileAttributes.ReadOnly;
        }

        public void Trim()
        {
            Trim(null, null, true);
        }

        public void TrimFull()
        {
            Trim(null, null);
        }

        public void Trim(long spaceNeeded)
        {
            Trim(null, spaceNeeded);
        }

        public void Trim(TimeSpan howOld)
        {
            DateTime deadline = DateTime.UtcNow - howOld;
            Trim(deadline, null);
        }

        public void Trim(DateTime? deadline, long? spaceNeeded, bool orphanOnly = false)
        {
            List<FileInfo> orphan = new List<FileInfo>();
            Dictionary<string, DateTime> LatestWriteTime = new Dictionary<string, DateTime>();
            HashSet<string> ExcludeVer = new HashSet<string>();
            List<FileInfo> versions = new List<FileInfo>();
            foreach (var i in EnumerateTreeFiles(RepoPath))
            {
                if (!File.Exists(Win32Path(PathRepo2Doc(i.FullName))))
                    orphan.Add(i);
                else if (!orphanOnly) versions.Add(i);
                string doc = PathRepo2Doc(i.FullName).ToLowerInvariant(); // NtPath
                DateTime time = NameRepo2Time(i.FullName);
                if (LatestWriteTime.TryGetValue(doc, out DateTime val))
                {
                    if (time > val)
                        LatestWriteTime[doc] = time;
                }
                else { LatestWriteTime.Add(doc, time); }
            }
            orphan.Sort((x, y) => Math.Sign((x.LastWriteTimeUtc - y.LastWriteTimeUtc).Ticks));
            versions.Sort((x, y) => Math.Sign((x.LastWriteTimeUtc - y.LastWriteTimeUtc).Ticks));
            foreach (var i in LatestWriteTime) ExcludeVer.Add(NameDoc2Repo(i.Key, i.Value).ToLowerInvariant());
            LatestWriteTime = null;

            long lenTotal = 0;
            foreach (var i in orphan.Concat(versions))
            {
                if (ExcludeVer.Contains(NtPath(i.FullName).ToLowerInvariant())) continue;
                if (deadline != null)
                    if (i.LastWriteTimeUtc >= deadline)
                        break;
                if (spaceNeeded != null)
                    if (lenTotal >= spaceNeeded) break;
                lenTotal += i.Length;
                try { DeleteVersion(i.FullName); }
                catch (Exception ex) { WriteDebugLog("WARNING", ex); }
            }
            orphan = null;
            TrimEmptyDirs(RepoPath);
        }

        /// <summary>
        /// 返回指定文档库路径的存档库路径
        /// </summary>
        /// <param name="fullName">文档库路径</param>
        /// <returns>NtPath</returns>
        private string PathDoc2Repo(string fullName)
        {
            string name = NtPath(fullName);
            return Path.Combine(RepoPath, name[0] + name.Substring(2));
        }

        /// <summary>
        /// 返回指定存档库路径的文档库路径
        /// </summary>
        /// <param name="fullName">存档库路径</param>
        /// <returns>NtPath</returns>
        private string PathRepo2Doc(string fullName)
        {
            string name = NtPath(fullName);
            return name.Substring(RepoPath.Length + 1, 1) + ':' + name.Substring(RepoPath.Length + 2);
        }

        public DateTime NameRepo2Time(string objName)
        {
            string name = Path.GetFileNameWithoutExtension(NtPath(objName));
            return DateTime.FromFileTimeUtc(long.Parse(name.Substring(name.LastIndexOf('_') + 1), System.Globalization.NumberStyles.AllowHexSpecifier));
        }

        private string NameDoc2Repo(string objName, DateTime lastWrite)
        {
            return Path.Combine(RepoPath, objName[0] + Path.GetDirectoryName(objName).Substring(2),
                Path.GetFileNameWithoutExtension(objName) + "_" + lastWrite.ToFileTimeUtc().ToString("X") + Path.GetExtension(objName));
        }

        private string NameRepo2Doc(string objName)
        {
            string name = Path.GetFileNameWithoutExtension(objName);
            return name.Substring(0, name.LastIndexOf('_')) + Path.GetExtension(objName);
        }
    }
}

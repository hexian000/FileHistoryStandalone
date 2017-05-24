using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static FileHistoryStandalone.Program;

namespace FileHistoryStandalone {
	class Repository {
		private string RepoPath;
		public string RepositoryPath { get; private set; }
		private long RepoSize, RepoMaxSize;

		public long Size { get { return RepoSize; } }
		public long MaxSize {
			get { return RepoMaxSize; }
			set {
				if (value < RepoSize) Trim(null, RepoSize - value);
				RepoMaxSize = value;
			}
		}

		public event EventHandler<string> CopyMade;
		public event EventHandler<string> Renamed;

		private Repository(string repoPath) {
			RepoPath = Win32Path(repoPath);
			RepositoryPath = NtPath(repoPath);
			RepoSize = 0; RepoMaxSize = long.MaxValue;
		}

		public static Repository Create(string path) {
			Directory.CreateDirectory(path);
			try {
				string name = NtPath(path.Trim());
				ProcessStartInfo psi = new ProcessStartInfo("compact.exe", $"/c /s:\"{name}\" /q") {
					WindowStyle = ProcessWindowStyle.Hidden
				};
				using (var proc = Process.Start(psi)) proc.WaitForExit(2000);
			} catch { }
			return new Repository(path);
		}

		public static Repository Open(string path) {
			return new Repository(path);
		}

		public void MakeCopy(string source) {
			try {
				source = NtPath(source);
				long sizeOverflow = RepoSize + new FileInfo(source).Length - RepoMaxSize;
				if (sizeOverflow > 0) Trim(null, sizeOverflow);
				string newPath = NameDoc2Repo(source, new FileInfo(source).LastWriteTimeUtc);
				WriteDebugLog("INFO", $"Copy {source} => {newPath}");
				Directory.CreateDirectory(Win32Path(Path.GetDirectoryName(newPath)));
				if (File.Exists(Win32Path(newPath))) {
					WriteDebugLog("WARNING", $"Overwriting {newPath}");
					var ovwAttr = new FileInfo(Win32Path(newPath));
					ovwAttr.Attributes = ovwAttr.Attributes & ~FileAttributes.ReadOnly;
				}
				File.Copy(Win32Path(source), Win32Path(newPath), true);
				var srcAttr = new FileInfo(Win32Path(source));
				srcAttr.Attributes = srcAttr.Attributes & ~FileAttributes.Archive;
				var attr = new FileInfo(Win32Path(newPath));
				attr.Attributes = attr.Attributes & ~FileAttributes.Archive | FileAttributes.ReadOnly;
				CopyMade?.Invoke(this, source);
			} catch (Exception ex) {
				WriteDebugLog("ERROR", ex);
				//DoUnhandledException(ex);
			}
		}

		public List<KeyValuePair<string, string>> ListDir(string path) {
			var ret = new List<KeyValuePair<string, string>>();
			path = RepoPath + path;
			foreach (var dir in Directory.EnumerateDirectories(Win32Path(path))) {
				ret.Add(new KeyValuePair<string, string>(NtPath(Path.GetFileName(dir)), NtPath(dir).Substring(RepositoryPath.Length) + Path.DirectorySeparatorChar));
			}
			var hs = new HashSet<string>();
			foreach (var file in Directory.EnumerateFiles(Win32Path(path))) {
				if (!hs.Contains(file.ToLowerInvariant())) {
					hs.Add(file.ToLowerInvariant());
					ret.Add(new KeyValuePair<string, string>(NtPath(NameRepo2Doc(Path.GetFileName(file))), NtPath(file).Substring(RepositoryPath.Length)));
				}
			}
			return ret;
		}

		public void Synchronize(string srcDir, CancellationToken cancel) {
			Dictionary<string, DateTime> repo = new Dictionary<string, DateTime>();
			string repoDir = PathDoc2Repo(srcDir);
			if (Directory.Exists(Win32Path(repoDir)))
				foreach (var file in Directory.EnumerateFiles(Win32Path(repoDir))) {
					string name = Path.GetFileName(file);
					string doc = NameRepo2Doc(name).ToLowerInvariant();
					DateTime date = NameRepo2Time(name);
					if (repo.ContainsKey(doc)) {
						if (repo[doc] < date) repo[doc] = date;
					} else repo[doc] = date;
					if (cancel.IsCancellationRequested) return;
				}
			foreach (var doc in new DirectoryInfo(Win32Path(srcDir)).EnumerateFiles()) {
                if (doc.Attributes.HasFlag(
                    FileAttributes.Hidden 
                    | FileAttributes.System 
                    | FileAttributes.Temporary 
                    | FileAttributes.SparseFile 
                    | FileAttributes.ReparsePoint)) continue;
				string id = doc.Name.ToLowerInvariant();
				if (!IsExcluded(doc.Name))
					if (repo.ContainsKey(id)) {
						if (repo[id] < doc.LastWriteTimeUtc) MakeCopy(doc.FullName);
					} else MakeCopy(doc.FullName);
				if (cancel.IsCancellationRequested) return;
			}
		}

		public void Rename(string source, string newSource) {
			WriteDebugLog("INFO", $"Rename {source} => {newSource}");
			var newName = Path.GetFileNameWithoutExtension(newSource);
			foreach (var ver in FindVersions(source)) {
				try {
					string verName = Path.GetFileName(ver);
					string newPath = Win32Path(
						Path.Combine(Path.GetDirectoryName(ver),
						newName + verName.Substring(verName.LastIndexOf('_'))));
					if (File.Exists(newPath)) {
						new FileInfo(newPath).Attributes &= ~FileAttributes.ReadOnly;
						File.Delete(newPath);
					}
					File.Move(Win32Path(ver), newPath);
				} catch (Exception ex) { WriteDebugLog("ERROR", ex); }
			}
			Renamed?.Invoke(this, newSource);
		}

		public void RenameDir(string source, string newSource) {
			WriteDebugLog("INFO", $"RenameDir {source} => {newSource}");
			try {
				Directory.Move(Win32Path(PathDoc2Repo(source)), Win32Path(PathDoc2Repo(newSource)));
			} catch (Exception ex) { WriteDebugLog("ERROR", ex); }
			Renamed?.Invoke(this, newSource);
		}

		//public bool HasCopy(string source)
		//{
		//    foreach (var i in FindVersions(source))
		//        return true;
		//    return false;
		//}

		public DateTime GetLatestCopyTimeUtc(string source, List<string> RepoDir) {
			DateTime? Latest = null;
			foreach (var i in FindVersions(source, RepoDir)) {
				DateTime time = NameRepo2Time(i);
				if (Latest == null) Latest = time;
				else if (Latest.Value < time) Latest = time;
			}
			if (Latest == null) throw new FileNotFoundException("未找到已备份副本", source);
			return Latest.Value;
		}

		public IEnumerable<string> FindVersions(string source) => FindVersions(source, GetRepoDir(Path.GetDirectoryName(source)));

		public IEnumerable<string> FindVersions(string source, List<string> repoDir) {
			string prefix = Path.GetFileNameWithoutExtension(source).ToLowerInvariant() + '_';
			string suffix = Path.GetExtension(source).ToLowerInvariant();
			foreach (var file in repoDir) {
				string name = Path.GetFileName(file).ToLowerInvariant();
				if (name.StartsWith(prefix) && name.EndsWith(suffix)) {
					string hex = name.Substring(prefix.Length, name.Length - prefix.Length - suffix.Length);
					if (hex.All((c) => char.IsDigit(c) || (c >= 'a' && c <= 'f')))
						yield return file;
				}
			}
		}

		public List<string> GetRepoDir(string sourceDir) {
			var ret = new List<string>();
			string RepoDir = Win32Path(PathDoc2Repo(sourceDir));
			if (Directory.Exists(RepoDir))
				foreach (var file in Directory.EnumerateFiles(RepoDir))
					ret.Add(file);
			return ret;
		}

		public void DeleteVersion(string version) {
			var attr = new FileInfo(Win32Path(version));
			attr.Attributes = attr.Attributes & ~FileAttributes.ReadOnly;
			RepoSize -= attr.Length;
			File.Delete(Win32Path(version));
		}

		public void SaveAs(string file, string to, bool overwrite = false) {
			File.Copy(Win32Path(file), Win32Path(to), overwrite);
			var attr = new FileInfo(Win32Path(file));
			attr.Attributes = attr.Attributes & ~FileAttributes.Archive & ~FileAttributes.ReadOnly;
		}

		public void Trim(CancellationToken? cancel) {
			Trim(cancel, null, null, true);
		}

		public void TrimFull(CancellationToken? cancel) {
			Trim(cancel, null, null);
		}

		public void Trim(CancellationToken? cancel, long spaceNeeded) {
			Trim(cancel, null, spaceNeeded);
		}

		public void Trim(CancellationToken? cancel, TimeSpan howOld) {
			DateTime deadline = DateTime.UtcNow - howOld;
			Trim(cancel, deadline, null);
		}

		public void Trim(CancellationToken? cancel, DateTime? deadline, long? spaceNeeded, bool orphanOnly = false) {
			WriteDebugLog("INFO", "Trim: start");
			List<FileInfo> orphan = new List<FileInfo>();
			Dictionary<string, DateTime> LatestWriteTime = new Dictionary<string, DateTime>();
			HashSet<string> ExcludeVer = new HashSet<string>();
			List<FileInfo> versions = new List<FileInfo>();
			foreach (var i in EnumerateTreeFiles(RepoPath)) {
				if (!File.Exists(Win32Path(PathRepo2Doc(i.FullName))))
					orphan.Add(i);
				else if (!orphanOnly) versions.Add(i);
				string doc = PathRepo2Doc(i.FullName).ToLowerInvariant(); // NtPath
				DateTime time = NameRepo2Time(i.FullName);
				if (LatestWriteTime.TryGetValue(doc, out DateTime val)) {
					if (time > val)
						LatestWriteTime[doc] = time;
				} else { LatestWriteTime.Add(doc, time); }
				if (cancel != null)
					if (cancel.Value.IsCancellationRequested) {
						WriteDebugLog("INFO", "Trim: cancelling");
						return;
					}
			}
			orphan.Sort((x, y) => Math.Sign((x.LastWriteTimeUtc - y.LastWriteTimeUtc).Ticks));
			versions.Sort((x, y) => Math.Sign((x.LastWriteTimeUtc - y.LastWriteTimeUtc).Ticks));
			foreach (var i in LatestWriteTime) ExcludeVer.Add(NameDoc2Repo(i.Key, i.Value).ToLowerInvariant());
			LatestWriteTime = null;
			WriteDebugLog("INFO", "Trim: versions enumerated");

			long lenTotal = 0;
			foreach (var i in orphan.Concat(versions)) {
				if (ExcludeVer.Contains(NtPath(i.FullName).ToLowerInvariant())) continue;
				if (deadline != null)
					if (i.LastWriteTimeUtc >= deadline)
						break;
				if (spaceNeeded != null)
					if (lenTotal >= spaceNeeded) break;
				lenTotal += i.Length;
				WriteDebugLog("VERBOSE", $"Trim: deleting {i.FullName}");
				try { DeleteVersion(i.FullName); } catch (Exception ex) { WriteDebugLog("WARNING", ex); }
				if (cancel != null)
					if (cancel.Value.IsCancellationRequested) {
						WriteDebugLog("INFO", "Trim: cancelling");
						return;
					}
			}
			orphan = null;
			WriteDebugLog("INFO", "Trim: empty dirs");
			TrimEmptyDirs(RepoPath);
			WriteDebugLog("INFO", "Trim: done");
		}

		/// <summary>
		/// 返回指定文档库路径的存档库路径
		/// </summary>
		/// <param name="fullName">文档库路径</param>
		/// <returns>NtPath</returns>
		public string PathDoc2Repo(string fullName) {
			string name = NtPath(fullName);
			return Path.Combine(RepoPath, name[0] + name.Substring(2));
		}

		/// <summary>
		/// 返回指定存档库路径的文档库路径
		/// </summary>
		/// <param name="fullName">存档库路径</param>
		/// <returns>NtPath</returns>
		public string PathRepo2Doc(string fullName) {
			string name = NtPath(fullName);
			int len = NtPath(RepoPath).Length;
			return name.Substring(len + 1, 1) + ':' + name.Substring(len + 2);
		}

		public DateTime NameRepo2Time(string objName) {
			string name = Path.GetFileNameWithoutExtension(NtPath(objName));
			return DateTime.FromFileTimeUtc(long.Parse(name.Substring(name.LastIndexOf('_') + 1), System.Globalization.NumberStyles.AllowHexSpecifier));
		}

		public string NameDoc2Repo(string objName, DateTime lastWrite) {
			return Path.Combine(RepoPath, objName[0] + Path.GetDirectoryName(objName).Substring(2),
				Path.GetFileNameWithoutExtension(objName) + "_" + lastWrite.ToFileTimeUtc().ToString("X") + Path.GetExtension(objName));
		}

		public string NameRepo2Doc(string objName) {
			string name = Path.GetFileNameWithoutExtension(objName);
			return name.Substring(0, name.LastIndexOf('_')) + Path.GetExtension(objName);
		}
	}
}

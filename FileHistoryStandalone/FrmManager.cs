using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileHistoryStandalone
{
    public partial class FrmManager : Form
    {
        public FrmManager()
        {
            InitializeComponent();
        }

        private object synclock = new object();
        private bool Exiting = false;
        private CancellationTokenSource cancelSrc = null;
        private Thread worker = null;

        private void NicTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
        }

        private void FrmManager_Shown(object sender, EventArgs e)
        {
            if (Program.CommandLine.Length > 0)
            {
                foreach (var i in Program.CommandLine)
                {
                    if (i.StartsWith("--debug:"))
                        Program.log = new StreamWriter(new FileStream(i.Substring(8), FileMode.Create), Encoding.UTF8);
                    else if (i == "--hide") Hide();
                    else MessageBox.Show($"无法识别的参数 - “{i}”");
                }
            }
            if (Properties.Settings.Default.FirstRun)
            {
                if (!Reconfigure())
                {
                    Close();
                    return;
                }
            }
            else
            {
                TsslStatus.Text = "正在打开存档库";
                Program.Repo = Repository.Open(Properties.Settings.Default.Repo.Trim());
                Program.Repo.CopyMade += Repo_CopyMade;
                Program.Repo.Renamed += Repo_Renamed;
                Program.DocLib = new DocLibrary(Program.Repo)
                {
                    Paths = Properties.Settings.Default.DocPath.Trim()
                };
                ScanLibAsync();
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        /// <summary>  
        /// 返回系统设置的图标  
        /// </summary>  
        /// <param name="pszPath">文件路径 如果为""  返回文件夹的</param>  
        /// <param name="dwFileAttributes">0</param>  
        /// <param name="psfi">结构体</param>  
        /// <param name="cbSizeFileInfo">结构体大小</param>  
        /// <param name="uFlags">枚举类型</param>  
        /// <returns>-1失败</returns>  
        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        public enum SHGFI
        {
            SHGFI_ICON = 0x100,
            SHGFI_LARGEICON = 0x0,
            SHGFI_USEFILEATTRIBUTES = 0x10
        }

        /// <summary>
        /// 获取文件图标
        /// </summary>
        /// <param name="p_Path">文件全路径</param>  
        /// <returns>图标</returns>  
        public static Icon GetFileIcon(string p_Path)
        {
            SHFILEINFO _SHFILEINFO = new SHFILEINFO();
            IntPtr _IconIntPtr = SHGetFileInfo(p_Path, 0, ref _SHFILEINFO, (uint)Marshal.SizeOf(_SHFILEINFO), (uint)(SHGFI.SHGFI_ICON | SHGFI.SHGFI_LARGEICON | SHGFI.SHGFI_USEFILEATTRIBUTES));
            if (_IconIntPtr.Equals(IntPtr.Zero)) return null;
            Icon _Icon = Icon.FromHandle(_SHFILEINFO.hIcon);
            return _Icon;
        }

        /// <summary>  
        /// 获取文件夹图标  zgke@sina.com qq:116149  
        /// </summary>  
        /// <returns>图标</returns>  
        public static Icon GetDirectoryIcon()
        {
            SHFILEINFO _SHFILEINFO = new SHFILEINFO();
            IntPtr _IconIntPtr = SHGetFileInfo(@"", 0, ref _SHFILEINFO, (uint)Marshal.SizeOf(_SHFILEINFO), (uint)(SHGFI.SHGFI_ICON | SHGFI.SHGFI_LARGEICON));
            if (_IconIntPtr.Equals(IntPtr.Zero)) return null;
            Icon _Icon = Icon.FromHandle(_SHFILEINFO.hIcon);
            return _Icon;
        }
        private Icon DirectoryIcon = GetDirectoryIcon();
        private Dictionary<string, Icon> IconCache = new Dictionary<string, Icon>();

        private void RefreshFileView(string cd)
        {
            var ret = Program.Repo.ListDir(cd);
            LvwFiles.BeginUpdate();
            LvwFiles.Items.Clear();
            ImlIcon.Images.Clear();
            foreach (var i in ret)
            {
                if (!i.Value.EndsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    string filePath = Program.Win32Path(Program.Repo.RepositoryPath + i.Value);
                    var info = new FileInfo(filePath);
                    var item = new ListViewItem(i.Key);
                    item.SubItems.Add(Program.FormatSize(info.Length));
                    item.SubItems.Add(Program.Repo.NameRepo2Time(i.Value).ToString());
                    string ext = Path.GetExtension(i.Value).ToLowerInvariant();
                    Icon icon;
                    if (ext != ".exe" && ext != ".ico")
                    {
                        if (!IconCache.TryGetValue(ext, out icon))
                        {
                            icon = Icon.ExtractAssociatedIcon(Program.NtPath(filePath));
                            IconCache[ext] = icon;
                        }
                    }
                    else icon = Icon.ExtractAssociatedIcon(Program.NtPath(filePath));
                    ImlIcon.Images.Add(icon);
                    item.ImageIndex = ImlIcon.Images.Count - 1;
                    item.Tag = i.Value;
                    LvwFiles.Items.Add(item);
                }
                else
                {
                    var item = new ListViewItem(i.Key);
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    ImlIcon.Images.Add(DirectoryIcon);
                    item.ImageIndex = ImlIcon.Images.Count - 1;
                    item.Tag = i.Value;
                    LvwFiles.Items.Add(item);
                }
            }
            LvwFiles.EndUpdate();
            TxtPath.Text = cd;
        }

        private void LvwFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem i in LvwFiles.SelectedItems)
            {
                string relPath = (string)i.Tag;
                if (relPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                    RefreshFileView(relPath.TrimEnd(Path.DirectorySeparatorChar));
                break;
            }
        }

        private void 另存为AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem it in LvwFiles.SelectedItems)
                if (it.Tag is string ver)
                {
                    if (ver.EndsWith(Path.DirectorySeparatorChar.ToString()))
                    {
                        MessageBox.Show("您不能对文件夹执行此操作", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    SfdSaveAs.FileName = it.Text;
                    if (SfdSaveAs.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            Program.Repo.SaveAs(Program.Repo.RepositoryPath + ver, SfdSaveAs.FileName, true);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Program.WriteDebugLog("WARNING", ex);
                        }
                    }
                    break;
                }
        }

        private void 删除DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem it in LvwFiles.SelectedItems)
                if (it.Tag is string ver)
                {
                    if (ver.EndsWith(Path.DirectorySeparatorChar.ToString()))
                    {
                        MessageBox.Show("您不能对文件夹执行此操作", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (MessageBox.Show(this, "确实要删除这个版本吗？", Application.ProductName,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        try
                        {
                            Program.Repo.DeleteVersion(Program.Repo.RepositoryPath + ver);
                            LvwFiles.Items.Remove(it);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Program.WriteDebugLog("WARNING", ex);
                        }
                    }
                    break;
                }
        }

        private void Repo_CopyMade(object sender, string e)
        {
            StatusStripDefault.BeginInvoke(new Action<DateTime>((t) =>
            {
                TsslStatus.Text = $"[{t:H:mm:ss}] 已备份 " + e;
            }), DateTime.Now);
        }

        private void Repo_Renamed(object sender, string e)
        {
            StatusStripDefault.BeginInvoke(new Action<DateTime>((t) =>
            {
                TsslStatus.Text = $"[{t:H:mm:ss}] 重命名 " + e;
            }), DateTime.Now);
        }

        private bool Reconfigure()
        {
            if (new FrmConfig().ShowDialog(this) == DialogResult.OK)
            {
                Program.Repo.CopyMade += Repo_CopyMade;
                Program.Repo.Renamed += Repo_Renamed;
                CancelIfWorking();
                ScanLibAsync();
                return true;
            }
            return false;
        }

        private void CancelIfWorking()
        {
            lock (synclock)
                if (cancelSrc != null)
                {
                    cancelSrc.Cancel();
                }
            worker?.Join();
        }

        private void ScanLibAsync()
        {
            if (InvokeRequired) Invoke(new Action(() => TsslStatus.Text = "已启动文档库扫描"));
            worker = new Thread(() =>
            {
                lock (synclock)
                    cancelSrc = new CancellationTokenSource();
                Program.DocLib.ScanLibrary(cancelSrc.Token);
                if (!cancelSrc.Token.IsCancellationRequested)
                    Invoke(new Action(() =>
                    {
                        TsslStatus.Text = $"[{DateTime.Now:H:mm:ss}] 文档库扫描完成";
                        RefreshFileView(@"\");
                    }));
                lock (synclock)
                {
                    cancelSrc.Dispose();
                    cancelSrc = null;
                    worker = null;
                }
            });
            worker.Start();
        }

        private void FrmManager_Load(object sender, EventArgs e)
        {
            NicTray.Icon = Icon;
        }

        private void FrmManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown || e.CloseReason == CloseReason.TaskManagerClosing) Exit();
            e.Cancel = !Exiting;
            if (!Exiting) Hide();
        }

        private void Exit()
        {
            CancelIfWorking();
            Program.DocLib?.Dispose();
            Exiting = true;
        }

        private void TrimFinished()
        {
            TsslStatus.Text = $"[{DateTime.Now:H:mm:ss}] 版本清理完成";
            RefreshFileView(@"\");
            LvwFiles.Enabled = true;
        }

        private bool CheckBusy()
        {
            bool busy;
            lock (synclock) busy = cancelSrc != null;
            if (busy)
            {
                MessageBox.Show(this, "工作中，现在不能执行此动作", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        private bool TrimPrompt()
        {
            if (MessageBox.Show(this, "确实要删除这些版本吗？", Application.ProductName,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                LvwFiles.Items.Clear();
                TsslStatus.Text = $"[{DateTime.Now:H:mm:ss}] 已启动版本清理";
                return true;
            }
            return false;
        }

        private void 仅保留最新版本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckBusy() && TrimPrompt())
            {
                LvwFiles.Enabled = false;
                worker = new Thread(() =>
                  {
                      lock (synclock)
                          cancelSrc = new CancellationTokenSource();
                      Program.Repo.TrimFull(cancelSrc.Token);
                      if (!cancelSrc.Token.IsCancellationRequested)
                          BeginInvoke(new Action(TrimFinished));
                      lock (synclock)
                      {
                          cancelSrc.Dispose();
                          cancelSrc = null;
                      }
                  });
                worker.Start();
            }
        }

        private void 删除90天以前的版本ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (CheckBusy() && TrimPrompt())
            {
                LvwFiles.Enabled = false;
                worker = new Thread(() =>
                {
                    lock (synclock)
                        cancelSrc = new CancellationTokenSource();
                    Program.Repo.Trim(cancelSrc.Token, new TimeSpan(90, 0, 0, 0));
                    if (!cancelSrc.Token.IsCancellationRequested)
                        BeginInvoke(new Action(TrimFinished));
                    lock (synclock)
                    {
                        cancelSrc.Dispose();
                        cancelSrc = null;
                    }
                });
                worker.Start();
            }
        }

        private void 删除已删除文件的备份ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckBusy() && TrimPrompt())
            {
                LvwFiles.Enabled = false;
                worker = new Thread(() =>
                {
                    lock (synclock)
                        cancelSrc = new CancellationTokenSource();
                    Program.Repo.Trim(cancelSrc.Token);
                    if (!cancelSrc.Token.IsCancellationRequested)
                        BeginInvoke(new Action(TrimFinished));
                    lock (synclock)
                    {
                        cancelSrc.Dispose();
                        cancelSrc = null;
                    }
                });
                worker.Start();
            }
        }

        private void 重新配置RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reconfigure();
        }

        private void 退出XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
            Close();
        }

        private void 显示SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
        }

        private void 退出EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
            Close();
        }

        private void BtnUp_Click(object sender, EventArgs e)
        {
            string cd = TxtPath.Text;
            if (cd != Path.DirectorySeparatorChar.ToString())
                RefreshFileView(Path.GetDirectoryName(cd));
        }

        private void 关于AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new FrmAbout();
            about.ShowDialog(this);
        }
    }
}

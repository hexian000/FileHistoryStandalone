using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
            RefreshFileView(null);
        }

        private void RefreshFileView(string cd)
        {
            var ret = Program.Repo.ListDir(cd);
            LvwFiles.BeginUpdate();
            LvwFiles.Items.Clear();
            ImlIcon.Images.Clear();
            foreach (var i in ret)
            {
                if (File.Exists(i.Value))
                {
                    var info = new FileInfo(i.Value);
                    var item = new ListViewItem(i.Key);
                    item.SubItems.Add(info.Length.ToString());
                    item.SubItems.Add(info.LastWriteTime.ToString());
                    ImlIcon.Images.Add(Icon.ExtractAssociatedIcon(Program.NtPath(i.Value)));
                    item.ImageIndex = ImlIcon.Images.Count - 1;
                    item.Tag = i.Value;
                    LvwFiles.Items.Add(item);
                }
                else if (Directory.Exists(i.Value))
                {
                    var info = new DirectoryInfo(i.Value);
                    var item = new ListViewItem(i.Key);
                    item.SubItems.Add("");
                    item.SubItems.Add(info.LastWriteTime.ToString());
                    //ImlIcon.Images.Add(Icon.ExtractAssociatedIcon(Program.NtPath( i.Value)));
                    //item.ImageIndex = ImlIcon.Images.Count - 1;
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
                RefreshFileView((string)i.Tag);
                break;
            }
        }

        private void 另存为AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem it in LvwFiles.SelectedItems)
                if (it.Tag is string ver)
                {
                    if (SfdSaveAs.ShowDialog() == DialogResult.OK)
                        Program.Repo.SaveAs(ver, SfdSaveAs.FileName, true);
                    break;
                }
        }

        private void 删除DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem it in LvwFiles.SelectedItems)
                if (it.Tag is string ver)
                {
                    if (MessageBox.Show(this, "确实要删除这个版本吗？", Application.ProductName,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        Program.Repo.DeleteVersion(ver);
                        LvwFiles.Items.Remove(it);
                    }
                    break;
                }
        }

        private void Repo_CopyMade(object sender, string e)
        {
            StatusStripDefault.BeginInvoke(new Action<DateTime>((t) => TsslStatus.Text = $"[{t:H:mm:ss}] 已备份 " + e), DateTime.Now);
        }

        private void Repo_Renamed(object sender, string e)
        {
            StatusStripDefault.BeginInvoke(new Action<DateTime>((t) => TsslStatus.Text = $"[{t:H:mm:ss}] 重命名 " + e), DateTime.Now);
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
                    Invoke(new Action(() => TsslStatus.Text = $"[{DateTime.Now:H:mm:ss}] 文档库扫描完成"));
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
            if (cd.Contains(Path.DirectorySeparatorChar))
                RefreshFileView(Path.GetDirectoryName(cd));
        }
    }
}

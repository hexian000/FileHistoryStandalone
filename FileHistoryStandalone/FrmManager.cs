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

        private void NicTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
        }

        private void BtnHide_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void BtnReconfig_Click(object sender, EventArgs e)
        {
            Reconfigure();
        }

        private void FrmManager_Shown(object sender, EventArgs e)
        {
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
                new Thread(() =>
                {
                    Program.Repo = Repository.Open(Properties.Settings.Default.Repo.Trim());
                    Program.Repo.CopyMade += Repo_CopyMade;
                    Program.DocLib = new DocLibrary(Program.Repo)
                    {
                        Paths = Properties.Settings.Default.DocPath.Trim()
                    };
                    ScanLibAsync();
                    BtnFind.Invoke(new Action(() => BtnFind.Enabled = true));
                }).Start();
            }
        }

        private void BtnFind_Click(object sender, EventArgs e)
        {
            if (OfdFind.ShowDialog() == DialogResult.OK)
            {
                TxtDoc.Text = OfdFind.FileName;
                string file = OfdFind.FileName.Trim();
                LvwFiles.BeginUpdate();
                LvwFiles.Items.Clear();
                var vers = Program.Repo.FindVersions(file);
                foreach (var ver in vers)
                {
                    var it = new ListViewItem(ver.LastModifiedTimeUtc.ToString());
                    it.SubItems.Add(ver.Length.ToString() + "字节");
                    LvwFiles.Items.Add(it);
                }
                LvwFiles.EndUpdate();
            }
        }

        private void Repo_CopyMade(object sender, string e)
        {
            StatusStripDefault.BeginInvoke(new Action(() => TsslStatus.Text = "已备份 " + e));
        }

        private bool Reconfigure()
        {
            if (new FrmConfig().ShowDialog(this) == DialogResult.OK)
            {
                Program.Repo.CopyMade += Repo_CopyMade;
                BtnReconfig.Enabled = false;
                ScanLibAsync();
                return true;
            }
            return false;
        }

        private Thread ScanThread = null;
        private void ScanLibAsync()
        {
            if (ScanThread != null) return;
            if (InvokeRequired) Invoke(new Action(() => TsslStatus.Text = "已启动文档库扫描"));
            ScanThread = new Thread(() =>
              {
                  Program.DocLib.ScanLibrary();
                  Invoke(new Action(() =>
                  {
                      // NicTray.ShowBalloonTip(5000, "FileHistoryStandalone", "文档库扫描完成", ToolTipIcon.Info);
                      BtnReconfig.Enabled = true;
                      TsslStatus.Text = "就绪";
                  }));
                  ScanThread = null;
              });
            ScanThread.Start();
        }

        private void FrmManager_Load(object sender, EventArgs e)
        {
            NicTray.Icon = Icon;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileHistoryStandalone {
    public partial class FrmExclude : Form {
        public FrmExclude() {
            InitializeComponent();
        }

        private void FrmExclude_FormClosing(object sender, FormClosingEventArgs e) {
            List<FileAttributes> attrs = new List<FileAttributes>();
            if (ChkHidden.Checked) attrs.Add(FileAttributes.Hidden);
            if (ChkSystem.Checked) attrs.Add(FileAttributes.System);
            if (ChkTemp.Checked) attrs.Add(FileAttributes.Temporary);
            if (ChkSparse.Checked) attrs.Add(FileAttributes.SparseFile);
            if (ChkEncrypted.Checked) attrs.Add(FileAttributes.Encrypted);
            Program.Repo.Excludes = TxtExclude.Text;
            Program.Repo.ExcludeAttributes = attrs;
            if (ChkSize.Checked) {
                Program.Repo.ExcludeSize = long.Parse(TxtSize.Text) * 1048576;
            } else Program.Repo.ExcludeSize = -1;
            Properties.Settings.Default.ExcludeRegex = TxtExclude.Text;
            Properties.Settings.Default.ExcludeAttr = string.Join(",", attrs.Cast<int>().Select((x) => x.ToString()));
            Properties.Settings.Default.ExcludeSize = Program.Repo.ExcludeSize;
            Properties.Settings.Default.Save();
        }

        private void FrmExclude_Load(object sender, EventArgs e) {
            TxtExclude.Text = Program.Repo.Excludes;
            ChkHidden.Checked = Program.Repo.ExcludeAttributes.Any((a) => a == FileAttributes.Hidden);
            ChkSystem.Checked = Program.Repo.ExcludeAttributes.Any((a) => a == FileAttributes.System);
            ChkTemp.Checked = Program.Repo.ExcludeAttributes.Any((a) => a == FileAttributes.Temporary);
            ChkSparse.Checked = Program.Repo.ExcludeAttributes.Any((a) => a == FileAttributes.SparseFile);
            ChkEncrypted.Checked = Program.Repo.ExcludeAttributes.Any((a) => a == FileAttributes.Encrypted);
            ChkSize.Checked = Program.Repo.ExcludeSize > 0;
            if (ChkSize.Checked) {
                TxtSize.Text = (Program.Repo.ExcludeSize / 1048576).ToString();
                TxtSize.Enabled = true;
            }
        }

        private void ChkSize_CheckedChanged(object sender, EventArgs e) {
            TxtSize.Enabled = ChkSize.Checked;
        }
    }
}

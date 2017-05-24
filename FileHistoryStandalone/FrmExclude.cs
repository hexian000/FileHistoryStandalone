using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            Program.Excludes = TxtExclude.Text;
            Properties.Settings.Default.ExcludeRegex = TxtExclude.Text;
            Properties.Settings.Default.Save();
        }

        private void FrmExclude_Load(object sender, EventArgs e) {
            TxtExclude.Text = Program.Excludes;
        }
    }
}

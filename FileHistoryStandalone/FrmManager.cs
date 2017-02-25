using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            NicTray.Visible = false;
        }

        private void BtnHide_Click(object sender, EventArgs e)
        {
            Hide();
            NicTray.Visible = true;
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
        }

        private bool Reconfigure() => new FrmConfig().ShowDialog(this) == DialogResult.OK;
    }
}

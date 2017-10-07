using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FileHistoryStandalone
{
	public partial class FrmConfig : Form
	{
		public FrmConfig()
		{
			InitializeComponent();
		}

		bool CreateRepo = false;

		private void BtnRepo_Click(object sender, EventArgs e)
		{
			if (FbdRepo.ShowDialog(this) == DialogResult.OK)
			{
				CreateRepo = false;
				TxtRepo.Text = FbdRepo.SelectedPath;
			}
		}

		private void BtnNewRepo_Click(object sender, EventArgs e)
		{
			if (FbdRepo.ShowDialog(this) == DialogResult.OK)
			{
				CreateRepo = true;
				TxtRepo.Text = Path.Combine(FbdRepo.SelectedPath, "FileHistoryS");
			}
		}

		private void BtnDoc_Click(object sender, EventArgs e)
		{
			if (FbdDoc.ShowDialog(this) == DialogResult.OK) TxtDocPath.AppendText(FbdDoc.SelectedPath + Environment.NewLine);
		}

		private void BtnClear_Click(object sender, EventArgs e)
		{
			TxtDocPath.Clear();
		}

		private void BtnOK_Click(object sender, EventArgs e)
		{
			try
			{
				if (CreateRepo)
					Program.Repo = Repository.Create(TxtRepo.Text);
				else
				{
					SetBusyUI(true);
					Thread load = new Thread((path) =>
					{
						Program.Repo = Repository.Open(path as string);
					});
					load.Start(TxtRepo.Text);
					Thread.Sleep(100);
					while (load.IsAlive) Application.DoEvents();
				}
				Properties.Settings.Default.Repo = TxtRepo.Text;
				Program.DocLib = new DocLibrary(Program.Repo)
				{
					Paths = TxtDocPath.Text
				};
				Properties.Settings.Default.DocPath = TxtDocPath.Text;
				Properties.Settings.Default.FirstRun = false;
				Properties.Settings.Default.Save();
			}
			catch (Exception ex)
			{
				Program.WriteDebugLog("ERROR", ex);
				MessageBox.Show(this, ex.Message, "设置", MessageBoxButtons.OK, MessageBoxIcon.Error);
				DialogResult = DialogResult.None;
				SetBusyUI();
			}
		}

		private void SetBusyUI(bool isBusy = false)
		{
			foreach (Control i in Controls) i.Enabled = !isBusy;
			UseWaitCursor = isBusy;
			if (isBusy) Text = "正在读取存档库";
			else Text = "设置";
		}

		private void FrmConfig_Load(object sender, EventArgs e)
		{
			TxtDocPath.Text = Properties.Settings.Default.DocPath;
			TxtRepo.Text = Properties.Settings.Default.Repo;
		}
	}
}

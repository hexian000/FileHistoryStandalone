namespace FileHistoryStandalone
{
    partial class FrmConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.TxtDocPath = new System.Windows.Forms.TextBox();
            this.TxtRepo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnDoc = new System.Windows.Forms.Button();
            this.BtnRepo = new System.Windows.Forms.Button();
            this.FbdDoc = new System.Windows.Forms.FolderBrowserDialog();
            this.FbdRepo = new System.Windows.Forms.FolderBrowserDialog();
            this.BtnClear = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnNewRepo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnOK
            // 
            this.BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOK.Location = new System.Drawing.Point(363, 225);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(75, 23);
            this.BtnOK.TabIndex = 0;
            this.BtnOK.Text = "保存";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(282, 225);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "放弃";
            // 
            // TxtDocPath
            // 
            this.TxtDocPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtDocPath.Location = new System.Drawing.Point(12, 24);
            this.TxtDocPath.Multiline = true;
            this.TxtDocPath.Name = "TxtDocPath";
            this.TxtDocPath.ReadOnly = true;
            this.TxtDocPath.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtDocPath.Size = new System.Drawing.Size(426, 89);
            this.TxtDocPath.TabIndex = 2;
            // 
            // TxtRepo
            // 
            this.TxtRepo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtRepo.Location = new System.Drawing.Point(12, 169);
            this.TxtRepo.Name = "TxtRepo";
            this.TxtRepo.ReadOnly = true;
            this.TxtRepo.Size = new System.Drawing.Size(426, 21);
            this.TxtRepo.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "文档文件夹路径（每行一个）";
            // 
            // BtnDoc
            // 
            this.BtnDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnDoc.Location = new System.Drawing.Point(303, 119);
            this.BtnDoc.Name = "BtnDoc";
            this.BtnDoc.Size = new System.Drawing.Size(135, 23);
            this.BtnDoc.TabIndex = 5;
            this.BtnDoc.Text = "添加文档文件夹";
            this.BtnDoc.UseVisualStyleBackColor = true;
            this.BtnDoc.Click += new System.EventHandler(this.BtnDoc_Click);
            // 
            // BtnRepo
            // 
            this.BtnRepo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnRepo.Location = new System.Drawing.Point(303, 196);
            this.BtnRepo.Name = "BtnRepo";
            this.BtnRepo.Size = new System.Drawing.Size(135, 23);
            this.BtnRepo.TabIndex = 6;
            this.BtnRepo.Text = "打开已有存档库";
            this.BtnRepo.UseVisualStyleBackColor = true;
            this.BtnRepo.Click += new System.EventHandler(this.BtnRepo_Click);
            // 
            // BtnClear
            // 
            this.BtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnClear.Location = new System.Drawing.Point(12, 119);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(118, 23);
            this.BtnClear.TabIndex = 7;
            this.BtnClear.Text = "清空文档文件夹";
            this.BtnClear.UseVisualStyleBackColor = true;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "历史记录存档库";
            // 
            // BtnNewRepo
            // 
            this.BtnNewRepo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnNewRepo.Location = new System.Drawing.Point(160, 196);
            this.BtnNewRepo.Name = "BtnNewRepo";
            this.BtnNewRepo.Size = new System.Drawing.Size(137, 23);
            this.BtnNewRepo.TabIndex = 9;
            this.BtnNewRepo.Text = "创建新存档库";
            this.BtnNewRepo.UseVisualStyleBackColor = true;
            this.BtnNewRepo.Click += new System.EventHandler(this.BtnNewRepo_Click);
            // 
            // FrmConfig
            // 
            this.AcceptButton = this.BtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(450, 260);
            this.ControlBox = false;
            this.Controls.Add(this.BtnNewRepo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnClear);
            this.Controls.Add(this.BtnRepo);
            this.Controls.Add(this.BtnDoc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtRepo);
            this.Controls.Add(this.TxtDocPath);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConfig";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置";
            this.Load += new System.EventHandler(this.FrmConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.TextBox TxtDocPath;
        private System.Windows.Forms.TextBox TxtRepo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnDoc;
        private System.Windows.Forms.Button BtnRepo;
        private System.Windows.Forms.FolderBrowserDialog FbdDoc;
        private System.Windows.Forms.FolderBrowserDialog FbdRepo;
        private System.Windows.Forms.Button BtnClear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnNewRepo;
    }
}
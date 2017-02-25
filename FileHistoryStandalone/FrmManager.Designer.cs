namespace FileHistoryStandalone
{
    partial class FrmManager
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.StatusStripDefault = new System.Windows.Forms.StatusStrip();
            this.TsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.BtnReconfig = new System.Windows.Forms.Button();
            this.LvwFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BtnHide = new System.Windows.Forms.Button();
            this.NicTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.StatusStripDefault.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusStripDefault
            // 
            this.StatusStripDefault.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsslStatus});
            this.StatusStripDefault.Location = new System.Drawing.Point(0, 340);
            this.StatusStripDefault.Name = "StatusStripDefault";
            this.StatusStripDefault.Size = new System.Drawing.Size(635, 22);
            this.StatusStripDefault.TabIndex = 0;
            // 
            // TsslStatus
            // 
            this.TsslStatus.Name = "TsslStatus";
            this.TsslStatus.Size = new System.Drawing.Size(620, 17);
            this.TsslStatus.Spring = true;
            this.TsslStatus.Text = "就绪";
            this.TsslStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnReconfig
            // 
            this.BtnReconfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnReconfig.Location = new System.Drawing.Point(531, 314);
            this.BtnReconfig.Name = "BtnReconfig";
            this.BtnReconfig.Size = new System.Drawing.Size(92, 23);
            this.BtnReconfig.TabIndex = 1;
            this.BtnReconfig.Text = "修改设置";
            this.BtnReconfig.UseVisualStyleBackColor = true;
            this.BtnReconfig.Click += new System.EventHandler(this.BtnReconfig_Click);
            // 
            // LvwFiles
            // 
            this.LvwFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LvwFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.LvwFiles.FullRowSelect = true;
            this.LvwFiles.Location = new System.Drawing.Point(12, 12);
            this.LvwFiles.MultiSelect = false;
            this.LvwFiles.Name = "LvwFiles";
            this.LvwFiles.Size = new System.Drawing.Size(611, 296);
            this.LvwFiles.TabIndex = 2;
            this.LvwFiles.UseCompatibleStateImageBehavior = false;
            this.LvwFiles.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "文件";
            this.columnHeader1.Width = 480;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "修改时间";
            this.columnHeader2.Width = 120;
            // 
            // BtnHide
            // 
            this.BtnHide.Location = new System.Drawing.Point(12, 314);
            this.BtnHide.Name = "BtnHide";
            this.BtnHide.Size = new System.Drawing.Size(148, 23);
            this.BtnHide.TabIndex = 3;
            this.BtnHide.Text = "隐藏到系统托盘";
            this.BtnHide.UseVisualStyleBackColor = true;
            this.BtnHide.Click += new System.EventHandler(this.BtnHide_Click);
            // 
            // NicTray
            // 
            this.NicTray.Text = "File History Standalone";
            this.NicTray.Visible = true;
            this.NicTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NicTray_MouseDoubleClick);
            // 
            // FrmManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 362);
            this.Controls.Add(this.BtnHide);
            this.Controls.Add(this.LvwFiles);
            this.Controls.Add(this.BtnReconfig);
            this.Controls.Add(this.StatusStripDefault);
            this.Name = "FrmManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File History Standalone";
            this.Shown += new System.EventHandler(this.FrmManager_Shown);
            this.StatusStripDefault.ResumeLayout(false);
            this.StatusStripDefault.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip StatusStripDefault;
        private System.Windows.Forms.ToolStripStatusLabel TsslStatus;
        private System.Windows.Forms.Button BtnReconfig;
        private System.Windows.Forms.ListView LvwFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button BtnHide;
        private System.Windows.Forms.NotifyIcon NicTray;
    }
}


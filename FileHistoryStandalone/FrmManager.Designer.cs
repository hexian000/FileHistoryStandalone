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
            this.TxtDoc = new System.Windows.Forms.TextBox();
            this.BtnFind = new System.Windows.Forms.Button();
            this.OfdFind = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.另存为AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SfdSaveAs = new System.Windows.Forms.SaveFileDialog();
            this.BtnTrim = new System.Windows.Forms.Button();
            this.StatusStripDefault.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusStripDefault
            // 
            this.StatusStripDefault.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsslStatus});
            this.StatusStripDefault.Location = new System.Drawing.Point(0, 341);
            this.StatusStripDefault.Name = "StatusStripDefault";
            this.StatusStripDefault.Size = new System.Drawing.Size(632, 22);
            this.StatusStripDefault.TabIndex = 0;
            // 
            // TsslStatus
            // 
            this.TsslStatus.Name = "TsslStatus";
            this.TsslStatus.Size = new System.Drawing.Size(617, 17);
            this.TsslStatus.Spring = true;
            this.TsslStatus.Text = "就绪";
            this.TsslStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnReconfig
            // 
            this.BtnReconfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnReconfig.Enabled = false;
            this.BtnReconfig.Location = new System.Drawing.Point(528, 315);
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
            this.LvwFiles.ContextMenuStrip = this.contextMenuStrip1;
            this.LvwFiles.FullRowSelect = true;
            this.LvwFiles.Location = new System.Drawing.Point(12, 41);
            this.LvwFiles.MultiSelect = false;
            this.LvwFiles.Name = "LvwFiles";
            this.LvwFiles.Size = new System.Drawing.Size(608, 268);
            this.LvwFiles.TabIndex = 2;
            this.LvwFiles.UseCompatibleStateImageBehavior = false;
            this.LvwFiles.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "版本修改时间";
            this.columnHeader1.Width = 240;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "版本大小";
            this.columnHeader2.Width = 240;
            // 
            // BtnHide
            // 
            this.BtnHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnHide.Location = new System.Drawing.Point(12, 315);
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
            // TxtDoc
            // 
            this.TxtDoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtDoc.Location = new System.Drawing.Point(12, 13);
            this.TxtDoc.Name = "TxtDoc";
            this.TxtDoc.ReadOnly = true;
            this.TxtDoc.Size = new System.Drawing.Size(493, 21);
            this.TxtDoc.TabIndex = 4;
            // 
            // BtnFind
            // 
            this.BtnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnFind.Enabled = false;
            this.BtnFind.Location = new System.Drawing.Point(511, 12);
            this.BtnFind.Name = "BtnFind";
            this.BtnFind.Size = new System.Drawing.Size(109, 23);
            this.BtnFind.TabIndex = 5;
            this.BtnFind.Text = "寻找版本";
            this.BtnFind.UseVisualStyleBackColor = true;
            this.BtnFind.Click += new System.EventHandler(this.BtnFind_Click);
            // 
            // OfdFind
            // 
            this.OfdFind.Filter = "所有文件|*.*";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.另存为AToolStripMenuItem,
            this.删除DToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(138, 48);
            // 
            // 另存为AToolStripMenuItem
            // 
            this.另存为AToolStripMenuItem.Name = "另存为AToolStripMenuItem";
            this.另存为AToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.另存为AToolStripMenuItem.Text = "另存为(&A)...";
            this.另存为AToolStripMenuItem.Click += new System.EventHandler(this.另存为AToolStripMenuItem_Click);
            // 
            // 删除DToolStripMenuItem
            // 
            this.删除DToolStripMenuItem.Name = "删除DToolStripMenuItem";
            this.删除DToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.删除DToolStripMenuItem.Text = "删除(&D)";
            this.删除DToolStripMenuItem.Click += new System.EventHandler(this.删除DToolStripMenuItem_Click);
            // 
            // SfdSaveAs
            // 
            this.SfdSaveAs.Filter = "所有文件|*.*";
            // 
            // BtnTrim
            // 
            this.BtnTrim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnTrim.Location = new System.Drawing.Point(367, 315);
            this.BtnTrim.Name = "BtnTrim";
            this.BtnTrim.Size = new System.Drawing.Size(155, 23);
            this.BtnTrim.TabIndex = 7;
            this.BtnTrim.Text = "删除所有冗余版本";
            this.BtnTrim.UseVisualStyleBackColor = true;
            this.BtnTrim.Click += new System.EventHandler(this.BtnTrim_Click);
            // 
            // FrmManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 363);
            this.Controls.Add(this.BtnTrim);
            this.Controls.Add(this.BtnFind);
            this.Controls.Add(this.TxtDoc);
            this.Controls.Add(this.BtnHide);
            this.Controls.Add(this.LvwFiles);
            this.Controls.Add(this.BtnReconfig);
            this.Controls.Add(this.StatusStripDefault);
            this.Name = "FrmManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File History Standalone";
            this.Load += new System.EventHandler(this.FrmManager_Load);
            this.Shown += new System.EventHandler(this.FrmManager_Shown);
            this.StatusStripDefault.ResumeLayout(false);
            this.StatusStripDefault.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox TxtDoc;
        private System.Windows.Forms.Button BtnFind;
        private System.Windows.Forms.OpenFileDialog OfdFind;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 另存为AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除DToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog SfdSaveAs;
        private System.Windows.Forms.Button BtnTrim;
    }
}


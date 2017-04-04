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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManager));
            this.StatusStripDefault = new System.Windows.Forms.StatusStrip();
            this.TsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.LvwFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.另存为AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImlIcon = new System.Windows.Forms.ImageList(this.components);
            this.NicTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OfdFind = new System.Windows.Forms.OpenFileDialog();
            this.SfdSaveAs = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.程序PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重新配置RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.退出XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清理CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除已删除文件的备份ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除90天以前的版本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.仅保留最新版本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnUp = new System.Windows.Forms.Button();
            this.TxtPath = new System.Windows.Forms.TextBox();
            this.StatusStripDefault.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusStripDefault
            // 
            this.StatusStripDefault.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsslStatus});
            this.StatusStripDefault.Location = new System.Drawing.Point(0, 313);
            this.StatusStripDefault.Name = "StatusStripDefault";
            this.StatusStripDefault.Size = new System.Drawing.Size(526, 22);
            this.StatusStripDefault.TabIndex = 0;
            // 
            // TsslStatus
            // 
            this.TsslStatus.Name = "TsslStatus";
            this.TsslStatus.Size = new System.Drawing.Size(511, 17);
            this.TsslStatus.Spring = true;
            this.TsslStatus.Text = "就绪";
            this.TsslStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LvwFiles
            // 
            this.LvwFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LvwFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.LvwFiles.ContextMenuStrip = this.contextMenuStrip1;
            this.LvwFiles.FullRowSelect = true;
            this.LvwFiles.Location = new System.Drawing.Point(12, 57);
            this.LvwFiles.MultiSelect = false;
            this.LvwFiles.Name = "LvwFiles";
            this.LvwFiles.Size = new System.Drawing.Size(502, 253);
            this.LvwFiles.SmallImageList = this.ImlIcon;
            this.LvwFiles.TabIndex = 2;
            this.LvwFiles.UseCompatibleStateImageBehavior = false;
            this.LvwFiles.View = System.Windows.Forms.View.Details;
            this.LvwFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LvwFiles_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "名称";
            this.columnHeader1.Width = 240;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "大小";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "修改时间";
            this.columnHeader3.Width = 240;
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
            // ImlIcon
            // 
            this.ImlIcon.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.ImlIcon.ImageSize = new System.Drawing.Size(16, 16);
            this.ImlIcon.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // NicTray
            // 
            this.NicTray.ContextMenuStrip = this.contextMenuStrip2;
            this.NicTray.Text = "File History Standalone";
            this.NicTray.Visible = true;
            this.NicTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NicTray_MouseDoubleClick);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示SToolStripMenuItem,
            this.退出EToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(116, 48);
            // 
            // 显示SToolStripMenuItem
            // 
            this.显示SToolStripMenuItem.Name = "显示SToolStripMenuItem";
            this.显示SToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.显示SToolStripMenuItem.Text = "显示(&S)";
            this.显示SToolStripMenuItem.Click += new System.EventHandler(this.显示SToolStripMenuItem_Click);
            // 
            // 退出EToolStripMenuItem
            // 
            this.退出EToolStripMenuItem.Name = "退出EToolStripMenuItem";
            this.退出EToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.退出EToolStripMenuItem.Text = "退出(&E)";
            this.退出EToolStripMenuItem.Click += new System.EventHandler(this.退出EToolStripMenuItem_Click);
            // 
            // OfdFind
            // 
            this.OfdFind.Filter = "所有文件|*.*";
            // 
            // SfdSaveAs
            // 
            this.SfdSaveAs.Filter = "所有文件|*.*";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.程序PToolStripMenuItem,
            this.清理CToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(526, 25);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 程序PToolStripMenuItem
            // 
            this.程序PToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.重新配置RToolStripMenuItem,
            this.toolStripMenuItem1,
            this.退出XToolStripMenuItem});
            this.程序PToolStripMenuItem.Name = "程序PToolStripMenuItem";
            this.程序PToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.程序PToolStripMenuItem.Text = "程序(&P)";
            // 
            // 重新配置RToolStripMenuItem
            // 
            this.重新配置RToolStripMenuItem.Name = "重新配置RToolStripMenuItem";
            this.重新配置RToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.重新配置RToolStripMenuItem.Text = "重新配置(&R)";
            this.重新配置RToolStripMenuItem.Click += new System.EventHandler(this.重新配置RToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // 退出XToolStripMenuItem
            // 
            this.退出XToolStripMenuItem.Name = "退出XToolStripMenuItem";
            this.退出XToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.退出XToolStripMenuItem.Text = "退出(&X)";
            this.退出XToolStripMenuItem.Click += new System.EventHandler(this.退出XToolStripMenuItem_Click);
            // 
            // 清理CToolStripMenuItem
            // 
            this.清理CToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除已删除文件的备份ToolStripMenuItem,
            this.删除90天以前的版本ToolStripMenuItem,
            this.仅保留最新版本ToolStripMenuItem});
            this.清理CToolStripMenuItem.Name = "清理CToolStripMenuItem";
            this.清理CToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.清理CToolStripMenuItem.Text = "清理(&C)";
            // 
            // 删除已删除文件的备份ToolStripMenuItem
            // 
            this.删除已删除文件的备份ToolStripMenuItem.Name = "删除已删除文件的备份ToolStripMenuItem";
            this.删除已删除文件的备份ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.删除已删除文件的备份ToolStripMenuItem.Text = "删除已删除文件的备份";
            this.删除已删除文件的备份ToolStripMenuItem.Click += new System.EventHandler(this.删除已删除文件的备份ToolStripMenuItem_Click);
            // 
            // 删除90天以前的版本ToolStripMenuItem
            // 
            this.删除90天以前的版本ToolStripMenuItem.Name = "删除90天以前的版本ToolStripMenuItem";
            this.删除90天以前的版本ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.删除90天以前的版本ToolStripMenuItem.Text = "删除90天以前的版本";
            this.删除90天以前的版本ToolStripMenuItem.Click += new System.EventHandler(this.删除90天以前的版本ToolStripMenuItem_Click);
            // 
            // 仅保留最新版本ToolStripMenuItem
            // 
            this.仅保留最新版本ToolStripMenuItem.Name = "仅保留最新版本ToolStripMenuItem";
            this.仅保留最新版本ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.仅保留最新版本ToolStripMenuItem.Text = "仅保留最新版本";
            this.仅保留最新版本ToolStripMenuItem.Click += new System.EventHandler(this.仅保留最新版本ToolStripMenuItem_Click);
            // 
            // BtnUp
            // 
            this.BtnUp.Location = new System.Drawing.Point(439, 28);
            this.BtnUp.Name = "BtnUp";
            this.BtnUp.Size = new System.Drawing.Size(75, 23);
            this.BtnUp.TabIndex = 10;
            this.BtnUp.Text = "向上一级";
            this.BtnUp.UseVisualStyleBackColor = true;
            this.BtnUp.Click += new System.EventHandler(this.BtnUp_Click);
            // 
            // TxtPath
            // 
            this.TxtPath.Location = new System.Drawing.Point(12, 29);
            this.TxtPath.Name = "TxtPath";
            this.TxtPath.ReadOnly = true;
            this.TxtPath.Size = new System.Drawing.Size(421, 21);
            this.TxtPath.TabIndex = 11;
            // 
            // FrmManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 335);
            this.Controls.Add(this.TxtPath);
            this.Controls.Add(this.BtnUp);
            this.Controls.Add(this.LvwFiles);
            this.Controls.Add(this.StatusStripDefault);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File History Standalone";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmManager_FormClosing);
            this.Load += new System.EventHandler(this.FrmManager_Load);
            this.Shown += new System.EventHandler(this.FrmManager_Shown);
            this.StatusStripDefault.ResumeLayout(false);
            this.StatusStripDefault.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip StatusStripDefault;
        private System.Windows.Forms.ToolStripStatusLabel TsslStatus;
        private System.Windows.Forms.ListView LvwFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.NotifyIcon NicTray;
        private System.Windows.Forms.OpenFileDialog OfdFind;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 另存为AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除DToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog SfdSaveAs;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 程序PToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重新配置RToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 退出XToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清理CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除已删除文件的备份ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除90天以前的版本ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 仅保留最新版本ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 显示SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出EToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ImageList ImlIcon;
        private System.Windows.Forms.Button BtnUp;
        private System.Windows.Forms.TextBox TxtPath;
    }
}


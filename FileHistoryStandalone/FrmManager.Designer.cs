﻿namespace FileHistoryStandalone
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "cancelSrc")]
        protected override void Dispose(bool disposing)
        {
            cancelSrc?.Dispose();
            DirectoryIcon?.Dispose();
            if (IconCache != null) foreach (System.Drawing.Icon i in IconCache.Values) i.Dispose();
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("请稍候...");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManager));
            this.SspMain = new System.Windows.Forms.StatusStrip();
            this.TsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.LvwFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CmsFile = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.另存为AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImlIcon = new System.Windows.Forms.ImageList(this.components);
            this.NicTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.CmsTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OfdFind = new System.Windows.Forms.OpenFileDialog();
            this.SfdSaveAs = new System.Windows.Forms.SaveFileDialog();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.程序PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.排除文件EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重新配置RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.退出XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清理CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除已删除文件的备份ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除90天以前的版本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.仅保留最新版本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnUp = new System.Windows.Forms.Button();
            this.TxtPath = new System.Windows.Forms.TextBox();
            this.SspMain.SuspendLayout();
            this.CmsFile.SuspendLayout();
            this.CmsTray.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // SspMain
            // 
            this.SspMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsslStatus});
            this.SspMain.Location = new System.Drawing.Point(0, 313);
            this.SspMain.Name = "SspMain";
            this.SspMain.Size = new System.Drawing.Size(526, 22);
            this.SspMain.TabIndex = 0;
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
            this.LvwFiles.ContextMenuStrip = this.CmsFile;
            this.LvwFiles.Enabled = false;
            this.LvwFiles.FullRowSelect = true;
            this.LvwFiles.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
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
            this.columnHeader1.Width = 270;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "大小";
            this.columnHeader2.Width = 90;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "修改时间";
            this.columnHeader3.Width = 120;
            // 
            // CmsFile
            // 
            this.CmsFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.另存为AToolStripMenuItem,
            this.删除DToolStripMenuItem});
            this.CmsFile.Name = "contextMenuStrip1";
            this.CmsFile.Size = new System.Drawing.Size(138, 48);
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
            this.NicTray.ContextMenuStrip = this.CmsTray;
            this.NicTray.Text = "File History Standalone 正在工作";
            this.NicTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NicTray_MouseDoubleClick);
            // 
            // CmsTray
            // 
            this.CmsTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示SToolStripMenuItem,
            this.退出EToolStripMenuItem});
            this.CmsTray.Name = "contextMenuStrip2";
            this.CmsTray.Size = new System.Drawing.Size(116, 48);
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
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.程序PToolStripMenuItem,
            this.清理CToolStripMenuItem,
            this.帮助HToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(526, 25);
            this.MainMenu.TabIndex = 9;
            this.MainMenu.Text = "menuStrip1";
            // 
            // 程序PToolStripMenuItem
            // 
            this.程序PToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.排除文件EToolStripMenuItem,
            this.重新配置RToolStripMenuItem,
            this.toolStripMenuItem1,
            this.退出XToolStripMenuItem});
            this.程序PToolStripMenuItem.Name = "程序PToolStripMenuItem";
            this.程序PToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.程序PToolStripMenuItem.Text = "程序(&P)";
            // 
            // 排除文件EToolStripMenuItem
            // 
            this.排除文件EToolStripMenuItem.Name = "排除文件EToolStripMenuItem";
            this.排除文件EToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.排除文件EToolStripMenuItem.Text = "排除文件(&E)";
            this.排除文件EToolStripMenuItem.Click += new System.EventHandler(this.排除文件EToolStripMenuItem_Click);
            // 
            // 重新配置RToolStripMenuItem
            // 
            this.重新配置RToolStripMenuItem.Name = "重新配置RToolStripMenuItem";
            this.重新配置RToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.重新配置RToolStripMenuItem.Text = "重新配置(&R)";
            this.重新配置RToolStripMenuItem.Click += new System.EventHandler(this.重新配置RToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(137, 6);
            // 
            // 退出XToolStripMenuItem
            // 
            this.退出XToolStripMenuItem.Name = "退出XToolStripMenuItem";
            this.退出XToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
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
            // 帮助HToolStripMenuItem
            // 
            this.帮助HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于AToolStripMenuItem});
            this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
            this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.帮助HToolStripMenuItem.Text = "帮助(&H)";
            // 
            // 关于AToolStripMenuItem
            // 
            this.关于AToolStripMenuItem.Name = "关于AToolStripMenuItem";
            this.关于AToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.关于AToolStripMenuItem.Text = "关于(&A)...";
            this.关于AToolStripMenuItem.Click += new System.EventHandler(this.关于AToolStripMenuItem_Click);
            // 
            // BtnUp
            // 
            this.BtnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.TxtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtPath.Location = new System.Drawing.Point(12, 29);
            this.TxtPath.Name = "TxtPath";
            this.TxtPath.ReadOnly = true;
            this.TxtPath.Size = new System.Drawing.Size(421, 21);
            this.TxtPath.TabIndex = 11;
            this.TxtPath.Text = "\\";
            // 
            // FrmManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(526, 335);
            this.Controls.Add(this.TxtPath);
            this.Controls.Add(this.BtnUp);
            this.Controls.Add(this.LvwFiles);
            this.Controls.Add(this.SspMain);
            this.Controls.Add(this.MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.Name = "FrmManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File History Standalone";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmManager_FormClosing);
            this.Load += new System.EventHandler(this.FrmManager_Load);
            this.Shown += new System.EventHandler(this.FrmManager_Shown);
            this.SspMain.ResumeLayout(false);
            this.SspMain.PerformLayout();
            this.CmsFile.ResumeLayout(false);
            this.CmsTray.ResumeLayout(false);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip SspMain;
        private System.Windows.Forms.ToolStripStatusLabel TsslStatus;
        private System.Windows.Forms.ListView LvwFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.NotifyIcon NicTray;
        private System.Windows.Forms.OpenFileDialog OfdFind;
        private System.Windows.Forms.ContextMenuStrip CmsFile;
        private System.Windows.Forms.ToolStripMenuItem 另存为AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除DToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog SfdSaveAs;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem 程序PToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重新配置RToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 退出XToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清理CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除已删除文件的备份ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除90天以前的版本ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 仅保留最新版本ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip CmsTray;
        private System.Windows.Forms.ToolStripMenuItem 显示SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出EToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ImageList ImlIcon;
        private System.Windows.Forms.Button BtnUp;
        private System.Windows.Forms.TextBox TxtPath;
        private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于AToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 排除文件EToolStripMenuItem;
	}
}


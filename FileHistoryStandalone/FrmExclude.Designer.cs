namespace FileHistoryStandalone {
	partial class FrmExclude {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExclude));
            this.GrpByAttr = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtSize = new System.Windows.Forms.TextBox();
            this.ChkSize = new System.Windows.Forms.CheckBox();
            this.ChkEncrypted = new System.Windows.Forms.CheckBox();
            this.ChkSparse = new System.Windows.Forms.CheckBox();
            this.ChkTemp = new System.Windows.Forms.CheckBox();
            this.ChkHidden = new System.Windows.Forms.CheckBox();
            this.ChkSystem = new System.Windows.Forms.CheckBox();
            this.GrpByName = new System.Windows.Forms.GroupBox();
            this.TxtExclude = new System.Windows.Forms.TextBox();
            this.LblInfo = new System.Windows.Forms.Label();
            this.GrpByAttr.SuspendLayout();
            this.GrpByName.SuspendLayout();
            this.SuspendLayout();
            // 
            // GrpByAttr
            // 
            this.GrpByAttr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpByAttr.Controls.Add(this.label1);
            this.GrpByAttr.Controls.Add(this.TxtSize);
            this.GrpByAttr.Controls.Add(this.ChkSize);
            this.GrpByAttr.Controls.Add(this.ChkEncrypted);
            this.GrpByAttr.Controls.Add(this.ChkSparse);
            this.GrpByAttr.Controls.Add(this.ChkTemp);
            this.GrpByAttr.Controls.Add(this.ChkHidden);
            this.GrpByAttr.Controls.Add(this.ChkSystem);
            this.GrpByAttr.Location = new System.Drawing.Point(12, 132);
            this.GrpByAttr.Name = "GrpByAttr";
            this.GrpByAttr.Size = new System.Drawing.Size(436, 93);
            this.GrpByAttr.TabIndex = 2;
            this.GrpByAttr.TabStop = false;
            this.GrpByAttr.Text = "按属性排除";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(270, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "MB 的文件";
            // 
            // TxtSize
            // 
            this.TxtSize.Location = new System.Drawing.Point(221, 62);
            this.TxtSize.Name = "TxtSize";
            this.TxtSize.Size = new System.Drawing.Size(43, 21);
            this.TxtSize.TabIndex = 6;
            this.TxtSize.Text = "30";
            // 
            // ChkSize
            // 
            this.ChkSize.AutoSize = true;
            this.ChkSize.Checked = true;
            this.ChkSize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkSize.Location = new System.Drawing.Point(169, 64);
            this.ChkSize.Name = "ChkSize";
            this.ChkSize.Size = new System.Drawing.Size(48, 16);
            this.ChkSize.TabIndex = 5;
            this.ChkSize.Text = "大于";
            this.ChkSize.UseVisualStyleBackColor = true;
            this.ChkSize.CheckedChanged += new System.EventHandler(this.ChkSize_CheckedChanged);
            // 
            // ChkEncrypted
            // 
            this.ChkEncrypted.AutoSize = true;
            this.ChkEncrypted.Checked = true;
            this.ChkEncrypted.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkEncrypted.Location = new System.Drawing.Point(169, 42);
            this.ChkEncrypted.Name = "ChkEncrypted";
            this.ChkEncrypted.Size = new System.Drawing.Size(72, 16);
            this.ChkEncrypted.TabIndex = 4;
            this.ChkEncrypted.Text = "加密文件";
            this.ChkEncrypted.UseVisualStyleBackColor = true;
            // 
            // ChkSparse
            // 
            this.ChkSparse.AutoSize = true;
            this.ChkSparse.Checked = true;
            this.ChkSparse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkSparse.Location = new System.Drawing.Point(169, 20);
            this.ChkSparse.Name = "ChkSparse";
            this.ChkSparse.Size = new System.Drawing.Size(72, 16);
            this.ChkSparse.TabIndex = 3;
            this.ChkSparse.Text = "稀疏文件";
            this.ChkSparse.UseVisualStyleBackColor = true;
            // 
            // ChkTemp
            // 
            this.ChkTemp.AutoSize = true;
            this.ChkTemp.Checked = true;
            this.ChkTemp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkTemp.Location = new System.Drawing.Point(6, 42);
            this.ChkTemp.Name = "ChkTemp";
            this.ChkTemp.Size = new System.Drawing.Size(72, 16);
            this.ChkTemp.TabIndex = 2;
            this.ChkTemp.Text = "临时文件";
            this.ChkTemp.UseVisualStyleBackColor = true;
            // 
            // ChkHidden
            // 
            this.ChkHidden.AutoSize = true;
            this.ChkHidden.Checked = true;
            this.ChkHidden.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkHidden.Location = new System.Drawing.Point(6, 64);
            this.ChkHidden.Name = "ChkHidden";
            this.ChkHidden.Size = new System.Drawing.Size(72, 16);
            this.ChkHidden.TabIndex = 1;
            this.ChkHidden.Text = "隐藏文件";
            this.ChkHidden.UseVisualStyleBackColor = true;
            // 
            // ChkSystem
            // 
            this.ChkSystem.AutoSize = true;
            this.ChkSystem.Checked = true;
            this.ChkSystem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkSystem.Location = new System.Drawing.Point(6, 20);
            this.ChkSystem.Name = "ChkSystem";
            this.ChkSystem.Size = new System.Drawing.Size(72, 16);
            this.ChkSystem.TabIndex = 0;
            this.ChkSystem.Text = "系统文件";
            this.ChkSystem.UseVisualStyleBackColor = true;
            // 
            // GrpByName
            // 
            this.GrpByName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpByName.Controls.Add(this.TxtExclude);
            this.GrpByName.Controls.Add(this.LblInfo);
            this.GrpByName.Location = new System.Drawing.Point(12, 12);
            this.GrpByName.Name = "GrpByName";
            this.GrpByName.Size = new System.Drawing.Size(436, 114);
            this.GrpByName.TabIndex = 8;
            this.GrpByName.TabStop = false;
            this.GrpByName.Text = "按名称排除";
            // 
            // TxtExclude
            // 
            this.TxtExclude.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtExclude.Location = new System.Drawing.Point(8, 50);
            this.TxtExclude.Multiline = true;
            this.TxtExclude.Name = "TxtExclude";
            this.TxtExclude.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtExclude.Size = new System.Drawing.Size(422, 58);
            this.TxtExclude.TabIndex = 3;
            // 
            // LblInfo
            // 
            this.LblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblInfo.Location = new System.Drawing.Point(6, 17);
            this.LblInfo.Name = "LblInfo";
            this.LblInfo.Size = new System.Drawing.Size(424, 30);
            this.LblInfo.TabIndex = 2;
            this.LblInfo.Text = "如果文件名符合下列正则表达式，则排除：（一行一个，不区分大小写）";
            this.LblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmExclude
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(460, 237);
            this.Controls.Add(this.GrpByName);
            this.Controls.Add(this.GrpByAttr);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmExclude";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "排除文件";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmExclude_FormClosing);
            this.Load += new System.EventHandler(this.FrmExclude_Load);
            this.GrpByAttr.ResumeLayout(false);
            this.GrpByAttr.PerformLayout();
            this.GrpByName.ResumeLayout(false);
            this.GrpByName.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
        private System.Windows.Forms.GroupBox GrpByAttr;
        private System.Windows.Forms.CheckBox ChkSparse;
        private System.Windows.Forms.CheckBox ChkTemp;
        private System.Windows.Forms.CheckBox ChkHidden;
        private System.Windows.Forms.CheckBox ChkSystem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtSize;
        private System.Windows.Forms.CheckBox ChkSize;
        private System.Windows.Forms.CheckBox ChkEncrypted;
        private System.Windows.Forms.GroupBox GrpByName;
        private System.Windows.Forms.TextBox TxtExclude;
        private System.Windows.Forms.Label LblInfo;
    }
}
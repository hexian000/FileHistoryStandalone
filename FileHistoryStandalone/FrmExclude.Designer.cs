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
			this.TxtExclude = new System.Windows.Forms.TextBox();
			this.LblInfo = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// TxtExclude
			// 
			this.TxtExclude.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TxtExclude.Location = new System.Drawing.Point(10, 41);
			this.TxtExclude.Multiline = true;
			this.TxtExclude.Name = "TxtExclude";
			this.TxtExclude.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TxtExclude.Size = new System.Drawing.Size(424, 224);
			this.TxtExclude.TabIndex = 0;
			// 
			// LblInfo
			// 
			this.LblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LblInfo.Location = new System.Drawing.Point(8, 8);
			this.LblInfo.Name = "LblInfo";
			this.LblInfo.Size = new System.Drawing.Size(429, 30);
			this.LblInfo.TabIndex = 1;
			this.LblInfo.Text = "如果文件名符合下列正则表达式，则排除：（一行一个，不区分大小写）";
			this.LblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FrmExclude
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(449, 277);
			this.Controls.Add(this.LblInfo);
			this.Controls.Add(this.TxtExclude);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FrmExclude";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "排除文件";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmExclude_FormClosing);
			this.Load += new System.EventHandler(this.FrmExclude_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox TxtExclude;
		private System.Windows.Forms.Label LblInfo;
	}
}
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WTFCode
{
	public class ShowCodeForm : Form
	{
		private IContainer components = null;

		private RichTextBox txtCode;

		public ShowCodeForm(string code)
		{
			this.InitializeComponent();
			this.txtCode.Text = code;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.txtCode = new RichTextBox();
			base.SuspendLayout();
			this.txtCode.Dock = DockStyle.Fill;
			this.txtCode.Location = new Point(0, 0);
			this.txtCode.Name = "txtCode";
			this.txtCode.Size = new Size(766, 461);
			this.txtCode.TabIndex = 0;
			this.txtCode.Text = "";
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(766, 461);
			base.Controls.Add(this.txtCode);
			base.Name = "ShowCodeForm";
			this.Text = "代码";
			base.ResumeLayout(false);
		}
	}
}

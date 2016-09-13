using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WTFCode
{
	public class ReferenceForm : Form
	{
		private SelectFileInfo _SelectFileInfo = null;

		private IContainer components = null;

		private SplitContainer splitContainer1;

		private TextBox txtReferencePath;

		private Label label1;

		private Button btnAdd;

		private GroupBox MySqlGroup;

		private CheckedListBox chkMySql;

		public ReferenceForm(SelectFileInfo objSelectFileInfo)
		{
			this.InitializeComponent();
			this._SelectFileInfo = objSelectFileInfo;
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
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
			this.splitContainer1 = new SplitContainer();
			this.label1 = new Label();
			this.txtReferencePath = new TextBox();
			this.btnAdd = new Button();
			this.MySqlGroup = new GroupBox();
			this.chkMySql = new CheckedListBox();
			((ISupportInitialize)this.splitContainer1).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.MySqlGroup.SuspendLayout();
			base.SuspendLayout();
			this.splitContainer1.Dock = DockStyle.Fill;
			this.splitContainer1.Location = new Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = Orientation.Horizontal;
			this.splitContainer1.Panel1.Controls.Add(this.btnAdd);
			this.splitContainer1.Panel1.Controls.Add(this.txtReferencePath);
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			this.splitContainer1.Panel2.Controls.Add(this.MySqlGroup);
			this.splitContainer1.Size = new Size(870, 482);
			this.splitContainer1.SplitterDistance = 39;
			this.splitContainer1.TabIndex = 0;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(23, 13);
			this.label1.Name = "label1";
			this.label1.Size = new Size(53, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "引用目录";
			this.txtReferencePath.Location = new Point(82, 10);
			this.txtReferencePath.Name = "txtReferencePath";
			this.txtReferencePath.Size = new Size(368, 21);
			this.txtReferencePath.TabIndex = 1;
			this.txtReferencePath.Text = "..\\..\\WTFFramework";
			this.btnAdd.Location = new Point(473, 10);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new Size(75, 23);
			this.btnAdd.TabIndex = 2;
			this.btnAdd.Text = "添加引用";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
			this.MySqlGroup.Controls.Add(this.chkMySql);
			this.MySqlGroup.Location = new Point(12, 18);
			this.MySqlGroup.Name = "MySqlGroup";
			this.MySqlGroup.Size = new Size(833, 84);
			this.MySqlGroup.TabIndex = 0;
			this.MySqlGroup.TabStop = false;
			this.MySqlGroup.Text = "MySql";
			this.chkMySql.Dock = DockStyle.Top;
			this.chkMySql.FormattingEnabled = true;
			this.chkMySql.Items.AddRange(new object[]
			{
				"MySql.Data.dll",
				"MySql.Data.Entity.dll"
			});
			this.chkMySql.Location = new Point(3, 17);
			this.chkMySql.Name = "chkMySql";
			this.chkMySql.Size = new Size(827, 52);
			this.chkMySql.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(870, 482);
			base.Controls.Add(this.splitContainer1);
			base.Name = "ReferenceForm";
			this.Text = "添加引用";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((ISupportInitialize)this.splitContainer1).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.MySqlGroup.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}

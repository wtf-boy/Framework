namespace WTF.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class RegexTypeEditor : Form
    {
        private Button btnCancel;
        private Button btnOK;
        private IContainer components = null;
        private Label label1;
        private Label label2;
        private ListBox lboxRegex;
        private Dictionary<string, string> objRegexType = new Dictionary<string, string>();
        private TextBox txtRegex;

        public RegexTypeEditor(string objRegexExpression)
        {
            this.InitializeComponent();
            this.BindRegexType();
            this.txtRegex.Text = objRegexExpression;
        }

        private void BindRegexType()
        {
            this.objRegexType.Add("Internet Url", @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            this.objRegexType.Add("电子邮件地址", @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            this.objRegexType.Add("中国电话号码", @"((\d{4}|\d{3})-?)?\d{8})");
            this.objRegexType.Add("电国身份证号码", @"\d{17}[\d|X]|\d{15}");
            this.objRegexType.Add("电国邮政编码", @"\d{6}");
            this.objRegexType.Add("手机号码", @"(13[0-9]|15[0|3|6|7|8|9]|18[8|9])\d{8}");
            this.objRegexType.Add("手机号码或电话号码", @"((13[0-9]|15[0|3|6|7|8|9]|18[8|9])\d{8}|((\d{4}|\d{3})-?)?\d{8})");
            this.lboxRegex.Items.Clear();
            foreach (string str in this.objRegexType.Keys)
            {
                this.lboxRegex.Items.Add(str);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.lboxRegex = new ListBox();
            this.label2 = new Label();
            this.txtRegex = new TextBox();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "常用表达式：";
            this.lboxRegex.FormattingEnabled = true;
            this.lboxRegex.ItemHeight = 12;
            this.lboxRegex.Location = new Point(12, 0x18);
            this.lboxRegex.Name = "lboxRegex";
            this.lboxRegex.Size = new Size(300, 0xac);
            this.lboxRegex.TabIndex = 1;
            this.lboxRegex.SelectedIndexChanged += new EventHandler(this.lboxRegex_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(10, 0xc7);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x4d, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "验证表达式：";
            this.txtRegex.Location = new Point(12, 0xd6);
            this.txtRegex.Name = "txtRegex";
            this.txtRegex.Size = new Size(300, 0x15);
            this.txtRegex.TabIndex = 3;
            this.btnOK.Location = new Point(0xcc, 0xf1);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(50, 0x17);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Location = new Point(260, 0xf1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x34, 0x17);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            base.AcceptButton = this.btnOK;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnCancel;
            base.ClientSize = new Size(0x144, 0x11e);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.txtRegex);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.lboxRegex);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Name = "RegexTypeEditor";
            this.Text = "正则表达式";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void lboxRegex_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtRegex.Text = this.objRegexType[this.lboxRegex.Text];
        }

        public string RegexExpression
        {
            get
            {
                return this.txtRegex.Text;
            }
        }
    }
}


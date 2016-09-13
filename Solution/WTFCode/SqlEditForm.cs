using WTF.CodeRule;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WTFCode
{
    public class SqlEditForm : Form
    {
        private List<ColumnEditSchema> _CurrentColumnList = new List<ColumnEditSchema>();

        private CodeConfigHelper _CodeConfigHelper = null;

        private SelectFileInfo _SelectFileInfo = null;

        private BusinessNodeInfo _BusinessNodeInfo = null;

        private IContainer components = null;

        private SplitContainer splitEditContainer;

        private DataGridView gvdData;

        private TextBox txtBackUrl;

        private Label label2;

        private Label label3;

        private TextBox txtUIProjectPath;

        private Label label4;

        private TextBox txtUIProjectName;

        private Button btnEdit;

        private Button btnCode;

        private Button btnUi;

        private DataGridViewTextBoxColumn FieldName;

        private DataGridViewTextBoxColumn FieldTitle;

        private DataGridViewComboBoxColumn ControlType;

        private DataGridViewCheckBoxColumn IsShow;

        private DataGridViewCheckBoxColumn IsEmpty;

        private DataGridViewTextBoxColumn ErrorMessage;

        private DataGridViewTextBoxColumn ValidationReg;

        private CheckBox chkPreviewCode;

        public SqlEditForm(SelectFileInfo objSelectFileInfo, BusinessNodeInfo objBusinessNodeInfo)
        {
            this.InitializeComponent();
            this._SelectFileInfo = objSelectFileInfo;
            this._CodeConfigHelper = new CodeConfigHelper(objSelectFileInfo.CodeConfigPath);
            if (this._CodeConfigHelper.LoadCodeConfigXml())
            {
                this._BusinessNodeInfo = objBusinessNodeInfo;
                this.Text = this._BusinessNodeInfo.TableName + "编辑界面生成";
                string text = "";
                string text2 = "";
                this._CodeConfigHelper.GetBusinessNodeInfoUIPath(objBusinessNodeInfo, out text, out text2);
                this.txtUIProjectPath.Text = text;
                this.txtUIProjectName.Text = text2;
                this.txtBackUrl.Text = (string.IsNullOrWhiteSpace(this._BusinessNodeInfo.ListUrl) ? (objBusinessNodeInfo.EntityName + "List.aspx") : this._BusinessNodeInfo.ListUrl);
                this.gvdData.AutoGenerateColumns = false;
                string schemaName = "";
                string connectionString = this._CodeConfigHelper.GetConnectionString(this._BusinessNodeInfo.ConnectionKey, out schemaName);
                this._CurrentColumnList = SqlSchemaHelper.GetTableColumnsSchema(schemaName, this._BusinessNodeInfo.TableName, this._CodeConfigHelper, connectionString);
                this.gvdData.DataSource = (from s in this._CurrentColumnList
                                           where s.ColumnType == "Common" && !s.FieldName.ToLower().Contains("guid")
                                           select s).ToList<ColumnEditSchema>();
            }
        }

        private bool CheckInput()
        {
            bool result;
            if (string.IsNullOrWhiteSpace(this.txtUIProjectName.Text))
            {
                MessageBox.Show("Web项目名称不能为空");
                result = false;
            }
            else if (string.IsNullOrWhiteSpace(this.txtUIProjectPath.Text))
            {
                MessageBox.Show("Web项目路经不能为空");
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        private void btnUi_Click(object sender, EventArgs e)
        {
            if (this.CheckInput())
            {
                TableEditSchema tableEditSchema = new TableEditSchema();
                tableEditSchema.TableName = this._BusinessNodeInfo.TableName;
                tableEditSchema.RuleName = "Biz" + this._BusinessNodeInfo.EntityName;
                tableEditSchema.EntityName = this._BusinessNodeInfo.EntityName;
                tableEditSchema.Description = this._BusinessNodeInfo.Description;
                IEnumerator enumerator = ((IEnumerable)this.gvdData.Rows).GetEnumerator();
                {
                    while (enumerator.MoveNext())
                    {
                        DataGridViewRow objDataGridViewRow = (DataGridViewRow)enumerator.Current;
                        string FieldName = objDataGridViewRow.ReadCell("FieldName", "");
                        ColumnEditSchema columnEditSchema = this._CurrentColumnList.First((ColumnEditSchema s) => s.FieldName == FieldName);
                        columnEditSchema.IsShow = bool.Parse(objDataGridViewRow.ReadCell("IsShow", ""));
                        columnEditSchema.ControlType = objDataGridViewRow.ReadCell("ControlType", "");
                        columnEditSchema.ErrorMessage = objDataGridViewRow.ReadCell("ErrorMessage", "");
                        columnEditSchema.FieldTitle = objDataGridViewRow.ReadCell("FieldTitle", "");
                        columnEditSchema.IsEmpty = bool.Parse(objDataGridViewRow.ReadCell("IsEmpty", ""));
                        columnEditSchema.ValidationReg = objDataGridViewRow.ReadCell("ValidationReg", "");
                    }
                }
                tableEditSchema.Columns = this._CurrentColumnList;
                foreach (ColumnEditSchema current in tableEditSchema.Columns)
                {
                    current.EntityName = this._BusinessNodeInfo.EntityName;
                }
                string text = this._BusinessNodeInfo.EntityName + "Edit.aspx";
                string uiPath = Common.GetUiPath(this.txtUIProjectPath.Text.Trim(new char[]
				{
					'\\'
				}), this._SelectFileInfo);
                string inherits = this.GetInherits(uiPath);
                if (string.IsNullOrWhiteSpace(inherits))
                {
                    MessageBox.Show("Web项目名称输入有误");
                }
                else
                {
                    string uiCode = tableEditSchema.ToUiStringSql();
                    Common.WriteCode(uiPath + "\\" + text, text, this.GetUiCode(uiCode, inherits), this.chkPreviewCode.Checked);
                    this.UpdateXml(tableEditSchema);
                }
            }
        }

        private string GetInherits(string dirPath)
        {
            int num = dirPath.IndexOf(this.txtUIProjectName.Text);
            string result;
            if (num == -1)
            {
                result = "";
            }
            else
            {
                string text = dirPath.Substring(num + this.txtUIProjectName.Text.Length).Trim(new char[]
				{
					'\\'
				}).Replace("\\", "_") + "_" + this._BusinessNodeInfo.EntityName + "Edit";
                result = text;
            }
            return result;
        }

        private string GetUiCode(string uiCode, string inherits)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Concat(new string[]
			{
				"<%@ Page Title=\"\" Language=\"C#\" MasterPageFile=\"~/App_Master/EditPage.master\" AutoEventWireup=\"true\" CodeFile=\"",
				this._BusinessNodeInfo.EntityName,
				"Edit.aspx.cs\" Inherits=\"",
				inherits,
				"\" %>"
			}));
            stringBuilder.AppendLine("<asp:Content ID=\"Content1\" ContentPlaceHolderID=\"cphHeader\" runat=\"Server\">");
            stringBuilder.AppendLine("</asp:Content>");
            stringBuilder.AppendLine("<asp:Content ID=\"Content2\" ContentPlaceHolderID=\"cphTopToolbar\" runat=\"Server\">");
            stringBuilder.AppendLine(" <WTF:MyToolbar ID=\"MyTopToolBar\" runat=\"server\" ModuleCode=\"\" OperatePlaceTypeValue=\"OperateTopBar\" OnItemCommand=\"CurrentTool_ItemCommand\" />");
            stringBuilder.AppendLine("</asp:Content>");
            stringBuilder.AppendLine("<asp:Content ID=\"Content3\" ContentPlaceHolderID=\"cphContent\" runat=\"Server\">");
            stringBuilder.AppendLine(uiCode);
            stringBuilder.AppendLine("</asp:Content>");
            stringBuilder.AppendLine("<asp:Content ID=\"Content4\" ContentPlaceHolderID=\"cphScriptcbar\" Runat=\"Server\">");
            stringBuilder.AppendLine("</asp:Content>");
            return stringBuilder.ToString();
        }

        private void btnCode_Click(object sender, EventArgs e)
        {
            if (this.CheckInput())
            {
                TableEditSchema tableEditSchema = new TableEditSchema();
                tableEditSchema.TableName = this._BusinessNodeInfo.TableName;
                tableEditSchema.RuleName = "Biz" + this._BusinessNodeInfo.EntityName;
                tableEditSchema.EntityName = this._BusinessNodeInfo.EntityName;
                tableEditSchema.BackUrl = this.txtBackUrl.Text;
                tableEditSchema.Description = this._BusinessNodeInfo.Description;
                IEnumerator enumerator = ((IEnumerable)this.gvdData.Rows).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    DataGridViewRow objDataGridViewRow = (DataGridViewRow)enumerator.Current;
                    string FieldName = objDataGridViewRow.ReadCell("FieldName", "");
                    ColumnEditSchema columnEditSchema = this._CurrentColumnList.First((ColumnEditSchema s) => s.FieldName == FieldName);
                    columnEditSchema.IsShow = bool.Parse(objDataGridViewRow.ReadCell("IsShow", ""));
                    columnEditSchema.ControlType = objDataGridViewRow.ReadCell("ControlType", "");
                    columnEditSchema.ErrorMessage = objDataGridViewRow.ReadCell("ErrorMessage", "");
                    columnEditSchema.FieldTitle = objDataGridViewRow.ReadCell("FieldTitle", "");
                    columnEditSchema.IsEmpty = bool.Parse(objDataGridViewRow.ReadCell("IsEmpty", ""));
                }
                tableEditSchema.Columns = this._CurrentColumnList;
                tableEditSchema.RuleName = "Biz" + this._BusinessNodeInfo.EntityName;
                foreach (ColumnEditSchema current in tableEditSchema.Columns)
                {
                    current.EntityName = this._BusinessNodeInfo.EntityName;
                }
                string text = this._BusinessNodeInfo.EntityName + "Edit.aspx.cs";
                string uiPath = Common.GetUiPath(this.txtUIProjectPath.Text.Trim(new char[]
				{
					'\\'
				}), this._SelectFileInfo);
                string inherits = this.GetInherits(uiPath);
                if (string.IsNullOrWhiteSpace(inherits))
                {
                    MessageBox.Show("Web项目名称输入有误");
                }
                else
                {
                    string codeCode = tableEditSchema.ToCodeStringSql();
                    Common.WriteCode(uiPath + "\\" + text, text, Common.GetCSCode(codeCode, this._SelectFileInfo.ProjectName, inherits), this.chkPreviewCode.Checked);
                    this.UpdateXml(tableEditSchema);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.CheckInput())
            {
                TableEditSchema tableEditSchema = new TableEditSchema();
                tableEditSchema.TableName = this._BusinessNodeInfo.TableName;
                tableEditSchema.RuleName = "Biz" + this._BusinessNodeInfo.EntityName;
                tableEditSchema.EntityName = this._BusinessNodeInfo.EntityName;
                tableEditSchema.BackUrl = this.txtBackUrl.Text;
                tableEditSchema.Description = this._BusinessNodeInfo.Description;
                IEnumerator enumerator = ((IEnumerable)this.gvdData.Rows).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    DataGridViewRow objDataGridViewRow = (DataGridViewRow)enumerator.Current;
                    string FieldName = objDataGridViewRow.ReadCell("FieldName", "");
                    ColumnEditSchema columnEditSchema = this._CurrentColumnList.First((ColumnEditSchema s) => s.FieldName == FieldName);
                    columnEditSchema.IsShow = bool.Parse(objDataGridViewRow.ReadCell("IsShow", ""));
                    columnEditSchema.ControlType = objDataGridViewRow.ReadCell("ControlType", "");
                    columnEditSchema.ErrorMessage = objDataGridViewRow.ReadCell("ErrorMessage", "");
                    columnEditSchema.FieldTitle = objDataGridViewRow.ReadCell("FieldTitle", "");
                    columnEditSchema.IsEmpty = bool.Parse(objDataGridViewRow.ReadCell("IsEmpty", ""));
                    columnEditSchema.ValidationReg = objDataGridViewRow.ReadCell("ValidationReg", "");
                }
                tableEditSchema.Columns = this._CurrentColumnList;
                foreach (ColumnEditSchema current in tableEditSchema.Columns)
                {
                    current.EntityName = this._BusinessNodeInfo.EntityName;
                }
                tableEditSchema.Columns = this._CurrentColumnList;
                string text = this._BusinessNodeInfo.EntityName + "Edit.aspx";
                string text2 = this._BusinessNodeInfo.EntityName + "Edit.aspx.cs";
                string uiPath = Common.GetUiPath(this.txtUIProjectPath.Text.Trim(new char[]
				{
					'\\'
				}), this._SelectFileInfo);
                string inherits = this.GetInherits(uiPath);
                if (string.IsNullOrWhiteSpace(inherits))
                {
                    MessageBox.Show("Web项目名称输入有误");
                }
                else
                {
                    string uiCode = tableEditSchema.ToUiStringSql();
                    string codeCode = tableEditSchema.ToCodeStringSql();
                    Common.WriteCode(uiPath + "\\" + text, text, this.GetUiCode(uiCode, inherits), this.chkPreviewCode.Checked);
                    Common.WriteCode(uiPath + "\\" + text2, text2, Common.GetCSCode(codeCode, this._SelectFileInfo.ProjectName, inherits), this.chkPreviewCode.Checked);
                    this.UpdateXml(tableEditSchema);
                }
            }
        }

        private void UpdateXml(TableEditSchema tableEditSchema)
        {
            XmlNode businessNode = this._CodeConfigHelper.GetBusinessNode(this._BusinessNodeInfo.TableName);
            if (businessNode == null)
            {
                MessageBox.Show(this._BusinessNodeInfo.TableName + "表未配置，请一键生成配置文件！");
            }
            else
            {
                XmlElement xmlElement = (XmlElement)businessNode;
                if (xmlElement.ReadAttribute("UIProjectName", "") != this.txtUIProjectName.Text || xmlElement.ReadAttribute("UIProjectPath", "") != this.txtUIProjectPath.Text || xmlElement.ReadAttribute("ListUrl", "") != this.txtBackUrl.Text)
                {
                    xmlElement.SetAttribute("UIProjectName", this.txtUIProjectName.Text);
                    xmlElement.SetAttribute("UIProjectPath", this.txtUIProjectPath.Text);
                    xmlElement.SetAttribute("ListUrl", this.txtBackUrl.Text);
                    this._CodeConfigHelper.Save();
                }
            }
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
            DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            this.splitEditContainer = new SplitContainer();
            this.btnEdit = new Button();
            this.txtUIProjectName = new TextBox();
            this.label4 = new Label();
            this.txtUIProjectPath = new TextBox();
            this.label3 = new Label();
            this.txtBackUrl = new TextBox();
            this.label2 = new Label();
            this.btnCode = new Button();
            this.btnUi = new Button();
            this.gvdData = new DataGridView();
            this.FieldName = new DataGridViewTextBoxColumn();
            this.FieldTitle = new DataGridViewTextBoxColumn();
            this.ControlType = new DataGridViewComboBoxColumn();
            this.IsShow = new DataGridViewCheckBoxColumn();
            this.IsEmpty = new DataGridViewCheckBoxColumn();
            this.ErrorMessage = new DataGridViewTextBoxColumn();
            this.ValidationReg = new DataGridViewTextBoxColumn();
            this.chkPreviewCode = new CheckBox();
            ((ISupportInitialize)this.splitEditContainer).BeginInit();
            this.splitEditContainer.Panel1.SuspendLayout();
            this.splitEditContainer.Panel2.SuspendLayout();
            this.splitEditContainer.SuspendLayout();
            ((ISupportInitialize)this.gvdData).BeginInit();
            base.SuspendLayout();
            this.splitEditContainer.Dock = DockStyle.Fill;
            this.splitEditContainer.Location = new Point(0, 0);
            this.splitEditContainer.Name = "splitEditContainer";
            this.splitEditContainer.Orientation = Orientation.Horizontal;
            this.splitEditContainer.Panel1.Controls.Add(this.chkPreviewCode);
            this.splitEditContainer.Panel1.Controls.Add(this.btnEdit);
            this.splitEditContainer.Panel1.Controls.Add(this.txtUIProjectName);
            this.splitEditContainer.Panel1.Controls.Add(this.label4);
            this.splitEditContainer.Panel1.Controls.Add(this.txtUIProjectPath);
            this.splitEditContainer.Panel1.Controls.Add(this.label3);
            this.splitEditContainer.Panel1.Controls.Add(this.txtBackUrl);
            this.splitEditContainer.Panel1.Controls.Add(this.label2);
            this.splitEditContainer.Panel1.Controls.Add(this.btnCode);
            this.splitEditContainer.Panel1.Controls.Add(this.btnUi);
            this.splitEditContainer.Panel2.Controls.Add(this.gvdData);
            this.splitEditContainer.Size = new Size(940, 488);
            this.splitEditContainer.SplitterDistance = 88;
            this.splitEditContainer.TabIndex = 0;
            this.btnEdit.Location = new Point(761, 56);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new Size(75, 23);
            this.btnEdit.TabIndex = 38;
            this.btnEdit.Text = "一键生成";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
            this.txtUIProjectName.Location = new Point(463, 12);
            this.txtUIProjectName.Name = "txtUIProjectName";
            this.txtUIProjectName.Size = new Size(222, 21);
            this.txtUIProjectName.TabIndex = 37;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(385, 15);
            this.label4.Name = "label4";
            this.label4.Size = new Size(83, 12);
            this.label4.TabIndex = 36;
            this.label4.Text = "Web项目名称：";
            this.txtUIProjectPath.Location = new Point(84, 12);
            this.txtUIProjectPath.Name = "txtUIProjectPath";
            this.txtUIProjectPath.Size = new Size(295, 21);
            this.txtUIProjectPath.TabIndex = 35;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(9, 15);
            this.label3.Name = "label3";
            this.label3.Size = new Size(83, 12);
            this.label3.TabIndex = 34;
            this.label3.Text = "Web项目路经：";
            this.txtBackUrl.Location = new Point(99, 56);
            this.txtBackUrl.Name = "txtBackUrl";
            this.txtBackUrl.Size = new Size(366, 21);
            this.txtBackUrl.TabIndex = 10;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new Size(89, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "返回界面地址：";
            this.btnCode.Location = new Point(670, 56);
            this.btnCode.Name = "btnCode";
            this.btnCode.Size = new Size(75, 23);
            this.btnCode.TabIndex = 5;
            this.btnCode.Text = "代码生成";
            this.btnCode.UseVisualStyleBackColor = true;
            this.btnCode.Click += new EventHandler(this.btnCode_Click);
            this.btnUi.Location = new Point(574, 56);
            this.btnUi.Name = "btnUi";
            this.btnUi.Size = new Size(75, 23);
            this.btnUi.TabIndex = 4;
            this.btnUi.Text = "界面生成";
            this.btnUi.UseVisualStyleBackColor = true;
            this.btnUi.Click += new EventHandler(this.btnUi_Click);
            this.gvdData.AllowUserToAddRows = false;
            this.gvdData.AllowUserToDeleteRows = false;
            this.gvdData.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle.BackColor = SystemColors.Control;
            dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
            this.gvdData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
            this.gvdData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvdData.Columns.AddRange(new DataGridViewColumn[]
			{
				this.FieldName,
				this.FieldTitle,
				this.ControlType,
				this.IsShow,
				this.IsEmpty,
				this.ErrorMessage,
				this.ValidationReg
			});
            this.gvdData.Dock = DockStyle.Fill;
            this.gvdData.Location = new Point(0, 0);
            this.gvdData.Name = "gvdData";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            this.gvdData.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvdData.RowHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.gvdData.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.gvdData.RowTemplate.Height = 23;
            this.gvdData.Size = new Size(940, 396);
            this.gvdData.TabIndex = 0;
            this.FieldName.DataPropertyName = "FieldName";
            this.FieldName.HeaderText = "字段名";
            this.FieldName.Name = "FieldName";
            this.FieldTitle.DataPropertyName = "FieldTitle";
            this.FieldTitle.HeaderText = "标题";
            this.FieldTitle.Name = "FieldTitle";
            this.FieldTitle.Width = 150;
            this.ControlType.DataPropertyName = "ControlType";
            this.ControlType.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            this.ControlType.HeaderText = "控件类型";
            this.ControlType.Items.AddRange(new object[]
			{
				"TextBox",
				"DropDown",
				"RadioButton",
				"Xhtml",
				"CheckBox",
				"CheckBoxList",
				"TextDateTime",
				"TextDate"
			});
            this.ControlType.Name = "ControlType";
            this.IsShow.DataPropertyName = "IsShow";
            this.IsShow.FalseValue = "False";
            this.IsShow.HeaderText = "是否显示";
            this.IsShow.Name = "IsShow";
            this.IsShow.TrueValue = "True";
            this.IsShow.Width = 60;
            this.IsEmpty.DataPropertyName = "IsEmpty";
            this.IsEmpty.FalseValue = "False";
            this.IsEmpty.HeaderText = "是否可空";
            this.IsEmpty.Name = "IsEmpty";
            this.IsEmpty.TrueValue = "True";
            this.IsEmpty.Width = 60;
            this.ErrorMessage.DataPropertyName = "ErrorMessage";
            this.ErrorMessage.HeaderText = "错误提示";
            this.ErrorMessage.Name = "ErrorMessage";
            this.ErrorMessage.Width = 200;
            this.ValidationReg.DataPropertyName = "ValidationReg";
            this.ValidationReg.HeaderText = "验证正则";
            this.ValidationReg.Name = "ValidationReg";
            this.ValidationReg.Resizable = DataGridViewTriState.True;
            this.chkPreviewCode.AutoSize = true;
            this.chkPreviewCode.Location = new Point(487, 59);
            this.chkPreviewCode.Name = "chkPreviewCode";
            this.chkPreviewCode.Size = new Size(72, 16);
            this.chkPreviewCode.TabIndex = 39;
            this.chkPreviewCode.Text = "预览代码";
            this.chkPreviewCode.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(940, 488);
            base.Controls.Add(this.splitEditContainer);
            base.Name = "SqlEditForm";
            this.Text = "新增编辑页";
            this.splitEditContainer.Panel1.ResumeLayout(false);
            this.splitEditContainer.Panel1.PerformLayout();
            this.splitEditContainer.Panel2.ResumeLayout(false);
            ((ISupportInitialize)this.splitEditContainer).EndInit();
            this.splitEditContainer.ResumeLayout(false);
            ((ISupportInitialize)this.gvdData).EndInit();
            base.ResumeLayout(false);
        }
    }
}

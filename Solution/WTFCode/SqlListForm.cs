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
    public class SqlListForm : Form
    {
        private List<ColumnEditSchema> _CurrentColumnList = new List<ColumnEditSchema>();

        private CodeConfigHelper _CodeConfigHelper = null;

        private BusinessNodeInfo _BusinessNodeInfo = null;

        private TableListSchema _TableListSchema = null;

        private SelectFileInfo _SelectFileInfo = null;

        private IContainer components = null;

        private SplitContainer splitContainer1;

        private Button btnUICreate;

        private Button btnCodeCreate;

        private TextBox txtEditUrl;

        private Label label3;

        private SplitContainer splitContainer2;

        private DataGridView gvdCommand;

        private DataGridView gvdTableCols;

        private TextBox txtModuleID;

        private Label label2;

        private DataGridViewTextBoxColumn ModuleName;

        private DataGridViewTextBoxColumn CommandName;

        private DataGridViewComboBoxColumn ProcessType;

        private DataGridViewTextBoxColumn RedirectUrl;

        private DataGridViewCheckBoxColumn IsTop;

        private DataGridViewCheckBoxColumn IsList;

        private DataGridViewCheckBoxColumn IsBotom;

        private DataGridViewTextBoxColumn FieldName;

        private DataGridViewTextBoxColumn FieldTitle;

        private DataGridViewCheckBoxColumn IsShow;

        private DataGridViewComboBoxColumn ControlType;

        private DataGridViewCheckBoxColumn IsSearch;

        private DataGridViewCheckBoxColumn IsSort;

        private new DataGridViewTextBoxColumn Width;

        private TextBox txtUIProjectName;

        private Label label7;

        private TextBox txtUIProjectPath;

        private Label label4;

        private Button btnLoadColumn;

        private Button btnList;

        private CheckBox chkPreviewCode;

        public SqlListForm(SelectFileInfo objSelectFileInfo, BusinessNodeInfo objBusinessNodeInfo)
        {
            this.InitializeComponent();
            this._SelectFileInfo = objSelectFileInfo;
            this._CodeConfigHelper = new CodeConfigHelper(objSelectFileInfo.CodeConfigPath);
            if (this._CodeConfigHelper.LoadCodeConfigXml())
            {
                this._BusinessNodeInfo = objBusinessNodeInfo;
                this.Text = this._BusinessNodeInfo.TableName + "列表界面生成";
                string text = "";
                string text2 = "";
                this._CodeConfigHelper.GetBusinessNodeInfoUIPath(objBusinessNodeInfo, out text, out text2);
                this.txtUIProjectPath.Text = text;
                this.txtUIProjectName.Text = text2;
                this.txtModuleID.Text = this._BusinessNodeInfo.ModuleID;
                this.txtEditUrl.Text = (string.IsNullOrWhiteSpace(this._BusinessNodeInfo.EditUrl) ? (objBusinessNodeInfo.EntityName + "Edit.aspx") : this._BusinessNodeInfo.EditUrl);
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

        private void btnLoadColumn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtModuleID.Text))
            {
                MessageBox.Show("请输入模块标识");
            }
            else
            {
                string schemaName = "";
                string connectionString = this._CodeConfigHelper.GetConnectionString(this._BusinessNodeInfo.ConnectionKey, out schemaName);
                this._TableListSchema = SqlSchemaHelper.GetTableListSchema(schemaName, this._BusinessNodeInfo.TableName, this.txtModuleID.Text, this.txtEditUrl.Text, connectionString, this._CodeConfigHelper, this._SelectFileInfo.WTFConfigPath);
                this.gvdCommand.AutoGenerateColumns = false;
                this.gvdCommand.DataSource = (from s in this._TableListSchema.Commands
                                              orderby s.SortIndex
                                              select s).ToList<CommandSchema>();
                this.gvdTableCols.AutoGenerateColumns = false;
                this.gvdTableCols.DataSource = (from s in this._TableListSchema.Columns
                                                where !s.FieldName.ToLower().Contains("guid")
                                                select s).ToList<ColumnListSchema>();
            }
        }

        private void btnUICreate_Click(object sender, EventArgs e)
        {
            if (this.CheckInput())
            {
                if (!this.IsLoadTableListSchema())
                {
                    this._TableListSchema.TableName = this._BusinessNodeInfo.TableName;
                    this._TableListSchema.EntityName = this._BusinessNodeInfo.EntityName;
                    this._TableListSchema.RuleName = "Biz" + this._BusinessNodeInfo.EntityName;
                    IEnumerator enumerator = ((IEnumerable)this.gvdTableCols.Rows).GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        DataGridViewRow objDataGridViewRow = (DataGridViewRow)enumerator.Current;
                        string FieldName = objDataGridViewRow.ReadCell("FieldName", "");
                        ColumnListSchema columnListSchema = this._TableListSchema.Columns.First((ColumnListSchema s) => s.FieldName == FieldName);
                        columnListSchema.IsShow = bool.Parse(objDataGridViewRow.ReadCell("IsShow", ""));
                        columnListSchema.ControlType = objDataGridViewRow.ReadCell("ControlType", "");
                        columnListSchema.FieldTitle = objDataGridViewRow.ReadCell("FieldTitle", "");
                        columnListSchema.Width = int.Parse(objDataGridViewRow.ReadCell("Width", ""));
                        columnListSchema.IsSearch = bool.Parse(objDataGridViewRow.ReadCell("IsSearch", ""));
                        columnListSchema.IsSort = bool.Parse(objDataGridViewRow.ReadCell("IsSort", ""));
                    }
                    string uiCode = this._TableListSchema.ToUiCode();
                    string uiSearch = this._TableListSchema.ToUiSearch();
                    string text = this._BusinessNodeInfo.EntityName + "List.aspx";
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
                        Common.WriteCode(uiPath + "\\" + text, text, this.GetUiCode(uiSearch, uiCode, inherits), this.chkPreviewCode.Checked);
                        this.UpdateXml(this._TableListSchema);
                    }
                }
            }
        }

        private void btnCodeCreate_Click(object sender, EventArgs e)
        {
            if (this.CheckInput())
            {
                if (!this.IsLoadTableListSchema())
                {
                    this._TableListSchema.TableName = this._BusinessNodeInfo.TableName;
                    this._TableListSchema.EntityName = this._BusinessNodeInfo.EntityName;
                    this._TableListSchema.RuleName = "Biz" + this._BusinessNodeInfo.EntityName;
                    IEnumerator enumerator = ((IEnumerable)this.gvdCommand.Rows).GetEnumerator();
                    {
                        while (enumerator.MoveNext())
                        {
                            DataGridViewRow objDataGridViewRow = (DataGridViewRow)enumerator.Current;
                            string CommandName = objDataGridViewRow.ReadCell("CommandName", "");
                            CommandSchema commandSchema = this._TableListSchema.Commands.First((CommandSchema s) => s.CommandName == CommandName);
                            commandSchema.IsTop = bool.Parse(objDataGridViewRow.ReadCell("IsTop", ""));
                            commandSchema.IsList = bool.Parse(objDataGridViewRow.ReadCell("IsList", ""));
                            commandSchema.IsBotom = bool.Parse(objDataGridViewRow.ReadCell("IsBotom", ""));
                            commandSchema.ProcessType = objDataGridViewRow.ReadCell("ProcessType", "");
                            commandSchema.RedirectUrl = objDataGridViewRow.ReadCell("RedirectUrl", "");
                        }
                    }
                    string text = this._BusinessNodeInfo.EntityName + "List.aspx.cs";
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
                        string codeCode = this._TableListSchema.ToCodeSql();
                        Common.WriteCode(uiPath + "\\" + text, text, Common.GetCSCode(codeCode, this._SelectFileInfo.ProjectName, inherits), this.chkPreviewCode.Checked);
                        this.UpdateXml(this._TableListSchema);
                    }
                }
            }
        }

        private bool IsLoadTableListSchema()
        {
            bool result;
            if (this._TableListSchema == null)
            {
                MessageBox.Show("请加载字段");
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            if (this.CheckInput())
            {
                if (!this.IsLoadTableListSchema())
                {
                    this._TableListSchema.TableName = this._BusinessNodeInfo.TableName;
                    this._TableListSchema.EntityName = this._BusinessNodeInfo.EntityName;
                    this._TableListSchema.RuleName = "Biz" + this._BusinessNodeInfo.EntityName;
                    IEnumerator enumerator = ((IEnumerable)this.gvdCommand.Rows).GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        DataGridViewRow objDataGridViewRow = (DataGridViewRow)enumerator.Current;
                        string CommandName = objDataGridViewRow.ReadCell("CommandName", "");
                        CommandSchema commandSchema = this._TableListSchema.Commands.First((CommandSchema s) => s.CommandName == CommandName);
                        commandSchema.IsTop = bool.Parse(objDataGridViewRow.ReadCell("IsTop", ""));
                        commandSchema.IsList = bool.Parse(objDataGridViewRow.ReadCell("IsList", ""));
                        commandSchema.IsBotom = bool.Parse(objDataGridViewRow.ReadCell("IsBotom", ""));
                        commandSchema.ProcessType = objDataGridViewRow.ReadCell("ProcessType", "");
                        commandSchema.RedirectUrl = objDataGridViewRow.ReadCell("RedirectUrl", "");
                    }
                    string text = this._BusinessNodeInfo.EntityName + "List.aspx";
                    string text2 = this._BusinessNodeInfo.EntityName + "List.aspx.cs";
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
                        string uiCode = this._TableListSchema.ToUiCode();
                        string uiSearch = this._TableListSchema.ToUiSearch();
                        string codeCode = this._TableListSchema.ToCodeSql();
                        Common.WriteCode(uiPath + "\\" + text, text, this.GetUiCode(uiSearch, uiCode, inherits), this.chkPreviewCode.Checked);
                        Common.WriteCode(uiPath + "\\" + text2, text2, Common.GetCSCode(codeCode, this._SelectFileInfo.ProjectName, inherits), this.chkPreviewCode.Checked);
                        this.UpdateXml(this._TableListSchema);
                    }
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
				}).Replace("\\", "_") + "_" + this._BusinessNodeInfo.EntityName + "List";
                result = text;
            }
            return result;
        }

        private string GetUiCode(string uiSearch, string uiCode, string inherits)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Concat(new string[]
			{
				"<%@ Page Title=\"\" Language=\"C#\" MasterPageFile=\"~/App_Master/ListPage.master\" AutoEventWireup=\"true\" CodeFile=\"",
				this._BusinessNodeInfo.EntityName,
				"List.aspx.cs\" Inherits=\"",
				inherits,
				"\" %>"
			}));
            stringBuilder.AppendLine("<asp:Content ID=\"Content1\" ContentPlaceHolderID=\"cphHeader\" runat=\"Server\">");
            stringBuilder.AppendLine("</asp:Content>");
            stringBuilder.AppendLine("<asp:Content ID=\"Content2\" ContentPlaceHolderID=\"cphTopToolbar\" runat=\"Server\">");
            stringBuilder.AppendLine(" <WTF:MyToolbar ID=\"MyTopToolBar\" runat=\"server\" ModuleCode=\"\" OperatePlaceTypeValue=\"OperateTopBar\" OnItemCommand=\"CurrentTool_ItemCommand\" />");
            stringBuilder.AppendLine("</asp:Content>");
            stringBuilder.AppendLine("<asp:Content ID=\"Content3\" ContentPlaceHolderID=\"cphSearchBar\" Runat=\"Server\">");
            stringBuilder.AppendLine(uiSearch);
            stringBuilder.AppendLine("</asp:Content>");
            stringBuilder.AppendLine("<asp:Content ID=\"Content4\" ContentPlaceHolderID=\"cphContent\" runat=\"Server\">");
            stringBuilder.AppendLine(uiCode);
            stringBuilder.AppendLine("</asp:Content>");
            stringBuilder.AppendLine("<asp:Content ID=\"Content5\" ContentPlaceHolderID=\"cphScriptcbar\" Runat=\"Server\">");
            stringBuilder.AppendLine("</asp:Content>");
            return stringBuilder.ToString();
        }

        private void UpdateXml(TableListSchema tableListSchema)
        {
            XmlNode businessNode = this._CodeConfigHelper.GetBusinessNode(this._BusinessNodeInfo.TableName);
            if (businessNode == null)
            {
                MessageBox.Show(this._BusinessNodeInfo.TableName + "表未配置，请一键生成配置文件！");
            }
            else
            {
                XmlElement xmlElement = (XmlElement)businessNode;
                if (xmlElement.ReadAttribute("UIProjectName", "") != this.txtUIProjectName.Text || xmlElement.ReadAttribute("UIProjectPath", "") != this.txtUIProjectPath.Text || xmlElement.ReadAttribute("ModuleID", "") != this.txtModuleID.Text || xmlElement.ReadAttribute("EditUrl", "") != this.txtEditUrl.Text)
                {
                    xmlElement.SetAttribute("UIProjectName", this.txtUIProjectName.Text);
                    xmlElement.SetAttribute("UIProjectPath", this.txtUIProjectPath.Text);
                    xmlElement.SetAttribute("ModuleID", this.txtModuleID.Text);
                    xmlElement.SetAttribute("EditUrl", this.txtEditUrl.Text);
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
            this.splitContainer1 = new SplitContainer();
            this.btnList = new Button();
            this.btnLoadColumn = new Button();
            this.txtUIProjectPath = new TextBox();
            this.label4 = new Label();
            this.txtUIProjectName = new TextBox();
            this.label7 = new Label();
            this.txtModuleID = new TextBox();
            this.label2 = new Label();
            this.txtEditUrl = new TextBox();
            this.label3 = new Label();
            this.btnCodeCreate = new Button();
            this.btnUICreate = new Button();
            this.splitContainer2 = new SplitContainer();
            this.gvdCommand = new DataGridView();
            this.ModuleName = new DataGridViewTextBoxColumn();
            this.CommandName = new DataGridViewTextBoxColumn();
            this.ProcessType = new DataGridViewComboBoxColumn();
            this.RedirectUrl = new DataGridViewTextBoxColumn();
            this.IsTop = new DataGridViewCheckBoxColumn();
            this.IsList = new DataGridViewCheckBoxColumn();
            this.IsBotom = new DataGridViewCheckBoxColumn();
            this.gvdTableCols = new DataGridView();
            this.FieldName = new DataGridViewTextBoxColumn();
            this.FieldTitle = new DataGridViewTextBoxColumn();
            this.IsShow = new DataGridViewCheckBoxColumn();
            this.ControlType = new DataGridViewComboBoxColumn();
            this.IsSearch = new DataGridViewCheckBoxColumn();
            this.IsSort = new DataGridViewCheckBoxColumn();
            this.Width = new DataGridViewTextBoxColumn();
            this.chkPreviewCode = new CheckBox();
            ((ISupportInitialize)this.splitContainer1).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((ISupportInitialize)this.splitContainer2).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((ISupportInitialize)this.gvdCommand).BeginInit();
            ((ISupportInitialize)this.gvdTableCols).BeginInit();
            base.SuspendLayout();
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = Orientation.Horizontal;
            this.splitContainer1.Panel1.Controls.Add(this.chkPreviewCode);
            this.splitContainer1.Panel1.Controls.Add(this.btnList);
            this.splitContainer1.Panel1.Controls.Add(this.btnLoadColumn);
            this.splitContainer1.Panel1.Controls.Add(this.txtUIProjectPath);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.txtUIProjectName);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.txtModuleID);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.txtEditUrl);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.btnCodeCreate);
            this.splitContainer1.Panel1.Controls.Add(this.btnUICreate);
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new Size(1133, 493);
            this.splitContainer1.SplitterDistance = 78;
            this.splitContainer1.TabIndex = 0;
            this.btnList.Location = new Point(919, 39);
            this.btnList.Name = "btnList";
            this.btnList.Size = new Size(75, 23);
            this.btnList.TabIndex = 47;
            this.btnList.Text = "一键生成";
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new EventHandler(this.btnList_Click);
            this.btnLoadColumn.Location = new Point(645, 10);
            this.btnLoadColumn.Name = "btnLoadColumn";
            this.btnLoadColumn.Size = new Size(75, 23);
            this.btnLoadColumn.TabIndex = 46;
            this.btnLoadColumn.Text = "加载字段";
            this.btnLoadColumn.UseVisualStyleBackColor = true;
            this.btnLoadColumn.Click += new EventHandler(this.btnLoadColumn_Click);
            this.txtUIProjectPath.Location = new Point(106, 9);
            this.txtUIProjectPath.Name = "txtUIProjectPath";
            this.txtUIProjectPath.Size = new Size(270, 21);
            this.txtUIProjectPath.TabIndex = 45;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(23, 12);
            this.label4.Name = "label4";
            this.label4.Size = new Size(83, 12);
            this.label4.TabIndex = 44;
            this.label4.Text = "Web项目路经：";
            this.txtUIProjectName.Location = new Point(461, 9);
            this.txtUIProjectName.Name = "txtUIProjectName";
            this.txtUIProjectName.Size = new Size(178, 21);
            this.txtUIProjectName.TabIndex = 41;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(382, 13);
            this.label7.Name = "label7";
            this.label7.Size = new Size(83, 12);
            this.label7.TabIndex = 40;
            this.label7.Text = "Web项目名称：";
            this.txtModuleID.Location = new Point(106, 39);
            this.txtModuleID.Name = "txtModuleID";
            this.txtModuleID.Size = new Size(172, 21);
            this.txtModuleID.TabIndex = 11;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(35, 43);
            this.label2.Name = "label2";
            this.label2.Size = new Size(65, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "模块标识：";
            this.txtEditUrl.Location = new Point(356, 39);
            this.txtEditUrl.Name = "txtEditUrl";
            this.txtEditUrl.Size = new Size(283, 21);
            this.txtEditUrl.TabIndex = 9;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(284, 43);
            this.label3.Name = "label3";
            this.label3.Size = new Size(77, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "编辑页地址：";
            this.btnCodeCreate.Location = new Point(814, 39);
            this.btnCodeCreate.Name = "btnCodeCreate";
            this.btnCodeCreate.Size = new Size(75, 23);
            this.btnCodeCreate.TabIndex = 5;
            this.btnCodeCreate.Text = "代码生成";
            this.btnCodeCreate.UseVisualStyleBackColor = true;
            this.btnCodeCreate.Click += new EventHandler(this.btnCodeCreate_Click);
            this.btnUICreate.Location = new Point(733, 39);
            this.btnUICreate.Name = "btnUICreate";
            this.btnUICreate.Size = new Size(75, 23);
            this.btnUICreate.TabIndex = 4;
            this.btnUICreate.Text = "界面生成";
            this.btnUICreate.UseVisualStyleBackColor = true;
            this.btnUICreate.Click += new EventHandler(this.btnUICreate_Click);
            this.splitContainer2.Dock = DockStyle.Fill;
            this.splitContainer2.Location = new Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Panel1.Controls.Add(this.gvdCommand);
            this.splitContainer2.Panel2.Controls.Add(this.gvdTableCols);
            this.splitContainer2.Size = new Size(1133, 411);
            this.splitContainer2.SplitterDistance = 494;
            this.splitContainer2.TabIndex = 0;
            this.gvdCommand.AllowUserToAddRows = false;
            this.gvdCommand.AllowUserToDeleteRows = false;
            dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle.BackColor = SystemColors.Control;
            dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
            this.gvdCommand.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
            this.gvdCommand.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvdCommand.Columns.AddRange(new DataGridViewColumn[]
			{
				this.ModuleName,
				this.CommandName,
				this.ProcessType,
				this.RedirectUrl,
				this.IsTop,
				this.IsList,
				this.IsBotom
			});
            this.gvdCommand.Dock = DockStyle.Fill;
            this.gvdCommand.Location = new Point(0, 0);
            this.gvdCommand.Name = "gvdCommand";
            this.gvdCommand.RowHeadersVisible = false;
            this.gvdCommand.RowTemplate.Height = 23;
            this.gvdCommand.Size = new Size(494, 411);
            this.gvdCommand.TabIndex = 0;
            this.ModuleName.DataPropertyName = "ModuleName";
            this.ModuleName.HeaderText = "模块名";
            this.ModuleName.Name = "ModuleName";
            this.ModuleName.Width = 70;
            this.CommandName.DataPropertyName = "CommandName";
            this.CommandName.HeaderText = "命令名";
            this.CommandName.Name = "CommandName";
            this.CommandName.Width = 70;
            this.ProcessType.DataPropertyName = "ProcessType";
            this.ProcessType.HeaderText = "处理方式";
            this.ProcessType.Items.AddRange(new object[]
			{
				"Redirect",
				"RedirectState",
				"RenderPage"
			});
            this.ProcessType.Name = "ProcessType";
            this.ProcessType.Width = 90;
            this.RedirectUrl.DataPropertyName = "RedirectUrl";
            this.RedirectUrl.HeaderText = "地址";
            this.RedirectUrl.Name = "RedirectUrl";
            this.RedirectUrl.Width = 80;
            this.IsTop.DataPropertyName = "IsTop";
            this.IsTop.FalseValue = "False";
            this.IsTop.HeaderText = "顶";
            this.IsTop.Name = "IsTop";
            this.IsTop.TrueValue = "True";
            this.IsTop.Width = 30;
            this.IsList.DataPropertyName = "IsList";
            this.IsList.FalseValue = "False";
            this.IsList.HeaderText = "列";
            this.IsList.Name = "IsList";
            this.IsList.TrueValue = "True";
            this.IsList.Width = 30;
            this.IsBotom.DataPropertyName = "IsBotom";
            this.IsBotom.FalseValue = "False";
            this.IsBotom.HeaderText = "底";
            this.IsBotom.Name = "IsBotom";
            this.IsBotom.TrueValue = "True";
            this.IsBotom.Width = 30;
            this.gvdTableCols.AllowUserToAddRows = false;
            this.gvdTableCols.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            this.gvdTableCols.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvdTableCols.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvdTableCols.Columns.AddRange(new DataGridViewColumn[]
			{
				this.FieldName,
				this.FieldTitle,
				this.IsShow,
				this.ControlType,
				this.IsSearch,
				this.IsSort,
				this.Width
			});
            this.gvdTableCols.Dock = DockStyle.Fill;
            this.gvdTableCols.Location = new Point(0, 0);
            this.gvdTableCols.Name = "gvdTableCols";
            this.gvdTableCols.RowHeadersVisible = false;
            this.gvdTableCols.RowTemplate.Height = 23;
            this.gvdTableCols.Size = new Size(635, 411);
            this.gvdTableCols.TabIndex = 0;
            this.FieldName.DataPropertyName = "FieldName";
            this.FieldName.HeaderText = "字段名";
            this.FieldName.Name = "FieldName";
            this.FieldTitle.DataPropertyName = "FieldTitle";
            this.FieldTitle.HeaderText = "标题";
            this.FieldTitle.Name = "FieldTitle";
            this.IsShow.DataPropertyName = "IsShow";
            this.IsShow.FalseValue = "False";
            this.IsShow.HeaderText = "是否显示";
            this.IsShow.Name = "IsShow";
            this.IsShow.TrueValue = "True";
            this.IsShow.Width = 60;
            this.ControlType.DataPropertyName = "ControlType";
            this.ControlType.HeaderText = "控件类型";
            this.ControlType.Items.AddRange(new object[]
			{
				"OperateField",
				"TemplateField",
				"BoundField"
			});
            this.ControlType.Name = "ControlType";
            this.IsSearch.DataPropertyName = "IsSearch";
            this.IsSearch.FalseValue = "False";
            this.IsSearch.HeaderText = "是否查询";
            this.IsSearch.Name = "IsSearch";
            this.IsSearch.TrueValue = "True";
            this.IsSearch.Width = 60;
            this.IsSort.DataPropertyName = "IsSort";
            this.IsSort.FalseValue = "False";
            this.IsSort.HeaderText = "是否排序";
            this.IsSort.Name = "IsSort";
            this.IsSort.TrueValue = "True";
            this.IsSort.Width = 60;
            this.Width.DataPropertyName = "Width";
            this.Width.HeaderText = "宽度";
            this.Width.Name = "Width";
            this.Width.Width = 60;
            this.chkPreviewCode.AutoSize = true;
            this.chkPreviewCode.Location = new Point(648, 43);
            this.chkPreviewCode.Name = "chkPreviewCode";
            this.chkPreviewCode.Size = new Size(72, 16);
            this.chkPreviewCode.TabIndex = 48;
            this.chkPreviewCode.Text = "预览代码";
            this.chkPreviewCode.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(1133, 493);
            base.Controls.Add(this.splitContainer1);
            base.Name = "SqlListForm";
            this.Text = "新增列表页";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((ISupportInitialize)this.splitContainer1).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((ISupportInitialize)this.splitContainer2).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((ISupportInitialize)this.gvdCommand).EndInit();
            ((ISupportInitialize)this.gvdTableCols).EndInit();
            base.ResumeLayout(false);
        }
    }
}

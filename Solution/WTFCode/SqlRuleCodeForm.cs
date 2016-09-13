using EnvDTE80;
using WTF.CodeRule;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WTFCode
{
    public class SqlRuleCodeForm : Form
    {
        private CodeConfigHelper _CodeConfigHelper = null;

        private SelectFileInfo _SelectFileInfo = null;

        private DTE2 _applicationObject;

        private List<TableRuleSchema> _TableRuleSchemaList = new List<TableRuleSchema>();

        private string _ConnectionString = string.Empty;

        private string _Database = string.Empty;

        private string CurrentEditTable = "";

        private IContainer components = null;

        private SplitContainer splitContainer1;

        private SplitContainer splitContainer2;

        private Button btnLoadRuleTable;

        private DataGridView gvwTableCols;

        private DataGridView gvdTables;

        private Label label2;

        private ComboBox boxLogType;

        private DataGridViewTextBoxColumn FieldName;

        private DataGridViewTextBoxColumn FieldTitle;

        private DataGridViewCheckBoxColumn IsCheck;

        private DataGridViewTextBoxColumn ErrorMessage;

        private ComboBox boxConnectString;

        private Label label5;

        private Button btnRuleCode;

        private Button BtnDataAccess;

        private Button btnEntity;

        private Button btnCreateRule;

        private TextBox txtConnectionKey;

        private Label label1;

        private CheckBox chkPreviewCode;

        private Button BtnSolrEntity;

        private Button btnSolrIndex;

        private CheckBox IsView;

        private Button btnEntityPros;

        private CheckBox chkFieldLength;

        private DataGridViewCheckBoxColumn IsCreate;

        private DataGridViewButtonColumn SetCheck;

        private DataGridViewTextBoxColumn TableName;

        private DataGridViewTextBoxColumn EntityName;

        private DataGridViewTextBoxColumn Description;

        private DataGridViewTextBoxColumn LogModuleType;

        private DataGridViewTextBoxColumn ConnectionKeyOrConnectionString;

        private DataGridViewCheckBoxColumn IsMongoDB;

        private DataGridViewCheckBoxColumn IsCheckFieldLength;

        private Button btnJavaEntity;

        private Button btnJavaDal;

        private Button btnJavaSet;

        private Button btnESMapping;

        private Button BtnEntityValue;

        public SqlRuleCodeForm(SelectFileInfo objSelectFileInfo)
        {
            this.InitializeComponent();
            this._SelectFileInfo = objSelectFileInfo;
            this._CodeConfigHelper = new CodeConfigHelper(objSelectFileInfo.CodeConfigPath);
            if (this._CodeConfigHelper.LoadCodeConfigXml())
            {
                this.txtConnectionKey.Text = this._CodeConfigHelper.GetBusinessNodeInfoExistsConnectionString();
                this._CodeConfigHelper.InitConnectionStrings(this.boxConnectString);
                this.LoadTableSchema(this.boxConnectString.SelectedItem.ToString());
                this.boxLogType.Items.Clear();
                foreach (DataRow dataRow in SqlSchemaHelper.GetLogType(objSelectFileInfo.WTFConfigPath).Rows)
                {
                    this.boxLogType.Items.Add(dataRow["ModuleTypeCode"].ToString());
                }
                this.boxLogType.SelectedItem = this._CodeConfigHelper.GetBusinessNodeInfoExistsLogModuleType();
            }
        }

        private void btnLoadRuleTable_Click(object sender, EventArgs e)
        {
            if (this.boxConnectString.SelectedItem == null)
            {
                MessageBox.Show("请选择连接串");
            }
            else
            {
                this.LoadTableSchema(this.boxConnectString.SelectedItem.ToString());
            }
        }

        private void LoadTableSchema(string ConnectionStringName)
        {
            if (!string.IsNullOrWhiteSpace(ConnectionStringName))
            {
                this.gvdTables.AutoGenerateColumns = false;
                this._ConnectionString = this._CodeConfigHelper.GetConnectionString(ConnectionStringName, out this._Database);
                try
                {
                    this._TableRuleSchemaList = SqlSchemaHelper.GetAllRuleTables(this._Database, this._ConnectionString, !this.IsView.Checked);
                    List<BusinessNodeInfo> business = this._CodeConfigHelper.GetBusiness();
                    using (List<TableRuleSchema>.Enumerator enumerator = this._TableRuleSchemaList.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            TableRuleSchema objTableRuleSchema = enumerator.Current;
                            BusinessNodeInfo objBusinessNodeInfo = business.FirstOrDefault((BusinessNodeInfo s) => s.TableName == objTableRuleSchema.TableName);
                            CodeConfigHelper.BusinessNodeToTableRuleSchema(objBusinessNodeInfo, objTableRuleSchema);
                        }
                    }
                    this.gvdTables.DataSource = this._TableRuleSchemaList;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("加载表错误，异常信息：" + ex.ToString());
                }
            }
        }

        private bool CheckIsXmlField(ColumnRuleSchema objColumnRuleSchema, DataGridViewRow objDataRow)
        {
            return objColumnRuleSchema.IsXmlField || (bool.Parse(objDataRow.ReadCell("IsCheck", "")) || objColumnRuleSchema.OldErrorMessage != objDataRow.ReadCell("ErrorMessage", "") || objColumnRuleSchema.OldFieldTitle != objDataRow.ReadCell("FieldTitle", ""));
        }

        private void gvdTables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.gvdTables.Columns[e.ColumnIndex].Name == "SetCheck")
            {
                if (!string.IsNullOrEmpty(this.CurrentEditTable))
                {
                    List<ColumnRuleSchema> columns = this._TableRuleSchemaList.First((TableRuleSchema s) => s.TableName == this.CurrentEditTable).Columns;
                    IEnumerator enumerator = ((IEnumerable)this.gvwTableCols.Rows).GetEnumerator();
                    {
                        while (enumerator.MoveNext())
                        {
                            DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator.Current;
                            string FieldName = dataGridViewRow.ReadCell("FieldName", "");
                            ColumnRuleSchema columnRuleSchema = columns.First((ColumnRuleSchema s) => s.FieldName == FieldName);
                            columnRuleSchema.IsXmlField = this.CheckIsXmlField(columnRuleSchema, dataGridViewRow);
                            columnRuleSchema.IsCheck = bool.Parse(dataGridViewRow.ReadCell("IsCheck", ""));
                            columnRuleSchema.ErrorMessage = dataGridViewRow.ReadCell("ErrorMessage", "");
                            columnRuleSchema.FieldTitle = dataGridViewRow.ReadCell("FieldTitle", "");
                        }
                    }
                }
                string TableName = this.gvdTables.Rows[e.RowIndex].ReadCell("TableName", "");
                this.CurrentEditTable = TableName;
                TableRuleSchema tableRuleSchema = this._TableRuleSchemaList.First((TableRuleSchema s) => s.TableName == TableName);
                if (tableRuleSchema.Columns.Count == 0)
                {
                    tableRuleSchema.Columns = SqlSchemaHelper.GetTableRuleColumnsSchema(this._ConnectionString, this._Database, TableName, this._CodeConfigHelper);
                }
                this.gvwTableCols.AutoGenerateColumns = false;
                this.gvwTableCols.DataSource = tableRuleSchema.Columns;
            }
        }

        private void gvwTableCols_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private List<TableRuleSchema> ProcessData()
        {
            if (!string.IsNullOrEmpty(this.CurrentEditTable))
            {
                List<ColumnRuleSchema> columns = this._TableRuleSchemaList.First((TableRuleSchema s) => s.TableName == this.CurrentEditTable).Columns;
                IEnumerator enumerator = ((IEnumerable)this.gvwTableCols.Rows).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator.Current;
                    string FieldName = dataGridViewRow.ReadCell("FieldName", "");
                    ColumnRuleSchema columnRuleSchema = columns.First((ColumnRuleSchema s) => s.FieldName == FieldName);
                    columnRuleSchema.IsXmlField = this.CheckIsXmlField(columnRuleSchema, dataGridViewRow);
                    columnRuleSchema.IsCheck = bool.Parse(dataGridViewRow.ReadCell("IsCheck", ""));
                    columnRuleSchema.ErrorMessage = dataGridViewRow.ReadCell("ErrorMessage", "");
                    columnRuleSchema.FieldTitle = dataGridViewRow.ReadCell("FieldTitle", "");
                }
            }
            List<TableRuleSchema> result;
            IEnumerator enumerator2 = ((IEnumerable)this.gvdTables.Rows).GetEnumerator();
            while (enumerator2.MoveNext())
            {
                DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator2.Current;
                string TableName = dataGridViewRow.ReadCell("TableName", "");
                string text = dataGridViewRow.ReadCell("LogModuleType", "");
                text = (string.IsNullOrWhiteSpace(text) ? ((this.boxLogType.SelectedItem == null) ? "" : this.boxLogType.SelectedItem.ToString()) : text);
                string text2 = dataGridViewRow.ReadCell("ConnectionKeyOrConnectionString", "");
                text2 = (string.IsNullOrWhiteSpace(text2) ? this.txtConnectionKey.Text : text2);
                TableRuleSchema tableRuleSchema = this._TableRuleSchemaList.First((TableRuleSchema s) => s.TableName == TableName);
                tableRuleSchema.IsCreate = bool.Parse(dataGridViewRow.ReadCell("IsCreate", ""));
                tableRuleSchema.Description = dataGridViewRow.ReadCell("Description", "");
                tableRuleSchema.EntityName = dataGridViewRow.ReadCell("EntityName", "");
                tableRuleSchema.IsMongoDB = bool.Parse(dataGridViewRow.ReadCell("IsMongoDB", ""));
                tableRuleSchema.IsCheckFieldLength = bool.Parse(dataGridViewRow.ReadCell("IsCheckFieldLength", ""));
                tableRuleSchema.ConnectionKeyOrConnectionString = text2;
                tableRuleSchema.LogModuleType = text;
                if (tableRuleSchema.IsCreate && string.IsNullOrWhiteSpace(tableRuleSchema.LogModuleType))
                {
                    MessageBox.Show("表名:" + TableName + "，请输入日志类型或选择日志类型");
                    result = null;
                    return result;
                }
                if (tableRuleSchema.IsCreate && tableRuleSchema.Columns.Count == 0)
                {
                    tableRuleSchema.Columns = SqlSchemaHelper.GetTableRuleColumnsSchema(this._ConnectionString, this._Database, tableRuleSchema.TableName, this._CodeConfigHelper);
                }
            }
            List<TableRuleSchema> list = (from s in this._TableRuleSchemaList
                                          where s.IsCreate
                                          select s).ToList<TableRuleSchema>();
            if (list.Count<TableRuleSchema>() == 0)
            {
                MessageBox.Show("请选择表");
                result = null;
            }
            else
            {
                result = list;
            }
            return result;
        }

        private void btnEntity_Click(object sender, EventArgs e)
        {
            List<TableRuleSchema> list = this.ProcessData();
            if (list != null)
            {
                this.CreateDataEntityProcess(list);
                this._CodeConfigHelper.UpdateXml(list, this.boxConnectString.SelectedItem.ToString());
            }
        }

        private void BtnDataAccess_Click(object sender, EventArgs e)
        {
            List<TableRuleSchema> list = this.ProcessData();
            if (list != null)
            {
                this.CreateDataAccessProcess(list);
                this._CodeConfigHelper.UpdateXml(list, this.boxConnectString.SelectedItem.ToString());
            }
        }

        private void btnCreateRule_Click(object sender, EventArgs e)
        {
            List<TableRuleSchema> list = this.ProcessData();
            if (list != null)
            {
                this.CreateBusinessProcess(list);
                this._CodeConfigHelper.UpdateXml(list, this.boxConnectString.SelectedItem.ToString());
            }
        }

        private void btnRuleCode_Click(object sender, EventArgs e)
        {
            List<TableRuleSchema> list = this.ProcessData();
            if (list != null)
            {
                this.CreateDataEntityProcess(list);
                this.CreateDataAccessProcess(list);
                this.CreateBusinessProcess(list);
                this._CodeConfigHelper.UpdateXml(list, this.boxConnectString.SelectedItem.ToString());
            }
        }

        public void CreateBusinessProcess(List<TableRuleSchema> tableRuleSchemaList)
        {
            string text = this._SelectFileInfo.ProjectPath + "\\Business";
            if (!Directory.Exists(text))
            {
                this._SelectFileInfo.Project.ProjectItems.AddFolder("Business", "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}");
            }
            foreach (TableRuleSchema current in tableRuleSchemaList)
            {
                if (current.PrimaryKey == null)
                {
                    if (current.PrimaryKey == null)
                    {
                        MessageBox.Show(current.TableName + ":未设置主键因此无法生成");
                        continue;
                    }
                }
                string text2 = current.ToSqlBizRule(this._SelectFileInfo.ProjectName);
                string text3 = text + "\\Biz" + current.EntityName + ".cs";
                if (this.chkPreviewCode.Checked)
                {
                    Common.ShowCodeForm(text2);
                }
                else if (!File.Exists(text3))
                {
                    FileStream fileStream = File.Create(text3);
                    fileStream.Close();
                    this._SelectFileInfo.Project.ProjectItems.AddFromFile(text3);
                    File.WriteAllText(text3, text2, Encoding.UTF8);
                    MessageBox.Show(current.TableName + ":生成业务逻辑层成功");
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("是否要覆盖Biz" + current.EntityName + ".cs文件内容?", "", MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.OK)
                    {
                        File.WriteAllText(text3, text2, Encoding.UTF8);
                    }
                    else
                    {
                        Common.ShowCodeForm(text2);
                    }
                }
            }
        }

        public void CreateJavaDalProcess(List<TableRuleSchema> tableRuleSchemaList)
        {
            foreach (TableRuleSchema current in tableRuleSchemaList)
            {
                if (current.PrimaryKey == null)
                {
                    if (current.PrimaryKey == null)
                    {
                        MessageBox.Show(current.TableName + ":未设置主键因此无法生成");
                        continue;
                    }
                }
                string code = current.ToJavaDalRule(this._SelectFileInfo.ProjectName);
                Common.ShowCodeForm(code);
            }
        }

        public void CreateDataAccessProcess(List<TableRuleSchema> tableRuleSchemaList)
        {
            string text = this._SelectFileInfo.ProjectPath + "\\DataAccess";
            if (!Directory.Exists(text))
            {
                this._SelectFileInfo.Project.ProjectItems.AddFolder("DataAccess", "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}");
            }
            foreach (TableRuleSchema current in tableRuleSchemaList)
            {
                if (current.PrimaryKey == null)
                {
                    MessageBox.Show(current.TableName + ":未设置主键因此无法生成");
                }
                else
                {
                    string text2 = current.ToSqlDaRule(this._SelectFileInfo.ProjectName);
                    if (this.chkPreviewCode.Checked)
                    {
                        Common.ShowCodeForm(text2);
                    }
                    else
                    {
                        string text3 = text + "\\Da" + current.EntityName + ".cs";
                        if (!File.Exists(text3))
                        {
                            FileStream fileStream = File.Create(text3);
                            fileStream.Close();
                            this._SelectFileInfo.Project.ProjectItems.AddFromFile(text3);
                            File.WriteAllText(text3, text2, Encoding.UTF8);
                            MessageBox.Show(current.TableName + ":生成数据访问成功");
                        }
                        else
                        {
                            DialogResult dialogResult = MessageBox.Show("是否要覆盖Da" + current.EntityName + ".cs文件内容?", "", MessageBoxButtons.OKCancel);
                            if (dialogResult == DialogResult.OK)
                            {
                                File.WriteAllText(text3, text2, Encoding.UTF8);
                            }
                            else
                            {
                                Common.ShowCodeForm(text2);
                            }
                        }
                    }
                }
            }
        }

        public void CreateDataEntitySolrIndexProcess(List<TableRuleSchema> tableRuleSchemaList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (TableRuleSchema current in tableRuleSchemaList)
            {
                string code = current.ToDataEntitySolrIndex(this._SelectFileInfo.ProjectName);
                Common.ShowCodeForm(code);
            }
        }

        public void CreateDataEntitySolrEntityProcess(List<TableRuleSchema> tableRuleSchemaList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (TableRuleSchema current in tableRuleSchemaList)
            {
                string code = current.ToDataEntitySolrEntity(this._SelectFileInfo.ProjectName);
                Common.ShowCodeForm(code);
            }
        }

        public void CreateDataEntityPropertiesProcess(List<TableRuleSchema> tableRuleSchemaList)
        {
            foreach (TableRuleSchema current in tableRuleSchemaList)
            {
                string code = current.ToDataEntityProperties(this._SelectFileInfo.ProjectName);
                Common.ShowCodeForm(code);
            }
        }

        public void CreateDataEntityProcess(List<TableRuleSchema> tableRuleSchemaList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string text = this._SelectFileInfo.ProjectPath + "\\DataEntity";
            if (!Directory.Exists(text))
            {
                this._SelectFileInfo.Project.ProjectItems.AddFolder("DataEntity", "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}");
            }
            foreach (TableRuleSchema current in tableRuleSchemaList)
            {
                string text2 = current.ToDataEntity(this._SelectFileInfo.ProjectName);
                string text3 = text + "\\" + current.EntityName + ".cs";
                if (this.chkPreviewCode.Checked)
                {
                    Common.ShowCodeForm(text2);
                }
                else if (!File.Exists(text3))
                {
                    FileStream fileStream = File.Create(text3);
                    fileStream.Close();
                    this._SelectFileInfo.Project.ProjectItems.AddFromFile(text3);
                    File.WriteAllText(text3, text2, Encoding.UTF8);
                    MessageBox.Show(current.TableName + ":生成实体成功");
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("是否要覆盖" + current.EntityName + ".cs文件内容?", "", MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.OK)
                    {
                        File.WriteAllText(text3, text2, Encoding.UTF8);
                    }
                    else
                    {
                        Common.ShowCodeForm(text2);
                    }
                }
            }
        }

        public void CreateJavaEntityProcess(List<TableRuleSchema> tableRuleSchemaList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (TableRuleSchema current in tableRuleSchemaList)
            {
                string code = current.ToJavaDataEntity(this._SelectFileInfo.ProjectName);
                Common.ShowCodeForm(code);
            }
        }

        public void CreateJavaEntitySetProcess(List<TableRuleSchema> tableRuleSchemaList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (TableRuleSchema current in tableRuleSchemaList)
            {
                string code = current.ToJavaDataSetEntity(this._SelectFileInfo.ProjectName);
                Common.ShowCodeForm(code);
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void btnSolrIndex_Click(object sender, EventArgs e)
        {
            List<TableRuleSchema> list = this.ProcessData();
            if (list != null)
            {
                this.CreateDataEntitySolrIndexProcess(list);
            }
        }

        private void BtnSolrEntity_Click(object sender, EventArgs e)
        {
            List<TableRuleSchema> list = this.ProcessData();
            if (list != null)
            {
                this.CreateDataEntitySolrEntityProcess(list);
            }
        }

        private void btnEntityPros_Click(object sender, EventArgs e)
        {
            List<TableRuleSchema> list = this.ProcessData();
            if (list != null)
            {
                this.CreateDataEntityPropertiesProcess(list);
            }
        }

        private void btnJavaEntity_Click(object sender, EventArgs e)
        {
            List<TableRuleSchema> list = this.ProcessData();
            if (list != null)
            {
                this.CreateJavaEntityProcess(list);
            }
        }

        private void btnJavaDal_Click(object sender, EventArgs e)
        {
            List<TableRuleSchema> list = this.ProcessData();
            if (list != null)
            {
                this.CreateJavaDalProcess(list);
            }
        }

        private void btnJavaSet_Click(object sender, EventArgs e)
        {
            List<TableRuleSchema> list = this.ProcessData();
            if (list != null)
            {
                this.CreateJavaEntitySetProcess(list);
            }
        }

        private void btnESMapping_Click(object sender, EventArgs e)
        {
            List<TableRuleSchema> list = this.ProcessData();
            if (list != null)
            {
                this.CreateDataEntityESMappingIndexProcess(list);
            }
        }

        public void CreateDataEntityESMappingIndexProcess(List<TableRuleSchema> tableRuleSchemaList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (TableRuleSchema current in tableRuleSchemaList)
            {
                string code = current.ToDataEntityESMappingIndex(this._SelectFileInfo.ProjectName);
                Common.ShowCodeForm(code);
            }
        }

        private void BtnEntityValue_Click(object sender, EventArgs e)
        {
            List<TableRuleSchema> list = this.ProcessData();
            if (list != null)
            {
                this.CreateDataEntityValueProcess(list);
            }
        }

        public void CreateDataEntityValueProcess(List<TableRuleSchema> tableRuleSchemaList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (TableRuleSchema current in tableRuleSchemaList)
            {
                string code = current.ToDataEntityValue();
                Common.ShowCodeForm(code);
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
            this.splitContainer1 = new SplitContainer();
            this.btnESMapping = new Button();
            this.btnJavaSet = new Button();
            this.btnJavaDal = new Button();
            this.btnJavaEntity = new Button();
            this.chkFieldLength = new CheckBox();
            this.btnEntityPros = new Button();
            this.IsView = new CheckBox();
            this.BtnSolrEntity = new Button();
            this.btnSolrIndex = new Button();
            this.chkPreviewCode = new CheckBox();
            this.txtConnectionKey = new TextBox();
            this.label1 = new Label();
            this.btnRuleCode = new Button();
            this.BtnDataAccess = new Button();
            this.btnEntity = new Button();
            this.boxConnectString = new ComboBox();
            this.label5 = new Label();
            this.boxLogType = new ComboBox();
            this.label2 = new Label();
            this.btnCreateRule = new Button();
            this.btnLoadRuleTable = new Button();
            this.splitContainer2 = new SplitContainer();
            this.gvdTables = new DataGridView();
            this.IsCreate = new DataGridViewCheckBoxColumn();
            this.SetCheck = new DataGridViewButtonColumn();
            this.TableName = new DataGridViewTextBoxColumn();
            this.EntityName = new DataGridViewTextBoxColumn();
            this.Description = new DataGridViewTextBoxColumn();
            this.LogModuleType = new DataGridViewTextBoxColumn();
            this.ConnectionKeyOrConnectionString = new DataGridViewTextBoxColumn();
            this.IsMongoDB = new DataGridViewCheckBoxColumn();
            this.IsCheckFieldLength = new DataGridViewCheckBoxColumn();
            this.gvwTableCols = new DataGridView();
            this.FieldName = new DataGridViewTextBoxColumn();
            this.FieldTitle = new DataGridViewTextBoxColumn();
            this.IsCheck = new DataGridViewCheckBoxColumn();
            this.ErrorMessage = new DataGridViewTextBoxColumn();
            this.BtnEntityValue = new Button();
            ((ISupportInitialize)this.splitContainer1).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((ISupportInitialize)this.splitContainer2).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((ISupportInitialize)this.gvdTables).BeginInit();
            ((ISupportInitialize)this.gvwTableCols).BeginInit();
            base.SuspendLayout();
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = Orientation.Horizontal;
            this.splitContainer1.Panel1.Controls.Add(this.BtnEntityValue);
            this.splitContainer1.Panel1.Controls.Add(this.btnESMapping);
            this.splitContainer1.Panel1.Controls.Add(this.btnJavaSet);
            this.splitContainer1.Panel1.Controls.Add(this.btnJavaDal);
            this.splitContainer1.Panel1.Controls.Add(this.btnJavaEntity);
            this.splitContainer1.Panel1.Controls.Add(this.chkFieldLength);
            this.splitContainer1.Panel1.Controls.Add(this.btnEntityPros);
            this.splitContainer1.Panel1.Controls.Add(this.IsView);
            this.splitContainer1.Panel1.Controls.Add(this.BtnSolrEntity);
            this.splitContainer1.Panel1.Controls.Add(this.btnSolrIndex);
            this.splitContainer1.Panel1.Controls.Add(this.chkPreviewCode);
            this.splitContainer1.Panel1.Controls.Add(this.txtConnectionKey);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btnRuleCode);
            this.splitContainer1.Panel1.Controls.Add(this.BtnDataAccess);
            this.splitContainer1.Panel1.Controls.Add(this.btnEntity);
            this.splitContainer1.Panel1.Controls.Add(this.boxConnectString);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.boxLogType);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.btnCreateRule);
            this.splitContainer1.Panel1.Controls.Add(this.btnLoadRuleTable);
            this.splitContainer1.Panel1.Paint += new PaintEventHandler(this.splitContainer1_Panel1_Paint);
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new Size(1242, 507);
            this.splitContainer1.SplitterDistance = 87;
            this.splitContainer1.TabIndex = 0;
            this.btnESMapping.Location = new Point(18, 48);
            this.btnESMapping.Name = "btnESMapping";
            this.btnESMapping.Size = new Size(75, 23);
            this.btnESMapping.TabIndex = 58;
            this.btnESMapping.Text = "ESMapping";
            this.btnESMapping.UseVisualStyleBackColor = true;
            this.btnESMapping.Click += new EventHandler(this.btnESMapping_Click);
            this.btnJavaSet.Location = new Point(975, 15);
            this.btnJavaSet.Name = "btnJavaSet";
            this.btnJavaSet.Size = new Size(75, 23);
            this.btnJavaSet.TabIndex = 57;
            this.btnJavaSet.Text = "Java赋值";
            this.btnJavaSet.UseVisualStyleBackColor = true;
            this.btnJavaSet.Click += new EventHandler(this.btnJavaSet_Click);
            this.btnJavaDal.Location = new Point(894, 15);
            this.btnJavaDal.Name = "btnJavaDal";
            this.btnJavaDal.Size = new Size(75, 23);
            this.btnJavaDal.TabIndex = 56;
            this.btnJavaDal.Text = "Java访问";
            this.btnJavaDal.UseVisualStyleBackColor = true;
            this.btnJavaDal.Click += new EventHandler(this.btnJavaDal_Click);
            this.btnJavaEntity.Location = new Point(813, 14);
            this.btnJavaEntity.Name = "btnJavaEntity";
            this.btnJavaEntity.Size = new Size(75, 23);
            this.btnJavaEntity.TabIndex = 55;
            this.btnJavaEntity.Text = "Java实体";
            this.btnJavaEntity.UseVisualStyleBackColor = true;
            this.btnJavaEntity.Click += new EventHandler(this.btnJavaEntity_Click);
            this.chkFieldLength.AutoSize = true;
            this.chkFieldLength.Location = new Point(429, 52);
            this.chkFieldLength.Name = "chkFieldLength";
            this.chkFieldLength.Size = new Size(120, 16);
            this.chkFieldLength.TabIndex = 54;
            this.chkFieldLength.Text = "是否验证字段长度";
            this.chkFieldLength.UseVisualStyleBackColor = true;
            this.btnEntityPros.Location = new Point(335, 47);
            this.btnEntityPros.Name = "btnEntityPros";
            this.btnEntityPros.Size = new Size(88, 23);
            this.btnEntityPros.TabIndex = 53;
            this.btnEntityPros.Text = "生成属性实体";
            this.btnEntityPros.UseVisualStyleBackColor = true;
            this.btnEntityPros.Click += new EventHandler(this.btnEntityPros_Click);
            this.IsView.AutoSize = true;
            this.IsView.Location = new Point(249, 14);
            this.IsView.Name = "IsView";
            this.IsView.Size = new Size(48, 16);
            this.IsView.TabIndex = 52;
            this.IsView.Text = "视图";
            this.IsView.UseVisualStyleBackColor = true;
            this.BtnSolrEntity.Location = new Point(254, 45);
            this.BtnSolrEntity.Name = "BtnSolrEntity";
            this.BtnSolrEntity.Size = new Size(75, 23);
            this.BtnSolrEntity.TabIndex = 51;
            this.BtnSolrEntity.Text = "SolrEntity";
            this.BtnSolrEntity.UseVisualStyleBackColor = true;
            this.BtnSolrEntity.Click += new EventHandler(this.BtnSolrEntity_Click);
            this.btnSolrIndex.Location = new Point(173, 45);
            this.btnSolrIndex.Name = "btnSolrIndex";
            this.btnSolrIndex.Size = new Size(75, 23);
            this.btnSolrIndex.TabIndex = 50;
            this.btnSolrIndex.Text = "SolrIndex";
            this.btnSolrIndex.UseVisualStyleBackColor = true;
            this.btnSolrIndex.Click += new EventHandler(this.btnSolrIndex_Click);
            this.chkPreviewCode.AutoSize = true;
            this.chkPreviewCode.Location = new Point(555, 52);
            this.chkPreviewCode.Name = "chkPreviewCode";
            this.chkPreviewCode.Size = new Size(72, 16);
            this.chkPreviewCode.TabIndex = 49;
            this.chkPreviewCode.Text = "预览代码";
            this.chkPreviewCode.UseVisualStyleBackColor = true;
            this.txtConnectionKey.Location = new Point(637, 12);
            this.txtConnectionKey.Name = "txtConnectionKey";
            this.txtConnectionKey.Size = new Size(170, 21);
            this.txtConnectionKey.TabIndex = 31;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(568, 15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(53, 12);
            this.label1.TabIndex = 30;
            this.label1.Text = "链接键值";
            this.btnRuleCode.Location = new Point(908, 47);
            this.btnRuleCode.Name = "btnRuleCode";
            this.btnRuleCode.Size = new Size(74, 23);
            this.btnRuleCode.TabIndex = 29;
            this.btnRuleCode.Text = "一键生成";
            this.btnRuleCode.UseVisualStyleBackColor = true;
            this.btnRuleCode.Click += new EventHandler(this.btnRuleCode_Click);
            this.BtnDataAccess.Location = new Point(723, 47);
            this.BtnDataAccess.Name = "BtnDataAccess";
            this.BtnDataAccess.Size = new Size(98, 23);
            this.BtnDataAccess.TabIndex = 20;
            this.BtnDataAccess.Text = "生成数据访问层";
            this.BtnDataAccess.UseVisualStyleBackColor = true;
            this.BtnDataAccess.Click += new EventHandler(this.BtnDataAccess_Click);
            this.btnEntity.Location = new Point(642, 48);
            this.btnEntity.Name = "btnEntity";
            this.btnEntity.Size = new Size(75, 23);
            this.btnEntity.TabIndex = 19;
            this.btnEntity.Text = "生成实体";
            this.btnEntity.UseVisualStyleBackColor = true;
            this.btnEntity.Click += new EventHandler(this.btnEntity_Click);
            this.boxConnectString.FormattingEnabled = true;
            this.boxConnectString.Location = new Point(87, 12);
            this.boxConnectString.Name = "boxConnectString";
            this.boxConnectString.Size = new Size(156, 20);
            this.boxConnectString.TabIndex = 18;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(16, 15);
            this.label5.Name = "label5";
            this.label5.Size = new Size(65, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "连接串名称";
            this.boxLogType.FormattingEnabled = true;
            this.boxLogType.Location = new Point(438, 12);
            this.boxLogType.Name = "boxLogType";
            this.boxLogType.Size = new Size(118, 20);
            this.boxLogType.TabIndex = 4;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(379, 15);
            this.label2.Name = "label2";
            this.label2.Size = new Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "日志类型";
            this.btnCreateRule.Location = new Point(827, 47);
            this.btnCreateRule.Name = "btnCreateRule";
            this.btnCreateRule.Size = new Size(75, 23);
            this.btnCreateRule.TabIndex = 1;
            this.btnCreateRule.Text = "生成业务层";
            this.btnCreateRule.UseVisualStyleBackColor = true;
            this.btnCreateRule.Click += new EventHandler(this.btnCreateRule_Click);
            this.btnLoadRuleTable.Location = new Point(298, 10);
            this.btnLoadRuleTable.Name = "btnLoadRuleTable";
            this.btnLoadRuleTable.Size = new Size(75, 23);
            this.btnLoadRuleTable.TabIndex = 0;
            this.btnLoadRuleTable.Text = "加载表";
            this.btnLoadRuleTable.UseVisualStyleBackColor = true;
            this.btnLoadRuleTable.Click += new EventHandler(this.btnLoadRuleTable_Click);
            this.splitContainer2.Dock = DockStyle.Fill;
            this.splitContainer2.Location = new Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Panel1.Controls.Add(this.gvdTables);
            this.splitContainer2.Panel2.Controls.Add(this.gvwTableCols);
            this.splitContainer2.Size = new Size(1242, 416);
            this.splitContainer2.SplitterDistance = 682;
            this.splitContainer2.TabIndex = 0;
            this.gvdTables.AllowUserToAddRows = false;
            this.gvdTables.AllowUserToDeleteRows = false;
            dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle.BackColor = SystemColors.Control;
            dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
            this.gvdTables.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
            this.gvdTables.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvdTables.Columns.AddRange(new DataGridViewColumn[]
			{
				this.IsCreate,
				this.SetCheck,
				this.TableName,
				this.EntityName,
				this.Description,
				this.LogModuleType,
				this.ConnectionKeyOrConnectionString,
				this.IsMongoDB,
				this.IsCheckFieldLength
			});
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            this.gvdTables.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvdTables.Dock = DockStyle.Fill;
            this.gvdTables.Location = new Point(0, 0);
            this.gvdTables.Name = "gvdTables";
            this.gvdTables.RowHeadersVisible = false;
            this.gvdTables.RowTemplate.Height = 23;
            this.gvdTables.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gvdTables.Size = new Size(682, 416);
            this.gvdTables.TabIndex = 0;
            this.gvdTables.CellClick += new DataGridViewCellEventHandler(this.gvdTables_CellClick);
            this.IsCreate.DataPropertyName = "IsCreate";
            this.IsCreate.FalseValue = "False";
            this.IsCreate.HeaderText = "生成";
            this.IsCreate.Name = "IsCreate";
            this.IsCreate.TrueValue = "True";
            this.IsCreate.Width = 40;
            this.SetCheck.HeaderText = "检查";
            this.SetCheck.Name = "SetCheck";
            this.SetCheck.Text = "检查";
            this.SetCheck.UseColumnTextForButtonValue = true;
            this.SetCheck.Width = 40;
            this.TableName.DataPropertyName = "TableName";
            this.TableName.HeaderText = "表名";
            this.TableName.Name = "TableName";
            this.TableName.Width = 150;
            this.EntityName.DataPropertyName = "EntityName";
            this.EntityName.HeaderText = "实体名称";
            this.EntityName.Name = "EntityName";
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "表说明";
            this.Description.Name = "Description";
            this.LogModuleType.DataPropertyName = "LogModuleType";
            this.LogModuleType.HeaderText = "日志";
            this.LogModuleType.Name = "LogModuleType";
            this.ConnectionKeyOrConnectionString.DataPropertyName = "ConnectionKeyOrConnectionString";
            this.ConnectionKeyOrConnectionString.HeaderText = "链接键值";
            this.ConnectionKeyOrConnectionString.Name = "ConnectionKeyOrConnectionString";
            this.IsMongoDB.DataPropertyName = "IsMongoDB";
            this.IsMongoDB.FalseValue = "False";
            this.IsMongoDB.HeaderText = "MDB";
            this.IsMongoDB.Name = "IsMongoDB";
            this.IsMongoDB.TrueValue = "True";
            this.IsMongoDB.Width = 40;
            this.IsCheckFieldLength.DataPropertyName = "IsCheckFieldLength";
            this.IsCheckFieldLength.FalseValue = "False";
            this.IsCheckFieldLength.HeaderText = "验字段长度";
            this.IsCheckFieldLength.Name = "IsCheckFieldLength";
            this.IsCheckFieldLength.TrueValue = "True";
            this.gvwTableCols.AllowUserToAddRows = false;
            this.gvwTableCols.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            this.gvwTableCols.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvwTableCols.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvwTableCols.Columns.AddRange(new DataGridViewColumn[]
			{
				this.FieldName,
				this.FieldTitle,
				this.IsCheck,
				this.ErrorMessage
			});
            this.gvwTableCols.Dock = DockStyle.Fill;
            this.gvwTableCols.Location = new Point(0, 0);
            this.gvwTableCols.Name = "gvwTableCols";
            this.gvwTableCols.RowHeadersVisible = false;
            this.gvwTableCols.RowTemplate.Height = 23;
            this.gvwTableCols.Size = new Size(556, 416);
            this.gvwTableCols.TabIndex = 0;
            this.gvwTableCols.CellContentClick += new DataGridViewCellEventHandler(this.gvwTableCols_CellContentClick);
            this.FieldName.DataPropertyName = "FieldName";
            this.FieldName.HeaderText = "字段名";
            this.FieldName.Name = "FieldName";
            this.FieldName.ReadOnly = true;
            this.FieldTitle.DataPropertyName = "FieldTitle";
            this.FieldTitle.HeaderText = "字段标题";
            this.FieldTitle.Name = "FieldTitle";
            this.FieldTitle.Width = 150;
            this.IsCheck.DataPropertyName = "IsCheck";
            this.IsCheck.FalseValue = "False";
            this.IsCheck.HeaderText = "是否检查";
            this.IsCheck.Name = "IsCheck";
            this.IsCheck.TrueValue = "True";
            this.IsCheck.Width = 60;
            this.ErrorMessage.DataPropertyName = "ErrorMessage";
            this.ErrorMessage.HeaderText = "错误提示";
            this.ErrorMessage.Name = "ErrorMessage";
            this.ErrorMessage.Resizable = DataGridViewTriState.True;
            this.ErrorMessage.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.ErrorMessage.Width = 200;
            this.BtnEntityValue.Location = new Point(988, 47);
            this.BtnEntityValue.Name = "BtnEntityValue";
            this.BtnEntityValue.Size = new Size(75, 23);
            this.BtnEntityValue.TabIndex = 59;
            this.BtnEntityValue.Text = "实体赋值";
            this.BtnEntityValue.UseVisualStyleBackColor = true;
            this.BtnEntityValue.Click += new EventHandler(this.BtnEntityValue_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(1242, 507);
            base.Controls.Add(this.splitContainer1);
            base.Name = "SqlRuleCodeForm";
            this.Text = "新增业务层";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((ISupportInitialize)this.splitContainer1).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((ISupportInitialize)this.splitContainer2).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((ISupportInitialize)this.gvdTables).EndInit();
            ((ISupportInitialize)this.gvwTableCols).EndInit();
            base.ResumeLayout(false);
        }
    }
}

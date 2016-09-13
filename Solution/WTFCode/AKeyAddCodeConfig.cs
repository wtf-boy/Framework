using WTF.CodeRule;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WTFCode
{
	public class AKeyAddCodeConfig : Form
	{
		private CodeConfigHelper _CodeConfigHelper = null;

		private SelectFileInfo _SelectFileInfo = null;

		private Dictionary<string, BusinessNodeInfo> _BusinessNodeInfoList = new Dictionary<string, BusinessNodeInfo>();

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

		private DataGridViewTextBoxColumn FieldName;

		private DataGridViewTextBoxColumn FieldTitle;

		private DataGridViewCheckBoxColumn IsCheck;

		private DataGridViewTextBoxColumn ErrorMessage;

		private ComboBox boxConnectString;

		private Label label5;

		private Button btnRuleCode;

		private TextBox txtUIProjectName;

		private Label label4;

		private TextBox txtUIProjectPath;

		private Label label3;

		private DataGridViewCheckBoxColumn IsCreate;

		private DataGridViewButtonColumn SetCheck;

		private DataGridViewTextBoxColumn TableName;

		private DataGridViewTextBoxColumn EntityName;

		private DataGridViewTextBoxColumn Description;

		private DataGridViewTextBoxColumn LogModuleType;

		private DataGridViewTextBoxColumn ConnectionKeyOrConnectionString;

		private DataGridViewCheckBoxColumn IsMongoDB;

		private DataGridViewCheckBoxColumn IsCheckFieldLength;

		private DataGridViewTextBoxColumn UIProjectPath;

		private DataGridViewTextBoxColumn UIProjectName;

		public AKeyAddCodeConfig(SelectFileInfo objSelectFileInfo, Dictionary<string, BusinessNodeInfo> businessNodeInfoList)
		{
			this.InitializeComponent();
			this._SelectFileInfo = objSelectFileInfo;
			this._CodeConfigHelper = new CodeConfigHelper(objSelectFileInfo.CodeConfigPath);
			if (this._CodeConfigHelper.LoadCodeConfigXml())
			{
				this._BusinessNodeInfoList = businessNodeInfoList;
				this._CodeConfigHelper.InitConnectionStrings(this.boxConnectString);
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
					this._TableRuleSchemaList = SqlSchemaHelper.GetAllRuleTables(this._Database, this._ConnectionString, true);
					this._TableRuleSchemaList = (from x in this._TableRuleSchemaList
					where this._BusinessNodeInfoList.Keys.Contains(x.TableName)
					select x).ToList<TableRuleSchema>();
					foreach (TableRuleSchema current in this._TableRuleSchemaList)
					{
						BusinessNodeInfo objBusinessNodeInfo = this._BusinessNodeInfoList[current.TableName];
						current.IsCreate = true;
						CodeConfigHelper.BusinessNodeToTableRuleSchema(objBusinessNodeInfo, current);
					}
					this.gvdTables.DataSource = this._TableRuleSchemaList;
				}
				catch (Exception ex)
				{
					MessageBox.Show("加载表错误，异常信息：" + ex.ToString());
				}
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

		private void btnRuleCode_Click(object sender, EventArgs e)
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
			IEnumerator enumerator2 = ((IEnumerable)this.gvdTables.Rows).GetEnumerator();
			{
				while (enumerator2.MoveNext())
				{
					DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator2.Current;
					string TableName = dataGridViewRow.ReadCell("TableName", "");
					TableRuleSchema tableRuleSchema = this._TableRuleSchemaList.First((TableRuleSchema s) => s.TableName == TableName);
					tableRuleSchema.IsCreate = bool.Parse(dataGridViewRow.ReadCell("IsCreate", ""));
					tableRuleSchema.Description = dataGridViewRow.ReadCell("Description", "");
					tableRuleSchema.EntityName = dataGridViewRow.ReadCell("EntityName", "");
					tableRuleSchema.IsMongoDB = bool.Parse(dataGridViewRow.ReadCell("IsMongoDB", ""));
					tableRuleSchema.IsCheckFieldLength = bool.Parse(dataGridViewRow.ReadCell("IsCheckFieldLength", ""));
					tableRuleSchema.ConnectionKeyOrConnectionString = dataGridViewRow.ReadCell("ConnectionKeyOrConnectionString", "");
					tableRuleSchema.LogModuleType = dataGridViewRow.ReadCell("LogModuleType", "");
					tableRuleSchema.UIProjectName = dataGridViewRow.ReadCell("UIProjectName", this.txtUIProjectName.Text);
					tableRuleSchema.UIProjectPath = dataGridViewRow.ReadCell("UIProjectPath", this.txtUIProjectPath.Text);
					if (tableRuleSchema.IsCreate && tableRuleSchema.Columns.Count == 0)
					{
						tableRuleSchema.Columns = SqlSchemaHelper.GetTableRuleColumnsSchema(this._ConnectionString, this._Database, tableRuleSchema.TableName, this._CodeConfigHelper);
					}
				}
			}
			int num = (from s in this._TableRuleSchemaList
			where s.IsCreate
			select s).Count<TableRuleSchema>();
			if (num == 0)
			{
				MessageBox.Show("请要生成的表");
			}
			else
			{
				this._CodeConfigHelper.UpdateXml((from s in this._TableRuleSchemaList
				where s.IsCreate
				select s).ToList<TableRuleSchema>(), this.boxConnectString.SelectedItem.ToString());
				MessageBox.Show("一键生成配置文件成功");
			}
		}

		private void gvdTables_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			this.splitContainer1 = new SplitContainer();
			this.txtUIProjectName = new TextBox();
			this.label4 = new Label();
			this.txtUIProjectPath = new TextBox();
			this.label3 = new Label();
			this.btnRuleCode = new Button();
			this.boxConnectString = new ComboBox();
			this.label5 = new Label();
			this.btnLoadRuleTable = new Button();
			this.splitContainer2 = new SplitContainer();
			this.gvdTables = new DataGridView();
			this.gvwTableCols = new DataGridView();
			this.FieldName = new DataGridViewTextBoxColumn();
			this.FieldTitle = new DataGridViewTextBoxColumn();
			this.IsCheck = new DataGridViewCheckBoxColumn();
			this.ErrorMessage = new DataGridViewTextBoxColumn();
			this.IsCreate = new DataGridViewCheckBoxColumn();
			this.SetCheck = new DataGridViewButtonColumn();
			this.TableName = new DataGridViewTextBoxColumn();
			this.EntityName = new DataGridViewTextBoxColumn();
			this.Description = new DataGridViewTextBoxColumn();
			this.LogModuleType = new DataGridViewTextBoxColumn();
			this.ConnectionKeyOrConnectionString = new DataGridViewTextBoxColumn();
			this.IsMongoDB = new DataGridViewCheckBoxColumn();
			this.IsCheckFieldLength = new DataGridViewCheckBoxColumn();
			this.UIProjectPath = new DataGridViewTextBoxColumn();
			this.UIProjectName = new DataGridViewTextBoxColumn();
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
			this.splitContainer1.Panel1.Controls.Add(this.txtUIProjectName);
			this.splitContainer1.Panel1.Controls.Add(this.label4);
			this.splitContainer1.Panel1.Controls.Add(this.txtUIProjectPath);
			this.splitContainer1.Panel1.Controls.Add(this.label3);
			this.splitContainer1.Panel1.Controls.Add(this.btnRuleCode);
			this.splitContainer1.Panel1.Controls.Add(this.boxConnectString);
			this.splitContainer1.Panel1.Controls.Add(this.label5);
			this.splitContainer1.Panel1.Controls.Add(this.btnLoadRuleTable);
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new Size(1242, 507);
			this.splitContainer1.SplitterDistance = 87;
			this.splitContainer1.TabIndex = 0;
			this.txtUIProjectName.Location = new Point(467, 57);
			this.txtUIProjectName.Name = "txtUIProjectName";
			this.txtUIProjectName.Size = new Size(222, 21);
			this.txtUIProjectName.TabIndex = 41;
			this.txtUIProjectName.Text = "Gao7.ManaeWeb";
			this.label4.AutoSize = true;
			this.label4.Location = new Point(389, 60);
			this.label4.Name = "label4";
			this.label4.Size = new Size(83, 12);
			this.label4.TabIndex = 40;
			this.label4.Text = "Web项目名称：";
			this.txtUIProjectPath.Location = new Point(88, 57);
			this.txtUIProjectPath.Name = "txtUIProjectPath";
			this.txtUIProjectPath.Size = new Size(295, 21);
			this.txtUIProjectPath.TabIndex = 39;
			this.txtUIProjectPath.Text = "Gao7.ManaeWeb\\Manage\\XXXX";
			this.label3.AutoSize = true;
			this.label3.Location = new Point(3, 60);
			this.label3.Name = "label3";
			this.label3.Size = new Size(83, 12);
			this.label3.TabIndex = 38;
			this.label3.Text = "Web项目路经：";
			this.btnRuleCode.Location = new Point(339, 24);
			this.btnRuleCode.Name = "btnRuleCode";
			this.btnRuleCode.Size = new Size(124, 23);
			this.btnRuleCode.TabIndex = 29;
			this.btnRuleCode.Text = "一键生成配置文件";
			this.btnRuleCode.UseVisualStyleBackColor = true;
			this.btnRuleCode.Click += new EventHandler(this.btnRuleCode_Click);
			this.boxConnectString.FormattingEnabled = true;
			this.boxConnectString.Location = new Point(84, 26);
			this.boxConnectString.Name = "boxConnectString";
			this.boxConnectString.Size = new Size(156, 20);
			this.boxConnectString.TabIndex = 18;
			this.label5.AutoSize = true;
			this.label5.Location = new Point(13, 29);
			this.label5.Name = "label5";
			this.label5.Size = new Size(65, 12);
			this.label5.TabIndex = 17;
			this.label5.Text = "连接串名称";
			this.btnLoadRuleTable.Location = new Point(246, 24);
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
			this.splitContainer2.SplitterDistance = 735;
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
				this.IsCheckFieldLength,
				this.UIProjectPath,
				this.UIProjectName
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
			this.gvdTables.Size = new Size(735, 416);
			this.gvdTables.TabIndex = 0;
			this.gvdTables.CellClick += new DataGridViewCellEventHandler(this.gvdTables_CellClick);
			this.gvdTables.CellContentClick += new DataGridViewCellEventHandler(this.gvdTables_CellContentClick);
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
			this.gvwTableCols.Size = new Size(503, 416);
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
			this.ConnectionKeyOrConnectionString.HeaderText = "连接键值";
			this.ConnectionKeyOrConnectionString.Name = "ConnectionKeyOrConnectionString";
			this.IsMongoDB.DataPropertyName = "IsMongoDB";
			this.IsMongoDB.FalseValue = "False";
			this.IsMongoDB.HeaderText = "MDB";
			this.IsMongoDB.Name = "IsMongoDB";
			this.IsMongoDB.TrueValue = "True";
			this.IsMongoDB.Width = 40;
			this.IsCheckFieldLength.DataPropertyName = "IsCheckFieldLength";
			this.IsCheckFieldLength.FalseValue = "False";
			this.IsCheckFieldLength.HeaderText = "检查字段长度";
			this.IsCheckFieldLength.Name = "IsCheckFieldLength";
			this.IsCheckFieldLength.Resizable = DataGridViewTriState.True;
			this.IsCheckFieldLength.TrueValue = "True";
			this.UIProjectPath.DataPropertyName = "UIProjectPath";
			this.UIProjectPath.HeaderText = "Web项目路经";
			this.UIProjectPath.Name = "UIProjectPath";
			this.UIProjectName.DataPropertyName = "UIProjectName";
			this.UIProjectName.HeaderText = "Web项目名称";
			this.UIProjectName.Name = "UIProjectName";
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(1242, 507);
			base.Controls.Add(this.splitContainer1);
			base.Name = "AKeyAddCodeConfig";
			this.Text = "一键生成配置文件 ";
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

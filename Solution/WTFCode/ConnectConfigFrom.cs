using WTF.CodeRule;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WTFCode
{
	public class ConnectConfigFrom : Form
	{
		private bool IsAdd = true;

		private CodeConfigHelper _CodeConfigHelper = null;

		private SelectFileInfo _SelectFileInfo = null;

		private IContainer components = null;

		private Label label1;

		private Label label2;

		private Label label3;

		private Button btnAdd;

		private Label label4;

		private Label label5;

		private Label label6;

		private Label lblCombName;

		private TextBox txtConnectString;

		private TextBox txtServerIP;

		private TextBox txtUserID;

		private TextBox txtPassword;

		private TextBox txtPort;

		private TextBox txtDatabase;

		private ComboBox cbbConnectString;

		private Button btnsave;

		private Button btntestcon;

		private GroupBox groupBox1;

		private Button btnLoad;

		private ComboBox boxDataBase;

		public ConnectConfigFrom(SelectFileInfo objSelectFileInfo)
		{
			this.InitializeComponent();
			this._SelectFileInfo = objSelectFileInfo;
			this._CodeConfigHelper = new CodeConfigHelper(objSelectFileInfo.CodeConfigPath);
			this.LoadConnectString();
		}

		private void cbbConnectString_SelectedValueChanged(object sender, EventArgs e)
		{
			this.LoadConnectString(this.cbbConnectString.SelectedItem.ToString());
			this.IsAdd = false;
		}

		private void LoadConnectString(string ConnectStringName)
		{
			if (!string.IsNullOrWhiteSpace(ConnectStringName))
			{
				if (this._CodeConfigHelper.LoadCodeConfigXml())
				{
					XmlNode connectionStringNode = this._CodeConfigHelper.GetConnectionStringNode(ConnectStringName, true);
					XmlElement xmlElement = (XmlElement)connectionStringNode;
					this.txtConnectString.Text = xmlElement.GetAttribute("Name");
					this.txtDatabase.Text = xmlElement.GetAttribute("Database");
					this.txtPassword.Text = xmlElement.GetAttribute("Pwd");
					this.txtPort.Text = xmlElement.GetAttribute("Port");
					this.txtServerIP.Text = xmlElement.GetAttribute("ServerIP");
					this.txtUserID.Text = xmlElement.GetAttribute("Uid");
				}
			}
		}

		private void SetInitConnectValue()
		{
			this.txtConnectString.Text = this._SelectFileInfo.ProjectName + ".ConnectionString";
			this.txtPort.Text = "3306";
			this.txtServerIP.Text = "127.0.0.1";
			this.txtUserID.Text = "root";
			this.txtDatabase.Text = "";
			this.txtPassword.Text = "";
			this.IsAdd = true;
		}

		private void LoadConnectString()
		{
			this.cbbConnectString.Items.Clear();
			if (this._CodeConfigHelper.LoadCodeConfigXml())
			{
				XmlNodeList xmlNodeList = this._CodeConfigHelper.CodeConfig.SelectNodes("//ConnectionStrings/ConnectionString");
				foreach (XmlNode xmlNode in xmlNodeList)
				{
					this.cbbConnectString.Items.Add(xmlNode.Attributes["Name"].Value);
				}
			}
		}

		private bool CheckInput()
		{
			bool result;
			if (string.IsNullOrWhiteSpace(this.txtConnectString.Text))
			{
				MessageBox.Show("连接串名称不能为空");
				result = false;
			}
			else if (string.IsNullOrWhiteSpace(this.txtServerIP.Text))
			{
				MessageBox.Show("SQL主机地址不能为空");
				result = false;
			}
			else if (string.IsNullOrWhiteSpace(this.txtUserID.Text))
			{
				MessageBox.Show("用户名不能为空");
				result = false;
			}
			else if (string.IsNullOrWhiteSpace(this.txtPassword.Text))
			{
				//MessageBox.Show("密码不能为空");
                MessageBox.Show("密码为空哦,请注意!!!");
				result = true;
			}
			else if (string.IsNullOrWhiteSpace(this.txtPort.Text))
			{
				MessageBox.Show("端口不能为空");
				result = false;
			}
			else if (string.IsNullOrWhiteSpace(this.txtDatabase.Text))
			{
				MessageBox.Show("数据库不能为空");
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		private void btnsave_Click(object sender, EventArgs e)
		{
			if (this.CheckInput())
			{
				if (this._CodeConfigHelper.LoadCodeConfigXml())
				{
					XmlElement xmlElement;
					if (this.IsAdd)
					{
						int num = this.cbbConnectString.FindString(this.txtConnectString.Text);
						if (num >= 0)
						{
							MessageBox.Show("此连接串名称已经存在");
							return;
						}
						xmlElement = (XmlElement)this._CodeConfigHelper.GetConnectionStringNode(this.txtConnectString.Text, true);
					}
					else
					{
						xmlElement = (XmlElement)this._CodeConfigHelper.GetConnectionStringNode(this.cbbConnectString.SelectedItem.ToString(), true);
					}
					xmlElement.SetAttribute("Name", this.txtConnectString.Text);
					xmlElement.SetAttribute("ServerIP", this.txtServerIP.Text);
					xmlElement.SetAttribute("Uid", this.txtUserID.Text);
					xmlElement.SetAttribute("Pwd", this.txtPassword.Text);
					xmlElement.SetAttribute("Port", this.txtPort.Text);
					xmlElement.SetAttribute("Database", this.txtDatabase.Text);
					if (!this.IsAdd && this.txtConnectString.Text != this.cbbConnectString.SelectedItem.ToString())
					{
						XmlNodeList xmlNodeList = this._CodeConfigHelper.CodeConfig.SelectNodes("//BusinessConfig/Business[@ConnectionKey='" + this.cbbConnectString.SelectedItem + "']");
						foreach (XmlNode xmlNode in xmlNodeList)
						{
							XmlElement xmlElement2 = (XmlElement)xmlNode;
							xmlElement2.SetAttribute("ConnectionKey", this.txtConnectString.Text);
						}
					}
					if (!File.Exists(this._CodeConfigHelper._CodeConfigPath))
					{
						this._CodeConfigHelper.Save();
						this._SelectFileInfo.Project.ProjectItems.AddFromFile(this._CodeConfigHelper._CodeConfigPath);
					}
					else
					{
						this._CodeConfigHelper.Save();
					}
					this.LoadConnectString();
					this.cbbConnectString.SelectedItem = this.txtConnectString.Text;
					this.IsAdd = false;
					MessageBox.Show("保存成功！");
				}
			}
		}

		private void btntestcon_Click(object sender, EventArgs e)
		{
			if (this.CheckInput())
			{
				try
				{
					string connectionKeyOrConnectionString = string.Concat(new string[]
					{
						"SERVER=",
						this.txtServerIP.Text,
						";Port=",
						this.txtPort.Text,
						";Database=",
						this.txtDatabase.Text,
						";Uid=",
						this.txtUserID.Text,
						";Pwd=",
						this.txtPassword.Text,
						";persist security info=True;Allow User Variables=True;Charset=utf8; "
					});
					DataTable dataTable = MySqlHelper.ExecuteDataTable(connectionKeyOrConnectionString, " SELECT * FROM INFORMATION_SCHEMA.SCHEMATA", CommandType.Text, null);
					if (dataTable == null)
					{
						MessageBox.Show("测试连接失败！");
					}
					else
					{
						MessageBox.Show("测试连接成功！");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("测试连接抛出异常，请查看连接串是否配置正确，异常信息：" + ex.ToString());
				}
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			this.SetInitConnectValue();
		}

		private void ConnectConfigFrom_Load(object sender, EventArgs e)
		{
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("SERVER=" + this.txtServerIP.Text + ";");
				stringBuilder.Append("Port=" + this.txtPort.Text + ";");
				stringBuilder.Append("user id=" + this.txtUserID.Text + ";");
				stringBuilder.Append("password=" + this.txtPassword.Text + ";");
				stringBuilder.Append("persist security info=True;Allow User Variables=True;Charset=utf8; ");
				string codeConnectionString = stringBuilder.ToString();
				DataTable allSchemata = SqlSchemaHelper.GetAllSchemata(codeConnectionString);
				this.boxDataBase.Items.Clear();
				foreach (DataRow dataRow in allSchemata.Rows)
				{
					this.boxDataBase.Items.Add(dataRow["SCHEMA_NAME"]);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("加载库出错" + ex.Message);
			}
		}

		private void boxDataBase_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.boxDataBase.SelectedItem != null)
			{
				this.txtDatabase.Text = this.boxDataBase.SelectedItem.ToString();
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
			this.label1 = new Label();
			this.label2 = new Label();
			this.label3 = new Label();
			this.btnAdd = new Button();
			this.label4 = new Label();
			this.label5 = new Label();
			this.label6 = new Label();
			this.lblCombName = new Label();
			this.txtConnectString = new TextBox();
			this.txtServerIP = new TextBox();
			this.txtUserID = new TextBox();
			this.txtPassword = new TextBox();
			this.txtPort = new TextBox();
			this.txtDatabase = new TextBox();
			this.cbbConnectString = new ComboBox();
			this.btnsave = new Button();
			this.btntestcon = new Button();
			this.groupBox1 = new GroupBox();
			this.boxDataBase = new ComboBox();
			this.btnLoad = new Button();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(33, 117);
			this.label1.Name = "label1";
			this.label1.Size = new Size(71, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "SQL主机地址";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(30, 129);
			this.label2.Name = "label2";
			this.label2.Size = new Size(41, 12);
			this.label2.TabIndex = 1;
			this.label2.Text = "用户名";
			this.label3.AutoSize = true;
			this.label3.Location = new Point(42, 168);
			this.label3.Name = "label3";
			this.label3.Size = new Size(29, 12);
			this.label3.TabIndex = 2;
			this.label3.Text = "密码";
			this.btnAdd.Location = new Point(450, 16);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new Size(63, 23);
			this.btnAdd.TabIndex = 3;
			this.btnAdd.Text = "新建连接";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
			this.label4.AutoSize = true;
			this.label4.Location = new Point(42, 213);
			this.label4.Name = "label4";
			this.label4.Size = new Size(29, 12);
			this.label4.TabIndex = 4;
			this.label4.Text = "端口";
			this.label5.AutoSize = true;
			this.label5.Location = new Point(36, 272);
			this.label5.Name = "label5";
			this.label5.Size = new Size(41, 12);
			this.label5.TabIndex = 5;
			this.label5.Text = "数据库";
			this.label6.AutoSize = true;
			this.label6.Location = new Point(33, 79);
			this.label6.Name = "label6";
			this.label6.Size = new Size(65, 12);
			this.label6.TabIndex = 6;
			this.label6.Text = "连接串名称";
			this.lblCombName.AutoSize = true;
			this.lblCombName.Location = new Point(33, 44);
			this.lblCombName.Name = "lblCombName";
			this.lblCombName.Size = new Size(65, 12);
			this.lblCombName.TabIndex = 7;
			this.lblCombName.Text = "已存在连接";
			this.txtConnectString.Location = new Point(130, 75);
			this.txtConnectString.Name = "txtConnectString";
			this.txtConnectString.Size = new Size(341, 21);
			this.txtConnectString.TabIndex = 8;
			this.txtServerIP.Location = new Point(130, 113);
			this.txtServerIP.Name = "txtServerIP";
			this.txtServerIP.Size = new Size(341, 21);
			this.txtServerIP.TabIndex = 9;
			this.txtUserID.Location = new Point(130, 146);
			this.txtUserID.Name = "txtUserID";
			this.txtUserID.Size = new Size(341, 21);
			this.txtUserID.TabIndex = 10;
			this.txtPassword.Location = new Point(130, 179);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new Size(341, 21);
			this.txtPassword.TabIndex = 11;
			this.txtPort.Location = new Point(130, 224);
			this.txtPort.Name = "txtPort";
			this.txtPort.Size = new Size(341, 21);
			this.txtPort.TabIndex = 12;
			this.txtDatabase.Location = new Point(103, 269);
			this.txtDatabase.Name = "txtDatabase";
			this.txtDatabase.Size = new Size(152, 21);
			this.txtDatabase.TabIndex = 13;
			this.cbbConnectString.FormattingEnabled = true;
			this.cbbConnectString.Location = new Point(130, 38);
			this.cbbConnectString.Name = "cbbConnectString";
			this.cbbConnectString.Size = new Size(341, 20);
			this.cbbConnectString.TabIndex = 14;
			this.cbbConnectString.SelectedValueChanged += new EventHandler(this.cbbConnectString_SelectedValueChanged);
			this.btnsave.Location = new Point(115, 318);
			this.btnsave.Name = "btnsave";
			this.btnsave.Size = new Size(75, 23);
			this.btnsave.TabIndex = 15;
			this.btnsave.Text = "保存";
			this.btnsave.UseVisualStyleBackColor = true;
			this.btnsave.Click += new EventHandler(this.btnsave_Click);
			this.btntestcon.Location = new Point(300, 318);
			this.btntestcon.Name = "btntestcon";
			this.btntestcon.Size = new Size(75, 23);
			this.btntestcon.TabIndex = 17;
			this.btntestcon.Text = "测试连接";
			this.btntestcon.UseVisualStyleBackColor = true;
			this.btntestcon.Click += new EventHandler(this.btntestcon_Click);
			this.groupBox1.Controls.Add(this.btnLoad);
			this.groupBox1.Controls.Add(this.boxDataBase);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.txtDatabase);
			this.groupBox1.Controls.Add(this.btnsave);
			this.groupBox1.Controls.Add(this.btntestcon);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.btnAdd);
			this.groupBox1.Location = new Point(27, 20);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(540, 363);
			this.groupBox1.TabIndex = 18;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "MySQL";
			this.boxDataBase.FormattingEnabled = true;
			this.boxDataBase.Location = new Point(278, 269);
			this.boxDataBase.Name = "boxDataBase";
			this.boxDataBase.Size = new Size(166, 20);
			this.boxDataBase.TabIndex = 18;
			this.boxDataBase.SelectedIndexChanged += new EventHandler(this.boxDataBase_SelectedIndexChanged);
			this.btnLoad.Location = new Point(450, 266);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new Size(75, 23);
			this.btnLoad.TabIndex = 19;
			this.btnLoad.Text = "加载库";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new EventHandler(this.btnLoad_Click);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(584, 397);
			base.Controls.Add(this.cbbConnectString);
			base.Controls.Add(this.txtPort);
			base.Controls.Add(this.txtPassword);
			base.Controls.Add(this.txtUserID);
			base.Controls.Add(this.txtServerIP);
			base.Controls.Add(this.txtConnectString);
			base.Controls.Add(this.lblCombName);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.groupBox1);
			base.MaximizeBox = false;
			this.MaximumSize = new Size(600, 436);
			base.Name = "ConnectConfigFrom";
			this.Text = "配置连接";
			base.Load += new EventHandler(this.ConnectConfigFrom_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}

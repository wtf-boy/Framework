using System;
using Extensibility;
using EnvDTE;
using EnvDTE80;
namespace WTFCode
{
    /// <summary>用于实现外接程序的对象。</summary>
    /// <seealso class='IDTExtensibility2' />
    public class Connect : IDTExtensibility2
    {
        private CommandBarEvents connectItemHandler;

        private CommandBarEvents addRuleCodeItemHandler;

        private CommandBarEvents AddCodeConfigItemHandler;

        private CommandBarEvents AddReferenceItemHandler;

        private CommandBarEvents updateHandler;

        private CommandBarEvents updateEDHandler;

        private CommandBarEvents SqlEditHandler;

        private CommandBarEvents SqlListHandler;
        private DTE2 _applicationObject;
        private AddIn _addInInstance;

        public void QueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object commandText)
        {
            if (neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone)
            {
                if (commandName == "WTFCode.Connect.WTFCode")
                {
                    status = (vsCommandStatus)3;
                }
            }
        }

        public void Exec(string commandName, vsCommandExecOption executeOption, ref object varIn, ref object varOut, ref bool handled)
        {
            handled = false;
            if (executeOption == vsCommandExecOption.vsCommandExecOptionDoDefault)
            {
                if (commandName == "WTFCode.Connect.WTFCode")
                {
                    handled = true;
                }
            }
        }

        public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
        {
            this._applicationObject = (DTE2)application;
            this._addInInstance = (AddIn)addInInst;
            if (connectMode == ext_ConnectMode.ext_cm_UISetup || connectMode == ext_ConnectMode.ext_cm_Startup)
            {
                object[] array = new object[0];
                Commands2 commands = (Commands2)this._applicationObject.Commands;
                string index = "Tools";
                CommandBar commandBar = ((CommandBars)this._applicationObject.CommandBars)["MenuBar"];
                CommandBarControl commandBarControl = commandBar.Controls[index];
                CommandBarPopup commandBarPopup = (CommandBarPopup)commandBarControl;
                CommandBars commandBars = (CommandBars)this._applicationObject.DTE.CommandBars;
                CommandBar commandBar2 = commandBars["Project"];
                CommandBar commandBar3 = commandBars["Item"];
                CommandBar commandBar4 = commandBars["Folder"];
                try
                {
                    CommandBarControl commandBarControl2 = commandBar2.Controls.Add(MsoControlType.msoControlButton, 1, "", 1, true);
                    commandBarControl2.Tag = "ConnectConfig";
                    commandBarControl2.Caption = "wtf配置连接";
                    commandBarControl2.TooltipText = "ConnectConfig";
                    this.connectItemHandler = (CommandBarEvents)this._applicationObject.DTE.Events.get_CommandBarEvents(commandBarControl2);
                    this.connectItemHandler.Click += ConnectItem_Click;
                    CommandBarControl commandBarControl3 = commandBar2.Controls.Add(MsoControlType.msoControlButton, 1, "", 2, true);
                    commandBarControl3.Tag = "AddRuleCode";
                    commandBarControl3.Caption = "wtf新增业务层";
                    commandBarControl3.TooltipText = "AddRuleCode";
                    this.addRuleCodeItemHandler = (CommandBarEvents)this._applicationObject.DTE.Events.get_CommandBarEvents(commandBarControl3);
                    this.addRuleCodeItemHandler.Click += AddRuleCodeItem_Click;
                    CommandBarControl commandBarControl4 = commandBar2.Controls.Add(MsoControlType.msoControlButton, 1, "", 3, true);
                    commandBarControl4.Tag = "AddCodeConfig";
                    commandBarControl4.Caption = "wtf一键生成配置文件";
                    commandBarControl4.TooltipText = "AddCodeConfig";
                    this.AddCodeConfigItemHandler = (CommandBarEvents)this._applicationObject.DTE.Events.get_CommandBarEvents(commandBarControl4);
                    this.AddCodeConfigItemHandler.Click += AddCodeConfigItem_Click;
                    CommandBarControl commandBarControl5 = commandBar3.Controls.Add(MsoControlType.msoControlButton, 1, "", 1, true);
                    commandBarControl5.Tag = "Update";
                    commandBarControl5.Caption = "wtf更新代码";
                    commandBarControl5.TooltipText = "Update";
                    this.updateHandler = (CommandBarEvents)this._applicationObject.DTE.Events.get_CommandBarEvents(commandBarControl5);
                    this.updateHandler.Click += CSItem_Click;
                    CommandBarControl commandBarControl6 = commandBar3.Controls.Add(MsoControlType.msoControlButton, 1, "", 2, true);
                    commandBarControl6.Tag = "Update";
                    commandBarControl6.Caption = "wtf更新实体和访问层";
                    commandBarControl6.TooltipText = "Update";
                    this.updateEDHandler = (CommandBarEvents)this._applicationObject.DTE.Events.get_CommandBarEvents(commandBarControl6);
                    this.updateEDHandler.Click += CSEDItem_Click;
                    CommandBarControl commandBarControl7 = commandBar3.Controls.Add(MsoControlType.msoControlButton, 1, "", 3, true);
                    commandBarControl7.Tag = "SqlEdit";
                    commandBarControl7.Caption = "wtf新增编辑页";
                    commandBarControl7.TooltipText = "SqlEdit";
                    this.SqlEditHandler = (CommandBarEvents)this._applicationObject.DTE.Events.get_CommandBarEvents(commandBarControl7);
                    this.SqlEditHandler.Click += SqlEditItem_Click;
                    CommandBarControl commandBarControl8 = commandBar3.Controls.Add(MsoControlType.msoControlButton, 1, "", 4, true);
                    commandBarControl8.Tag = "SqlLsit";
                    commandBarControl8.Caption = "wtf新增列表页";
                    commandBarControl8.TooltipText = "SqlLsit";
                    this.SqlListHandler = (CommandBarEvents)this._applicationObject.DTE.Events.get_CommandBarEvents(commandBarControl8);
                    this.SqlListHandler.Click += SqlListItem_Click;
                    Command command = commands.AddNamedCommand2(this._addInInstance, "WTFCode", "WTFCode", "Executes the command for WTFCode", true, 59, ref array, 3, 3, vsCommandControlType.vsCommandControlTypeButton);
                    if (command != null && commandBarPopup != null)
                    {
                        command.AddControl(commandBarPopup.CommandBar, 1);
                    }
                }
                catch (ArgumentException)
                {
                }
            }
        }

        private void ConnectItem_Click(object commandBarControl, ref bool handled, ref bool cancelDefault)
        {
            try
            {
                SelectFileInfo selectFileInfo = this.GetSelectFileInfo();
                if (selectFileInfo != null)
                {
                    ConnectConfigFrom connectConfigFrom = new ConnectConfigFrom(selectFileInfo);
                    connectConfigFrom.Show();
                    handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("配置链接串" + ex.Message);
            }
        }

        private void AddRuleCodeItem_Click(object commandBarControl, ref bool handled, ref bool cancelDefault)
        {
            try
            {
                SelectFileInfo selectFileInfo = this.GetSelectFileInfo();
                if (selectFileInfo == null)
                {
                    return;
                }
                if (!selectFileInfo.IsExistsCodeConfig())
                {
                    return;
                }
                if (!selectFileInfo.IsExistsSevenConfig())
                {
                    return;
                }
                SqlRuleCodeForm sqlRuleCodeForm = new SqlRuleCodeForm(selectFileInfo);
                sqlRuleCodeForm.Show();
                handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("新建业务层抛出异常，异常信息：" + ex.ToString());
            }
            handled = true;
        }

        private void AddCodeConfigItem_Click(object commandBarControl, ref bool handled, ref bool cancelDefault)
        {
            try
            {
                SelectFileInfo selectFileInfo = this.GetSelectFileInfo();
                if (selectFileInfo == null)
                {
                    return;
                }
                if (!selectFileInfo.IsExistsCodeConfig())
                {
                    handled = true;
                    return;
                }
                CodeConfigHelper codeConfigHelper = new CodeConfigHelper(selectFileInfo.CodeConfigPath);
                if (!codeConfigHelper.LoadCodeConfigXml())
                {
                    return;
                }
                if (!Directory.Exists(selectFileInfo.ProjectPath + "\\DataAccess") || !Directory.Exists(selectFileInfo.ProjectPath + "\\Business") || !Directory.Exists(selectFileInfo.ProjectPath + "\\DataEntity"))
                {
                    DialogResult dialogResult = MessageBox.Show("业务层不存在，要新增业务层吗？", "", MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.OK)
                    {
                        SqlRuleCodeForm sqlRuleCodeForm = new SqlRuleCodeForm(selectFileInfo);
                        sqlRuleCodeForm.Show();
                    }
                    handled = true;
                    return;
                }
                string[] files = Directory.GetFiles(selectFileInfo.ProjectPath + "\\DataAccess", "Da*.cs");
                string[] files2 = Directory.GetFiles(selectFileInfo.ProjectPath + "\\Business", "Biz*.cs");
                string[] files3 = Directory.GetFiles(selectFileInfo.ProjectPath + "\\DataEntity", "*.cs");
                if (files.Count<string>() <= 0 || files2.Count<string>() <= 0 || files3.Count<string>() <= 0)
                {
                    DialogResult dialogResult = MessageBox.Show("业务层不存在，要新增业务层吗？", "", MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.OK)
                    {
                        SqlRuleCodeForm sqlRuleCodeForm = new SqlRuleCodeForm(selectFileInfo);
                        sqlRuleCodeForm.Show();
                    }
                    handled = true;
                    return;
                }
                Dictionary<string, BusinessNodeInfo> dictionary = new Dictionary<string, BusinessNodeInfo>();
                List<BusinessNodeInfo> business = codeConfigHelper.GetBusiness();
                string[] array = files3;
                for (int i = 0; i < array.Length; i++)
                {
                    string path = array[i];
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
                    string value = Path.Combine(selectFileInfo.ProjectPath, "DataAccess", "Da" + fileNameWithoutExtension + ".cs");
                    string value2 = Path.Combine(selectFileInfo.ProjectPath, "Business", "Biz" + fileNameWithoutExtension + ".cs");
                    if (files.Contains(value) && files2.Contains(value2))
                    {
                        BusinessNodeInfo objBusinessNodeInfo = CodeConfigHelper.GetReadFileBusinessNodeInfo(fileNameWithoutExtension, selectFileInfo.ProjectPath);
                        if (objBusinessNodeInfo != null)
                        {
                            BusinessNodeInfo businessNodeInfo = business.FirstOrDefault((BusinessNodeInfo s) => s.TableName == objBusinessNodeInfo.TableName);
                            if (businessNodeInfo == null)
                            {
                                dictionary.Add(objBusinessNodeInfo.TableName, objBusinessNodeInfo);
                            }
                            else
                            {
                                businessNodeInfo.ConnectionKeyOrConnectionString = objBusinessNodeInfo.ConnectionKeyOrConnectionString;
                                businessNodeInfo.LogModuleType = objBusinessNodeInfo.LogModuleType;
                                businessNodeInfo.IsMongoDB = objBusinessNodeInfo.IsMongoDB;
                                dictionary.Add(businessNodeInfo.TableName, businessNodeInfo);
                            }
                        }
                    }
                }
                AKeyAddCodeConfig aKeyAddCodeConfig = new AKeyAddCodeConfig(selectFileInfo, dictionary);
                aKeyAddCodeConfig.Show();
                handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("一键生成配置:" + ex.Message);
            }
            handled = true;
        }

        private void SqlEditItem_Click(object CommandBarControl, ref bool Handled, ref bool CancelDefault)
        {
            try
            {
                SelectFileInfo selectFileInfo = this.GetSelectFileInfo();
                if (selectFileInfo != null)
                {
                    if (selectFileInfo.IsExistsCodeConfig())
                    {
                        BusinessNodeInfo businessNodeInfo = this.GetBusinessNodeInfo(selectFileInfo);
                        if (businessNodeInfo == null)
                        {
                            Handled = true;
                        }
                        else
                        {
                            SqlEditForm sqlEditForm = new SqlEditForm(selectFileInfo, businessNodeInfo);
                            sqlEditForm.Show();
                            Handled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("新建编辑页抛出异常，异常信息：" + ex.ToString());
            }
        }

        private void SqlListItem_Click(object CommandBarControl, ref bool Handled, ref bool CancelDefault)
        {
            try
            {
                SelectFileInfo selectFileInfo = this.GetSelectFileInfo();
                if (selectFileInfo == null)
                {
                    return;
                }
                if (!selectFileInfo.IsExistsCodeConfig())
                {
                    return;
                }
                if (!selectFileInfo.IsExistsSevenConfig())
                {
                    return;
                }
                BusinessNodeInfo businessNodeInfo = this.GetBusinessNodeInfo(selectFileInfo);
                if (businessNodeInfo == null)
                {
                    Handled = true;
                    return;
                }
                SqlListForm sqlListForm = new SqlListForm(selectFileInfo, businessNodeInfo);
                sqlListForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("新建列表页抛出异常，异常信息：" + ex.ToString());
                return;
            }
            Handled = true;
        }

        public SelectFileInfo GetSelectFileInfo()
        {
            Array array = (Array)this._applicationObject.ActiveSolutionProjects;
            SelectFileInfo result;
            if (array.Length <= 0)
            {
                result = null;
            }
            else
            {
                Project project = (Project)array.GetValue(0);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(project.FullName);
                string directoryName = Path.GetDirectoryName(project.FullName);
                string projectItemPath = "";
                string itemFileName = "";
                ProjectItem projectItem = this._applicationObject.SelectedItems.Item(1).ProjectItem;
                if (projectItem != null)
                {
                    projectItemPath = (string)projectItem.Properties.Item("FullPath").Value;
                    itemFileName = Path.GetFileNameWithoutExtension((string)projectItem.Properties.Item("FullPath").Value);
                }
                string codeConfigPath = directoryName + "\\CodeConfig.xml";
                string sevenConfigPath = Path.Combine(Path.GetDirectoryName(this._addInInstance.SatelliteDllPath), "SevenCode.config");
                result = new SelectFileInfo
                {
                    SevenConfigPath = sevenConfigPath,
                    Project = project,
                    ApplicationObject = this._applicationObject,
                    ProjectItem = projectItem,
                    ProjectName = fileNameWithoutExtension,
                    ItemFileName = itemFileName,
                    ProjectItemPath = projectItemPath,
                    ProjectPath = directoryName,
                    CodeConfigPath = codeConfigPath
                };
            }
            return result;
        }
        private string GetSevenCodeConfigPath()
        {
            string text = Path.Combine(Path.GetDirectoryName(this._addInInstance.SatelliteDllPath), "SevenCode.config");
            string result;
            if (!File.Exists(text))
            {
                MessageBox.Show("插件根目录SevenCode.config不存在");
                result = "";
            }
            else
            {
                result = text;
            }
            return result;
        }

        public BusinessNodeInfo GetBusinessNodeInfo(SelectFileInfo objSelectFileInfo)
        {
            BusinessNodeInfo result;
            if (!objSelectFileInfo.IsExistsCodeConfig())
            {
                result = null;
            }
            else
            {
                string itemPathEntityName = objSelectFileInfo.GetItemPathEntityName();
                if (string.IsNullOrWhiteSpace(itemPathEntityName))
                {
                    MessageBox.Show(objSelectFileInfo.ItemFileName + ".cs,不是基础业务层类,因此无法更新或新增");
                    result = null;
                }
                else
                {
                    CodeConfigHelper codeConfigHelper = new CodeConfigHelper(objSelectFileInfo.CodeConfigPath);
                    if (!codeConfigHelper.LoadCodeConfigXml())
                    {
                        result = null;
                    }
                    else
                    {
                        BusinessNodeInfo business = codeConfigHelper.GetBusiness(itemPathEntityName);
                        if (business == null)
                        {
                            if (MessageBox.Show(itemPathEntityName + "实体未配置业务XML?是否配置吗？", "", MessageBoxButtons.OKCancel) != DialogResult.OK)
                            {
                                result = null;
                            }
                            else
                            {
                                Dictionary<string, BusinessNodeInfo> dictionary = new Dictionary<string, BusinessNodeInfo>();
                                BusinessNodeInfo readFileBusinessNodeInfo = CodeConfigHelper.GetReadFileBusinessNodeInfo(itemPathEntityName, objSelectFileInfo.ProjectPath);
                                if (readFileBusinessNodeInfo == null)
                                {
                                    MessageBox.Show(objSelectFileInfo.ItemFileName + ".cs,不是基础业务层类,因此无法配置XML");
                                    result = null;
                                }
                                else
                                {
                                    dictionary.Add(readFileBusinessNodeInfo.TableName, readFileBusinessNodeInfo);
                                    AKeyAddCodeConfig aKeyAddCodeConfig = new AKeyAddCodeConfig(objSelectFileInfo, dictionary);
                                    aKeyAddCodeConfig.Show();
                                    result = null;
                                }
                            }
                        }
                        else
                        {
                            result = business;
                        }
                    }
                }
            }
            return result;
        }

        public TableRuleSchema CreateItemTableRuleSchema(SelectFileInfo objSelectFileInfo)
        {
            TableRuleSchema result;
            if (!objSelectFileInfo.IsExistsCodeConfig())
            {
                result = null;
            }
            else
            {
                string itemPathEntityName = objSelectFileInfo.GetItemPathEntityName();
                if (string.IsNullOrWhiteSpace(itemPathEntityName))
                {
                    MessageBox.Show(objSelectFileInfo.ItemFileName + ".cs,不是基础业务层类,因此无法更新或新增");
                    result = null;
                }
                else
                {
                    CodeConfigHelper codeConfigHelper = new CodeConfigHelper(objSelectFileInfo.CodeConfigPath);
                    if (!codeConfigHelper.LoadCodeConfigXml())
                    {
                        result = null;
                    }
                    else
                    {
                        BusinessNodeInfo business = codeConfigHelper.GetBusiness(itemPathEntityName);
                        if (business == null)
                        {
                            if (MessageBox.Show(itemPathEntityName + "实体未配置业务XML?是否配置吗？", "", MessageBoxButtons.OKCancel) != DialogResult.OK)
                            {
                                result = null;
                            }
                            else
                            {
                                Dictionary<string, BusinessNodeInfo> dictionary = new Dictionary<string, BusinessNodeInfo>();
                                BusinessNodeInfo readFileBusinessNodeInfo = CodeConfigHelper.GetReadFileBusinessNodeInfo(itemPathEntityName, objSelectFileInfo.ProjectPath);
                                if (readFileBusinessNodeInfo == null)
                                {
                                    MessageBox.Show(objSelectFileInfo.ItemFileName + ".cs,不是基础业务层类,因此无法配置XML");
                                    result = null;
                                }
                                else
                                {
                                    dictionary.Add(readFileBusinessNodeInfo.TableName, readFileBusinessNodeInfo);
                                    AKeyAddCodeConfig aKeyAddCodeConfig = new AKeyAddCodeConfig(objSelectFileInfo, dictionary);
                                    aKeyAddCodeConfig.Show();
                                    result = null;
                                }
                            }
                        }
                        else
                        {
                            string schemaName = "";
                            string connectionString = codeConfigHelper.GetConnectionString(business.ConnectionKey, out schemaName);
                            if (string.IsNullOrWhiteSpace(connectionString))
                            {
                                if (MessageBox.Show("连接串键值(" + business.ConnectionKey + ")不存在,要配置连接吗？", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
                                {
                                    ConnectConfigFrom connectConfigFrom = new ConnectConfigFrom(objSelectFileInfo);
                                    connectConfigFrom.Show();
                                }
                                result = null;
                            }
                            else
                            {
                                string tableName = business.TableName;
                                try
                                {
                                    TableRuleSchema tableRuleSchema = SqlSchemaHelper.GetAllRuleTables(schemaName, connectionString, true).FirstOrDefault((TableRuleSchema s) => s.TableName == tableName);
                                    if (tableRuleSchema == null)
                                    {
                                        MessageBox.Show("数据库找不到此:" + tableName + ",请检查配置是否正确");
                                        result = null;
                                    }
                                    else
                                    {
                                        CodeConfigHelper.BusinessNodeToTableRuleSchema(business, tableRuleSchema);
                                        tableRuleSchema.Columns = SqlSchemaHelper.GetTableRuleColumnsSchema(connectionString, schemaName, tableRuleSchema.TableName, codeConfigHelper);
                                        result = tableRuleSchema;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("更新代码出现异常,异常错误：" + ex.ToString());
                                    result = null;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        private static string GetItemPathEntityName(string projectItemPath, string itemFileName)
        {
            string result = string.Empty;
            if (projectItemPath.IndexOf("DataEntity") > 0)
            {
                result = itemFileName;
            }
            else if (projectItemPath.IndexOf("DataAccess") > 0)
            {
                result = itemFileName.Substring(2);
            }
            else if (projectItemPath.IndexOf("Business") > 0)
            {
                result = itemFileName.Substring(3);
            }
            return result;
        }

        private void AddReferenceItem_Click(object commandBarControl, ref bool handled, ref bool cancelDefault)
        {
            try
            {
                SelectFileInfo selectFileInfo = this.GetSelectFileInfo();
                if (selectFileInfo != null)
                {
                    ReferenceForm referenceForm = new ReferenceForm(selectFileInfo);
                    referenceForm.Show();
                    handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加引用出错" + ex.Message);
            }
        }

        private void CSItem_Click(object CommandBarControl, ref bool Handled, ref bool CancelDefault)
        {
            try
            {
                SelectFileInfo selectFileInfo = this.GetSelectFileInfo();
                if (selectFileInfo == null)
                {
                    return;
                }
                if (!selectFileInfo.IsExistsCodeConfig())
                {
                    return;
                }
                TableRuleSchema tableRuleSchema = this.CreateItemTableRuleSchema(selectFileInfo);
                if (tableRuleSchema == null)
                {
                    Handled = true;
                    return;
                }
                string text = "";
                if (selectFileInfo.ProjectItemPath.IndexOf("DataEntity") > 0)
                {
                    text = tableRuleSchema.ToDataEntity(selectFileInfo.ProjectName);
                }
                else if (selectFileInfo.ProjectItemPath.IndexOf("DataAccess") > 0)
                {
                    text = tableRuleSchema.ToSqlDaRule(selectFileInfo.ProjectName);
                }
                else if (selectFileInfo.ProjectItemPath.IndexOf("Business") > 0)
                {
                    text = tableRuleSchema.ToSqlBizRule(selectFileInfo.ProjectName);
                }
                DialogResult dialogResult = MessageBox.Show("确定要更新" + selectFileInfo.ItemFileName + "文件吗?此更新不可恢复！", "", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    File.WriteAllText(selectFileInfo.ProjectItemPath, text, Encoding.UTF8);
                    MessageBox.Show("更新成功");
                }
                else
                {
                    Common.ShowCodeForm(text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("更新代码出现异常,异常错误：" + ex.ToString());
                return;
            }
            Handled = true;
        }

        private void CSEDItem_Click(object CommandBarControl, ref bool Handled, ref bool CancelDefault)
        {
            try
            {
                SelectFileInfo selectFileInfo = this.GetSelectFileInfo();
                if (selectFileInfo == null)
                {
                    return;
                }
                if (!selectFileInfo.IsExistsCodeConfig())
                {
                    return;
                }
                string itemPathEntityName = selectFileInfo.GetItemPathEntityName();
                string text = Path.Combine(selectFileInfo.ProjectPath, "DataEntity");
                if (!Directory.Exists(text))
                {
                    selectFileInfo.Project.ProjectItems.AddFolder("DataEntity", "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}");
                }
                string path = Path.Combine(text, itemPathEntityName + ".cs");
                string path2 = Path.Combine(selectFileInfo.ProjectPath, "DataAccess");
                if (!Directory.Exists(path2))
                {
                    selectFileInfo.Project.ProjectItems.AddFolder("DataAccess", "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}");
                }
                string path3 = Path.Combine(selectFileInfo.ProjectPath, "DataAccess", "Da" + itemPathEntityName + ".cs");
                TableRuleSchema tableRuleSchema = this.CreateItemTableRuleSchema(selectFileInfo);
                if (tableRuleSchema == null)
                {
                    Handled = true;
                    return;
                }
                string text2 = tableRuleSchema.ToDataEntity(selectFileInfo.ProjectName);
                DialogResult dialogResult = MessageBox.Show("确定要更新" + tableRuleSchema.EntityName + "实体层吗?此更新不可恢复！", "", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    File.WriteAllText(path, text2, Encoding.UTF8);
                }
                else
                {
                    Common.ShowCodeForm(text2);
                }
                text2 = tableRuleSchema.ToSqlDaRule(selectFileInfo.ProjectName);
                dialogResult = MessageBox.Show("确定要更新" + tableRuleSchema.EntityName + "数据访问层吗?此更新不可恢复！", "", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    File.WriteAllText(path3, text2, Encoding.UTF8);
                }
                else
                {
                    Common.ShowCodeForm(text2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("更新代码出现异常,异常错误：" + ex.ToString());
                return;
            }
            Handled = true;
        }

        /// <summary>实现外接程序对象的构造函数。请将您的初始化代码置于此方法内。</summary>
        public Connect()
        {
        }

        /// <summary>实现 IDTExtensibility2 接口的 OnConnection 方法。接收正在加载外接程序的通知。</summary>
        /// <param term='application'>宿主应用程序的根对象。</param>
        /// <param term='connectMode'>描述外接程序的加载方式。</param>
        /// <param term='addInInst'>表示此外接程序的对象。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
        {
            _applicationObject = (DTE2)application;
            _addInInstance = (AddIn)addInInst;

        }

        /// <summary>实现 IDTExtensibility2 接口的 OnDisconnection 方法。接收正在卸载外接程序的通知。</summary>
        /// <param term='disconnectMode'>描述外接程序的卸载方式。</param>
        /// <param term='custom'>特定于宿主应用程序的参数数组。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
        {
        }

        /// <summary>实现 IDTExtensibility2 接口的 OnAddInsUpdate 方法。当外接程序集合已发生更改时接收通知。</summary>
        /// <param term='custom'>特定于宿主应用程序的参数数组。</param>
        /// <seealso class='IDTExtensibility2' />		
        public void OnAddInsUpdate(ref Array custom)
        {
        }

        /// <summary>实现 IDTExtensibility2 接口的 OnStartupComplete 方法。接收宿主应用程序已完成加载的通知。</summary>
        /// <param term='custom'>特定于宿主应用程序的参数数组。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnStartupComplete(ref Array custom)
        {
        }

        /// <summary>实现 IDTExtensibility2 接口的 OnBeginShutdown 方法。接收正在卸载宿主应用程序的通知。</summary>
        /// <param term='custom'>特定于宿主应用程序的参数数组。</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnBeginShutdown(ref Array custom)
        {
        }

    }
}
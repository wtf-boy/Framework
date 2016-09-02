namespace WTF.Pages
{
    using WTF.Controls;
    using WTF.Framework;
    using WTF.Logging;
    using WTF.Power;
    using WTF.Power.Entity;
    using WTF.Theme;
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class PageBase : ModulePage
    {
        private ModuleRule _CurrentModuleRule = null;
        private Logger _Logger = new Logger("Application");
        private List<Sys_User> _Sys_UserList = null;
        private string _tAccountTypeAdminUserID = "";
        private UserRule _UserRule = null;

        public virtual void AutoObjectSetValue<T>(T objectT) where T: EntityObject
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo info in properties)
            {
                foreach (Attribute attribute in info.GetCustomAttributes(typeof(EdmScalarPropertyAttribute), false))
                {
                    EdmScalarPropertyAttribute attribute2 = attribute as EdmScalarPropertyAttribute;
                    if ((attribute2 != null) && !attribute2.EntityKeyProperty)
                    {
                        string str = null;
                        this.FindFieldValue(info.Name, this.Controls, ref str);
                        if (str != null)
                        {
                            if (info.PropertyType == typeof(Guid))
                            {
                                info.SetValue(objectT, new Guid(str), null);
                            }
                            else if (info.PropertyType == typeof(string))
                            {
                                info.SetValue(objectT, str, null);
                            }
                            else if (info.PropertyType == typeof(DateTime))
                            {
                                info.SetValue(objectT, DateTime.Parse(str), null);
                            }
                            else if (info.PropertyType == typeof(int))
                            {
                                info.SetValue(objectT, int.Parse(str), null);
                            }
                            else if (info.PropertyType == typeof(double))
                            {
                                info.SetValue(objectT, double.Parse(str), null);
                            }
                            else if (info.PropertyType == typeof(bool))
                            {
                                info.SetValue(objectT, (str.ToString().ToLower() == "true") ? ((object) 1) : ((object) (str.ToString().ToLower() == "1")), null);
                            }
                        }
                    }
                }
            }
        }

        public override bool CheckIsPowerData(string moduleTypeID, string powerPageCode)
        {
            if (this.CurrentUser.IsSuper)
            {
                return false;
            }
            return this.CurrentModuleRule.CheckIsPowerData(moduleTypeID, powerPageCode);
        }

        public override string CheckPowerFieldData(string moduleTypeID, string pageName, string fieldNameSource, string fieldName)
        {
            if (this.CurrentUser.IsSuper)
            {
                return "";
            }
            if (!this.IsCacheDataPower)
            {
                return this.CurrentUserRule.CheckPowerFieldData(moduleTypeID, pageName, fieldNameSource, fieldName, this.CurrentUser.UserID);
            }
            string childKey = "CheckPowerFieldData" + moduleTypeID.ToString() + pageName + fieldName + this.CurrentUser.Role;
            object fromCache = CacheHelper.GetFromCache(WTF.Framework.CacheType.Module.ToString(), childKey);
            if (fromCache == null)
            {
                fromCache = this.CurrentUserRule.CheckPowerFieldData(moduleTypeID, pageName, fieldNameSource, fieldName, this.CurrentUser.UserID);
                fromCache.AddToCache(WTF.Framework.CacheType.Module.ToString(), childKey, this.CachePowerMinute);
            }
            return (string) fromCache;
        }

        public virtual bool CheckPowerFrame()
        {
            return (this.CurrentUser.IsSuper || this.CurrentUserRule.CheckPowerFrame(this.ModuleTypeID, this.CurrentUser.UserID));
        }

        public override bool CheckPowerOperateButton(string commandName)
        {
            if (this.CurrentUser.IsSuper)
            {
                return true;
            }
            if (this.CoteID.IsNoNull() && this.CoteModuleID.IsNoNull())
            {
                return this.GetPowerOperateButton(this.ModuleTypeID, this.PowerPageCode, this.CoteModuleID, this.CoteID, commandName);
            }
            return this.GetPowerOperateButton(this.ModuleTypeID, this.PowerPageCode, commandName);
        }

        public override bool CheckPowerOperateButton(string moduleCode, string commandName)
        {
            if (this.CurrentUser.IsSuper)
            {
                return true;
            }
            if (this.CoteID.IsNoNull() && this.CoteModuleID.IsNoNull())
            {
                return this.GetPowerOperateButton(this.ModuleTypeID, moduleCode, this.CoteModuleID, this.CoteID, commandName);
            }
            return this.GetPowerOperateButton(this.ModuleTypeID, moduleCode, commandName);
        }

        public virtual bool CheckPowerPage()
        {
            if (this.CurrentUser.IsSuper)
            {
                return true;
            }
            if (this.CoteModuleID.IsNoNull() && this.CoteID.IsNoNull())
            {
                return this.CurrentUserRule.CheckPowerPage(this.ModuleTypeID, this.CoteModuleID, this.CoteID, this.PowerPageCode, this.CurrentUser.UserID);
            }
            return this.CurrentUserRule.CheckPowerPage(this.ModuleTypeID, this.PowerPageCode, this.CurrentUser.UserID);
        }

        public void CurrentBindData(MyGridView objGridView, ObjectQuery<DbDataRecord> objObjectQuery)
        {
            int recordCount = 0;
            objGridView.DataSource = objObjectQuery.GetPage<DbDataRecord>(this.Condition, this.SearchSortExpression(), this.PageSize, this.PageIndex, out recordCount);
            objGridView.RecordCount = recordCount;
            objGridView.PageSize = this.PageSize;
            objGridView.PageIndex = this.PageIndex;
            objGridView.DataBind();
        }

        public void CurrentBindData<T>(MyGridView objGridView, ObjectQuery<T> objObjectQuery) where T: EntityObject
        {
            int recordCount = 0;
            objGridView.DataSource = objObjectQuery.GetPage<T>(this.SearchCondition<T>(), this.SearchSortExpression(), this.PageSize, this.PageIndex, out recordCount);
            objGridView.RecordCount = recordCount;
            objGridView.PageSize = this.PageSize;
            objGridView.PageIndex = this.PageIndex;
            objGridView.DataBind();
        }

        public void CurrentBindData<T>(MyGridView objGridView, ObjectQuery<DbDataRecord> objObjectQuery) where T: EntityObject
        {
            int recordCount = 0;
            objGridView.DataSource = objObjectQuery.GetPage<DbDataRecord>(this.SearchCondition<T>(), this.SearchSortExpression(), this.PageSize, this.PageIndex, out recordCount);
            objGridView.RecordCount = recordCount;
            objGridView.PageSize = this.PageSize;
            objGridView.PageIndex = this.PageIndex;
            objGridView.DataBind();
        }

        public void CurrentBindData(DataBoundControl objDataBoundControl, ObjectQuery<DbDataRecord> objObjectQuery)
        {
            int recordCount = 0;
            objDataBoundControl.DataSource = objObjectQuery.GetPage<DbDataRecord>(this.Condition, this.SearchSortExpression(), this.PageSize, this.PageIndex, out recordCount);
            this.RecordCount = recordCount;
            objDataBoundControl.DataBind();
        }

        public void CurrentBindData<T>(DataBoundControl objDataBoundControl, ObjectQuery<T> objObjectQuery) where T: EntityObject
        {
            int recordCount = 0;
            objDataBoundControl.DataSource = objObjectQuery.GetPage<T>(this.SearchCondition<T>(), this.SearchSortExpression(), this.PageSize, this.PageIndex, out recordCount);
            this.RecordCount = recordCount;
            objDataBoundControl.DataBind();
        }

        public void CurrentBindData(Repeater objRepeater, ObjectQuery<DbDataRecord> objObjectQuery)
        {
            int recordCount = 0;
            objRepeater.DataSource = objObjectQuery.GetPage<DbDataRecord>(this.Condition, this.SortExpression, this.PageSize, this.PageIndex, out recordCount);
            this.RecordCount = recordCount;
            objRepeater.DataBind();
        }

        public void CurrentBindData<T>(Repeater objRepeater, ObjectQuery<T> objObjectQuery) where T: EntityObject
        {
            int recordCount = 0;
            objRepeater.DataSource = objObjectQuery.GetPage<T>(this.SearchCondition<T>(), this.SortExpression, this.PageSize, this.PageIndex, out recordCount);
            this.RecordCount = recordCount;
            objRepeater.DataBind();
        }

        public void CurrentBindData<T>(MyGridView objGridView, ObjectQuery<T> objObjectQuery, string fields) where T: EntityObject
        {
            int recordCount = 0;
            objGridView.DataSource = objObjectQuery.GetPage<T>(fields, this.SearchCondition<T>(), this.SearchSortExpression(), this.PageSize, this.PageIndex, out recordCount);
            objGridView.RecordCount = recordCount;
            objGridView.PageSize = this.PageSize;
            objGridView.PageIndex = this.PageIndex;
            objGridView.DataBind();
        }

        protected virtual void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        public override void CurrentContent_Sorting(object sender, GridViewSortEventArgs e)
        {
            e.SortDirection = SortDirection.Descending;
            if ((this.ViewState["ClickSortField"] != null) && (this.ViewState["ClickSortField"].ToString() == e.SortExpression))
            {
                if ((this.ViewState["SortDirection"] != null) && (this.ViewState["SortDirection"].ToString() == "ASC"))
                {
                    this.ViewState["SortDirection"] = "DESC";
                }
                else
                {
                    this.ViewState["SortDirection"] = "ASC";
                }
            }
            else
            {
                this.ViewState["SortDirection"] = "DESC";
            }
            this.ViewState["ClickSortField"] = e.SortExpression;
            string str = (string) this.ViewState["SortDirection"];
            this.ClickSortExpression = "it." + e.SortExpression + ((str == "ASC") ? "" : (" " + str));
            this.RenderPage();
            MyGridView view = (MyGridView) sender;
            string str2 = (str == "ASC") ? view.SortedAscendingHeaderStyle.CssClass : view.SortedDescendingHeaderStyle.CssClass;
            view.HeaderRow.Cells[this.GetColumnIndex(e.SortExpression, view.Columns)].CssClass = str2;
        }

        public string CurrentLayoutPath(string UserID)
        {
            UserThemeInfo userTheme = new UserThemeRule().GetUserTheme(this.ModuleTypeID, UserID);
            if (userTheme != null)
            {
                return userTheme.LayoutPath;
            }
            return this.LayoutTheme;
        }

        public virtual void CurrentPager_PagerChangeCommand(object sender, PagerEventArgs e)
        {
            if (e.PagerChangeType == PagerChangeType.PageIndex)
            {
                this.PageIndex = e.ChangeValue;
                this.RenderPage();
            }
            else
            {
                this.PageIndex = 0;
                this.PageSize = e.ChangeValue;
                this.RenderPage();
            }
        }

        protected virtual void CurrentTool_ItemCommand(object sender, MyCommandEventArgs e)
        {
        }

        public virtual string DataConditon()
        {
            return this.DataConditon(this.ModuleTypeID, this.PowerPageCode);
        }

        public virtual string DataConditon(string moduleCode)
        {
            return this.DataConditon(this.ModuleTypeID, moduleCode);
        }

        public virtual string DataConditon(string moduleTypeID, string moduleCode)
        {
            if (this.IsPowerDataCheck)
            {
                if (this.CurrentUser.IsSuper)
                {
                    return "";
                }
                if (this.CheckIsPowerData(moduleTypeID, moduleCode))
                {
                    if (!this.IsCacheDataPower)
                    {
                        return this.CurrentUserRule.CheckPowerDataCondition(moduleTypeID, moduleCode, this.CurrentUser.UserID);
                    }
                    string childKey = "CheckDataCondition" + moduleTypeID.ToString() + moduleCode + this.CurrentUser.Role;
                    object fromCache = CacheHelper.GetFromCache(WTF.Framework.CacheType.Module.ToString(), childKey);
                    if (fromCache == null)
                    {
                        fromCache = this.CurrentUserRule.CheckPowerDataCondition(moduleTypeID, moduleCode, this.CurrentUser.UserID);
                        fromCache.AddToCache(WTF.Framework.CacheType.Module.ToString(), childKey, this.CachePowerMinute);
                    }
                    return (string) fromCache;
                }
            }
            return "";
        }

        public virtual string DataFieldConditon(string fieldSourceName, string fieldName)
        {
            return this.DataFieldConditon(this.ModuleTypeID, this.PowerPageCode, fieldSourceName, fieldName);
        }

        public virtual string DataFieldConditon(string moduleTypeID, string moduleCode, string fieldSourceName, string fieldName)
        {
            if (this.IsPowerDataCheck && this.CheckIsPowerData(moduleTypeID, moduleCode))
            {
                return this.CheckPowerFieldData(moduleTypeID, moduleCode, fieldSourceName, fieldName);
            }
            return "";
        }

        private void FindFieldValue(string field, ControlCollection objControls, ref string value)
        {
            foreach (Control control in objControls)
            {
                if (control.ID == field)
                {
                    if (control is MyTextBox)
                    {
                        MyTextBox box = (MyTextBox) control;
                        if (box.MaxCharLength == 0)
                        {
                            value = box.Text;
                        }
                        else
                        {
                            value = box.TextCut(box.MaxCharLength);
                        }
                    }
                    else if (control is MyXheditor)
                    {
                        MyXheditor xheditor = (MyXheditor) control;
                        value = xheditor.Text;
                    }
                    else if (control is MyDropDownList)
                    {
                        MyDropDownList list = (MyDropDownList) control;
                        value = list.SelectedValue;
                    }
                    else if (control is MyCheckBoxList)
                    {
                        MyCheckBoxList list2 = (MyCheckBoxList) control;
                        value = list2.SelectValueString;
                    }
                    else if (control is MyRadioButtonList)
                    {
                        MyRadioButtonList list3 = (MyRadioButtonList) control;
                        value = list3.SelectedValue;
                    }
                    else
                    {
                        if (!(control is CheckBox))
                        {
                            throw new ArgumentException("未能识别" + field + "控制类型，请使用MyTextBox,MyDropDownList,MyCheckBoxList,MyRadioButtonList,CheckBox,MyXheditor的控件");
                        }
                        CheckBox box2 = (CheckBox) control;
                        value = box2.Checked.ToString();
                    }
                }
                if (control.HasControls())
                {
                    this.FindFieldValue(field, control.Controls, ref value);
                }
            }
        }

        private int GetColumnIndex(string SortExpression, DataControlFieldCollection columns)
        {
            int num = 0;
            foreach (DataControlField field in columns)
            {
                if (field.SortExpression == SortExpression)
                {
                    return num;
                }
                num++;
            }
            return num;
        }

        public override bool GetPowerOperateButton(string moduleTypeID, string moduleCode, string commandName)
        {
            return (this.CurrentUser.IsSuper || this.CurrentModuleRule.GetPowerOperateButton(moduleTypeID, moduleCode, this.CurrentUser.UserID, commandName));
        }

        public override bool GetPowerOperateButton(string moduleTypeID, string moduleCode, string coteModuleID, string coteID, string commandName)
        {
            return (this.CurrentUser.IsSuper || this.CurrentModuleRule.GetPowerOperateButton(moduleTypeID, moduleCode, this.CurrentUser.UserID, coteModuleID, coteID, commandName));
        }

        public override List<OperateModuleInfo> GetPowerOperateModule(string moduleTypeID, string moduleCode, OperatePlaceType operatePlaceType)
        {
            return (from s in this.CurrentModuleRule.GetPowerOperateModule(moduleTypeID, moduleCode, this.CurrentUser.UserID, operatePlaceType) select new OperateModuleInfo { MenuCal = s.MenuCal, ClickScriptFun = s.ClickScriptFun, CommandName = s.CommandName, PlaceType = s.PlaceType, ImageUrl = s.ImageUrl, MenuField = s.MenuField, MenuValue = s.MenuValue, ModuleID = s.ModuleID, ModuleName = s.ModuleName, SortIndex = s.SortIndex, CommandArgument = s.CommandArgument, ToolTip = s.ToolTip, ValGroupName = s.ValGroupName }).ToList<OperateModuleInfo>();
        }

        public override List<OperateModuleInfo> GetPowerOperateModule(string moduleTypeID, string moduleCode, string coteModuleID, string coteID, OperatePlaceType operatePlaceType)
        {
            return (from s in this.CurrentModuleRule.GetPowerOperateModule(moduleTypeID, moduleCode, this.CurrentUser.UserID, operatePlaceType, coteModuleID, coteID) select new OperateModuleInfo { MenuCal = s.MenuCal, ClickScriptFun = s.ClickScriptFun, CommandName = s.CommandName, PlaceType = s.PlaceType, ImageUrl = s.ImageUrl, MenuField = s.MenuField, MenuValue = s.MenuValue, ModuleID = s.ModuleID, ModuleName = s.ModuleName, SortIndex = s.SortIndex, CommandArgument = s.CommandArgument, ToolTip = s.ToolTip, ValGroupName = s.ValGroupName }).ToList<OperateModuleInfo>();
        }

        public virtual string GetSystemUserName(int userID)
        {
            Sys_User user = this.GetSystemUserList.FirstOrDefault<Sys_User>(S => S.ID == userID);
            if (user == null)
            {
                return "";
            }
            return user.UserName;
        }

        public virtual string GetSystemUserName(string userID)
        {
            Sys_User user = this.GetSystemUserList.FirstOrDefault<Sys_User>(s => s.UserID == userID);
            if (user == null)
            {
                return "";
            }
            return user.UserName;
        }

        public virtual void InitDataPage()
        {
        }

        public override bool IsCheckCotePower(string coteModuleID)
        {
            Sys_Module instance = this.CurrentModuleRule.Sys_Module.FirstOrDefault<Sys_Module>(s => s.ModuleID == coteModuleID);
            return (instance.IsNoNull() && (instance.ModuleCoteID > 0));
        }

        public void LogWrite(string logtitle, Exception objExp)
        {
            LogHelper.Write(this.GetLogModuleInfo().ModuleTypeCode, LogCategory.ExceptionError.ToString(), logtitle, objExp, "");
        }

        protected override void OnError(EventArgs e)
        {
            Exception lastError = base.Server.GetLastError();
            if ((lastError.GetType() == typeof(HttpException)) && lastError.Message.IsMatch("超过了最大请求长度"))
            {
                base.Response.Write("请求出错,请检查上传的文件的大小是否超过" + ((int) (ConfigHelper.GetIntValue("MaxFileSize", 0x2800) * 0x400)).RenderFileSize() + "或提交的请求超过" + ((int) (ConfigHelper.GetIntValue("MaxFileSize", 0x2800) * 0x400)).RenderFileSize());
                base.Response.End();
                base.OnError(e);
            }
            else
            {
                LogModuleInfo logModuleInfo = this.GetLogModuleInfo();
                if (logModuleInfo.IsDispose)
                {
                    base.Server.ClearError();
                    LogHelper.Write(logModuleInfo.ModuleTypeCode, LogCategory.ExceptionError.ToString(), "访问界面出错", lastError, "");
                    this.PageErrorProcess();
                }
                base.OnError(e);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.IsPowerCheck && this.ModuleTypeID.IsNoNull())
            {
                int userTypeCID = this.CurrentUser.UserTypeCID;
                if (!this.CheckUrlQuery())
                {
                    this.RedirectPageError("对不起你访问不正确的地址");
                    return;
                }
                if (!this.UserTypeList.Contains(userTypeCID))
                {
                    this.PowerRedirect(PowerType.LoginPower, (this.CurrentUser.UserTypeCID == -1) ? "对不起你登录超时，请重新登录" : "对不起你没有权限访问此平台");
                    return;
                }
                if (this.CheckPowerType == PowerType.PagePower)
                {
                    if (!this.CheckPowerPage())
                    {
                        if (!this.CheckPowerFrame())
                        {
                            this.PowerRedirect(PowerType.PagePower, "对不起你没有权限访问此界面");
                        }
                        else
                        {
                            this.PowerWriteError("对不起你没有权限访问此界面");
                        }
                        return;
                    }
                }
                else if ((this.CheckPowerType == PowerType.FramePower) && !this.CheckPowerFrame())
                {
                    this.PowerRedirect(PowerType.PagePower, "对不起你没有权限访问此平台");
                    return;
                }
            }
            if (!base.IsPostBack)
            {
                this.InitDataPage();
            }
            if (!(base.IsPostBack && !base.IsLoadPageStateFromToSession))
            {
                this.RenderPage();
            }
        }

        private void PageErrorProcess()
        {
            if (!this.ErrorIsRedirect)
            {
                base.Response.Write(this.ErrorHint);
                base.Response.End();
            }
            else
            {
                base.Response.Redirect(this.ErrorUrl);
            }
        }

        public virtual void PowerRedirect(PowerType powerType)
        {
            if (this.PowerIsRedirect)
            {
                string str = this.LoginUrl + "?PowerExit=" + powerType.ToString();
                ("window.top.location='" + this.LoginUrl + "'").RegisterScript(RegisterType.ClientBlock);
            }
            else
            {
                base.Response.Clear();
                base.Response.Write("对不起你没有权限访问此界面");
                base.Response.End();
            }
        }

        public virtual void PowerRedirect(PowerType powerType, string message)
        {
            if (this.PowerIsRedirect)
            {
                string str = this.LoginUrl + "?PowerExit=" + powerType.ToString() + "&PowerMessage=" + message.EncodeUrl();
                ("alert('" + message + "'); window.top.location='" + str + "';").RegisterScript(RegisterType.ClientBlock);
            }
            else
            {
                base.Response.Clear();
                base.Response.Write(message);
                base.Response.End();
            }
        }

        public virtual void PowerWriteError(string message)
        {
            base.Response.Clear();
            base.Response.Write(message);
            base.Response.End();
        }

        public virtual void RedirectPageError(string errors)
        {
            if (!this.ErrorIsRedirect)
            {
                base.Response.Clear();
                base.Response.Write(errors);
                base.Response.End();
            }
            else
            {
                base.Response.Redirect(this.ErrorUrl + "?Errors=" + errors.EncodeUrl());
            }
        }

        public virtual void RenderPage()
        {
        }

        public virtual string SearchCondition<T>()
        {
            string condition = this.Condition;
            string str2 = this.QueryModel.ToConditionLinq<T>();
            string str3 = this.DataConditon(this.ModuleTypeID, this.PowerPageCode);
            if (condition.IsNoNull())
            {
                condition = condition + (str2.IsNull() ? "" : (" and " + str2));
            }
            else
            {
                condition = str2.IsNull() ? "" : str2;
            }
            if (condition.IsNoNull())
            {
                return (condition + (str3.IsNull() ? "" : (" and " + str3)));
            }
            return (str3.IsNull() ? "" : str3);
        }

        public virtual void SearchCondition()
        {
            this.PageIndex = 0;
            this.RenderPage();
        }

        public virtual string SearchSortExpression()
        {
            if (string.IsNullOrEmpty(this.ClickSortExpression))
            {
                return this.SortExpression;
            }
            return this.ClickSortExpression;
        }

        public ObjectQuery<T> WhereFieldData<T>(ObjectQuery<T> objObjectQuery, string fieldName)
        {
            string str = this.DataFieldConditon(fieldName, fieldName);
            if (str.IsNoNull())
            {
                objObjectQuery = objObjectQuery.Where(str, new ObjectParameter[0]);
            }
            return objObjectQuery;
        }

        public ObjectQuery<T> WhereFieldData<T>(ObjectQuery<T> objObjectQuery, string fieldSourceName, string fieldName)
        {
            string str = this.DataFieldConditon(fieldSourceName, fieldName);
            if (str.IsNoNull())
            {
                objObjectQuery = objObjectQuery.Where(str, new ObjectParameter[0]);
            }
            return objObjectQuery;
        }

        public ObjectQuery<T> WhereFieldData<T>(ObjectQuery<T> objObjectQuery, string pageCode, string fieldSourceName, string fieldName)
        {
            string str = this.DataFieldConditon(this.ModuleTypeID, pageCode, fieldSourceName, fieldName);
            if (str.IsNoNull())
            {
                objObjectQuery = objObjectQuery.Where(str, new ObjectParameter[0]);
            }
            return objObjectQuery;
        }

        public void WriteOperatorLog(OperationType operationType, string Title, object OperationData)
        {
            this.WriteOperatorLog(operationType, Title, "", OperationData);
        }

        public void WriteOperatorLog(OperationType operationType, string Title, string Description, object OperationData)
        {
            if (!string.IsNullOrWhiteSpace(this.MenuPowerID))
            {
                this.WriteOperatorLog(operationType, this.MenuPowerID, this.PowerName, Title, Description, OperationData);
            }
        }

        public void WriteOperatorLog(OperationType operationType, string MenuPowerID, string MenuName, string Title, string Description, object OperationData)
        {
            this.WriteOperatorLog(operationType, MenuPowerID, MenuName, "", Title, Description, OperationData);
        }

        public void WriteOperatorLog(OperationType operationType, string menuPowerID, string MenuName, string CommandName, string Title, string Description, object OperationData)
        {
            OperationLoger.WriteOperatorLog(operationType, menuPowerID, MenuName, this.CurrentUser.ID, this.CurrentUser.UserName, CommandName, Title, Description, OperationData);
        }

        public virtual PowerType CheckPowerType
        {
            get
            {
                return PowerType.PagePower;
            }
        }

        public virtual string Condition
        {
            get
            {
                return "";
            }
        }

        public virtual string ConditionSolr
        {
            get
            {
                return "";
            }
        }

        public string CurrentAccountTypeAdminUserID
        {
            get
            {
                if (this._tAccountTypeAdminUserID == "")
                {
                    Sys_User user = (from s in this.CurrentUserRule.Sys_User
                        where ((s.AccountTypeID == this.CurrentUser.AccountTypeID) && s.IsAdmin) && (s.UserTypeCID == this.CurrentUser.UserTypeCID)
                        select s).FirstOrDefault<Sys_User>();
                    this._tAccountTypeAdminUserID = (user == null) ? Guid.NewGuid().ToString() : user.UserID;
                }
                return this._tAccountTypeAdminUserID;
            }
        }

        public ModuleRule CurrentModuleRule
        {
            get
            {
                if (this._CurrentModuleRule == null)
                {
                    this._CurrentModuleRule = new ModuleRule();
                }
                return this._CurrentModuleRule;
            }
        }

        public UserThemeInfo CurrentThemeInfo
        {
            get
            {
                if (this.CurrentUser.UserID.IsNoNull())
                {
                    if (SysVariable.CurrentContext.Session["CurrentThemeInfo"] != null)
                    {
                        return (UserThemeInfo) SysVariable.CurrentContext.Session["CurrentThemeInfo"];
                    }
                    UserThemeInfo userTheme = new UserThemeRule().GetUserTheme(this.ModuleTypeID, this.CurrentUser.UserID);
                    if (userTheme == null)
                    {
                        userTheme = new UserThemeInfo {
                            OperateStyle = base.OperateStyle,
                            Theme = this.StyleTheme,
                            LayoutPath = this.LayoutTheme
                        };
                    }
                    SysVariable.CurrentContext.Session["CurrentThemeInfo"] = userTheme;
                    return userTheme;
                }
                return new UserThemeInfo { OperateStyle = base.OperateStyle, Theme = this.StyleTheme, LayoutPath = base.LayoutTheme };
            }
            set
            {
                SysVariable.CurrentContext.Session["CurrentThemeInfo"] = value;
            }
        }

        public UserInfo CurrentUser
        {
            get
            {
                return this.CurrentUserRule.CurrentUser;
            }
        }

        public UserRule CurrentUserRule
        {
            get
            {
                if (this._UserRule == null)
                {
                    this._UserRule = new UserRule();
                }
                return this._UserRule;
            }
        }

        public string EditID
        {
            get
            {
                return string.Empty;
            }
        }

        public List<Sys_User> GetSystemUserList
        {
            get
            {
                if (this._Sys_UserList == null)
                {
                    this._Sys_UserList = this.CurrentUserRule.Sys_User.ToList<Sys_User>();
                }
                return this._Sys_UserList;
            }
        }

        public Logger Log
        {
            get
            {
                return this._Logger;
            }
        }

        public override WTF.Framework.OperateStyle OperateStyle
        {
            get
            {
                return this.CurrentThemeInfo.OperateStyle;
            }
        }

        public int PageCount
        {
            get
            {
                if ((this.RecordCount % this.PageSize) > 0)
                {
                    return ((this.RecordCount / this.PageSize) + 1);
                }
                return (this.RecordCount / this.PageSize);
            }
        }

        public virtual bool PowerIsRedirect
        {
            get
            {
                return true;
            }
        }

        public WTF.Framework.QueryModel QueryModel
        {
            get
            {
                return SearchModelHelper.CreateQueryModel();
            }
        }

        public int RecordCount
        {
            get
            {
                int num = this.ViewState["_RecordCount"].ConvertInt();
                return ((num == 0) ? 0 : num);
            }
            set
            {
                this.ViewState["_RecordCount"] = value;
            }
        }

        public virtual string SortExpression
        {
            get
            {
                return "";
            }
        }

        public override string StyleSheetTheme
        {
            get
            {
                if (base.StyleSheetTheme.IsNoNull())
                {
                    return this.CurrentThemeInfo.Theme;
                }
                return base.StyleSheetTheme;
            }
            set
            {
                base.StyleSheetTheme = value;
            }
        }
    }
}


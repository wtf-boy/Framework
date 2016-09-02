using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Power;
using WTF.Framework;
using System.Data;
using WTF.Power.Entity;
public partial class SystemSafe_Role_RoleDataPower : SupportPageBase
{

    /// <summary>
    /// 标识
    /// </summary>
    public string RoleID
    {
        get
        {
            return GetString("RoleID");
        }
    }

    public string ModuleID
    {
        get
        {
            return GetString("ModuleID");
        }
    }
    string ValueFormant(string value, int FileDataType)
    {
        ///int
        if (FileDataType == 1)
        {

        }
        ///string
        else if (FileDataType == 2)
        {
            value = value.ConvertStringID();
        }
        ///bool
        else if (FileDataType == 3)
        {
        }
        ///guid
        else if (FileDataType == 4)
        {
            value = value.ConvertStringID();
        }
        return value;
    }
    public DataTable GetModuleData(Sys_ModuleData objSys_ModuleData)
    {

        MySqlHelper objSqlHelper = new MySqlHelper(objSys_ModuleData.ConnectionKey);


        string sqlPower = @"select Sys_RoleData.DataSelect from Sys_RoleData ,Sys_RoleUser where Sys_RoleUser.UserID='{0}' and Sys_RoleData.RoleID=Sys_RoleUser.RoleID
and Sys_RoleData.ModuleDataID='{1}'";
        string PowerSelect = objModuleRule.CurrentEntities.ExecuteStoreQuery<string>(string.Format(sqlPower, IsRolePowerManage ? CurrentAccountTypeAdminUserID : CurrentUser.UserID, objSys_ModuleData.ModuleDataID)).ToList<string>().ConvertListToString();
        DataTable objDataTable = objSqlHelper.ExecuteDataTable(" select * from (" + objSys_ModuleData.DataSelect + ") as DataPowers where DataValue in (" + ValueFormant(PowerSelect, objSys_ModuleData.FieldSourceType) + ")");
        objDataTable.Columns.Add("DataSelect");
        objDataTable.Columns.Add("DataID");
        string dataSelect = "";
        Sys_RoleData objSys_RoleData = objUserRule.Sys_RoleData.FirstOrDefault(s => s.ModuleDataID == objSys_ModuleData.ModuleDataID && s.RoleID == RoleID);
        if (objSys_RoleData.IsNoNull())
        {
            dataSelect = objSys_RoleData.DataSelect;
        }

        foreach (DataRow objRow in objDataTable.Rows)
        {
            objRow["DataSelect"] = dataSelect;
            objRow["DataID"] = objSys_ModuleData.ModuleDataID.ToString() + objSys_ModuleData.ModuleID.ToString();
        }
        return objDataTable;

    }
    ModuleRule objModuleRule = new ModuleRule();
    UserRule objUserRule = new UserRule();
    /// <summary>
    /// 页面初始化
    /// </summary>
    public override void RenderPage()
    {
        if (RoleID.IsNoNull())
        {


            rptDatalList.DataSource = objModuleRule.GetUserRolePowerModuleData(ModuleID, CurrentAccountTypeAdminUserID);
            rptDatalList.DataBind();

        }
        else
        {


        }
    }


    /// <summary>
    /// 保存信息
    /// </summary>
    public void SaveInfo()
    {
        if (hidDataVale.Value.IsNoNull())
        {

            foreach (string selectValue in hidDataVale.Value.Split(';'))
            {
                string[] select = selectValue.Split(':');
                string ModuleDataID = select[0];
                Sys_ModuleData objSys_ModuleData = objModuleRule.Sys_ModuleData.First(s => s.ModuleDataID == ModuleDataID);
                Sys_RoleData objSys_RoleData = objUserRule.Sys_RoleData.FirstOrDefault(s => s.RoleID == RoleID && s.ModuleDataID == ModuleDataID);

                string value = select[1];
                if (value.IsNoNull() && objSys_RoleData.IsNull())
                {
                    objSys_RoleData = new Sys_RoleData();
                    objSys_RoleData.ModuleDataID = ModuleDataID;
                    objSys_RoleData.RoleDataID = Guid.NewGuid().ToString();
                    objSys_RoleData.RoleID = RoleID;
                    objSys_RoleData.ModuleID = objSys_ModuleData.ModuleID;
                    objSys_RoleData.DataSelect = value;
                    objUserRule.CurrentEntities.AddTosys_roledata(objSys_RoleData);

                    Sys_Role objaddSys_Role = objUserRule.Sys_Role.FirstOrDefault(s => s.RoleID == RoleID && s.IsSystem == true);
                    if (objaddSys_Role.IsNoNull())
                    {
                        ///已经有设置了数据权限
                        string sqlUpdate = "";
                        foreach (Sys_RoleData sys_RoleData in objUserRule.CurrentEntities.ExecuteStoreQuery<Sys_RoleData>(string.Format(@"select Sys_RoleData.* from  Sys_RoleData 
                    where  RoleID in(select RoleID from Sys_Role where 
                     ModuleTypeID='{0}' and IsSystem=0 ) and ModuleDataID='{1}'", objaddSys_Role.ModuleTypeID.ToString(), ModuleDataID.ToString())))
                        {
                            var setvalue = sys_RoleData.DataSelect.ConvertListString().Intersect(value.ConvertListString()).ConvertListToString();
                            if (setvalue.IsNull())
                            {
                                sqlUpdate += string.Format("delete   from  Sys_RoleDat  where RoleDataID='{0}';", sys_RoleData.RoleDataID);
                            }
                            else
                            {
                                sqlUpdate += string.Format("update  Sys_RoleData set DataSelect='{0}' where RoleDataID='{1}';", setvalue, sys_RoleData.RoleDataID);
                            }
                        }
                        if (sqlUpdate.IsNoNull())
                        {
                            objUserRule.CurrentEntities.ExecuteStoreCommand(sqlUpdate);
                        }
                    }
                }
                else if (value.IsNoNull() && objSys_RoleData.IsNoNull())
                {
                    objSys_RoleData.DataSelect = value;
                    objUserRule.SaveChanges();
                    Sys_Role objaddSys_Role = objUserRule.Sys_Role.FirstOrDefault(s => s.RoleID == RoleID && s.IsSystem == true);
                    if (objaddSys_Role.IsNoNull())
                    {
                        ///已经有设置了数据权限
                        string sqlUpdate = "";
                        foreach (Sys_RoleData sys_RoleData in objUserRule.CurrentEntities.ExecuteStoreQuery<Sys_RoleData>(string.Format(@"select Sys_RoleData.* from  Sys_RoleData 
                    where  RoleID in(select RoleID from Sys_Role where 
                     ModuleTypeID='{0}' and IsSystem=0 ) and ModuleDataID='{1}'", objaddSys_Role.ModuleTypeID.ToString(), ModuleDataID.ToString())))
                        {
                            var setvalue = sys_RoleData.DataSelect.ConvertListString().Intersect(value.ConvertListString()).ConvertListToString();
                            if (setvalue.IsNull())
                            {
                                sqlUpdate += string.Format("delete   from  Sys_RoleData  where RoleDataID='{0}';", sys_RoleData.RoleDataID);
                            }
                            else
                            {
                                sqlUpdate += string.Format("update  Sys_RoleData set DataSelect='{0}' where RoleDataID='{1}';", setvalue, sys_RoleData.RoleDataID);
                            }
                        }
                        if (sqlUpdate.IsNoNull())
                        {
                            objUserRule.CurrentEntities.ExecuteStoreCommand(sqlUpdate);
                        }
                    }

                }
                else if (value.IsNull() && objSys_RoleData.IsNoNull())
                {
                    objUserRule.CurrentEntities.DeleteObject(objSys_RoleData);
                    objUserRule.SaveChanges();
                    Sys_Role objaddSys_Role = objUserRule.Sys_Role.FirstOrDefault(s => s.RoleID == RoleID && s.IsSystem == true);
                    if (objaddSys_Role.IsNoNull())
                    {
                        objUserRule.CurrentEntities.ExecuteStoreCommand(string.Format(@"delete from    Sys_RoleData 
                    where  RoleID in(select RoleID from Sys_Role where 
                     ModuleTypeID='{0}' and IsSystem=0 ) and ModuleDataID='{1}'", objaddSys_Role.ModuleTypeID.ToString(), ModuleDataID.ToString()));
                    }
                }
                else if (value.IsNull() && objSys_RoleData.IsNull())
                {
                }
                objUserRule.SaveChanges();


            }
            RenderPage();

        }
        MessageDialog("保存成功", this.RawUrl);
    }



    /// <summary>
    /// 操作栏
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Save":
                SaveInfo();
                break;

        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using WTF.Logging;
using WTF.Framework;
using WTF.Business;
using WTF.Project.DataAccess;
using WTF.Project.DataEntity;



namespace WTF.Project.Business
{
    /// <summary>
    /// 微信精选字典Dict业务逻辑层
    /// </summary>
    public partial class BizDict : BizBase<DaDict, Dict, Int32>
    {

        #region 变量

        /// <summary>
        /// 数据访问层变量
        /// </summary>
        DaDict objDaDict = null;

        /// <summary>
        /// 数据访问层
        /// </summary>
        public override DaDict Dal
        {
            get { return objDaDict; }
        }



        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BizDict()
            : this("")
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataObjectParam">表扩展名，如：us、jp</param>
        public BizDict(string dataObjectParam)
            : this("WTF.Project.ConnectionString", dataObjectParam)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionKeyOrConnectionString"></param>
        /// <param name="dataObjectParam">表扩展名，如：us、jp</param>
        public BizDict(string connectionKeyOrConnectionString, string dataObjectParam)
        {
            objDaDict = new DaDict(connectionKeyOrConnectionString, dataObjectParam);
            objDaDict.Log.LogModuleType = "WeiXinLog";
        }

        #endregion

        #region 新增

        /// <summary>
        /// 新增微信精选字典
        /// </summary>
        /// <param name="dict">Dict</param>
        /// <returns></returns>
        public Int32 Add(Dict dict)
        {
            dict.GUID.CheckIsNull("请输入GUID", LogModuleType);
            dict.TableName.CheckIsNull("请输入表名", LogModuleType);
            dict.FieldName.CheckIsNull("请输入字段名", LogModuleType);
            dict.DictDesc.CheckIsNull("请输入字典值描述", LogModuleType);

            return Dal.Add(dict);
        }


        #endregion

        #region 修改

        /// <summary>
        /// 修改微信精选字典
        /// </summary>
        /// <param name="dict">Dict</param>
        /// <returns></returns>
        public int Update(Dict dict)
        {
            dict.GUID.CheckIsNull("请输入GUID", LogModuleType);
            dict.TableName.CheckIsNull("请输入表名", LogModuleType);
            dict.FieldName.CheckIsNull("请输入字段名", LogModuleType);
            dict.DictDesc.CheckIsNull("请输入字典值描述", LogModuleType);

            return Dal.Update(dict);
        }


        #endregion


    }
}
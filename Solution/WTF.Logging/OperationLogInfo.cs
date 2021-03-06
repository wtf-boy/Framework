﻿namespace WTF.Logging
{
    using WTF.Framework;
    using System;
    using System.Runtime.CompilerServices;

    public class OperationLogInfo
    {
        public OperationLogInfo()
        {
            this.CreateDate = DateTime.Now;
            if ((SysVariable.CurrentContext != null) && (SysVariable.CurrentContext.Request != null))
            {
                this.UrlReferrer = (SysVariable.CurrentContext.Request.UrlReferrer == null) ? "" : SysVariable.CurrentContext.Request.UrlReferrer.ToString();
                this.RawUrl = SysVariable.CurrentContext.Request.RawUrl;
                this.UserHostAddress = RequestHelper.UserHostAddress;
            }
            else
            {
                this.UrlReferrer = "";
                this.RawUrl = "";
                this.UserHostAddress = "";
            }
            this.UserAccount = this.CurrentAccount();
        }

        public string CurrentAccount()
        {
            if (((SysVariable.CurrentContext != null) && (SysVariable.CurrentContext.Session != null)) && (SysVariable.CurrentContext.Session["CurrentUser"] != null))
            {
                return ((UserInfo) SysVariable.CurrentContext.Session["CurrentUser"]).Account;
            }
            return "";
        }

        public DateTime CreateDate { get; set; }

        public string LogModuleTypeCode { get; set; }

        public WTF.Logging.OperationType OperationType { get; set; }

        public string RawUrl { get; set; }

        public string SqlQuery { get; set; }

        public string TableName { get; set; }

        public string UrlReferrer { get; set; }

        public string UserAccount { get; set; }

        public string UserHostAddress { get; set; }
    }
}


namespace WTF.Framework
{
    using System;

    public class LogModuleInfo
    {
        private bool _IsDispose;
        private string _ModuleTypeCode;

        public LogModuleInfo(bool isDispose, string moduleTypeCode)
        {
            this._IsDispose = isDispose;
            this._ModuleTypeCode = moduleTypeCode;
        }

        public static LogModuleInfo CreateApplicationModuleInfo()
        {
            return new LogModuleInfo(LogSectionHelper.IsDispose, "Application");
        }

        public bool IsDispose
        {
            get
            {
                return this._IsDispose;
            }
            set
            {
                this._IsDispose = value;
            }
        }

        public string ModuleTypeCode
        {
            get
            {
                return this._ModuleTypeCode;
            }
            set
            {
                this._ModuleTypeCode = value;
            }
        }
    }
}


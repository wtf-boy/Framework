namespace WTF.Controls
{
    using System;
    using System.Runtime.InteropServices;

    public class PagerEventArgs : EventArgs
    {
        private int _ChangeValue = 0;
        private WTF.Controls.PagerChangeType _PagerChangeType = WTF.Controls.PagerChangeType.PageIndex;

        public PagerEventArgs(int changeValue, WTF.Controls.PagerChangeType pagerChangeType = 0)
        {
            this._ChangeValue = changeValue;
            this._PagerChangeType = pagerChangeType;
        }

        public int ChangeValue
        {
            get
            {
                return this._ChangeValue;
            }
            set
            {
                this._ChangeValue = value;
            }
        }

        public WTF.Controls.PagerChangeType PagerChangeType
        {
            get
            {
                return this._PagerChangeType;
            }
        }
    }
}


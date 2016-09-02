namespace WTF.Power
{
    using System;
    using System.Runtime.CompilerServices;

    public class CoteInfo
    {
        public CoteInfo()
        {
            this.Name = "";
            this.ID = "";
            this.ParentID = "";
        }

        public string ID { get; set; }

        public string Name { get; set; }

        public string ParentID { get; set; }
    }
}


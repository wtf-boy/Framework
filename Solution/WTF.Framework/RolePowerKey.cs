namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class RolePowerKey
    {
        public RolePowerKey()
        {
            this.CoteModuleID = "";
            this.CoteID = "";
            this.IsShare = false;
            this.IsCoteSupper = false;
        }

        public RolePowerKey(string toPowerKey)
        {
            string[] strArray = toPowerKey.Split(new char[] { ':' });
            if (strArray.Length == 1)
            {
                this.ModuleID = toPowerKey;
                this.CoteID = "";
                this.CoteModuleID = "";
                this.IsShare = false;
                this.IsCoteSupper = false;
            }
            else if (strArray.Length == 5)
            {
                this.ModuleID = strArray[0];
                this.CoteModuleID = strArray[1];
                this.CoteID = strArray[2];
                this.IsShare = strArray[3] == "1";
                this.IsCoteSupper = strArray[4] == "1";
            }
            else
            {
                this.ModuleID = strArray[0];
                this.CoteModuleID = strArray[1];
                this.CoteID = strArray[2];
                this.IsShare = strArray[3] == "1";
                this.IsCoteSupper = false;
            }
        }

        public static List<RolePowerKey> ConvertRolePowerKeyValue(IEnumerable<string> PowerKeyValues)
        {
            List<RolePowerKey> source = new List<RolePowerKey>();
            foreach (string str in PowerKeyValues.Distinct<string>().ToList<string>())
            {
                source.Add(new RolePowerKey(str));
            }
            List<RolePowerKey> list2 = (from s in source
                where s.IsCoteSupper
                select s).ToList<RolePowerKey>();
            using (List<RolePowerKey>.Enumerator enumerator2 = list2.GetEnumerator())
            {
                while (enumerator2.MoveNext())
                {
                    Func<RolePowerKey, bool> func = null;
                    Predicate<RolePowerKey> match = null;
                    RolePowerKey objRolePowerKey = enumerator2.Current;
                    if (func == null)
                    {
                        func = s => (s.ModuleID == objRolePowerKey.CoteModuleID) && (s.CoteModuleID == "");
                    }
                    if (source.Any<RolePowerKey>(func))
                    {
                        if (match == null)
                        {
                            match = s => (s.CoteModuleID == objRolePowerKey.CoteModuleID) && !s.IsCoteSupper;
                        }
                        source.RemoveAll(match);
                    }
                }
            }
            return source;
        }

        public static RolePowerKey Create(string moduleID)
        {
            return new RolePowerKey { ModuleID = moduleID };
        }

        public static RolePowerKey Create(string CoteModuleID, string CoteID, string ModuleID, bool isShare = false)
        {
            return new RolePowerKey { ModuleID = ModuleID, CoteModuleID = CoteModuleID, CoteID = CoteID, IsShare = isShare, IsCoteSupper = false };
        }

        public static RolePowerKey Create(string CoteModuleID, string CoteID, string ModuleID, bool isShare, bool IsCoteSupper)
        {
            return new RolePowerKey { ModuleID = ModuleID, CoteModuleID = CoteModuleID, CoteID = CoteID, IsShare = isShare, IsCoteSupper = IsCoteSupper };
        }

        public string CoteID { get; set; }

        public string CoteModuleID { get; set; }

        public bool IsCoteSupper { get; set; }

        public bool IsShare { get; set; }

        public string ModuleID { get; set; }

        public string ToKey
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.CoteModuleID) && string.IsNullOrWhiteSpace(this.CoteID))
                {
                    return this.ModuleID.ToString();
                }
                if (this.IsCoteSupper)
                {
                    return string.Concat(new object[] { this.ModuleID.ToString(), ":", this.CoteModuleID, ":", this.CoteID, ":", this.IsShare ? 1 : 0, ":", this.IsCoteSupper ? 1 : 0 });
                }
                return string.Concat(new object[] { this.ModuleID.ToString(), ":", this.CoteModuleID, ":", this.CoteID, ":", this.IsShare ? 1 : 0 });
            }
        }
    }
}


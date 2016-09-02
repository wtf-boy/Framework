namespace WTF.Power
{
    using System;
    using System.Collections.Generic;

    public class CoteInfoComparer : IEqualityComparer<CoteInfo>
    {
        public bool Equals(CoteInfo x, CoteInfo y)
        {
            return (x.ID == y.ID);
        }

        public int GetHashCode(CoteInfo obj)
        {
            if (obj == null)
            {
                return 0;
            }
            return obj.ToString().GetHashCode();
        }
    }
}


namespace WTF.Framework
{
    using System;
    using System.Runtime.CompilerServices;

    public class TreeData
    {
        public object Data { get; set; }

        public int ParentID { get; set; }

        public int SortIndex { get; set; }

        public int TreeNodeID { get; set; }

        public string TreeNodeName { get; set; }
    }
}


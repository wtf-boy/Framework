using System;
using System.Collections.Generic;

namespace WTF.Solr
{
	[Serializable]
	public class GroupResultInfo<T> where T : class, new()
	{
		public long RecordCount
		{
			get;
			set;
		}

		public int MatchesCount
		{
			get;
			set;
		}

		public object GroupValue
		{
			get;
			set;
		}

		public int GroupValueInt
		{
			get
			{
				return Convert.ToInt32(this.GroupValue);
			}
		}

		public string GroupValueString
		{
			get
			{
				return Convert.ToString(this.GroupValue);
			}
		}

		public IEnumerable<T> Data
		{
			get;
			set;
		}

		public object ExpandData
		{
			get;
			set;
		}

		public GroupResultInfo()
		{
			this.RecordCount = 0L;
			this.MatchesCount = 0;
			this.Data = new List<T>();
		}

		public ExpandT GetExpandData<ExpandT>()
		{
			return (ExpandT)((object)this.ExpandData);
		}
	}
}

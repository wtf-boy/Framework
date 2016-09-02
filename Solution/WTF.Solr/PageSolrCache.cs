using System;
using System.Collections.Generic;
using System.Data;

namespace WTF.Solr
{
	[Serializable]
	public class PageSolrCache
	{
		public long RecordCount
		{
			get;
			set;
		}

		public DataSet Data
		{
			get;
			set;
		}
	}
	[Serializable]
	public class PageSolrCache<T>
	{
		public long RecordCount
		{
			get;
			set;
		}

		public IEnumerable<T> Data
		{
			get;
			set;
		}
	}
}

using System;
using System.Collections.Generic;

namespace WTF.Solr
{
	public class FacetResult<T>
	{
		public IDictionary<string, IList<FacetFieldInfo>> Facet
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

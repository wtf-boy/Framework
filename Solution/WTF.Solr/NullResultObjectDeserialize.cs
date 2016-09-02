using EasyNet.Solr;
using EasyNet.Solr.Commons;
using System;
using System.Collections.Generic;

namespace WTF.Solr
{
	public class NullResultObjectDeserialize : IObjectDeserializer<NullResult>
	{
		public IEnumerable<NullResult> Deserialize(SolrDocumentList result)
		{
			if (result == null)
			{
				yield return null;
			}
			foreach (SolrDocument current in result)
			{
			}
			yield break;
		}
	}
}

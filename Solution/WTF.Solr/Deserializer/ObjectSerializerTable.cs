using EasyNet.Solr;
using EasyNet.Solr.Commons;
using System;
using System.Collections.Generic;

namespace WTF.Solr.Deserializer
{
	public class ObjectSerializerTable : IObjectSerializer<SolrInputDocument>
	{
		public IEnumerable<SolrInputDocument> Serialize(IEnumerable<SolrInputDocument> objs)
		{
			return objs;
		}
	}
}

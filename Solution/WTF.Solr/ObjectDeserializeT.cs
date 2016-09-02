using EasyNet.Solr;
using EasyNet.Solr.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WTF.Solr
{
	public class ObjectDeserializeT<T> : DeserializeHelper<T>, IObjectDeserializer<T> where T : class, new()
	{
		public ObjectDeserializeT(string tableName, string fields = "") : base(tableName, fields)
		{
		}

		public IEnumerable<T> Deserialize(SolrDocumentList result)
		{
			if (result == null)
			{
				yield return default(T);
			}
			foreach (SolrDocument current in result)
			{
				T t = Activator.CreateInstance<T>();
				if (base.Highlight != null && base.Highlight.Count > 0 && base.PrimaryKey != null && current.ContainsKey(base.PrimaryKey.KeyName))
				{
					string key = current[base.PrimaryKey.KeyName].ToString();
					IDictionary<string, IList<string>> dictionary = base.Highlight[key];
					foreach (PropertyInfo current2 in this._PropertyList)
					{
						if (dictionary.ContainsKey(current2.Name))
						{
							current2.SetValue(t, dictionary[current2.Name].FirstOrDefault<string>(), null);
						}
						else if (dictionary.ContainsKey(current2.Name.ToLower()))
						{
							current2.SetValue(t, dictionary[current2.Name.ToLower()].FirstOrDefault<string>(), null);
						}
						else if (current.ContainsKey(current2.Name))
						{
							current2.SetValue(t, base.ConvertValue(current[current2.Name], current2.PropertyType), null);
						}
						else if (current.ContainsKey(current2.Name.ToLower()))
						{
							current2.SetValue(t, base.ConvertValue(current[current2.Name.ToLower()], current2.PropertyType), null);
						}
					}
				}
				else
				{
					foreach (PropertyInfo current2 in this._PropertyList)
					{
						if (current.ContainsKey(current2.Name))
						{
							current2.SetValue(t, base.ConvertValue(current[current2.Name], current2.PropertyType), null);
						}
						else if (current.ContainsKey(current2.Name.ToLower()))
						{
							current2.SetValue(t, base.ConvertValue(current[current2.Name.ToLower()], current2.PropertyType), null);
						}
					}
				}
				yield return t;
			}
			yield break;
		}
	}
}

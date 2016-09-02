using EasyNet.Solr;
using EasyNet.Solr.Commons;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace WTF.Solr
{
	public class ObjectDeserializeTable<T> : DeserializeHelper<T>, IObjectDeserializer<DataRow> where T : class, new()
	{
		public ObjectDeserializeTable(string tableName, string fields = "") : base(tableName, fields)
		{
		}

		public IEnumerable<DataRow> Deserialize(SolrDocumentList result)
		{
			if (result == null)
			{
				yield return null;
			}
			string[] source = this._Fields.ToLower().Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (SolrDocument current in result)
			{
				DataRow dataRow = base.CreateDataTableNewRow();
				if (base.Highlight != null && base.Highlight.Count > 0 && base.PrimaryKey != null && current.ContainsKey(base.PrimaryKey.KeyName))
				{
					string key = current[base.PrimaryKey.KeyName].ToString();
					IDictionary<string, IList<string>> dictionary = base.Highlight[key];
					foreach (PropertyInfo current2 in this._PropertyList)
					{
						if (dictionary.ContainsKey(current2.Name))
						{
							dataRow[current2.Name] = dictionary[current2.Name].FirstOrDefault<string>();
						}
						else if (dictionary.ContainsKey(current2.Name.ToLower()))
						{
							dataRow[current2.Name] = dictionary[current2.Name.ToLower()].FirstOrDefault<string>();
						}
						else if (current.ContainsKey(current2.Name))
						{
							dataRow[current2.Name] = base.ConvertValue(current[current2.Name], current2.PropertyType);
						}
						else if (current.ContainsKey(current2.Name.ToLower()))
						{
							dataRow[current2.Name] = base.ConvertValue(current[current2.Name.ToLower()], current2.PropertyType);
						}
					}
					if (!string.IsNullOrWhiteSpace(this._Fields))
					{
						if (source.Contains("score"))
						{
							if (current.ContainsKey("score"))
							{
								dataRow["score"] = current["score"];
							}
						}
						if (source.Contains("_version_"))
						{
							if (current.ContainsKey("_version_"))
							{
								dataRow["_version_"] = current["_version_"];
							}
						}
					}
				}
				else
				{
					foreach (PropertyInfo current2 in this._PropertyList)
					{
						if (current.ContainsKey(current2.Name))
						{
							dataRow[current2.Name] = base.ConvertValue(current[current2.Name], current2.PropertyType);
						}
						else if (current.ContainsKey(current2.Name.ToLower()))
						{
							dataRow[current2.Name] = base.ConvertValue(current[current2.Name.ToLower()], current2.PropertyType);
						}
					}
					if (!string.IsNullOrWhiteSpace(this._Fields))
					{
						if (source.Contains("score"))
						{
							if (current.ContainsKey("score"))
							{
								dataRow["score"] = current["score"];
							}
						}
						if (source.Contains("_version_"))
						{
							if (current.ContainsKey("_version_"))
							{
								dataRow["_version_"] = current["_version_"];
							}
						}
					}
				}
				yield return dataRow;
			}
			yield break;
		}
	}
}

namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    public static class XmlHelper
    {
        public static XmlElement AppendChildElement(this XmlNode node, string name)
        {
            XmlElement newChild = ((node is XmlDocument) ? ((XmlDocument) node) : node.OwnerDocument).CreateElement(name, node.NamespaceURI);
            node.AppendChild(newChild);
            return newChild;
        }

        public static XmlDocument CreateTreeXmlDocument(this IEnumerable<TreeData> objTreeDataList, int rootParentID, string navigateUrl = "")
        {
            return objTreeDataList.CreateTreeXmlDocument(rootParentID, navigateUrl, null);
        }

        public static XmlDocument CreateTreeXmlDocument(this IEnumerable<TreeData> objTreeDataList, int rootParentID, string navigateUrl, Action<TreeData, XmlElement> objAction)
        {
            return objTreeDataList.CreateTreeXmlDocument(rootParentID, navigateUrl, "", "", objAction);
        }

        public static XmlDocument CreateTreeXmlDocument(this IEnumerable<TreeData> objTreeDataList, int rootParentID, string navigateUrl, string IDParName, string ParentIDParName, Action<TreeData, XmlElement> objAction)
        {
            List<TreeData> source = (from s in objTreeDataList
                where s.ParentID == rootParentID
                select s).ToList<TreeData>();
            TreeData data = null;
            if (source.Count > 1)
            {
                data = new TreeData {
                    ParentID = -1,
                    TreeNodeName = "节点管理",
                    TreeNodeID = rootParentID
                };
            }
            else
            {
                data = source.FirstOrDefault<TreeData>();
            }
            if (data == null)
            {
                throw new ArgumentNullException("找不到相应的:rootParentID=" + rootParentID + "子节点");
            }
            XmlDocument xmlDocSource = new XmlDocument();
            string str = ((navigateUrl.IndexOf("?") >= 0) ? (navigateUrl + "&") : (navigateUrl + "?")) + (IDParName.IsNull() ? "TransferID" : IDParName) + "={0}&" + (ParentIDParName.IsNull() ? "TransferTwoID" : ParentIDParName) + "={1}&IsChild={2}";
            XmlElement element = xmlDocSource.CreateElement("TreeNodeMember");
            if (objAction != null)
            {
                objAction(data, element);
            }
            else
            {
                element.SetAttribute("TreeNodeID", data.TreeNodeID.ToString());
                element.SetAttribute("TreeNodeName", data.TreeNodeName);
                if (!string.IsNullOrWhiteSpace(str))
                {
                    element.SetAttribute("NavigateUrl", string.Format(str, data.TreeNodeID.ToString(), data.ParentID, 1).EncryptModuleQuery());
                }
            }
            CreateTreeXmlElement(xmlDocSource, data.TreeNodeID, element, objTreeDataList, str, objAction);
            xmlDocSource.AppendChild(element);
            return xmlDocSource;
        }

        private static void CreateTreeXmlElement(XmlDocument xmlDocSource, int ParentID, XmlElement objXmlElement, IEnumerable<TreeData> objTreeDataList, string urlformat, Action<TreeData, XmlElement> objAction)
        {
            using (IEnumerator<TreeData> enumerator = (from s in objTreeDataList
                where s.ParentID == ParentID
                orderby s.SortIndex
                select s).GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Func<TreeData, bool> predicate = null;
                    TreeData objTreeData = enumerator.Current;
                    XmlElement element = xmlDocSource.CreateElement("TreeNodeMember");
                    if (objAction != null)
                    {
                        objAction(objTreeData, element);
                        objXmlElement.AppendChild(element);
                    }
                    else
                    {
                        if (predicate == null)
                        {
                            predicate = s => s.ParentID == objTreeData.TreeNodeID;
                        }
                        int num = objTreeDataList.Any<TreeData>(predicate) ? 1 : 0;
                        element.SetAttribute("TreeNodeID", objTreeData.TreeNodeID.ToString());
                        element.SetAttribute("TreeNodeName", objTreeData.TreeNodeName);
                        if (!string.IsNullOrWhiteSpace(urlformat))
                        {
                            element.SetAttribute("NavigateUrl", string.Format(urlformat, objTreeData.TreeNodeID.ToString(), objTreeData.ParentID, num).EncryptModuleQuery());
                        }
                        objXmlElement.AppendChild(element);
                    }
                    CreateTreeXmlElement(xmlDocSource, objTreeData.TreeNodeID, element, objTreeDataList, urlformat, objAction);
                }
            }
        }

        public static void DeleteNode(this string path, string node, string attribute = "")
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);
                XmlNode oldChild = document.SelectSingleNode(node);
                XmlElement element = (XmlElement) oldChild;
                if (string.IsNullOrWhiteSpace(attribute))
                {
                    oldChild.ParentNode.RemoveChild(oldChild);
                }
                else
                {
                    element.RemoveAttribute(attribute);
                }
                document.Save(path);
            }
            catch
            {
            }
        }

        public static void InsertNode(string path, string node, string element, string attribute, string value)
        {
            try
            {
                XmlElement element2;
                XmlDocument document = new XmlDocument();
                document.Load(path);
                XmlNode node2 = document.SelectSingleNode(node);
                if (string.IsNullOrWhiteSpace(element))
                {
                    if (!string.IsNullOrWhiteSpace(attribute))
                    {
                        element2 = (XmlElement) node2;
                        element2.SetAttribute(attribute, value);
                    }
                }
                else
                {
                    element2 = document.CreateElement(element);
                    if (string.IsNullOrWhiteSpace(attribute))
                    {
                        element2.InnerText = value;
                    }
                    else
                    {
                        element2.SetAttribute(attribute, value);
                    }
                    node2.AppendChild(element2);
                }
                document.Save(path);
            }
            catch
            {
            }
        }

        public static bool IsXmlNodeNull(this XmlNode canShu)
        {
            try
            {
                return (null == canShu);
            }
            catch
            {
                return true;
            }
        }

        public static void LoadLocalCacheFile(this XmlDocument objXmlDocument, string filePath, Encoding objEncoding = null)
        {
            string str = filePath.ReadLocalCacheFile(objEncoding);
            if (!string.IsNullOrWhiteSpace(str))
            {
                objXmlDocument.LoadXml(str);
            }
        }

        public static XmlDocument LoadLocalCacheFileXml(this string filePath, Encoding objEncoding = null)
        {
            XmlDocument document = new XmlDocument();
            string str = filePath.ReadLocalCacheFile(objEncoding);
            if (!string.IsNullOrWhiteSpace(str))
            {
                document.LoadXml(str);
            }
            else
            {
                return null;
            }
            return document;
        }

        public static string ObjectToXmlString<T>(this T obj) where T: class
        {
            string str;
            if (obj == null)
            {
                return string.Empty;
            }
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize((Stream) stream, obj);
                stream.Seek(0L, SeekOrigin.Begin);
                byte[] buffer = new byte[stream.Length];
                int count = stream.Read(buffer, 0, buffer.Length);
                str = Encoding.UTF8.GetString(buffer, 0, count);
            }
            return str.Replace("<?xml version=\"1.0\"?>", "");
        }

        public static string ReadAttribute(this XmlNode xmlNode, string attrName, bool required = false)
        {
            return xmlNode.ReadAttribute(attrName, "", required);
        }

        public static string ReadAttribute(this XmlNode xmlNode, string attrName, string defalutValue, bool required = false)
        {
            XmlAttribute attribute = xmlNode.Attributes[attrName];
            if (required)
            {
                if (attribute == null)
                {
                    throw new ApplicationException(string.Format("属性{0}不存在", attrName));
                }
                if (string.IsNullOrWhiteSpace(attribute.Value))
                {
                    throw new ApplicationException(string.Format("属性{0}值不能空值", attrName));
                }
            }
            string str = "";
            if (attribute != null)
            {
                str = attribute.Value;
            }
            if (string.IsNullOrWhiteSpace(str))
            {
                return defalutValue;
            }
            return str;
        }

        public static bool ReadAttributeBoolean(this XmlNode xmlNode, string attrName, bool defalutValue)
        {
            string str = xmlNode.ReadAttribute(attrName, false);
            if (string.IsNullOrWhiteSpace(str))
            {
                return defalutValue;
            }
            if (str.Length == 1)
            {
                return (str == "1");
            }
            return Convert.ToBoolean(str);
        }

        public static DateTime ReadAttributeDateTime(this XmlNode xmlNode, string attrName, DateTime defalutValue)
        {
            string str = xmlNode.ReadAttribute(attrName, false);
            if (string.IsNullOrWhiteSpace(str))
            {
                return defalutValue;
            }
            return Convert.ToDateTime(str);
        }

        public static int ReadAttributeInt(this XmlNode xmlNode, string attrName, int defalutValue = 0)
        {
            string str = xmlNode.ReadAttribute(attrName, false);
            if (string.IsNullOrWhiteSpace(str))
            {
                return defalutValue;
            }
            return int.Parse(str);
        }

        public static long ReadAttributeInt64(this XmlNode xmlNode, string attrName, long defalutValue = 0L)
        {
            string str = xmlNode.ReadAttribute(attrName, false);
            if (string.IsNullOrWhiteSpace(str))
            {
                return defalutValue;
            }
            return long.Parse(str);
        }

        public static string ReadNode(string path, string node, string attribute = "")
        {
            string str = "";
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);
                XmlNode node2 = document.SelectSingleNode(node);
                str = string.IsNullOrWhiteSpace(attribute) ? node2.InnerText : node2.Attributes[attribute].Value;
            }
            catch
            {
            }
            return str;
        }

        [Obsolete("请使用ReadAttributeBoolean调用")]
        public static bool ReadXmlAttributeBoolean(this XmlNode node, string attrName, bool defaultVal)
        {
            return node.ReadAttributeBoolean(attrName, defaultVal);
        }

        [Obsolete("请使用ReadAttributeDateTime调用")]
        public static DateTime ReadXmlAttributeDateTime(this XmlNode node, string attrName, DateTime defaultVal)
        {
            return node.ReadAttributeDateTime(attrName, defaultVal);
        }

        public static T ReadXmlAttributeEnum<T>(this XmlNode node, string attrName, T defaultVal)
        {
            int num = node.ReadAttributeInt(attrName, -2147483648);
            if (num == -2147483648)
            {
                return defaultVal;
            }
            try
            {
                return (T) Enum.ToObject(typeof(T), num);
            }
            catch
            {
                return defaultVal;
            }
        }

        [Obsolete("请使用ReadAttributeInt调用")]
        public static int ReadXmlAttributeInt32(this XmlNode node, string attrName, int defaultVal)
        {
            return node.ReadAttributeInt(attrName, defaultVal);
        }

        [Obsolete("请使用ReadAttributeInt64调用")]
        public static long ReadXmlAttributeInt64(this XmlNode node, string attrName, long defaultVal)
        {
            return node.ReadAttributeInt64(attrName, defaultVal);
        }

        [Obsolete("请使用ReadAttribute调用")]
        public static string ReadXmlAttributeString(this XmlNode node, string attrName)
        {
            return node.ReadXmlAttributeString(attrName, false);
        }

        [Obsolete("请使用ReadAttribute调用")]
        public static string ReadXmlAttributeString(this XmlNode node, string attrName, bool required)
        {
            return node.ReadAttribute(attrName, "", required);
        }

        public static void SetNodeAttribute(this XmlNode node, string attrName, string value)
        {
            XmlDocument document = (node is XmlDocument) ? ((XmlDocument) node) : node.OwnerDocument;
            XmlAttribute attribute = node.Attributes[attrName];
            if (attribute == null)
            {
                attribute = node.Attributes.Append(document.CreateAttribute(attrName));
            }
            attribute.Value = value;
        }

        public static void SetNodeAttributeBool(this XmlNode node, string attrName, bool value)
        {
            node.SetNodeAttribute(attrName, value ? "1" : "0");
        }

        public static void SetNodeAttributeInt32(this XmlNode node, string attrName, int value)
        {
            node.SetNodeAttribute(attrName, value.ToString());
        }

        public static string TableToXmlString(DataTable table)
        {
            table.WriteXmlSchema(table.TableName + ".xsd");
            table.WriteXml("xx.xml");
            using (MemoryStream stream = new MemoryStream())
            {
                table.WriteXml(stream, XmlWriteMode.WriteSchema, false);
                stream.Seek(0L, SeekOrigin.Begin);
                byte[] buffer = new byte[stream.Length];
                int count = stream.Read(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer, 0, count);
            }
        }

        public static void UpdateNode(this string path, string value, string node, string attribute = "")
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);
                XmlElement element = (XmlElement) document.SelectSingleNode(node);
                if (string.IsNullOrWhiteSpace(attribute))
                {
                    element.InnerText = value;
                }
                else
                {
                    element.SetAttribute(attribute, value);
                }
                document.Save(path);
            }
            catch
            {
            }
        }

        public static bool Validate(this string documentBody, string xsdMarkup, out string error)
        {
            return XDocument.Parse(documentBody).Validate(xsdMarkup, out error);
        }

        public static bool Validate(this XDocument objXDocument, string xsdMarkup, out string error)
        {
            XmlSchemaSet objXmlSchemaSet = new XmlSchemaSet();
            objXmlSchemaSet.Add("", XmlReader.Create(new StringReader(xsdMarkup)));
            return objXDocument.Validate(objXmlSchemaSet, out error);
        }

        public static bool Validate(this XDocument objXDocument, XmlSchemaSet objXmlSchemaSet, out string error)
        {
            bool result = true;
            string resulterror = "";
            objXDocument.Validate(objXmlSchemaSet, delegate (object obj, ValidationEventArgs e) {
                resulterror = resulterror + e.Message;
                result = false;
            });
            error = resulterror;
            return result;
        }

        public static T XmlStringToObject<T>(this string xmlString)
        {
            xmlString = "<?xml version=\"1.0\"?>\r\n" + xmlString;
            using (MemoryStream stream = new MemoryStream())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(xmlString);
                stream.Write(bytes, 0, bytes.Length);
                stream.Seek(0L, SeekOrigin.Begin);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T) serializer.Deserialize(stream);
            }
        }

        public static DataTable XmlStringToTable(string xmlString)
        {
            DataTable table = new DataTable();
            byte[] bytes = Encoding.UTF8.GetBytes(xmlString);
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Seek(0L, SeekOrigin.Begin);
                table.ReadXml(stream);
            }
            return table;
        }
    }
}


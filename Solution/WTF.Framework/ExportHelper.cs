namespace WTF.Framework
{
    using System;
    using System.Data;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Xml;
    using System.Xml.XPath;
    using System.Xml.Xsl;

    public static class ExportHelper
    {
        private static void CreateStylesheet(XmlTextWriter writer, string[] headers, string[] fields, ExportFormat exportFormat)
        {
            int num;
            string ns = "http://www.w3.org/1999/XSL/Transform";
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("xsl", "stylesheet", ns);
            writer.WriteAttributeString("version", "1.0");
            writer.WriteStartElement("xsl:output");
            writer.WriteAttributeString("method", "text");
            writer.WriteAttributeString("version", "4.0");
            writer.WriteEndElement();
            writer.WriteStartElement("xsl:template");
            writer.WriteAttributeString("match", "/");
            for (num = 0; num < headers.Length; num++)
            {
                writer.WriteString("\"");
                writer.WriteStartElement("xsl:value-of");
                writer.WriteAttributeString("select", "'" + headers[num] + "'");
                writer.WriteEndElement();
                writer.WriteString("\"");
                if (num != (fields.Length - 1))
                {
                    writer.WriteString((exportFormat == ExportFormat.CSV) ? "," : "\t");
                }
            }
            writer.WriteStartElement("xsl:for-each");
            writer.WriteAttributeString("select", "Export/Values");
            writer.WriteString("\r\n");
            for (num = 0; num < fields.Length; num++)
            {
                writer.WriteString("\"");
                writer.WriteStartElement("xsl:value-of");
                writer.WriteAttributeString("select", fields[num]);
                writer.WriteEndElement();
                writer.WriteString("\"");
                if (num != (fields.Length - 1))
                {
                    writer.WriteString((exportFormat == ExportFormat.CSV) ? "," : "\t");
                }
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        public static void Export(this DataTable dt, ExportFormat exportFormat, string fileName, Encoding encoding)
        {
            DataSet ds = new DataSet("Export");
            DataTable table = dt.Copy();
            table.TableName = "Values";
            ds.Tables.Add(table);
            string[] headers = new string[table.Columns.Count];
            string[] fields = new string[table.Columns.Count];
            for (int i = 0; i < table.Columns.Count; i++)
            {
                headers[i] = table.Columns[i].ColumnName;
                fields[i] = table.Columns[i].ColumnName.ReplaceSpecialChars();
            }
            ds.Export(headers, fields, exportFormat, fileName, encoding);
        }

        public static void Export(this DataTable dt, int[] columnIndexList, ExportFormat exportFormat, string fileName, Encoding encoding)
        {
            DataSet ds = new DataSet("Export");
            DataTable table = dt.Copy();
            table.TableName = "Values";
            ds.Tables.Add(table);
            string[] headers = new string[columnIndexList.Length];
            string[] fields = new string[columnIndexList.Length];
            for (int i = 0; i < columnIndexList.Length; i++)
            {
                headers[i] = table.Columns[columnIndexList[i]].ColumnName;
                fields[i] = table.Columns[columnIndexList[i]].ColumnName.ReplaceSpecialChars();
            }
            ds.Export(headers, fields, exportFormat, fileName, encoding);
        }

        private static void Export(this DataSet ds, string[] headers, string[] fields, ExportFormat exportFormat, string fileName, Encoding encoding)
        {
            SysVariable.CurrentContext.Response.Clear();
            SysVariable.CurrentContext.Response.Buffer = true;
            SysVariable.CurrentContext.Response.ContentType = string.Format("text/{0}", exportFormat.ToString().ToLower());
            SysVariable.CurrentContext.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.{1}", fileName, exportFormat.ToString().ToLower()));
            SysVariable.CurrentContext.Response.ContentEncoding = encoding;
            MemoryStream w = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(w, encoding);
            CreateStylesheet(writer, headers, fields, exportFormat);
            writer.Flush();
            w.Seek(0L, SeekOrigin.Begin);
            XmlDataDocument document = new XmlDataDocument(ds);
            XslCompiledTransform transform = new XslCompiledTransform();
            transform.Load(new XmlTextReader(w));
            StringWriter writer2 = new StringWriter();
            transform.Transform((IXPathNavigable) document, null, (TextWriter) writer2);
            SysVariable.CurrentContext.Response.Write(writer2.ToString());
            writer2.Close();
            writer.Close();
            w.Close();
            SysVariable.CurrentContext.Response.End();
        }

        public static void Export(this DataTable dt, int[] columnIndexList, string[] headers, ExportFormat exportFormat, string fileName, Encoding encoding)
        {
            DataSet ds = new DataSet("Export");
            DataTable table = dt.Copy();
            table.TableName = "Values";
            ds.Tables.Add(table);
            string[] fields = new string[columnIndexList.Length];
            for (int i = 0; i < columnIndexList.Length; i++)
            {
                fields[i] = table.Columns[columnIndexList[i]].ColumnName.ReplaceSpecialChars();
            }
            ds.Export(headers, fields, exportFormat, fileName, encoding);
        }

        public enum ExportFormat
        {
            CSV,
            DOC,
            TXT
        }
    }
}


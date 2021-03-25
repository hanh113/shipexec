using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace ExportExcel
{
	public class CreateExcel
	{
		private const string SPREADSHEETSTRING = "urn:schemas-microsoft-com:office:spreadsheet";

		private const string OFFICESTRING = "urn:schemas-microsoft-com:office:office";

		private const string EXCELSTRING = "urn:schemas-microsoft-com:office:excel";

		private string _FilePath;

		private string _filter;

		private string _Sort;

		private string _Title;

		public List<string> IgnoreColumns = null;

		public string FilePath
		{
			get
			{
				return _FilePath;
			}
			set
			{
				_FilePath = value;
			}
		}

		public string Filter
		{
			get
			{
				return _filter;
			}
			set
			{
				_filter = value;
			}
		}

		public string Sort
		{
			get
			{
				return _Sort;
			}
			set
			{
				_Sort = value;
			}
		}

		public string Title
		{
			get
			{
				return _Title;
			}
			set
			{
				_Title = value;
			}
		}

		public CreateExcel()
			: this(string.Empty, string.Empty)
		{
		}

		public CreateExcel(string FilePath)
			: this(FilePath, string.Empty)
		{
		}

		public CreateExcel(string FilePath, string title)
		{
			this.FilePath = FilePath;
			Title = title;
			_filter = string.Empty;
			_Sort = string.Empty;
		}

		public void ExportToExcel(DataGridView GridView)
		{
			int num = 0;
			string[] array = new string[GridView.Columns.Count];
			for (int i = 0; i <= GridView.Columns.Count - 1; i++)
			{
				int displayIndex = GridView.Columns[i].DisplayIndex;
				if (GridView.Columns[i].Visible)
				{
					num++;
					array[displayIndex] = GridView.Columns[i].Name;
				}
			}
			Array.Resize(ref array, num);
			DataSet dataSet = new DataSet();
			dataSet.Tables.Add();
			for (int i = 0; i <= array.Length - 1; i++)
			{
				dataSet.Tables[0].Columns.Add(GridView.Columns[array[i].ToString()].HeaderText);
			}
			for (int i = 0; i <= GridView.Rows.Count - 1; i++)
			{
				dataSet.Tables[0].Rows.Add();
				int num2 = -1;
				for (int j = 0; j <= array.Length - 1; j++)
				{
					num2++;
					dataSet.Tables[0].Rows[i][num2] = GridView.Rows[i].Cells[array[j].ToString()].Value;
				}
			}
			ExportToExcel(dataSet);
		}

		public void ExportToExcel(DataTable table)
		{
			DataSet dataSet = new DataSet();
			dataSet.Tables.Add(table.Copy());
			ExportToExcel(dataSet);
		}

		public void ExportToExcel(DataSet ds)
		{
			XmlDocument xmlDocument = CreateFile();
			XmlNode nodeWorkbook = xmlDocument.SelectSingleNode("Workbook");
			for (int i = 0; i < ds.Tables.Count; i++)
			{
				DataTable dataTable = new DataTable();
				dataTable = ds.Tables[i].Copy();
				Hashtable dDTable = GetDDTable(dataTable);
				DataRow[] array = dataTable.Select(Filter, Sort);
				int num = (Title.Length > 0) ? 65534 : 65535;
				XmlNode xmlNode = CreateWorkSheet(dataTable.TableName, nodeWorkbook, dataTable, dDTable);
				for (int j = 0; j < array.Length; j++)
				{
					if (j != 0 && j % num == 0)
					{
						xmlNode = CreateWorkSheet($"{dataTable.TableName}{j / num}", nodeWorkbook, dataTable, dDTable);
					}
					XmlNode xmlNode2 = xmlDocument.CreateElement("Row");
					xmlNode.AppendChild(xmlNode2);
					foreach (DataColumn column in dataTable.Columns)
					{
						if (IgnoreColumns == null || !IgnoreColumns.Contains(column.ColumnName))
						{
							ToExcel(xmlNode2, array[j][column], column.DataType);
						}
					}
				}
			}
			xmlDocument.Save(FilePath);
		}

		private Hashtable GetDDTable(DataTable table)
		{
			Hashtable hashtable = new Hashtable();
			foreach (DataColumn column in table.Columns)
			{
				hashtable.Add(column.ColumnName, column.ColumnName);
			}
			return hashtable;
		}

		private void ToExcel(XmlNode nodeRow, object value, Type type)
		{
			XmlDocument ownerDocument = nodeRow.OwnerDocument;
			XmlNode xmlNode = ownerDocument.CreateElement("Cell");
			nodeRow.AppendChild(xmlNode);
			XmlNode xmlNode2 = ownerDocument.CreateElement("Data");
			XmlAttribute xmlAttribute = ownerDocument.CreateAttribute("ss", "Type", "urn:schemas-microsoft-com:office:spreadsheet");
			if (type == typeof(uint) || type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong) || type == typeof(int) || type == typeof(short) || type == typeof(int) || type == typeof(long) || type == typeof(float) || type == typeof(double) || type == typeof(decimal))
			{
				xmlAttribute.Value = "Number";
				if (value == DBNull.Value)
				{
					xmlNode2.InnerText = "0";
				}
				else
				{
					xmlNode2.InnerText = value.ToString();
				}
			}
			else
			{
				xmlAttribute.Value = "String";
				xmlNode2.InnerText = value.ToString().Replace(">", "&gt").Replace("<", "&lt");
			}
			xmlNode2.Attributes.Append(xmlAttribute);
			xmlNode.AppendChild(xmlNode2);
		}

		private XmlNode CreateWorkSheet(string sheetName, XmlNode nodeWorkbook, DataTable table, Hashtable tableDD)
		{
			XmlDocument ownerDocument = nodeWorkbook.OwnerDocument;
			XmlNode xmlNode = ownerDocument.CreateElement("Worksheet");
			XmlAttribute xmlAttribute = ownerDocument.CreateAttribute("ss", "Name", "urn:schemas-microsoft-com:office:spreadsheet");
			xmlAttribute.Value = sheetName;
			xmlNode.Attributes.Append(xmlAttribute);
			XmlAttribute xmlAttribute2 = ownerDocument.CreateAttribute("xmlns");
			xmlAttribute2.Value = "urn:schemas-microsoft-com:office:spreadsheet";
			xmlNode.Attributes.Append(xmlAttribute2);
			nodeWorkbook.AppendChild(xmlNode);
			XmlNode xmlNode2 = ownerDocument.CreateElement("Table");
			xmlNode.AppendChild(xmlNode2);
			if (Title.Length > 0)
			{
				XmlNode xmlNode3 = ownerDocument.CreateElement("Row");
				xmlNode2.AppendChild(xmlNode3);
				XmlNode xmlNode4 = ownerDocument.CreateElement("Cell");
				xmlNode3.AppendChild(xmlNode4);
				if (table.Columns.Count > 1)
				{
					XmlAttribute xmlAttribute3 = ownerDocument.CreateAttribute("ss", "MergeAcross", "urn:schemas-microsoft-com:office:spreadsheet");
					xmlAttribute3.Value = (table.Columns.Count - 1).ToString();
					xmlNode4.Attributes.Append(xmlAttribute3);
				}
				XmlAttribute xmlAttribute4 = ownerDocument.CreateAttribute("ss", "StyleID", "urn:schemas-microsoft-com:office:spreadsheet");
				xmlAttribute4.Value = "title";
				xmlNode4.Attributes.Append(xmlAttribute4);
				XmlNode xmlNode5 = ownerDocument.CreateElement("Data");
				XmlAttribute xmlAttribute5 = ownerDocument.CreateAttribute("ss", "Type", "urn:schemas-microsoft-com:office:spreadsheet");
				xmlAttribute5.Value = "String";
				xmlNode5.InnerText = Title;
				xmlNode5.Attributes.Append(xmlAttribute5);
				xmlNode4.AppendChild(xmlNode5);
			}
			XmlNode xmlNode6 = ownerDocument.CreateElement("Row");
			xmlNode2.AppendChild(xmlNode6);
			foreach (DataColumn column in table.Columns)
			{
				if (IgnoreColumns == null || !IgnoreColumns.Contains(column.ColumnName))
				{
					ToExcel(xmlNode6, tableDD[column.ColumnName], typeof(string));
				}
			}
			return xmlNode2;
		}

		private XmlDocument CreateFile()
		{
			string directoryName = Path.GetDirectoryName(FilePath);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", null, null));
			XmlNode xmlNode = xmlDocument.CreateElement("Workbook");
			XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("xmlns");
			xmlAttribute.Value = "urn:schemas-microsoft-com:office:spreadsheet";
			XmlAttribute xmlAttribute2 = xmlDocument.CreateAttribute("xmlns:o");
			xmlAttribute2.Value = "urn:schemas-microsoft-com:office:office";
			XmlAttribute xmlAttribute3 = xmlDocument.CreateAttribute("xmlns:x");
			xmlAttribute3.Value = "urn:schemas-microsoft-com:office:excel";
			XmlAttribute xmlAttribute4 = xmlDocument.CreateAttribute("xmlns:ss");
			xmlAttribute4.Value = "urn:schemas-microsoft-com:office:spreadsheet";
			xmlNode.Attributes.Append(xmlAttribute);
			xmlNode.Attributes.Append(xmlAttribute2);
			xmlNode.Attributes.Append(xmlAttribute3);
			xmlNode.Attributes.Append(xmlAttribute4);
			xmlDocument.AppendChild(xmlNode);
			XmlNode xmlNode2 = xmlDocument.CreateElement("Styles");
			xmlNode.AppendChild(xmlNode2);
			XmlNode xmlNode3 = xmlDocument.CreateElement("Style");
			XmlAttribute xmlAttribute5 = xmlDocument.CreateAttribute("ss", "ID", "urn:schemas-microsoft-com:office:spreadsheet");
			xmlAttribute5.Value = "title";
			xmlNode3.Attributes.Append(xmlAttribute5);
			xmlNode2.AppendChild(xmlNode3);
			XmlElement xmlElement = xmlDocument.CreateElement("Alignment");
			XmlAttribute xmlAttribute6 = xmlDocument.CreateAttribute("ss", "Horizontal", "urn:schemas-microsoft-com:office:spreadsheet");
			xmlAttribute6.Value = "Center";
			xmlElement.Attributes.Append(xmlAttribute6);
			xmlNode3.AppendChild(xmlElement);
			return xmlDocument;
		}
	}
}

using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace ClientUtilsDll
{
	internal class Localization
	{
		private static void GetDropDownItem(ToolStripItemCollection menuStripItem, string fileName, string path, bool changeFont)
		{
			int num = 0;
			foreach (object item in menuStripItem)
			{
				if (changeFont)
				{
					menuStripItem[num].Font = ClientUtils.fModuleFont;
				}
				string name = item.GetType().Name;
				if (name != null)
				{
					if (name == "ToolStripComboBox")
					{
						ToolStripComboBox toolStripComboBox = (ToolStripComboBox)item;
						toolStripComboBox.Text = GetLanguage(toolStripComboBox.Text, fileName, path);
						for (int i = 0; i <= toolStripComboBox.Items.Count - 1; i++)
						{
							toolStripComboBox.Items[i] = GetLanguage(toolStripComboBox.Items[i].ToString(), fileName, path);
						}
					}
					else if (name == "DropDownButton")
					{
						GetDropDownItem(((ToolStripDropDownButton)item).DropDownItems, fileName, path, changeFont);
					}
					else if (name == "ToolStripMenuItem")
					{
						ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)item;
						toolStripMenuItem.Text = GetLanguage(toolStripMenuItem.Text, fileName, path);
						GetDropDownItem(toolStripMenuItem.DropDownItems, fileName, path, changeFont);
					}
				}
				num++;
			}
		}

		public static string GetLanguage(string message, string fileName, string path)
		{
			string text = message;
			object[] args = new object[4]
			{
				path,
				Path.DirectorySeparatorChar,
				fileName,
				".xml"
			};
			if (File.Exists(string.Concat(args)))
			{
				try
				{
					XmlDocument xmlDocument = new XmlDocument();
					args = new object[4]
					{
						path,
						Path.DirectorySeparatorChar,
						fileName,
						".xml"
					};
					xmlDocument.Load(string.Concat(args));
					string xpath;
					if (message.IndexOf("'") != -1)
					{
						string[] values = new string[5]
						{
							"//",
							ClientUtils.cultureName,
							"/Message[@Name=\"",
							message,
							"\"]"
						};
						xpath = string.Concat(values);
					}
					else
					{
						string[] values = new string[5]
						{
							"//",
							ClientUtils.cultureName,
							"/Message[@Name='",
							message,
							"']"
						};
						xpath = string.Concat(values);
					}
					XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
					if (xmlNode != null)
					{
						text = xmlNode.Attributes["Value"].Value;
						if (string.IsNullOrEmpty(text))
						{
							text = message;
						}
					}
					return text;
				}
				catch
				{
					return text;
				}
			}
			return text;
		}

		public static string GetOrderField(string fileName, string moduleName)
		{
			string result = "";
			object[] args = new object[6]
			{
				Application.StartupPath,
				Path.DirectorySeparatorChar,
				moduleName,
				Path.DirectorySeparatorChar,
				fileName,
				".xml"
			};
			fileName = string.Concat(args);
			if (File.Exists(fileName))
			{
				DataSet dataSet = new DataSet();
				dataSet.ReadXml(fileName);
				try
				{
					Convert.ToString(dataSet.Tables["Order"].Rows[0]["Field"]);
				}
				catch
				{
				}
			}
			return result;
		}

		public static string GetValue(string message, string moduleName, string fileName, out bool visible)
		{
			string result = message;
			visible = true;
			object[] args = new object[6]
			{
				Application.StartupPath,
				Path.DirectorySeparatorChar,
				moduleName,
				Path.DirectorySeparatorChar,
				fileName,
				".xml"
			};
			fileName = string.Concat(args);
			if (File.Exists(fileName))
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(fileName);
				string xpath;
				if (message.IndexOf("'") != -1)
				{
					string[] values = new string[5]
					{
						"//",
						ClientUtils.cultureName,
						"/Field[@Name=\"",
						message,
						"\"]"
					};
					xpath = string.Concat(values);
				}
				else
				{
					string[] values = new string[5]
					{
						"//",
						ClientUtils.cultureName,
						"/Field[@Name='",
						message,
						"']"
					};
					xpath = string.Concat(values);
				}
				XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
				if (xmlNode != null)
				{
					result = xmlNode.Attributes["Value"].Value;
					if (xmlNode.Attributes["Visible"].Value == "0")
					{
						visible = false;
					}
				}
			}
			return result;
		}

		private static void SetDropDownItemFont(ToolStripItemCollection menuStripItem)
		{
			int num = 0;
			foreach (object item in menuStripItem)
			{
				menuStripItem[num].Font = ClientUtils.fModuleFont;
				string name = item.GetType().Name;
				if (name != null)
				{
					if (name == "DropDownButton")
					{
						SetDropDownItemFont(((ToolStripDropDownButton)item).DropDownItems);
					}
					else if (name == "ToolStripMenuItem")
					{
						SetDropDownItemFont(((ToolStripMenuItem)item).DropDownItems);
					}
				}
				num++;
			}
		}

		public static void SetFont(Control ctrl)
		{
			foreach (Control control in ctrl.Controls)
			{
				string name = control.GetType().Name;
				control.Font = ClientUtils.fModuleFont;
				string text = name;
				if (text != null)
				{
					if (text == "BindingNavigator")
					{
						BindingNavigator bindingNavigator = (BindingNavigator)control;
						for (int i = 0; i <= bindingNavigator.Items.Count - 1; i++)
						{
							text = bindingNavigator.Items[i].GetType().Name;
							if (text != null)
							{
								if (text == "ToolStripDropDownButton")
								{
									SetDropDownItemFont(((ToolStripDropDownButton)bindingNavigator.Items[i]).DropDownItems);
								}
								else if (text == "ToolStripSplitButton")
								{
									SetDropDownItemFont(((ToolStripSplitButton)bindingNavigator.Items[i]).DropDownItems);
								}
							}
						}
					}
					else if (text == "MenuStrip")
					{
						SetDropDownItemFont(((MenuStrip)control).Items);
					}
					else if (text == "ToolStrip")
					{
						ToolStrip toolStrip = (ToolStrip)control;
						int num = 0;
						foreach (object item in toolStrip.Items)
						{
							toolStrip.Items[num].Font = ClientUtils.fModuleFont;
							text = item.GetType().Name;
							if (text != null)
							{
								if (text == "ToolStripDropDownButton")
								{
									SetDropDownItemFont(((ToolStripDropDownButton)item).DropDownItems);
								}
								else if (text == "ToolStripSplitButton")
								{
									SetDropDownItemFont(((ToolStripSplitButton)item).DropDownItems);
								}
							}
							num++;
						}
					}
				}
				if (control.HasChildren)
				{
					SetFont(control);
				}
			}
		}

		public static void SetLanguage(Control ctrl, string xmlFileName, string moduleName, bool changeFont)
		{
			string text = moduleName + Path.DirectorySeparatorChar + xmlFileName;
			string startupPath = Application.StartupPath;
			object[] args = new object[4]
			{
				startupPath,
				Path.DirectorySeparatorChar,
				text,
				".xml"
			};
			if (File.Exists(string.Concat(args)))
			{
				ctrl.Text = GetLanguage(ctrl.Text, text, startupPath);
			}
			else if (ClientUtils.bChangeFont)
			{
				SetFont(ctrl);
			}
		}

		public static void SetLanguage(ContextMenuStrip ctrl, string xmlFileName, string moduleName, bool changeFont)
		{
			string text = moduleName + Path.DirectorySeparatorChar + xmlFileName;
			string startupPath = Application.StartupPath;
			object[] args = new object[4]
			{
				Application.StartupPath,
				Path.DirectorySeparatorChar,
				text,
				".xml"
			};
			if (File.Exists(string.Concat(args)))
			{
				ctrl.Text = GetLanguage(ctrl.Text, text, startupPath);
				GetDropDownItem(ctrl.Items, text, startupPath, changeFont);
			}
		}

		public static string SetLanguage(string sMessage, string xmlFileName, string moduleName, string path)
		{
			string text = xmlFileName;
			if (moduleName != "")
			{
				text = moduleName + Path.DirectorySeparatorChar + xmlFileName;
			}
			object[] args = new object[4]
			{
				path,
				Path.DirectorySeparatorChar,
				text,
				".xml"
			};
			return (!File.Exists(string.Concat(args))) ? sMessage : GetLanguage(sMessage, text, path);
		}

		public static DialogResult ShowMessage(string message, int iType, string fileName, string moduleName)
		{
			string[] array = new string[4]
			{
				"Error",
				"Warning",
				"Confirm",
				""
			};
			string[] array2 = array;
			string text = null;
			string text2 = message;
			string startupPath = Application.StartupPath;
			MessageBoxButtons buttons;
			MessageBoxIcon icon;
			switch (iType)
			{
			case 0:
				text = array2[iType];
				buttons = MessageBoxButtons.OK;
				icon = MessageBoxIcon.Hand;
				break;
			case 1:
				text = array2[iType];
				buttons = MessageBoxButtons.OK;
				icon = MessageBoxIcon.Exclamation;
				break;
			case 2:
				text = array2[iType];
				buttons = MessageBoxButtons.YesNo;
				icon = MessageBoxIcon.Question;
				break;
			default:
				text = array2[3];
				buttons = MessageBoxButtons.OK;
				icon = MessageBoxIcon.Asterisk;
				break;
			}
			if (!string.IsNullOrEmpty(fileName))
			{
				object[] args = new object[6]
				{
					startupPath,
					Path.DirectorySeparatorChar,
					moduleName,
					Path.DirectorySeparatorChar,
					fileName,
					".xml"
				};
				if (File.Exists(string.Concat(args)))
				{
					XmlDocument xmlDocument = new XmlDocument();
					args = new object[6]
					{
						startupPath,
						Path.DirectorySeparatorChar,
						moduleName,
						Path.DirectorySeparatorChar,
						fileName,
						".xml"
					};
					xmlDocument.Load(string.Concat(args));
					XmlNode xmlNode = null;
					try
					{
						string xpath;
						if (message.IndexOf("'") != -1)
						{
							array = new string[5]
							{
								"//",
								ClientUtils.cultureName,
								"/Message[@Name=\"",
								message,
								"\"]"
							};
							xpath = string.Concat(array);
						}
						else
						{
							array = new string[5]
							{
								"//",
								ClientUtils.cultureName,
								"/Message[@Name='",
								message,
								"']"
							};
							xpath = string.Concat(array);
						}
						xmlNode = xmlDocument.SelectSingleNode(xpath);
					}
					catch
					{
					}
					if (xmlNode != null)
					{
						if (xmlNode.Attributes["Caption"] != null && !string.IsNullOrEmpty(xmlNode.Attributes["Caption"].Value))
						{
							text = xmlNode.Attributes["Caption"].Value;
						}
						text2 = xmlNode.Attributes["Value"].Value;
					}
				}
			}
			text = SetLanguage(text, "SajetMES", "", startupPath);
			return MessageBox.Show(text2, text, buttons, icon);
		}
	}
}

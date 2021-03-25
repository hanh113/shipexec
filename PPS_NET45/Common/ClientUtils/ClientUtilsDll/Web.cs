using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace ClientUtilsDll
{
	internal class Web
	{
		private static void GetControls(Control ctrl, string fileName, string path, string language)
		{
			foreach (Control control in ctrl.Controls)
			{
				string name = control.GetType().Name;
				if (name != null)
				{
					switch (name)
					{
					case "GridView":
					{
						GridView gridView = (GridView)control;
						for (int i = 0; i < gridView.Columns.Count; i++)
						{
							gridView.Columns[i].HeaderText = GetLanguage(gridView.Columns[i].HeaderText, fileName, path, language);
						}
						break;
					}
					case "Label":
					{
						Label label = (Label)control;
						label.Text = GetLanguage(label.Text, fileName, path, language);
						break;
					}
					case "Button":
					{
						Button button = (Button)control;
						button.Text = GetLanguage(button.Text, fileName, path, language);
						break;
					}
					case "CheckBox":
					{
						CheckBox checkBox = (CheckBox)control;
						checkBox.Text = GetLanguage(checkBox.Text, fileName, path, language);
						break;
					}
					case "RadioButton":
					{
						RadioButton radioButton = (RadioButton)control;
						radioButton.Text = GetLanguage(radioButton.Text, fileName, path, language);
						break;
					}
					case "LinkButton":
					{
						LinkButton linkButton = (LinkButton)control;
						linkButton.Text = GetLanguage(linkButton.Text, fileName, path, language);
						break;
					}
					case "DropDownList":
					{
						DropDownList dropDownList = (DropDownList)control;
						if (dropDownList.DataSource == null)
						{
							for (int i = 0; i < dropDownList.Items.Count; i++)
							{
								dropDownList.Items[i].Text = GetLanguage(dropDownList.Items[i].Text, fileName, path, language);
							}
						}
						break;
					}
					case "CheckBoxList":
					{
						CheckBoxList checkBoxList = (CheckBoxList)control;
						for (int i = 0; i < checkBoxList.Items.Count; i++)
						{
							checkBoxList.Items[i].Text = GetLanguage(checkBoxList.Items[i].Text, fileName, path, language);
						}
						break;
					}
					case "BulletedList":
					{
						BulletedList bulletedList = (BulletedList)control;
						for (int i = 0; i < bulletedList.Items.Count; i++)
						{
							bulletedList.Items[i].Text = GetLanguage(bulletedList.Items[i].Text, fileName, path, language);
						}
						break;
					}
					case "RadioButtonList":
					{
						RadioButtonList radioButtonList = (RadioButtonList)control;
						for (int i = 0; i < radioButtonList.Items.Count; i++)
						{
							radioButtonList.Items[i].Text = GetLanguage(radioButtonList.Items[i].Text, fileName, path, language);
						}
						break;
					}
					case "ListBox":
					{
						ListBox listBox = (ListBox)control;
						for (int i = 0; i < listBox.Items.Count; i++)
						{
							listBox.Items[i].Text = GetLanguage(listBox.Items[i].Text, fileName, path, language);
						}
						break;
					}
					}
				}
				if (control.HasControls())
				{
					GetControls(control, fileName, path, language);
				}
			}
		}

		public static string GetLanguage(string message, string fileName, string path, string language)
		{
			string text = message;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				object[] args = new object[4]
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
						language,
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
						language,
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

		public static void SetLanguage(Control ctrl, string xmlFileName, string moduleName, string path, string language)
		{
			string text = moduleName + Path.DirectorySeparatorChar + xmlFileName;
			object[] args = new object[4]
			{
				path,
				Path.DirectorySeparatorChar,
				text,
				".xml"
			};
			if (File.Exists(string.Concat(args)))
			{
				GetControls(ctrl, text, path, language);
			}
		}

		public static string SetLanguage(string sMessage, string xmlFileName, string moduleName, string path, string language)
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
			return (!File.Exists(string.Concat(args))) ? sMessage : GetLanguage(sMessage, text, path, language);
		}
	}
}

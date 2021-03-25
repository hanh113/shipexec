using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace ClientUtilsDll
{
    internal class Web
    {
        public Web()
        {
        }

        private static void GetControls(Control ctrl, string fileName, string path, string language)
        {
            int i;
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
                                for (i = 0; i < gridView.Columns.Count; i++)
                                {
                                    gridView.Columns[i].HeaderText = Web.GetLanguage(gridView.Columns[i].HeaderText, fileName, path, language);
                                }
                                break;
                            }
                        case "Label":
                            {
                                Label label = (Label)control;
                                label.Text = Web.GetLanguage(label.Text, fileName, path, language);
                                break;
                            }
                        case "Button":
                            {
                                Button button = (Button)control;
                                button.Text = Web.GetLanguage(button.Text, fileName, path, language);
                                break;
                            }
                        case "CheckBox":
                            {
                                CheckBox checkBox = (CheckBox)control;
                                checkBox.Text = Web.GetLanguage(checkBox.Text, fileName, path, language);
                                break;
                            }
                        case "RadioButton":
                            {
                                RadioButton radioButton = (RadioButton)control;
                                radioButton.Text = Web.GetLanguage(radioButton.Text, fileName, path, language);
                                break;
                            }
                        case "LinkButton":
                            {
                                LinkButton linkButton = (LinkButton)control;
                                linkButton.Text = Web.GetLanguage(linkButton.Text, fileName, path, language);
                                break;
                            }
                        case "DropDownList":
                            {
                                DropDownList dropDownList = (DropDownList)control;
                                if (dropDownList.DataSource == null)
                                {
                                    for (i = 0; i < dropDownList.Items.Count; i++)
                                    {
                                        dropDownList.Items[i].Text = Web.GetLanguage(dropDownList.Items[i].Text, fileName, path, language);
                                    }
                                }
                                break;
                            }
                        case "CheckBoxList":
                            {
                                CheckBoxList checkBoxList = (CheckBoxList)control;
                                for (i = 0; i < checkBoxList.Items.Count; i++)
                                {
                                    checkBoxList.Items[i].Text = Web.GetLanguage(checkBoxList.Items[i].Text, fileName, path, language);
                                }
                                break;
                            }
                        case "BulletedList":
                            {
                                BulletedList bulletedList = (BulletedList)control;
                                for (i = 0; i < bulletedList.Items.Count; i++)
                                {
                                    bulletedList.Items[i].Text = Web.GetLanguage(bulletedList.Items[i].Text, fileName, path, language);
                                }
                                break;
                            }
                        case "RadioButtonList":
                            {
                                RadioButtonList radioButtonList = (RadioButtonList)control;
                                for (i = 0; i < radioButtonList.Items.Count; i++)
                                {
                                    radioButtonList.Items[i].Text = Web.GetLanguage(radioButtonList.Items[i].Text, fileName, path, language);
                                }
                                break;
                            }
                        case "ListBox":
                            {
                                ListBox listBox = (ListBox)control;
                                for (i = 0; i < listBox.Items.Count; i++)
                                {
                                    listBox.Items[i].Text = Web.GetLanguage(listBox.Items[i].Text, fileName, path, language);
                                }
                                break;
                            }
                    }
                }
                if (control.HasControls())
                {
                    Web.GetControls(control, fileName, path, language);
                }
            }
        }

        public static string GetLanguage(string message, string fileName, string path, string language)
        {
            string str;
            string str1;
            string[] strArrays;
            string value = message;
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                object[] objArray = new object[] { path, Path.DirectorySeparatorChar, fileName, ".xml" };
                xmlDocument.Load(string.Concat(objArray));
                if (message.IndexOf("'") != -1)
                {
                    strArrays = new string[] { "//", language, "/Message[@Name=\"", message, "\"]" };
                    str = string.Concat(strArrays);
                }
                else
                {
                    strArrays = new string[] { "//", language, "/Message[@Name='", message, "']" };
                    str = string.Concat(strArrays);
                }
                XmlNode xmlNodes = xmlDocument.SelectSingleNode(str);
                if (xmlNodes != null)
                {
                    value = xmlNodes.Attributes["Value"].Value;
                    if (string.IsNullOrEmpty(value))
                    {
                        value = message;
                    }
                }
                str1 = value;
            }
            catch
            {
                str1 = value;
            }
            return str1;
        }

        public static void SetLanguage(Control ctrl, string xmlFileName, string moduleName, string path, string language)
        {
            string str = string.Concat(moduleName, Path.DirectorySeparatorChar, xmlFileName);
            object[] objArray = new object[] { path, Path.DirectorySeparatorChar, str, ".xml" };
            if (File.Exists(string.Concat(objArray)))
            {
                Web.GetControls(ctrl, str, path, language);
            }
        }

        public static string SetLanguage(string sMessage, string xmlFileName, string moduleName, string path, string language)
        {
            string str;
            string str1 = xmlFileName;
            if (moduleName != "")
            {
                str1 = string.Concat(moduleName, Path.DirectorySeparatorChar, xmlFileName);
            }
            object[] objArray = new object[] { path, Path.DirectorySeparatorChar, str1, ".xml" };
            str = (!File.Exists(string.Concat(objArray)) ? sMessage : Web.GetLanguage(sMessage, str1, path, language));
            return str;
        }
    }
}
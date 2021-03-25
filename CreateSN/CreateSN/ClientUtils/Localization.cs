using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;

namespace ClientUtilsDll
{
    internal class Localization
    {
        public Localization()
        {
        }


        private static void GetDropDownItem(ToolStripItemCollection menuStripItem, string fileName, string path, bool changeFont)
        {
            int num = 0;
            foreach (object obj in menuStripItem)
            {
                if (changeFont)
                {
                    menuStripItem[num].Font = ClientUtils.fModuleFont;
                }
                string name = obj.GetType().Name;
                if (name != null)
                {
                    if (name == "ToolStripComboBox")
                    {
                        ToolStripComboBox language = (ToolStripComboBox)obj;
                        language.Text = Localization.GetLanguage(language.Text, fileName, path);
                        for (int i = 0; i <= language.Items.Count - 1; i++)
                        {
                            language.Items[i] = Localization.GetLanguage(language.Items[i].ToString(), fileName, path);
                        }
                    }
                    else if (name == "DropDownButton")
                    {
                        Localization.GetDropDownItem(((ToolStripDropDownButton)obj).DropDownItems, fileName, path, changeFont);
                    }
                    else if (name == "ToolStripMenuItem")
                    {
                        ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)obj;
                        toolStripMenuItem.Text = Localization.GetLanguage(toolStripMenuItem.Text, fileName, path);
                        Localization.GetDropDownItem(toolStripMenuItem.DropDownItems, fileName, path, changeFont);
                    }
                }
                num++;
            }
        }

        public static string GetLanguage(string message, string fileName, string path)
        {
            string str;
            string str1;
            string[] strArrays;
            string value = message;
            object[] objArray = new object[] { path, Path.DirectorySeparatorChar, fileName, ".xml" };
            if (File.Exists(string.Concat(objArray)))
            {
                try
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    objArray = new object[] { path, Path.DirectorySeparatorChar, fileName, ".xml" };
                    xmlDocument.Load(string.Concat(objArray));
                    if (message.IndexOf("'") != -1)
                    {
                        strArrays = new string[] { "//", ClientUtils.cultureName, "/Message[@Name=\"", message, "\"]" };
                        str = string.Concat(strArrays);
                    }
                    else
                    {
                        strArrays = new string[] { "//", ClientUtils.cultureName, "/Message[@Name='", message, "']" };
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
            }
            else
            {
                str1 = value;
            }
            return str1;
        }

        public static string GetOrderField(string fileName, string moduleName)
        {
            string str = "";
            object[] startupPath = new object[] { Application.StartupPath, Path.DirectorySeparatorChar, moduleName, Path.DirectorySeparatorChar, fileName, ".xml" };
            fileName = string.Concat(startupPath);
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
            return str;
        }

        public static string GetValue(string message, string moduleName, string fileName, out bool visible)
        {
            string str;
            string[] strArrays;
            string value = message;
            visible = true;
            object[] startupPath = new object[] { Application.StartupPath, Path.DirectorySeparatorChar, moduleName, Path.DirectorySeparatorChar, fileName, ".xml" };
            fileName = string.Concat(startupPath);
            if (File.Exists(fileName))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                if (message.IndexOf("'") != -1)
                {
                    strArrays = new string[] { "//", ClientUtils.cultureName, "/Field[@Name=\"", message, "\"]" };
                    str = string.Concat(strArrays);
                }
                else
                {
                    strArrays = new string[] { "//", ClientUtils.cultureName, "/Field[@Name='", message, "']" };
                    str = string.Concat(strArrays);
                }
                XmlNode xmlNodes = xmlDocument.SelectSingleNode(str);
                if (xmlNodes != null)
                {
                    value = xmlNodes.Attributes["Value"].Value;
                    if (xmlNodes.Attributes["Visible"].Value == "0")
                    {
                        visible = false;
                    }
                }
            }
            return value;
        }

        private static void SetDropDownItemFont(ToolStripItemCollection menuStripItem)
        {
            int num = 0;
            foreach (object obj in menuStripItem)
            {
                menuStripItem[num].Font = ClientUtils.fModuleFont;
                string name = obj.GetType().Name;
                if (name != null)
                {
                    if (name == "DropDownButton")
                    {
                        Localization.SetDropDownItemFont(((ToolStripDropDownButton)obj).DropDownItems);
                    }
                    else if (name == "ToolStripMenuItem")
                    {
                        Localization.SetDropDownItemFont(((ToolStripMenuItem)obj).DropDownItems);
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
                string str = name;
                if (str != null)
                {
                    if (str == "BindingNavigator")
                    {
                        BindingNavigator bindingNavigator = (BindingNavigator)control;
                        for (int i = 0; i <= bindingNavigator.Items.Count - 1; i++)
                        {
                            str = bindingNavigator.Items[i].GetType().Name;
                            if (str != null)
                            {
                                if (str == "ToolStripDropDownButton")
                                {
                                    Localization.SetDropDownItemFont(((ToolStripDropDownButton)bindingNavigator.Items[i]).DropDownItems);
                                }
                                else if (str == "ToolStripSplitButton")
                                {
                                    Localization.SetDropDownItemFont(((ToolStripSplitButton)bindingNavigator.Items[i]).DropDownItems);
                                }
                            }
                        }
                    }
                    else if (str == "MenuStrip")
                    {
                        Localization.SetDropDownItemFont(((MenuStrip)control).Items);
                    }
                    else if (str == "ToolStrip")
                    {
                        ToolStrip toolStrip = (ToolStrip)control;
                        int num = 0;
                        foreach (object item in toolStrip.Items)
                        {
                            toolStrip.Items[num].Font = ClientUtils.fModuleFont;
                            str = item.GetType().Name;
                            if (str != null)
                            {
                                if (str == "ToolStripDropDownButton")
                                {
                                    Localization.SetDropDownItemFont(((ToolStripDropDownButton)item).DropDownItems);
                                }
                                else if (str == "ToolStripSplitButton")
                                {
                                    Localization.SetDropDownItemFont(((ToolStripSplitButton)item).DropDownItems);
                                }
                            }
                            num++;
                        }
                    }
                }
                if (control.HasChildren)
                {
                    Localization.SetFont(control);
                }
            }
        }

        public static void SetLanguage(Control ctrl, string xmlFileName, string moduleName, bool changeFont)
        {
            string str = string.Concat(moduleName, Path.DirectorySeparatorChar, xmlFileName);
            string startupPath = Application.StartupPath;
            object[] directorySeparatorChar = new object[] { startupPath, Path.DirectorySeparatorChar, str, ".xml" };
            if (File.Exists(string.Concat(directorySeparatorChar)))
            {
                ctrl.Text = Localization.GetLanguage(ctrl.Text, str, startupPath);
                //Localization.GetControls(ctrl, str, startupPath, changeFont);
            }
            else if (ClientUtils.bChangeFont)
            {
                Localization.SetFont(ctrl);
            }
        }

        public static void SetLanguage(ContextMenuStrip ctrl, string xmlFileName, string moduleName, bool changeFont)
        {
            string str = string.Concat(moduleName, Path.DirectorySeparatorChar, xmlFileName);
            string startupPath = Application.StartupPath;
            object[] objArray = new object[] { Application.StartupPath, Path.DirectorySeparatorChar, str, ".xml" };
            if (File.Exists(string.Concat(objArray)))
            {
                ctrl.Text = Localization.GetLanguage(ctrl.Text, str, startupPath);
                Localization.GetDropDownItem(ctrl.Items, str, startupPath, changeFont);
            }
        }

        public static string SetLanguage(string sMessage, string xmlFileName, string moduleName, string path)
        {
            string str;
            string str1 = xmlFileName;
            if (moduleName != "")
            {
                str1 = string.Concat(moduleName, Path.DirectorySeparatorChar, xmlFileName);
            }
            object[] objArray = new object[] { path, Path.DirectorySeparatorChar, str1, ".xml" };
            str = (!File.Exists(string.Concat(objArray)) ? sMessage : Localization.GetLanguage(sMessage, str1, path));
            return str;
        }

        public static DialogResult ShowMessage(string message, int iType, string fileName, string moduleName)
        {
            MessageBoxButtons messageBoxButton;
            MessageBoxIcon messageBoxIcon;
            string str;
            string[] strArrays = new string[] { "Error", "Warning", "Confirm", "" };
            string[] strArrays1 = strArrays;
            string value = null;
            string value1 = message;
            string startupPath = Application.StartupPath;
            switch (iType)
            {
                case 0:
                    {
                        value = strArrays1[iType];
                        messageBoxButton = MessageBoxButtons.OK;
                        messageBoxIcon = MessageBoxIcon.Hand;
                        break;
                    }
                case 1:
                    {
                        value = strArrays1[iType];
                        messageBoxButton = MessageBoxButtons.OK;
                        messageBoxIcon = MessageBoxIcon.Exclamation;
                        break;
                    }
                case 2:
                    {
                        value = strArrays1[iType];
                        messageBoxButton = MessageBoxButtons.YesNo;
                        messageBoxIcon = MessageBoxIcon.Question;
                        break;
                    }
                default:
                    {
                        value = strArrays1[3];
                        messageBoxButton = MessageBoxButtons.OK;
                        messageBoxIcon = MessageBoxIcon.Asterisk;
                        break;
                    }
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                object[] directorySeparatorChar = new object[] { startupPath, Path.DirectorySeparatorChar, moduleName, Path.DirectorySeparatorChar, fileName, ".xml" };
                if (File.Exists(string.Concat(directorySeparatorChar)))
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    directorySeparatorChar = new object[] { startupPath, Path.DirectorySeparatorChar, moduleName, Path.DirectorySeparatorChar, fileName, ".xml" };
                    xmlDocument.Load(string.Concat(directorySeparatorChar));
                    XmlNode xmlNodes = null;
                    try
                    {
                        if (message.IndexOf("'") != -1)
                        {
                            strArrays = new string[] { "//", ClientUtils.cultureName, "/Message[@Name=\"", message, "\"]" };
                            str = string.Concat(strArrays);
                        }
                        else
                        {
                            strArrays = new string[] { "//", ClientUtils.cultureName, "/Message[@Name='", message, "']" };
                            str = string.Concat(strArrays);
                        }
                        xmlNodes = xmlDocument.SelectSingleNode(str);
                    }
                    catch
                    {
                    }
                    if (xmlNodes != null)
                    {
                        if (xmlNodes.Attributes["Caption"] != null && !string.IsNullOrEmpty(xmlNodes.Attributes["Caption"].Value))
                        {
                            value = xmlNodes.Attributes["Caption"].Value;
                        }
                        value1 = xmlNodes.Attributes["Value"].Value;
                    }
                }
            }
            value = Localization.SetLanguage(value, "SajetMES", "", startupPath);
            return MessageBox.Show(value1, value, messageBoxButton, messageBoxIcon);
        }
    }
}
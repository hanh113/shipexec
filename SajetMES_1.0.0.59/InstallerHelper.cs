using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Xml;
using System.Reflection;
using NetFwTypeLib;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data;
using System.Runtime.InteropServices;

namespace EDIPPS
{
    [RunInstaller(true)]
    public partial class InstallerHelper : Installer
    {
        public InstallerHelper()
        {
            InitializeComponent();
        }
        internal static void SetValue(string sPath, string Node, string sField, string Value)
        {
            string xmlFile = sPath + Path.DirectorySeparatorChar + "PPS.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFile);
            XmlNode xElmntRoot = xmlDoc.SelectSingleNode("//" + Node);
            if (xElmntRoot == null)
            {
                xElmntRoot = xmlDoc.DocumentElement;
                XmlElement elem = xmlDoc.CreateElement(Node);
                elem.SetAttribute(sField, Value);
                xElmntRoot.AppendChild(elem);
            }
            else
            {
                if (xElmntRoot.Attributes[sField] == null)
                {
                    XmlAttribute attr = xElmntRoot.Attributes[sField];
                    attr = xElmntRoot.OwnerDocument.CreateAttribute(sField);
                    attr.Value = Value;
                    xElmntRoot.Attributes.Append(attr);
                }
                else
                    xElmntRoot.Attributes[sField].Value = Value;
            }
            xmlDoc.Save(xmlFile);
        }
        private static void SaveSECS(string xmlFile, string node, string sIP, string sPort)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement xElmntRoot;
            xmlDoc.Load(xmlFile);
            xElmntRoot = (XmlElement)xmlDoc.SelectSingleNode("EDIPPS");
            XmlElement xElem = InsertTextNode(xmlDoc, xElmntRoot, node);
            xElem.SetAttribute("IP", sIP);
            xElem.SetAttribute("Port", sPort);
            xElem.SetAttribute("Ref", "tcp");
            xmlDoc.Save(xmlFile);
        }
        private static XmlElement InsertTextNode(XmlDocument xDoc, XmlNode xNode, string strTag)
        {
            XmlNode xNodeTemp;
            xNodeTemp = xDoc.CreateElement(strTag);
            xNode.AppendChild(xNodeTemp);
            return (XmlElement)(xNodeTemp);
        }
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);
            //string sShortCut = "Chroma MES Client";
            if (Context.Parameters["TYPE"] == "SECS")
            {
                if (string.IsNullOrEmpty(Context.Parameters["SECS1"]))
                    throw new InstallException(string.Format("The 'SECS#1 IP' parameter has not been provided.", this.GetType()));
                //sShortCut = "Chroma Application Client";
            }
            try
            {
                string sPath = Context.Parameters["TARGETDIR"];
                string sSource = Context.Parameters["SOURCEDIR"];
                string sHostFile = "";
                string[] MyFiles = Directory.GetFiles(sPath + Path.DirectorySeparatorChar + "Host");
                foreach (string MyFile in MyFiles)
                {
                    sHostFile = Path.GetFileName(MyFile);
                    break;
                }
                if (Context.Parameters["AP1"] != null)
                {
                    if (!string.IsNullOrEmpty(Context.Parameters["AP1"]) && !string.IsNullOrEmpty(Context.Parameters["APPORT"]))
                        SaveSECS(sPath + Path.DirectorySeparatorChar + "Host" + Path.DirectorySeparatorChar + sHostFile, "APServer", Context.Parameters["AP1"], Context.Parameters["APPORT"]);
                }
                if (Context.Parameters["AP2"] != null)
                {
                    if (!string.IsNullOrEmpty(Context.Parameters["AP2"]) && !string.IsNullOrEmpty(Context.Parameters["APPORT"]))
                        SaveSECS(sPath + Path.DirectorySeparatorChar + "Host" + Path.DirectorySeparatorChar + sHostFile, "APServer", Context.Parameters["AP2"], Context.Parameters["APPORT"]);
                }
                string sBroadCastPort = Context.Parameters["BROADCAST"];
                SetValue(sPath, "Setting", "BroadCast", sBroadCastPort);
                string sTimeOut = Context.Parameters["TIMEOUT"];
                SetValue(sPath, "Setting", "Timeout", sTimeOut);
                SetValue(sPath, "Setting", "ModuleStyle", Context.Parameters["STYLE"]);
                if (Context.Parameters["SECS1"] != null)
                {
                    if (!string.IsNullOrEmpty(Context.Parameters["SECS1"]) && !string.IsNullOrEmpty(Context.Parameters["PORT"]))
                        SaveSECS(sPath + Path.DirectorySeparatorChar + "PPS.xml", "SECS", Context.Parameters["SECS1"], Context.Parameters["PORT"]);
                    if (!string.IsNullOrEmpty(Context.Parameters["SECS2"]) && !string.IsNullOrEmpty(Context.Parameters["PORT"]))
                        SaveSECS(sPath + Path.DirectorySeparatorChar + "PPS.xml", "SECS", Context.Parameters["SECS2"], Context.Parameters["PORT"]);
                }
                SetupWindowsFirewall();
                OpenFirewall(int.Parse(sBroadCastPort), "EDIPPS " + Context.Parameters["TYPE"] + " System");
                //appShortcutToDesktop(sShortCut);
            }
            catch (FormatException e)
            {
                string s = e.Message;
            }
        }
        private void appShortcutToDesktop(string linkName)
        {
            string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            using (StreamWriter writer = new StreamWriter(deskDir + "\\" + linkName + ".url"))
            {
                string app = System.Reflection.Assembly.GetExecutingAssembly().Location;
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=file:///" + app);
                writer.WriteLine("IconIndex=0");
                string icon = app.Replace('\\', '/');
                writer.WriteLine("IconFile=" + icon);
                writer.Flush();
            }
        }
        public bool OpenFirewall(int portNo, string sName)
        {
            INetFwMgr icfMgr = null;
            try
            {
                Type TicfMgr = Type.GetTypeFromProgID("HNetCfg.FwMgr");
                icfMgr = (INetFwMgr)Activator.CreateInstance(TicfMgr);
            }
            catch
            {
                return false;
            }
            try
            {
                INetFwProfile profile;
                INetFwOpenPort openport;
                Type TportClass = Type.GetTypeFromProgID("HNetCfg.FWOpenPort");
                openport = (INetFwOpenPort)Activator.CreateInstance(TportClass);
                profile = icfMgr.LocalPolicy.CurrentProfile;
                openport.Scope = NetFwTypeLib.NET_FW_SCOPE_.NET_FW_SCOPE_ALL;
                openport.Enabled = true;
                openport.Name = "TCP (" + portNo + "): " + sName;
                openport.Port = portNo;
                openport.Protocol = NetFwTypeLib.NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
                profile.GloballyOpenPorts.Add(openport);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void SetupWindowsFirewall()
        {
            try
            {
                Type NetFwMgrType = Type.GetTypeFromProgID("HNetCfg.FwMgr", false);
                INetFwMgr mgr = (INetFwMgr)Activator.CreateInstance(NetFwMgrType);

                Type NetFwAuthorizedApplicationType = Type.GetTypeFromProgID("HNetCfg.FwAuthorizedApplication", false);
                INetFwAuthorizedApplication app = (INetFwAuthorizedApplication)Activator.CreateInstance(NetFwAuthorizedApplicationType);

                app.Name = Path.GetFileName(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileName);
                app.Enabled = true;
                app.ProcessImageFileName = Assembly.GetExecutingAssembly().Location;
                app.Scope = NET_FW_SCOPE_.NET_FW_SCOPE_ALL;

                mgr.LocalPolicy.CurrentProfile.AuthorizedApplications.Add(app);
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("EDI PPS System", ex.ToString());
            }
        }
        private void RemoveWindowsFirewall()
        {
            Type netFwMgrType = Type.GetTypeFromProgID("HNetCfg.FwMgr");
            INetFwMgr mgr = (INetFwMgr)Activator.CreateInstance(netFwMgrType);
            INetFwProfile curProfile = mgr.LocalPolicy.CurrentProfile;
            curProfile.AuthorizedApplications.Remove(Assembly.GetExecutingAssembly().Location);
        }
        public static int GetValue(string fileName, string column)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(fileName);
                int iPort = Int32.Parse(ds.Tables["Setting"].Rows[0][column].ToString());
                ds.Dispose();
                return iPort;
            }
            finally
            {
            }
        }
        public override void Uninstall(System.Collections.IDictionary stateSaver)
        {
            base.Uninstall(stateSaver);
            RegistrationServices r = new RegistrationServices();
            r.UnregisterAssembly(this.GetType().Assembly);
            try
            {
                CloseFirewall(GetValue(Context.Parameters["TARGETDIR"] + Path.DirectorySeparatorChar + "PPS.xml", "BroadCast"));
            }
            catch
            {
            }
            RemoveWindowsFirewall();
            string sShortCut = "EDI PPS Client";
            if (Context.Parameters["TYPE"] == "SECS")
                sShortCut = "PPS Application Client";
            string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            File.Delete(deskDir + "\\" + sShortCut + ".url");
        }
        public bool CloseFirewall(int portNo)
        {
            INetFwMgr icfMgr = null;
            try
            {
                Type TicfMgr = Type.GetTypeFromProgID("HNetCfg.FwMgr");
                icfMgr = (INetFwMgr)Activator.CreateInstance(TicfMgr);
            }
            catch
            {
                return false;
            }
            try
            {
                INetFwProfile profile = icfMgr.LocalPolicy.CurrentProfile;
                INetFwOpenPorts ports = profile.GloballyOpenPorts;
                ports.Remove(portNo, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP);
                ports = null;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
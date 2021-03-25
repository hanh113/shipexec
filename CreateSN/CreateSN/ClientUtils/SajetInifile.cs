using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;


namespace ClientUtilsDll
{
    public class SajetInifile : Component
    {
        public string inipath;

        private IContainer components = null;

        public SajetInifile()
        {
            this.InitializeComponent();
        }

        public SajetInifile(IContainer container)
        {
            container.Add(this);
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if ((!disposing ? false : this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        [DllImport("kernel32", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
        }

        public string ReadIniFile(string sFile, string sSection, string sKey, string sDefault)
        {
            StringBuilder stringBuilder = new StringBuilder(500);
            SajetInifile.GetPrivateProfileString(sSection, sKey, sDefault, stringBuilder, 500, sFile);
            return stringBuilder.ToString();
        }

        public void WriteIniFile(string sFile, string sSection, string sKey, string sValue)
        {
            SajetInifile.WritePrivateProfileString(sSection, sKey, sValue, sFile);
        }

        [DllImport("kernel32", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    }
}
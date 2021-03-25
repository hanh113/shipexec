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
			InitializeComponent();
		}

		public SajetInifile(IContainer container)
		{
			container.Add(this);
			InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

		private void InitializeComponent()
		{
			components = new Container();
		}

		public string ReadIniFile(string sFile, string sSection, string sKey, string sDefault)
		{
			StringBuilder stringBuilder = new StringBuilder(500);
			GetPrivateProfileString(sSection, sKey, sDefault, stringBuilder, 500, sFile);
			return stringBuilder.ToString();
		}

		public void WriteIniFile(string sFile, string sSection, string sKey, string sValue)
		{
			WritePrivateProfileString(sSection, sKey, sValue, sFile);
		}

		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
	}
}

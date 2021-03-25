using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstallConsole
{
    public partial class Form1 : Form
    {
        Dictionary<string, string> keyValues;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpenFileDialog_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folder = new System.Windows.Forms.FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(folder.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                tbPath.Text = folder.SelectedPath;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (ConfigurationManager.AppSettings.HasKeys())
            {
                keyValues = new Dictionary<string, string>();
                foreach (string theKey in ConfigurationManager.AppSettings.Keys)
                {
                    keyValues.Add(theKey, ConfigurationManager.AppSettings.Get(theKey));
                }
                cbHost.Items.AddRange(keyValues.Values.ToArray());
                cbHost.Text = cbHost.Items[0].ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbHost.Text) && !string.IsNullOrEmpty(tbPath.Text))
            {
                InstallService ws = new InstallService(cbHost.Text, tbPath.Text);
                ws.Install(false);
            }
        }

        private void btnReinstall_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbHost.Text) && !string.IsNullOrEmpty(tbPath.Text))
            {
                InstallService ws = new InstallService(cbHost.Text, tbPath.Text);
                ws.Install(true);
            }
        }

        private void cbHost_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (keyValues.Count > 0)
            {
                tbDesc.Text = keyValues.FirstOrDefault(g => g.Value == cbHost.Text).Key;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using ClientUtilsDll;

namespace EDIPPS
{
    public partial class fSkin : Form
    {
        internal string g_sSkinName = Program.skinName;
        public fSkin()
        {
            InitializeComponent();
        }
        private void formSkin_Load(object sender, EventArgs e)
        {
            ClientUtils.SetLanguage(this, "");
            treeView1.ExpandAll();
            string[] dirs = Directory.GetDirectories(@Program.skinPath);
            foreach (string localDir in dirs)
            {
                listBox1.Items.Add(Path.GetFileName(localDir));
                if (Path.GetFileName(localDir) == Program.skinName)
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
            combStyle.SelectedIndex = fMain.g_iModuleStyle;
            combModuleSelect.SelectedIndex = fMain.g_iModuleSelect + 1;
            chkLoadImage.Checked = ClientUtils.bLoadImage;
            chkChangeFont.Checked = ClientUtils.bChangeFont;
            lablFont.Font = fMain.fFont;
            lablFont.Text = lablFont.Font.Name + "; " + lablFont.Font.Size.ToString() + "; " + lablFont.Font.Style.ToString();
            lablLargeFont.Font = fMain.fLargeFont;
            lablLargeFont.Text = lablLargeFont.Font.Name + "; " + lablLargeFont.Font.Size.ToString() + "; " + lablLargeFont.Font.Style.ToString();
            lablSmallFont.Font = fMain.fSmallFont;
            lablSmallFont.Text = lablSmallFont.Font.Name + "; " + lablSmallFont.Font.Size.ToString() + "; " + lablLargeFont.Font.Style.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            fMain.SetValue("Setting", "Skin", listBox1.SelectedItem.ToString());
            fMain.SetValue("Font", "ChangeFlag", chkChangeFont.Checked.ToString());
            fMain.SetValue("Font", "Default", lablFont.Text);
            fMain.SetValue("Font", "Large", lablLargeFont.Text);
            fMain.SetValue("Font", "Small", lablSmallFont.Text);
            fMain.fFont = lablFont.Font;
            fMain.fLargeFont = lablLargeFont.Font;
            fMain.fSmallFont = lablSmallFont.Font;
            ClientUtils.bChangeFont = chkChangeFont.Checked;
            ClientUtils.bLoadImage = chkLoadImage.Checked;
            fMain.SetValue("Module", "SelectMode", Convert.ToString(combModuleSelect.SelectedIndex - 1));
            fMain.SetValue("Module", "LoadImage", chkLoadImage.Checked.ToString());
            fMain.g_iModuleSelect = combModuleSelect.SelectedIndex - 1;
            switch (combStyle.SelectedIndex)
            {
                case 0: fMain.SetValue("Module", "Style", "Button");
                    break;
                case 1: fMain.SetValue("Module", "Style", "ComboBox");
                    break;
                default: fMain.SetValue("Module", "Style", "2xComboBox");
                    break;
            }
            g_sSkinName = listBox1.SelectedItem.ToString();
            if (fMain.g_iModuleStyle != combStyle.SelectedIndex)
            {
                if (ClientUtils.ShowMessage("Restart Application?", 2) == DialogResult.Yes)
                {
                    //Program.mutex.ReleaseMutex(); 
                    Application.ExitThread();                     
                    System.Diagnostics.Process.Start(Application.ExecutablePath, ClientUtils.UserPara1);
                }
            }
            else
                this.DialogResult = DialogResult.Yes;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                switch (treeView1.SelectedNode.Level)
                {
                    case 0:
                        if (e.Node.IsExpanded)
                        {
                            e.Node.Collapse();
                            e.Node.ImageIndex = 0;
                        }
                        else
                        {
                            e.Node.ImageIndex = 2;
                            e.Node.ExpandAll();
                        }
                        treeView1.SelectedNode = null;
                        break;
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path = Program.skinPath + listBox1.SelectedItem.ToString();
            if (File.Exists(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "login.jpg"))
                pnlLogin.BackgroundImage = Image.FromFile(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "login.jpg");
            else
                pnlLogin.BackgroundImage = null;
            if (File.Exists(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "btnClose.jpg"))
                btnClose.Image = Image.FromFile(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "btnClose.jpg");
            else
                btnClose.Image = global::EDIPPS.Properties.Resources.btnClose;
            if (File.Exists(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "btnMin.jpg"))
                btnMin.Image = Image.FromFile(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "btnMin.jpg");
            else
                btnMin.Image = global::EDIPPS.Properties.Resources.btnMin;
            if (File.Exists(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "btnResize.jpg"))
                btnResize.Image = Image.FromFile(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "btnResize.jpg");
            else
                btnResize.Image = global::EDIPPS.Properties.Resources.btnResize;
            if (File.Exists(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "btnMax.jpg"))
                btnMax.Image = Image.FromFile(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "btnMax.jpg");
            else
                btnMax.Image = global::EDIPPS.Properties.Resources.btnMax;
            if (File.Exists(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "Title.jpg"))
                pnlTitle.BackgroundImage = Image.FromFile(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "Title.jpg");
            else
                pnlTitle.BackgroundImage = global::EDIPPS.Properties.Resources.Title;
            if (File.Exists(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "MinBar.jpg"))
                pnlMin.BackgroundImage = Image.FromFile(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "MinBar.jpg");
            else
                pnlMin.BackgroundImage = global::EDIPPS.Properties.Resources.MinBar;
            if (File.Exists(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "MN01.jpg"))
            {
                picMN01.BackgroundImage = Image.FromFile(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "MN01.jpg");
                picMenuStrip.Visible = false;
                if (File.Exists(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "MN02.jpg"))
                    picMN02.BackgroundImage = Image.FromFile(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "MN02.jpg");
                else
                    picMN02.BackgroundImage = null;
                pnlMenu.Height = 31;
            }
            else
            {
                picMenuStrip.Visible = true;
                picMN01.BackgroundImage = null;
                pnlMenu.Height = 14;
                if (File.Exists(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "MenuStrip.jpg"))
                    picMenuStrip.BackgroundImage = Image.FromFile(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "MenuStrip.jpg");
                else
                    picMenuStrip.BackgroundImage = null;
            }
            if (File.Exists(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "ToolStrip.jpg"))
                picToolStrip.BackgroundImage = Image.FromFile(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "ToolStrip.jpg");
            else
                picToolStrip.BackgroundImage = null;
            if (File.Exists(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "function.jpg"))
                pnlFun.BackgroundImage = Image.FromFile(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "function.jpg");
            else
                pnlFun.BackgroundImage = null;
            if (File.Exists(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "background.jpg"))
                pnlBack.BackgroundImage = Image.FromFile(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "background.jpg");
            else
                pnlBack.BackgroundImage = null;
            if (File.Exists(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "button.jpg"))
                pnlBtn.BackgroundImage = Image.FromFile(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "button.jpg");
            else
                pnlBtn.BackgroundImage = null;
            if (File.Exists(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "folder.bmp"))
                imageList1.Images[0] = Image.FromFile(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "folder.bmp");
            else
                imageList1.Images[0] = global::EDIPPS.Properties.Resources.folder;
            if (File.Exists(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "file.bmp"))
                imageList1.Images[1] = Image.FromFile(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "file.bmp");
            else
                imageList1.Images[1] = global::EDIPPS.Properties.Resources.file;
            if (File.Exists(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "folderopen.bmp"))
                imageList1.Images[2] = Image.FromFile(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "folderopen.bmp");
            else
                imageList1.Images[2] = global::EDIPPS.Properties.Resources.folderopen;
            if (File.Exists(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "Bottom.jpg"))
                pnlBottom.BackgroundImage = Image.FromFile(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "Bottom.jpg");
            else
                pnlBottom.BackgroundImage = null;
            if (File.Exists(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "CopyRight.jpg"))
            {
                pnlCopyRight.BackgroundImage = Image.FromFile(Program.skinPath + listBox1.SelectedItem + Path.DirectorySeparatorChar + "CopyRight.jpg");
                lablCopyRight.Visible = false;
                pnlCopyRight.Visible = true;
            }
            else
            {
                pnlCopyRight.BackgroundImage = null;
                pnlCopyRight.Visible = false;
                lablCopyRight.Visible = true;
            }
            if (treeView1.Nodes[0].IsExpanded)
                treeView1.Nodes[0].ImageIndex = 2;
            else
                treeView1.Nodes[0].ImageIndex = 0;
            treeView1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = lablFont.Font;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                lablFont.Font = fontDialog1.Font;
                lablFont.Text = fontDialog1.Font.Name + "; " + fontDialog1.Font.Size.ToString() + "; " + fontDialog1.Font.Style.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = lablLargeFont.Font;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                lablLargeFont.Font = fontDialog1.Font;
                lablLargeFont.Text = fontDialog1.Font.Name + "; " + fontDialog1.Font.Size.ToString() + "; " + fontDialog1.Font.Style.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = lablSmallFont.Font;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                lablSmallFont.Font = fontDialog1.Font;
                lablSmallFont.Text = fontDialog1.Font.Name + "; " + fontDialog1.Font.Size.ToString() + "; " + fontDialog1.Font.Style.ToString();
            }
        }
    }
}
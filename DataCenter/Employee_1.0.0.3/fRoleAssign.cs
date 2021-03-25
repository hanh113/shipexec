using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SajetClass;

namespace CEmp
{
    public partial class fRoleAssign : Form
    {
        public fRoleAssign()
        {
            InitializeComponent();
        }
       
        DataSet dsTemp;
        public String g_sEmpID, g_sEmpName;

        private void Show_Role()
        {            
            LVAll.Items.Clear();
            string sSQL = " select * from sajet.sys_role "
                        + " where enabled = 'Y' "
                        + " order by role_name ";
            dsTemp = ClientUtils.ExecuteSQL(sSQL);

            for (int i = 0; i <= dsTemp.Tables[0].Rows.Count - 1; i++)
            {
                LVAll.Items.Add(dsTemp.Tables[0].Rows[i]["ROLE_NAME"].ToString());
                LVAll.Items[i].SubItems.Add(dsTemp.Tables[0].Rows[i]["ROLE_DESC"].ToString());
                LVAll.Items[i].SubItems.Add(dsTemp.Tables[0].Rows[i]["ROLE_ID"].ToString());
                LVAll.Items[i].ImageIndex = 0;
            }
        }

        private void bbtnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= LVAll.Items.Count - 1; i++)
            {
                LVAll.Items[i].Checked = true;
            }            
        }

        private void bbtnSelectNone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= LVAll.Items.Count - 1; i++)
            {
                LVAll.Items[i].Checked = false;
            }
        }

        private void bbtnChoose_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= LVAll.Items.Count - 1; i++)
            {
                if (LVAll.Items[i].Checked)
                {
                    //Find必須用Name來找,因此將Name設成跟Text相同
                    if (LVChoose.Items.Find(LVAll.Items[i].Text, false).Length == 0)
                    {                        
                        LVChoose.Items.Add(LVAll.Items[i].Text);                        
                        LVChoose.Items[LVChoose.Items.Count - 1].SubItems.Add(LVAll.Items[i].SubItems[2].Text);
                        LVChoose.Items[LVChoose.Items.Count - 1].Name = LVAll.Items[i].Text;
                        LVChoose.Items[LVChoose.Items.Count - 1].ImageIndex = 0;
                    }
                }
            }
        }

        private void fRoleAssign_Load(object sender, EventArgs e)
        {
            SajetCommon.SetLanguageControl(this);
            panel1.BackgroundImage = ClientUtils.LoadImage("ImgButton.jpg");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackgroundImage = ClientUtils.LoadImage("ImgMain.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            LabEmp.Text = g_sEmpName;
            Show_Role();            
            LVChoose.Items.Clear();
            string sSQL = " select a.role_id,b.role_name,b.role_desc "
                        + " from sajet.sys_role_emp a,sajet.sys_role b "
                        + " where a.emp_id = '" + g_sEmpID + "' "
                        + " and a.role_id = b.role_id "
                        + " and b.enabled='Y' "
                        + " order by b.role_name ";
            dsTemp = ClientUtils.ExecuteSQL(sSQL);
            for (int i = 0; i <= dsTemp.Tables[0].Rows.Count - 1; i++)
            {
                LVChoose.Items.Add(dsTemp.Tables[0].Rows[i]["role_name"].ToString());
                LVChoose.Items[i].Name = dsTemp.Tables[0].Rows[i]["role_name"].ToString();
                LVChoose.Items[i].SubItems.Add(dsTemp.Tables[0].Rows[i]["role_id"].ToString());
                LVChoose.Items[i].ImageIndex = 0;
            }
        }

        private void bbtnSave_Click(object sender, EventArgs e)
        {                    
            string sSQL = " Delete sajet.sys_role_emp "
                        + " Where EMP_ID = '" + g_sEmpID + "' ";
            dsTemp = ClientUtils.ExecuteSQL(sSQL);

            for (int i = 0; i <= LVChoose.Items.Count - 1; i++)
            {
                string sRoleID = LVChoose.Items[i].SubItems[1].Text;
                sSQL = " Insert Into sajet.sys_role_emp "                     
                     + " (ROLE_ID,EMP_ID,UPDATE_USERID) "
                     + " Values "
                     + " ('" + sRoleID + "','" + g_sEmpID + "','" + fMain.g_sUserID + "') ";
                dsTemp = ClientUtils.ExecuteSQL(sSQL);
            }
            DialogResult = DialogResult.OK;
        }

        private void bbtnRemove_Click(object sender, EventArgs e)
        {
            for (int i = LVChoose.Items.Count - 1; i >=0 ; i--)
            {
                if (LVChoose.Items[i].Checked)
                    LVChoose.Items[i].Remove();
            }
        }

        private void bbtnSelectAll1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= LVChoose.Items.Count - 1; i++)
            {
                LVChoose.Items[i].Checked = true;
            }
        }

        private void bbtnSelectNone1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= LVChoose.Items.Count - 1; i++)
            {
                LVChoose.Items[i].Checked = false;
            }
        }
    }
}
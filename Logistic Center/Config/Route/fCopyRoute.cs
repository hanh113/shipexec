using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SajetClass;

namespace CRoute
{
    public partial class fCopyRoute : Form
    {
        public fCopyRoute()
        {
            InitializeComponent();
        }
        
        public string sCopyRouteName,sCopyFrom;

        private void combCopyRoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combCopyRoute.SelectedIndex != -1)
            {
                fMain fRoute = new fMain();
                fRoute.GetRouteDetail(combCopyRoute.Text, TVRoute);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (editNew.Text.Trim() == "")
            {
                string sData = LabName.Text;
                string sMsg = SajetCommon.SetLanguage("Data is null", 2) + Environment.NewLine + sData;
                SajetCommon.Show_Message(sMsg, 0);                
                editNew.Focus();
                editNew.SelectAll();
                return;
            }
            if (combCopyRoute.SelectedIndex == -1)
            {
                string sData = LabCopy.Text;
                string sMsg = SajetCommon.SetLanguage("Data is null", 2) + Environment.NewLine + sData;
                SajetCommon.Show_Message(sMsg, 0);                                  
                return;
            }

            if (combCopyRoute.FindString(editNew.Text) > -1)
            {
                string sData = LabName.Text + " : " + editNew.Text;
                string sMsg = SajetCommon.SetLanguage("Data Duplicate", 2) + Environment.NewLine + sData;
                SajetCommon.Show_Message(sMsg, 0);
                editNew.Focus();
                editNew.SelectAll();
                return;
            }

            sCopyFrom = combCopyRoute.Text;
            sCopyRouteName = editNew.Text;
            DialogResult = DialogResult.OK;
        }

        private void fCopyRoute_Load(object sender, EventArgs e)
        {
            SajetCommon.SetLanguageControl(this);
            panel1.BackgroundImage = ClientUtils.LoadImage("ImgButton.jpg");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            editNew.Text = sCopyRouteName;

            combCopyRoute.Items.Clear();
            string sSQL = "Select Route_Name from sajet.sys_route "
                        + "where enabled='Y' order by route_name ";
            DataSet dtTemp = ClientUtils.ExecuteSQL(sSQL);
            for (int i = 0; i <= dtTemp.Tables[0].Rows.Count - 1; i++)
            {
                combCopyRoute.Items.Add(dtTemp.Tables[0].Rows[i]["Route_Name"].ToString());
            }
            combCopyRoute.SelectedIndex = -1;            
        }        
    }
}
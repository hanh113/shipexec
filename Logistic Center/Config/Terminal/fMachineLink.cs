using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SajetClass;
using SajetFilter;

namespace CTerminal
{
    public partial class fMachineLink : Form
    {
        public fMachineLink()
        {
            InitializeComponent();
        }

        public string g_sFactoryID, g_sLineID, g_sLineName;

        private void button1_Click(object sender, EventArgs e)
        {
            ShowData();
        }

        private void ShowData()
        {
            gvData.Rows.Clear();
            string sSQL = " Select B.PDLINE_NAME,C.STAGE_NAME,D.PROCESS_NAME "
                        + "       ,A.TERMINAL_NAME,A.TGS_CONTROL "
                        + "       ,A.PDLINE_ID,A.STAGE_ID,A.PROCESS_ID,A.TERMINAL_ID "
                        + "       ,A.MACHINE_LINK,decode(A.SMT_SIDE_TYPE,'0','1','1','2') SMT_SIDE "
                        + "       ,E.MACHINE_CODE "
                        + " From SAJET.SYS_TERMINAL A "
                        + " ,SAJET.SYS_PDLINE B "
                        + " ,SAJET.SYS_STAGE C "
                        + " ,SAJET.SYS_PROCESS D "
                        + " ,SAJET.SYS_MACHINE E "
                        + " Where B.FACTORY_ID = '" + g_sFactoryID + "' "
                        + " and A.PDLINE_ID = '" + g_sLineID + "' "
                        + " and A.PDLINE_ID = B.PDLINE_ID(+) "
                        + " and A.STAGE_ID = C.STAGE_ID(+) "
                        + " and A.PROCESS_ID = D.PROCESS_ID(+) "
                        + " and A.MACHINE_ID = E.MACHINE_ID(+) "
                        + " and B.ENABLED = 'Y' "
                        + " and C.ENABLED = 'Y' "
                        + " and D.ENABLED = 'Y' "
                        + " and A.ENABLED = 'Y' ";
            if (editStage.Text != "")
                sSQL = sSQL + " and C.STAGE_NAME = '" + editStage.Text + "'";
            if (editProcess.Text != "")
                sSQL = sSQL + " and D.PROCESS_NAME = '" + editProcess.Text + "'";
            if (combTerminalType.SelectedIndex == 1)
                sSQL = sSQL + " and A.MACHINE_LINK = 'N' and A.TGS_CONTROL = '0' ";
            else if (combTerminalType.SelectedIndex == 2)
                sSQL = sSQL + " and A.MACHINE_LINK = 'N' and A.TGS_CONTROL = '1' ";
            else if (combTerminalType.SelectedIndex == 3)
                sSQL = sSQL + " and A.MACHINE_LINK = 'ATE' ";
            else if (combTerminalType.SelectedIndex == 4)
                sSQL = sSQL + " and A.MACHINE_LINK = 'SMT' ";

            sSQL = sSQL + " Order By PDLINE_NAME,STAGE_CODE,STAGE_NAME,PROCESS_CODE,PROCESS_NAME,TERMINAL_NAME ";
            DataSet dsData = ClientUtils.ExecuteSQL(sSQL);
            foreach (DataRow dr in dsData.Tables[0].Rows)
            {
                gvData.Rows.Add();
                gvData.Rows[gvData.Rows.Count - 1].Tag = dr["TERMINAL_ID"].ToString();
                gvData.Rows[gvData.Rows.Count - 1].Cells["STAGE_NAME"].Value = dr["STAGE_NAME"].ToString();
                gvData.Rows[gvData.Rows.Count - 1].Cells["PROCESS_NAME"].Value = dr["PROCESS_NAME"].ToString();
                gvData.Rows[gvData.Rows.Count - 1].Cells["TERMINAL_NAME"].Value = dr["TERMINAL_NAME"].ToString();
                if (dr["MACHINE_LINK"].ToString() == "N")
                {
                    if (dr["TGS_CONTROL"].ToString() == "1")
                        gvData.Rows[gvData.Rows.Count - 1].Cells["TERMINAL_TYPE"].Value = SajetCommon.SetLanguage("DCN");
                    else
                        gvData.Rows[gvData.Rows.Count - 1].Cells["TERMINAL_TYPE"].Value = SajetCommon.SetLanguage("PC");
                    gvData.Rows[gvData.Rows.Count - 1].Cells["SMT_SIDE"].Value = "";
                    gvData.Rows[gvData.Rows.Count - 1].Cells["MACHINE_CODE"].Value = "";         
                }      
                else
                {
                    if (dr["MACHINE_LINK"].ToString() == "ATE")
                        gvData.Rows[gvData.Rows.Count - 1].Cells["TERMINAL_TYPE"].Value = SajetCommon.SetLanguage("ATE Link");
                    else if (dr["MACHINE_LINK"].ToString() == "SMT")
                        gvData.Rows[gvData.Rows.Count - 1].Cells["TERMINAL_TYPE"].Value = SajetCommon.SetLanguage("SMT Machine");
                    gvData.Rows[gvData.Rows.Count - 1].Cells["SMT_SIDE"].Value = dr["SMT_SIDE"].ToString();
                    gvData.Rows[gvData.Rows.Count - 1].Cells["MACHINE_CODE"].Value = dr["MACHINE_CODE"].ToString();                
                }                
                
            }
        }

        private void fMachineLink_Load(object sender, EventArgs e)
        {            
            labLine.Text = g_sLineName;

            combTerminalType.Items.Clear();
            combTerminalType.Items.Add("");
            combTerminalType.Items.Add("PC");
            combTerminalType.Items.Add("DCN");
            combTerminalType.Items.Add("ATE Link");
            combTerminalType.Items.Add("SMT Machine");

            ShowData();

            SajetCommon.SetLanguageControl(this);
            panel1.BackgroundImage = ClientUtils.LoadImage("ImgFilter.jpg");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
        }


        private void btnSearchStage_Click(object sender, EventArgs e)
        {
            string sSQL = "select distinct b.stage_name "
                 + " from sajet.sys_terminal a,sajet.sys_stage b "
                 + " where a.enabled = 'Y' "
                 + " and b.enabled = 'Y' "
                 + " and a.stage_id = b.stage_id(+) "
                 + " Order By b.stage_name ";
            fFilter f = new fFilter();
            f.sSQL = sSQL;
            if (f.ShowDialog() == DialogResult.OK)
            {
                editStage.Text = f.dgvData.CurrentRow.Cells["stage_name"].Value.ToString();
            }
        }

        private void btnSearchProcess_Click(object sender, EventArgs e)
        {
            string sSQL = "select distinct b.process_name "
                 + " from sajet.sys_terminal a,sajet.sys_process b "
                 + " where a.enabled = 'Y' "
                 + " and b.enabled = 'Y' "
                 + " and a.process_id = b.process_id(+) "
                 + " Order By b.process_name ";
            fFilter f = new fFilter();
            f.sSQL = sSQL;
            if (f.ShowDialog() == DialogResult.OK)
            {
                editProcess.Text = f.dgvData.CurrentRow.Cells["process_name"].Value.ToString();
            }
        }

        private void combTerminalType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
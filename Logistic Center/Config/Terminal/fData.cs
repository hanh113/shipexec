using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SajetClass;
using SajetFilter;
using System.Collections.Specialized;

namespace CTerminal
{
    public partial class fData : Form
    {
        public fData()
        {
            InitializeComponent();
        }
        public string g_sTerminalID, g_sTerminalType, g_sLineID, g_sUpdateType;
        public string g_sProcessID;
        public string g_sMachineID, g_sATELink, g_sSideType;
        StringCollection g_tsSide;

        private void btnOK_Click(object sender, EventArgs e)
        {
            editTerminal.Text = editTerminal.Text.Trim();
            editMachine.Text = editMachine.Text.Trim();

            if (editTerminal.Text == "")
            {                
                string sData = LabTerminal.Text;
                string sMsg = SajetCommon.SetLanguage("Data is null") + Environment.NewLine + sData;
                SajetCommon.Show_Message(sMsg, 0);
                editTerminal.Focus();                
                return;
            }
            if (rdbtnMachine.Checked && editMachine.Text == "")
            {
                string sData = SajetCommon.SetLanguage("Machine");
                string sMsg = SajetCommon.SetLanguage("Data is null") + Environment.NewLine + sData;
                SajetCommon.Show_Message(sMsg, 0);                
                editMachine.Focus();
                return;
            }
            if (rdbtnSMT.Checked)
            {
                if (editSMTMachine.Text == "")
                {
                    string sData = SajetCommon.SetLanguage("SMT Machine");
                    string sMsg = SajetCommon.SetLanguage("Data is null") + Environment.NewLine + sData;
                    SajetCommon.Show_Message(sMsg, 0);
                    editSMTMachine.Focus();
                    return;
                }
                if (combSide.SelectedIndex == -1)
                {
                    string sData = SajetCommon.SetLanguage("SMT Side");
                    string sMsg = SajetCommon.SetLanguage("Data is null") + Environment.NewLine + sData;
                    SajetCommon.Show_Message(sMsg, 0);
                    combSide.Focus();
                    return;
                }
            }
            //Terminal是否重複
            string sSQL = " Select terminal_id from SAJET.SYS_TERMINAL "
                        + " Where PDLINE_ID = '" + g_sLineID + "' "
                        + " and Terminal_Name = '" + editTerminal.Text + "' "
                        + " and terminal_id <> '" + g_sTerminalID + "' ";
            DataSet DS = ClientUtils.ExecuteSQL(sSQL);;
            if (DS.Tables[0].Rows.Count > 0)
            {
                string sData = LabTerminal.Text + " : " + editTerminal.Text;
                string sMsg = SajetCommon.SetLanguage("Data Duplicate") + Environment.NewLine + sData;
                SajetCommon.Show_Message(sMsg, 0);                                
                editTerminal.Focus();
                editTerminal.SelectAll();
                return;
            }

            string typename = string.Empty;
            string strsql = "SELECT A.TYPE_NAME FROM SAJET.SYS_OPERATE_TYPE A,SAJET.SYS_PROCESS B WHERE A.OPERATE_ID=B.OPERATE_ID  AND B.PROCESS_ID='" + g_sProcessID + "'";
            DataTable dt = ClientUtils.ExecuteSQL(strsql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                typename = dt.Rows[0]["TYPE_NAME"].ToString();
                
            }

            if (editTerminal.Text.StartsWith("ITKS") || editTerminal.Text.StartsWith("ITJX") || editTerminal.Text.StartsWith("ITBG") || typename == "PDCA")
            {
                string sqlstr = " Select terminal_id from SAJET.SYS_TERMINAL "
                            + " Where Terminal_Name = '" + editTerminal.Text + "' "
                            + " and terminal_id <> '" + g_sTerminalID + "' ";
                DataSet dstemp = ClientUtils.ExecuteSQL(sqlstr); 
                if (dstemp.Tables[0].Rows.Count > 0)
                {
                    string sData = LabTerminal.Text + " : " + editTerminal.Text;
                    string sMsg = SajetCommon.SetLanguage("Test Station Data Duplicate") + Environment.NewLine + sData;
                    SajetCommon.Show_Message(sMsg, 0);
                    editTerminal.Focus();
                    editTerminal.SelectAll();
                    return;
                }
            }



            g_sATELink = "N";
            g_sMachineID = "0";
            g_sSideType = "";
            if (rdbtnPC.Checked)
                g_sTerminalType = "0";
            else 
                g_sTerminalType = "1";

            if (rdbtnMachine.Checked)
            {
                g_sATELink = "ATE";
                g_sMachineID = GetMachineID(editMachine.Text);
                if (g_sMachineID == "0")
                    return;
            }
            else if (rdbtnSMT.Checked)
            {
                g_sATELink = "SMT";
                g_sMachineID = SajetCommon.GetID("SAJET.SYS_MACHINE","MACHINE_ID","MACHINE_CODE",editSMTMachine.Text);
                if (g_sMachineID == "0")
                {
                    string sMsg = SajetCommon.SetLanguage("Data Error") + Environment.NewLine
                                + editSMTMachine.Text;
                    SajetCommon.Show_Message(sMsg, 0);
                    return;
                }
                g_sSideType = g_tsSide[combSide.SelectedIndex].ToString();                
            }

            if (rdbtnMachine.Checked || rdbtnSMT.Checked)
            {
                if (!Check_MachineTerminal())
                    return;
            }
            DialogResult = DialogResult.OK;
        }
        private bool Check_MachineTerminal()
        {
            //同個Machine只可在相同Line下
            string sSQL = "Select distinct a.pdline_id,b.pdline_name from sajet.sys_terminal a,sajet.sys_pdline b "
                        + "where a.machine_id = '" + g_sMachineID + "' "
                        + "and a.pdline_id = b.pdline_id(+) ";
            DataSet dsTemp = ClientUtils.ExecuteSQL(sSQL);
            if (dsTemp.Tables[0].Rows.Count >= 2)
            {
                SajetCommon.Show_Message("Machine must at only one Line",0);
                return false;
            }
            else if (dsTemp.Tables[0].Rows.Count == 1)
            {
                if (dsTemp.Tables[0].Rows[0]["pdline_id"].ToString() != g_sLineID)
                {
                    string sMsg = SajetCommon.SetLanguage("Machine had at other Line")+Environment.NewLine
                                + dsTemp.Tables[0].Rows[0]["pdline_name"].ToString();
                    SajetCommon.Show_Message(sMsg, 0);
                    return false;
                }                
            }

            if (rdbtnSMT.Checked)
            {
                sSQL = "Select * from sajet.sys_terminal "
                     + "where machine_id = '" + g_sMachineID + "' ";
                if (g_sUpdateType == "Modify")
                    sSQL = sSQL + " and Terminal_id <> '" + g_sTerminalID + "'";
                dsTemp = ClientUtils.ExecuteSQL(sSQL);
                //機台+正反面不可重複
                DataRow[] dr = dsTemp.Tables[0].Select("SMT_SIDE_TYPE='" + g_sSideType + "'");
                if (dr.Length > 0)
                {
                    string sMsg = SajetCommon.SetLanguage("Data Duplicate") + Environment.NewLine
                                + SajetCommon.SetLanguage("Machine") + ":" + editSMTMachine.Text + Environment.NewLine
                                + SajetCommon.SetLanguage("Side") + ":" + combSide.Text + Environment.NewLine
                                + SajetCommon.SetLanguage("Terminal") + ":" + dr[0]["TERMINAL_NAME"].ToString();

                    SajetCommon.Show_Message(sMsg, 0);
                    return false;
                }

                //設定第二面時,必須先設定好第一面
                if (combSide.SelectedIndex == 1)
                {
                    string sFilter = "SMT_SIDE_TYPE='" + g_tsSide[0].ToString() + "'";
                    DataRow[] dr1 = dsTemp.Tables[0].Select(sFilter);
                    if (dr1.Length == 0)
                    {
                        SajetCommon.Show_Message("Please define side 1", 0);
                        return false;
                    }
                }
            }
            return true;
        }
        private string GetMachineID(string sMachineCode)
        {
            string sSQL = "";
            sSQL = "Select machine_id,Enabled "
                 + "from sajet.sys_machine "
                 + "where machine_code = '" + sMachineCode + "'";
            DataSet dsTemp = ClientUtils.ExecuteSQL(sSQL);
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                if (dsTemp.Tables[0].Rows[0]["ENABLED"].ToString() != "Y")
                {
                    SajetCommon.Show_Message("Machine disabled", 0);
                    return "0";
                }
                return dsTemp.Tables[0].Rows[0]["machine_id"].ToString();
            }
            else
            {
                //自動填至Machine Table
                string sMaxMachineID = SajetCommon.GetMaxID("SAJET.SYS_MACHINE", "MACHINE_ID", 8);
                sSQL = "Insert Into sajet.sys_machine "
                     + "(machine_id,machine_code,machine_loc,update_userid) "
                     + "Values "
                     + "('" + sMaxMachineID + "','" + sMachineCode + "','" + g_sLineID + "','" + fMain.g_sUserID + "')";
                ClientUtils.ExecuteSQL(sSQL);
                return sMaxMachineID;
            }
        }

        private void fData_Load(object sender, EventArgs e)
        {
            SajetCommon.SetLanguageControl(this);
            panel2.BackgroundImage = ClientUtils.LoadImage("ImgButton.jpg");
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackgroundImage = ClientUtils.LoadImage("ImgMain.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            g_tsSide = new StringCollection();

            combSide.Items.Clear();
            combSide.Items.Add("1");
            combSide.Items.Add("2");
            g_tsSide.Add("0");
            g_tsSide.Add("1");

            if (g_sTerminalType == "0") //For PC
                rdbtnPC.Checked = true;
            else if (g_sTerminalType == "1") //For DCN
                rdbtnDCN.Checked = true;
            if (g_sATELink == "ATE")  //For ATE
                rdbtnMachine.Checked = true; 
            else if (g_sATELink == "SMT")  //For SMT
            {
                rdbtnSMT.Checked = true;
                if (g_sSideType == "")
                    combSide.SelectedIndex = -1;
                else
                    combSide.SelectedIndex = Convert.ToInt32(g_sSideType);
            }
        }        

        private void btnSearchMachine_Click(object sender, EventArgs e)
        {
            string sSQL = " select a.machine_code,a.machine_desc,b.machine_type_name "
                 + " from sajet.sys_machine a,sajet.sys_machine_type b "
                 + " where a.enabled = 'Y' "
                 + " and a.machine_type_id = b.machine_type_id(+) "
                 + " Order By a.machine_code ";
            fFilter f = new fFilter();           
            f.sSQL = sSQL;
            if (f.ShowDialog() == DialogResult.OK)
            {
                editMachine.Text = f.dgvData.CurrentRow.Cells["machine_code"].Value.ToString();               
            }
        }

        private void rdbtnMachine_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtnMachine.Checked)
            {
                if (editMachine.Text == "")
                    editMachine.Text = editTerminal.Text;
                editMachine.Enabled = true;
            }
            else
            {               
                editMachine.Enabled = false;
            }
        }

        private void editMachine_EnabledChanged(object sender, EventArgs e)
        {
            btnSearchMachine.Enabled = editMachine.Enabled;
        }

        private void btnSearchSMT_Click(object sender, EventArgs e)
        {
            string sSQL = " select a.machine_code,a.machine_desc,b.machine_type_name "                
                 + " from sajet.sys_machine a,sajet.sys_machine_type b "
                 + " where a.enabled = 'Y' "
                 + " and a.machine_type_id = b.machine_type_id(+) "
                 + " Order By a.machine_code ";
            fFilter f = new fFilter();
            f.sSQL = sSQL;
            if (f.ShowDialog() == DialogResult.OK)
            {
                editSMTMachine.Text = f.dgvData.CurrentRow.Cells["machine_code"].Value.ToString();
                //KeyPressEventArgs sKey = new KeyPressEventArgs((char)Keys.Return);
                //editSMTMachine_KeyPress(sender, sKey);
            }
        }

        private void rdbtnSMT_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtnSMT.Checked)
            {                                
                editSMTMachine.Enabled = true;
            }
            else
            {
                editSMTMachine.Enabled = false;
            }
        }

        private void editSMTMachine_EnabledChanged(object sender, EventArgs e)
        {
            btnSearchSMT.Enabled = editSMTMachine.Enabled;
            combSide.Enabled = editSMTMachine.Enabled;
        }

        private void editSMTMachine_KeyPress(object sender, KeyPressEventArgs e)
        {
            combSide.SelectedIndex = -1;
            if (e.KeyChar != (char)Keys.Return)
                return;

            string sSQL = " select machine_id "                
                 + " from sajet.sys_machine "
                 + " where enabled = 'Y' "
                 + " and machine_code = '" + editSMTMachine.Text + "' ";
            DataSet dsTemp = ClientUtils.ExecuteSQL(sSQL);

            if (dsTemp.Tables[0].Rows.Count == 0)
            {
                string sMsg = SajetCommon.SetLanguage("Data Error") + Environment.NewLine
                            + editSMTMachine.Text;
                SajetCommon.Show_Message(sMsg,0);
                return;
            }           
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using SajetClass;
using SajetTable;

namespace CEmp
{
    public partial class fData : Form
    {
        public fData()
        {
            InitializeComponent();
        }
        public string g_sUpdateType;
        public string g_sKeyID;
        public DataGridViewRow dataCurrentRow;
        string sSQL;
        DataSet dsTemp;
        bool bAppendSucess = false;
        public string g_sFactoryID, g_sFactoryName, g_sformText;

        private void fData_Load(object sender, EventArgs e)
        {
            SajetCommon.SetLanguageControl(this);
            panel2.BackgroundImage = ClientUtils.LoadImage("ImgButton.jpg");
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackgroundImage = ClientUtils.LoadImage("ImgMain.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Text = g_sformText;
            LabFactoryName.Text = g_sFactoryName;            

            //Dept
            combDept.Items.Clear();
            combDept.Items.Add("");
            sSQL = " select dept_name from sajet.sys_dept "
                 + " where enabled = 'Y' "
                 + " order by dept_name ";
            dsTemp = ClientUtils.ExecuteSQL(sSQL);
            for (int i = 0; i <= dsTemp.Tables[0].Rows.Count - 1; i++)
            {
                combDept.Items.Add(dsTemp.Tables[0].Rows[i]["DEPT_NAME"].ToString());
            }             
            //Shift
            combShift.Items.Clear();
            combShift.Items.Add("");
            sSQL = " select shift_name from sajet.sys_shift "
                 + " where enabled = 'Y' "
                 + " order by shift_name ";
            dsTemp = ClientUtils.ExecuteSQL(sSQL);
            for (int i = 0; i <= dsTemp.Tables[0].Rows.Count - 1; i++)
            {
                combShift.Items.Add(dsTemp.Tables[0].Rows[i]["shift_name"].ToString());
            }

            dtQuitDate.Value = DateTime.Today;
            dtQuitDate.Checked = false;
            if (g_sUpdateType == "MODIFY")
            {
                g_sKeyID = dataCurrentRow.Cells[TableDefine.gsDef_KeyField].Value.ToString();

                editNo.Text = dataCurrentRow.Cells["EMP_NO"].Value.ToString();
                editName.Text = dataCurrentRow.Cells["EMP_NAME"].Value.ToString();
                combDept.SelectedIndex = combDept.Items.IndexOf(dataCurrentRow.Cells["DEPT_NAME"].Value.ToString());
                combShift.SelectedIndex = combShift.Items.IndexOf(dataCurrentRow.Cells["SHIFT_NAME"].Value.ToString());
                editEMail.Text = dataCurrentRow.Cells["EMAIL"].Value.ToString();
                editHost.Text = dataCurrentRow.Cells["HOST_NAME"].Value.ToString();
                editRemark.Text = dataCurrentRow.Cells["REMARK"].Value.ToString();
                memoList.Text = dataCurrentRow.Cells["TRAINING_LIST"].Value.ToString();

                string sQuitDate = dataCurrentRow.Cells["QUIT_DATE"].Value.ToString();
                if (sQuitDate == "")
                    dtQuitDate.Checked = false;
                else
                {
                    dtQuitDate.Checked = true;
                    dtQuitDate.Value = DateTime.Parse(sQuitDate);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= panelControl.Controls.Count - 1; i++)
            {
                if (panelControl.Controls[i] is TextBox)
                {
                    panelControl.Controls[i].Text = panelControl.Controls[i].Text.Trim();
                }
            }

            if (editNo.Text == "")
            {
                string sData = LabNo.Text;
                string sMsg = SajetCommon.SetLanguage("Data is null", 2) + Environment.NewLine + sData;
                SajetCommon.Show_Message(sMsg, 0);
                editNo.Focus();
                editNo.SelectAll();
                return;
            }  
            if (editName.Text == "")
            {
                string sData = LabName.Text;
                string sMsg = SajetCommon.SetLanguage("Data is null", 2) + Environment.NewLine + sData;
                SajetCommon.Show_Message(sMsg, 0);
                editName.Focus();
                editName.SelectAll();
                return;
            }

            //檢查No是否重複
            sSQL = " Select * from " + TableDefine.gsDef_Table + " "
                 + " Where EMP_NO = '" + editNo.Text + "' ";
            if (g_sUpdateType == "MODIFY")
                sSQL = sSQL + " and " + TableDefine.gsDef_KeyField + " <> '" + g_sKeyID + "'";
            dsTemp = ClientUtils.ExecuteSQL(sSQL);
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                string sData = LabNo.Text + " : " + editNo.Text;
                string sMsg = SajetCommon.SetLanguage("Data Duplicate", 2)
                            + Environment.NewLine + sData;
                SajetCommon.Show_Message(sMsg, 0);
                editNo.Focus();
                editNo.SelectAll();
                return;
            }
            //檢查Name是否重複
            sSQL = " Select * from " + TableDefine.gsDef_Table + " "
                 + " Where EMP_NAME = '" + editName.Text + "' ";
            if (g_sUpdateType == "MODIFY")
                sSQL = sSQL + " and " + TableDefine.gsDef_KeyField + " <> '" + g_sKeyID + "'";
            dsTemp = ClientUtils.ExecuteSQL(sSQL);
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                string sData = LabName.Text + " : " + editName.Text;
                string sMsg = SajetCommon.SetLanguage("Data Duplicate", 2) 
                            + Environment.NewLine + sData;
                SajetCommon.Show_Message(sMsg, 0);
                editName.Focus();
                editName.SelectAll();
                return;
            }

            //Update DB
            try
            {
                if (g_sUpdateType == "APPEND")
                {
                    AppendData();
                    bAppendSucess = true;
                    string sMsg = SajetCommon.SetLanguage("Data Append OK", 2) + " !" 
                                + Environment.NewLine 
                                + SajetCommon.SetLanguage("Append Other Data", 2) + " ?";
                    if (SajetCommon.Show_Message(sMsg, 2) == DialogResult.Yes)
                    {
                        ClearData();
                        memoList.Clear();
                        
                        editNo.Focus();
                        return;
                    }
                    DialogResult = DialogResult.OK;
                }
                else if (g_sUpdateType == "MODIFY")
                {
                    ModifyData();
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                SajetCommon.Show_Message("Exception : " + ex.Message, 0);
                return;
            }
        }

        private void AppendData()
        {
            string sMaxID = SajetCommon.GetMaxID(TableDefine.gsDef_Table, TableDefine.gsDef_KeyField, 8);
            string sShiftID = GetID("SAJET.SYS_SHIFT","SHIFT_ID","SHIFT_NAME",combShift.Text);
            string sDeptID = GetID("SAJET.SYS_DEPT", "DEPT_ID", "DEPT_NAME", combDept.Text);                  

            object[][] Params = new object[13][];
            sSQL = " Insert into SAJET.SYS_EMP "
                 + " (EMP_ID,EMP_NO,EMP_NAME,SHIFT,DEPT_ID,FACTORY_ID,PASSWD "
                 + " ,EMAIL,HOST_NAME,REMARK,TRAINING_LIST,QUIT_DATE "
                 + " ,ENABLED,UPDATE_USERID,UPDATE_TIME) "
                 + " Values "
                 + " (:EMP_ID,:EMP_NO,:EMP_NAME,:SHIFT,:DEPT_ID,:FACTORY_ID,sajet.password.encrypt(:PASSWD) "
                 + " ,:EMAIL,:HOST_NAME,:REMARK,:TRAINING_LIST,:QUIT_DATE "
                 + " ,'Y',:UPDATE_USERID,SYSDATE) ";
            Params[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "EMP_ID", sMaxID };
            Params[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "EMP_NO", editNo.Text };
            Params[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "EMP_NAME", editName.Text };
            Params[3] = new object[] { ParameterDirection.Input, OracleType.VarChar, "SHIFT", sShiftID };
            Params[4] = new object[] { ParameterDirection.Input, OracleType.VarChar, "DEPT_ID", sDeptID };
            Params[5] = new object[] { ParameterDirection.Input, OracleType.VarChar, "FACTORY_ID", g_sFactoryID };
            Params[6] = new object[] { ParameterDirection.Input, OracleType.VarChar, "PASSWD", editNo.Text };
            Params[7] = new object[] { ParameterDirection.Input, OracleType.VarChar, "EMAIL",  editEMail.Text};
            Params[8] = new object[] { ParameterDirection.Input, OracleType.VarChar, "HOST_NAME", editHost.Text };
            Params[9] = new object[] { ParameterDirection.Input, OracleType.VarChar, "REMARK", editRemark.Text};
            Params[10] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TRAINING_LIST", memoList.Text };
            if (dtQuitDate.Checked)
                Params[11] = new object[] { ParameterDirection.Input, OracleType.DateTime, "QUIT_DATE", dtQuitDate.Value };
            else
                Params[11] = new object[] { ParameterDirection.Input, OracleType.VarChar, "QUIT_DATE", "" };          
            Params[12] = new object[] { ParameterDirection.Input, OracleType.VarChar, "UPDATE_USERID", fMain.g_sUserID };            
            dsTemp = ClientUtils.ExecuteSQL(sSQL, Params);

            fMain.CopyToHistory(sMaxID);
        }
        private void ModifyData()
        {
            string sShiftID = GetID("SAJET.SYS_SHIFT", "SHIFT_ID", "SHIFT_NAME", combShift.Text);
            string sDeptID = GetID("SAJET.SYS_DEPT", "DEPT_ID", "DEPT_NAME", combDept.Text);            

            object[][] Params = new object[12][];
            sSQL = " Update SAJET.SYS_EMP "
                 + " set EMP_NO = :EMP_NO "
                 + "    ,EMP_NAME = :EMP_NAME "
                 + "    ,SHIFT = :SHIFT "
                 + "    ,DEPT_ID = :DEPT_ID "
                 + "    ,FACTORY_ID = :FACTORY_ID "
                 + "    ,EMAIL = :EMAIL "
                 + "    ,HOST_NAME = :HOST_NAME "
                 + "    ,REMARK = :REMARK "
                 + "    ,TRAINING_LIST = :TRAINING_LIST "
                 + "    ,QUIT_DATE = :QUIT_DATE "
                 + "    ,UPDATE_USERID = :UPDATE_USERID "
                 + "    ,UPDATE_TIME = SYSDATE "
                 + " where EMP_ID = :EMP_ID ";            
            Params[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "EMP_NO", editNo.Text };
            Params[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "EMP_NAME", editName.Text };
            Params[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "SHIFT", sShiftID };
            Params[3] = new object[] { ParameterDirection.Input, OracleType.VarChar, "DEPT_ID", sDeptID };
            Params[4] = new object[] { ParameterDirection.Input, OracleType.VarChar, "FACTORY_ID", g_sFactoryID };
            Params[5] = new object[] { ParameterDirection.Input, OracleType.VarChar, "EMAIL", editEMail.Text };
            Params[6] = new object[] { ParameterDirection.Input, OracleType.VarChar, "HOST_NAME", editHost.Text };
            Params[7] = new object[] { ParameterDirection.Input, OracleType.VarChar, "REMARK", editRemark.Text };
            Params[8] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TRAINING_LIST", memoList.Text };
            if (dtQuitDate.Checked)
                Params[9] = new object[] { ParameterDirection.Input, OracleType.DateTime, "QUIT_DATE", dtQuitDate.Value };
            else
                Params[9] = new object[] { ParameterDirection.Input, OracleType.VarChar, "QUIT_DATE", "" };          
            Params[10] = new object[] { ParameterDirection.Input, OracleType.VarChar, "UPDATE_USERID", fMain.g_sUserID };            
            Params[11] = new object[] { ParameterDirection.Input, OracleType.VarChar, "EMP_ID", g_sKeyID };
            dsTemp = ClientUtils.ExecuteSQL(sSQL, Params);

            fMain.CopyToHistory(g_sKeyID);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (bAppendSucess)
                DialogResult = DialogResult.OK;
        }

        private string GetID(string sTable,string sID,string sField,string sValue)
        {
            string sResID = "0";
            if (sValue != "")
            {
                sSQL = " select " + sID + " from " + sTable
                     + " where " + sField + " = '" + sValue + "' ";
                dsTemp = ClientUtils.ExecuteSQL(sSQL);
                if (dsTemp.Tables[0].Rows.Count > 0)
                    sResID = dsTemp.Tables[0].Rows[0][sID].ToString();
            }
            return sResID;
        }

        private void ClearData()
        {
            for (int i = 0; i <= panelControl.Controls.Count - 1; i++)
            {
                if (panelControl.Controls[i] is TextBox)
                {
                    panelControl.Controls[i].Text = "";
                }
                else if (panelControl.Controls[i] is ComboBox)
                {
                    ((ComboBox)panelControl.Controls[i]).SelectedIndex = -1;
                }
            }
        }
    }
}
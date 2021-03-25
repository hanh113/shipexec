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

namespace CDept
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
            LabFactoryName.Text = g_sFactoryName;
            this.Text = g_sformText;
            if (g_sUpdateType == "MODIFY")
            {
                g_sKeyID = dataCurrentRow.Cells[TableDefine.gsDef_KeyField].Value.ToString();

                editName.Text = dataCurrentRow.Cells["DEPT_NAME"].Value.ToString();
                editDesc.Text = dataCurrentRow.Cells["DEPT_DESC"].Value.ToString();
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

            if (editName.Text == "")
            {
                string sData = LabName.Text;
                string sMsg = SajetCommon.SetLanguage("Data is null", 2) + Environment.NewLine + sData;
                SajetCommon.Show_Message(sMsg, 0);
                editName.Focus();
                editName.SelectAll();
                return;
            }           
           
            //ÀË¬dName¬O§_­«½Æ
            sSQL = " Select * from " + TableDefine.gsDef_Table + " "
                 + " Where DEPT_NAME = '" + editName.Text + "' ";
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

                        editName.Focus();
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
            string sMaxID = SajetCommon.GetMaxID(TableDefine.gsDef_Table, TableDefine.gsDef_KeyField, 5);

            object[][] Params = new object[5][];
            sSQL = " Insert into SAJET.SYS_DEPT "
                 + " (FACTORY_ID,DEPT_ID,DEPT_NAME,DEPT_DESC"
                 + " ,ENABLED,UPDATE_USERID,UPDATE_TIME) "
                 + " Values "
                 + " (:FACTORY_ID,:DEPT_ID,:DEPT_NAME,:DEPT_DESC "
                 + " ,'Y',:UPDATE_USERID,SYSDATE) ";
            Params[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "FACTORY_ID", g_sFactoryID };
            Params[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "DEPT_ID", sMaxID };
            Params[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "DEPT_NAME", editName.Text };
            Params[3] = new object[] { ParameterDirection.Input, OracleType.VarChar, "DEPT_DESC", editDesc.Text };
            Params[4] = new object[] { ParameterDirection.Input, OracleType.VarChar, "UPDATE_USERID", fMain.g_sUserID };
            dsTemp = ClientUtils.ExecuteSQL(sSQL, Params);

            fMain.CopyToHistory(sMaxID);
        }
        private void ModifyData()
        {
            object[][] Params = new object[4][];
            sSQL = " Update SAJET.SYS_DEPT "
                 + " set DEPT_NAME = :DEPT_NAME "
                 + "    ,DEPT_DESC = :DEPT_DESC "
                 + "    ,UPDATE_USERID = :UPDATE_USERID "
                 + "    ,UPDATE_TIME = SYSDATE "
                 + " where DEPT_ID = :DEPT_ID ";            
            Params[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "DEPT_NAME", editName.Text };
            Params[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "DEPT_DESC", editDesc.Text };
            Params[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "UPDATE_USERID", fMain.g_sUserID };
            Params[3] = new object[] { ParameterDirection.Input, OracleType.VarChar, "DEPT_ID", g_sKeyID };                       
            dsTemp = ClientUtils.ExecuteSQL(sSQL, Params);

            fMain.CopyToHistory(g_sKeyID);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (bAppendSucess)
                DialogResult = DialogResult.OK;
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
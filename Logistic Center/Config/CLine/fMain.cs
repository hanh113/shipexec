using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SajetClass;
using SajetTable;
using System.IO;
using System.Collections.Specialized;

namespace CLine
{
    public partial class fMain : Form
    {
        private MESGridView.Cache memoryCache;

        public fMain()
        {
            InitializeComponent();
        }

        string sSQL;
        public static String g_sUserID;
        public String g_sProgram, g_sFunction;
        String g_sFactoryID;
        String g_sOrderField;
        StringCollection ListFactoryID = new StringCollection();
        string g_sDataSQL;

        private void Initial_Form()
        {
            g_sUserID = ClientUtils.UserPara1;
            g_sProgram = ClientUtils.fProgramName;
            g_sFunction = ClientUtils.fFunctionName;
            g_sOrderField = TableDefine.gsDef_OrderField;

            SajetCommon.SetLanguageControl(this);
            panel1.BackgroundImage = ClientUtils.LoadImage("ImgFilter.jpg");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
        }
        private void fMain_Load(object sender, EventArgs e)
        {
            gvData.Font = new Font("冼极", 10);

            TableDefine.Initial_Table();
            Initial_Form();

            //Factory            
            sSQL = @"Select FACTORY_ID, FACTORY_CODE, FACTORY_NAME
                              From SAJET.SYS_FACTORY
                             Where ENABLED = 'Y'
                             Order By FACTORY_NAME ";
            DataSet dsTemp = ClientUtils.ExecuteSQL(sSQL);
            for (int i = 0; i <= dsTemp.Tables[0].Rows.Count - 1; i++)
            {
                combFactory.Items.Add(dsTemp.Tables[0].Rows[i]["FACTORY_NAME"].ToString());
                ListFactoryID.Add(dsTemp.Tables[0].Rows[i]["FACTORY_ID"].ToString());
            }
            sSQL = " Select NVL(FACTORY_ID,0) FACTORY_ID "
                 + " From SAJET.SYS_EMP "
                 + " Where EMP_ID = '" + g_sUserID + "'";
            dsTemp = ClientUtils.ExecuteSQL(sSQL);
            string sUserFactory = dsTemp.Tables[0].Rows[0]["FACTORY_ID"].ToString();
            if (sUserFactory != "0")
            {
                int iIndex = ListFactoryID.IndexOf(sUserFactory);
                combFactory.SelectedIndex = iIndex;
                combFactory.Enabled = false;
            }
            else
            {
                combFactory.SelectedIndex = 0;
            }

            //
            combShow.SelectedIndex = 0;
            this.Text = this.Text + "(" + SajetCommon.g_sFileVersion + ")";

            //Filter
            combFilter.Items.Clear();
            combFilterField.Items.Clear();
            for (int i = 0; i <= TableDefine.tGridField.Length - 1; i++)
            {
                combFilter.Items.Add(TableDefine.tGridField[i].sCaption);
                combFilterField.Items.Add(TableDefine.tGridField[i].sFieldName);
            }
            if (combFilter.Items.Count > 0)
                combFilter.SelectedIndex = 0;

            Check_Privilege();
        }

        private void Check_Privilege()
        {
            string sPrivilege = ClientUtils.GetPrivilege(g_sUserID, g_sFunction, g_sProgram).ToString();

            btnAppend.Enabled = SajetCommon.CheckEnabled("INSERT", sPrivilege);
            btnModify.Enabled = SajetCommon.CheckEnabled("UPDATE", sPrivilege);
            btnEnabled.Enabled = SajetCommon.CheckEnabled("ENABLED", sPrivilege);
            btnDisabled.Enabled = SajetCommon.CheckEnabled("DISABLED", sPrivilege);
            btnDelete.Enabled = SajetCommon.CheckEnabled("DELETE", sPrivilege);
        }

        private void combShow_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDelete.Visible = (combShow.SelectedIndex == 1);
            btnDisabled.Visible = (combShow.SelectedIndex == 0);
            btnEnabled.Visible = (combShow.SelectedIndex == 1);

            ShowData();
            SetSelectRow(gvData, "", TableDefine.gsDef_KeyField);
        }

        public void ShowData()
        {
            sSQL = string.Format(@"select a.LINE_NAME,A.LINE_DESC,A.EMP_NO,B.FACTORY_CODE,B.FACTORY_NAME,A.CREATE_DATE,A.ENABLED
                      from mesuser.t_line_info a, sajet.sys_factory b
                     where b.factory_id = '{0}'
                       and a.factory_ID = b.factory_ID", g_sFactoryID);
            if (combShow.SelectedIndex == 0)
                sSQL = sSQL + " and a.Enabled = 'Y' ";
            else if (combShow.SelectedIndex == 1)
                sSQL = sSQL + " and a.Enabled = 'N' ";

            if (combFilter.SelectedIndex > -1 && editFilter.Text.Trim() != "")
            {
                string sFieldName = combFilterField.Items[combFilter.SelectedIndex].ToString();
                sSQL = sSQL + " and " + sFieldName + " like '" + editFilter.Text.Trim() + "%'";
            }
            sSQL = sSQL + " order by " + g_sOrderField;
            g_sDataSQL = sSQL;
            (new MESGridView.DisplayGridView()).GetGridView(gvData, sSQL, out memoryCache);

            //欄位Title  
            for (int i = 0; i <= gvData.Columns.Count - 1; i++)
            {
                gvData.Columns[i].Visible = false;
            }
            for (int i = 0; i <= TableDefine.tGridField.Length - 1; i++)
            {
                string sGridField = TableDefine.tGridField[i].sFieldName;

                if (gvData.Columns.Contains(sGridField))
                {
                    gvData.Columns[sGridField].HeaderText = TableDefine.tGridField[i].sCaption;
                    gvData.Columns[sGridField].DisplayIndex = i; //欄位顯示順序
                    gvData.Columns[sGridField].Visible = true;
                }
            }
            gvData.Focus();
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            if (gvData.Rows.Count == 0 || gvData.CurrentRow == null)
                return;

            string sID = gvData.CurrentRow.Cells[TableDefine.gsDef_KeyField].Value.ToString();
            string sType = "";
            string sEnabled = "";
            if (sender == btnDisabled)
            {
                sType = btnDisabled.Text;
                sEnabled = "N";
            }
            else if (sender == btnEnabled)
            {
                sType = btnEnabled.Text;
                sEnabled = "Y";
            }

            string sData = gvData.Columns[TableDefine.gsDef_KeyData].HeaderText + " : " + gvData.CurrentRow.Cells[TableDefine.gsDef_KeyData].Value.ToString();
            string sMsg = sType + " ?" + Environment.NewLine + sData;

            if (SajetCommon.Show_Message(sMsg, 2) != DialogResult.Yes)
                return;

            sSQL = " Update " + TableDefine.gsDef_Table + " "
                 + " set Enabled = '" + sEnabled + "'  "
                 + "    ,EMP_NO = '" + ClientUtils.fLoginUser + "'  "
                 + "    ,CREATE_DATE = SYSDATE  "
                 + " where " + TableDefine.gsDef_KeyField + " = '" + sID + "'";
            ClientUtils.ExecuteSQL(sSQL);
            CopyToHistory(sID);

            ShowData();
            SetSelectRow(gvData, "", TableDefine.gsDef_KeyField);
        }

        private void editFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Return)
                return;

            ShowData();
            SetSelectRow(gvData, "", TableDefine.gsDef_KeyField);

            editFilter.Focus();
        }

        private void btnAppend_Click(object sender, EventArgs e)
        {
            fData f = new fData();
            try
            {
                f.g_sUpdateType = "APPEND";
                f.g_sFactoryID = g_sFactoryID;
                f.g_sFactoryName = combFactory.Text;
                f.g_sformText = btnAppend.Text;
                if (f.ShowDialog() == DialogResult.OK)
                {
                    ShowData();
                    SetSelectRow(gvData, "", TableDefine.gsDef_KeyField);
                }
            }
            finally
            {
                f.Dispose();
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (gvData.Rows.Count == 0 || gvData.CurrentRow == null)
                return;
            fData f = new fData();
            try
            {
                f.g_sUpdateType = "MODIFY";
                f.g_sFactoryID = g_sFactoryID;
                f.g_sFactoryName = combFactory.Text;
                f.dataCurrentRow = gvData.CurrentRow;
                f.g_sformText = btnModify.Text;
                string sSelectKeyValue = gvData.CurrentRow.Cells[TableDefine.gsDef_KeyField].Value.ToString();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    ShowData();
                    SetSelectRow(gvData, sSelectKeyValue, TableDefine.gsDef_KeyField);
                }
            }
            finally
            {
                f.Dispose();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gvData.Rows.Count == 0 || gvData.CurrentRow == null)
                return;

            string sID = gvData.CurrentRow.Cells[TableDefine.gsDef_KeyField].Value.ToString();

            string sData = gvData.Columns[TableDefine.gsDef_KeyData].HeaderText + " : " + gvData.CurrentRow.Cells[TableDefine.gsDef_KeyData].Value.ToString();
            string sMsg = btnDelete.Text + " ?" + Environment.NewLine + sData;
            if (SajetCommon.Show_Message(sMsg, 2) != DialogResult.Yes)
                return;

            sSQL = " Update " + TableDefine.gsDef_Table + " "
                 + " set Enabled = 'D'  "
                 + "    ,EMP_NO = '" + ClientUtils.fLoginUser + "'  "
                 + "    ,CREATE_DATE = SYSDATE  "
                 + " where " + TableDefine.gsDef_KeyField + " = '" + sID + "'";
            ClientUtils.ExecuteSQL(sSQL);
            CopyToHistory(sID);
            sSQL = " Delete " + TableDefine.gsDef_Table + " "
                 + " where " + TableDefine.gsDef_KeyField + " = '" + sID + "'";
            ClientUtils.ExecuteSQL(sSQL);

            ShowData();
            SetSelectRow(gvData, "", TableDefine.gsDef_KeyField);
        }

        public static void CopyToHistory(string sID)
        {
            string sSQL = " Insert into " + TableDefine.gsDef_HTTable + " "
                        + " Select * from " + TableDefine.gsDef_Table + " "
                        + " where " + TableDefine.gsDef_KeyField + " = '" + sID + "' ";
            DataSet dsTemp = ClientUtils.ExecuteSQL(sSQL);

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = "xls";
            saveFileDialog1.Filter = "All Files(*.xls)|*.xls";
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            string sFileName = saveFileDialog1.FileName;

            ExportExcel.CreateExcel Export = new ExportExcel.CreateExcel(sFileName);
            Export.ExportToExcel(gvData);
        }

        private void SetSelectRow(DataGridView GridData, String sPrimaryKey, String sField)
        {
            if (GridData.Rows.Count > 0)
            {
                int iIndex = 0;
                string sShowField = GridData.Columns[0].Name;
                for (int i = 0; i <= GridData.Columns.Count - 1; i++)
                {
                    if (GridData.Columns[i].Visible)
                    {
                        //第一個有顯示的欄位(focus到隱藏欄位會錯誤)
                        sShowField = GridData.Columns[i].Name;
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(sPrimaryKey))
                {
                    string sCondition = "";
                    string[] tsField = sField.Split(',');
                    string[] tsValue = sPrimaryKey.Split(',');
                    for (int j = 0; j <= tsField.Length - 1; j++)
                    {
                        if (j == 0)
                            sCondition = " Where " + tsField[j].ToString() + "='" + tsValue[j].ToString() + "' ";
                        else
                            sCondition = sCondition + " and " + tsField[j].ToString() + "='" + tsValue[j].ToString() + "' ";

                    }
                    //改用SQL找,不由Grid讀值,否則速度會慢
                    string sText = "select idx from ("
                                 + " Select aa.*,rownum-1 idx from ("
                                 + g_sDataSQL
                                 + " ) aa ) "
                                 + sCondition;
                    DataSet ds = ClientUtils.ExecuteSQL(sText);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        iIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["idx"].ToString());
                    }
                }
                GridData.Focus();
           //     GridData.CurrentCell = GridData.Rows[iIndex].Cells[sShowField];
                GridData.Rows[iIndex].Selected = true;
            }
        }

        private void gvData_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            e.Value = memoryCache.RetrieveElement(e.RowIndex, e.ColumnIndex);
        }

        private void MenuHistory_Click(object sender, EventArgs e)
        {
            if (gvData.Rows.Count == 0 || gvData.CurrentRow == null)
                return;

            string sFieldID = gvData.CurrentRow.Cells[TableDefine.gsDef_KeyField].Value.ToString();
            string sSQL = TableDefine.History_SQL(sFieldID);
            DataSet dsTemp = ClientUtils.ExecuteSQL(sSQL);

            fHistory fHistory = new fHistory();
            fHistory.dgvHistory.DataSource = dsTemp;
            fHistory.dgvHistory.DataMember = dsTemp.Tables[0].ToString();

            //替換欄位名稱
            for (int i = 0; i <= fHistory.dgvHistory.Columns.Count - 1; i++)
            {
                string sGridField = fHistory.dgvHistory.Columns[i].HeaderText;
                string sField = "";
                for (int j = 0; j <= gvData.Columns.Count - 1; j++)
                {
                    sField = gvData.Columns[j].Name;
                    if (sGridField == sField)
                    {
                        fHistory.dgvHistory.Columns[i].HeaderText = gvData.Columns[j].HeaderText;
                        break;
                    }
                }
            }

            fHistory.ShowDialog();
            fHistory.Dispose();
        }

        private void gvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex > -1)
            {
                g_sOrderField = gvData.Columns[e.ColumnIndex].Name;
                ShowData();
                SetSelectRow(gvData, "", TableDefine.gsDef_KeyField);
            }
        }

        private void combFactory_SelectedIndexChanged(object sender, EventArgs e)
        {
            g_sFactoryID = ListFactoryID[combFactory.SelectedIndex].ToString();
            ShowData();
        }
    }
}
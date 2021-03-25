using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SajetClass;
using System.Collections.Specialized;

namespace CTerminal
{
    public partial class fMain : Form
    {
        public fMain()
        {           
            InitializeComponent();
        }

        public String g_sFactoryID;
        public String g_sLineID;
        public int g_iPrivilege = 0;
        public static String g_sUserID;
        public String g_sProgram, g_sFunction;
        string sSQL;
        StringCollection g_tsEnabled = new StringCollection();

        private void Initial_Form()
        {
            g_sUserID = ClientUtils.UserPara1;
            g_sProgram = ClientUtils.fProgramName;
            g_sFunction = ClientUtils.fFunctionName;

            SajetCommon.SetLanguageControl(this);
            panel1.BackgroundImage = ClientUtils.LoadImage("ImgFilter.jpg");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
        }

        public void check_privilege()
        {
            string sPrivilege = ClientUtils.GetPrivilege(g_sUserID, g_sFunction, g_sProgram).ToString();
            
            g_iPrivilege = ClientUtils.GetPrivilege(g_sUserID, g_sFunction, g_sProgram);
            
            if (SajetCommon.CheckEnabled("INSERT", sPrivilege))
                g_tsEnabled.Add("INSERT");
            if (SajetCommon.CheckEnabled("UPDATE", sPrivilege))
                g_tsEnabled.Add("UPDATE");
            if (SajetCommon.CheckEnabled("DELETE", sPrivilege))
                g_tsEnabled.Add("DELETE");
            if (SajetCommon.CheckEnabled("DISABLED", sPrivilege))
                g_tsEnabled.Add("DISABLED");
            if (SajetCommon.CheckEnabled("ENABLED", sPrivilege))
                g_tsEnabled.Add("ENABLED");

            MenuItemAdd.Enabled = (g_tsEnabled.IndexOf("INSERT") > -1);
            MenuItemModify.Enabled = (g_tsEnabled.IndexOf("UPDATE") > -1);
            MenuItemDisable.Enabled = (g_tsEnabled.IndexOf("DISABLED") > -1);
            MenuItemChange.Enabled = (g_tsEnabled.IndexOf("UPDATE") > -1);
            MenuItemEnable.Enabled = (g_tsEnabled.IndexOf("ENABLED") > -1);

            btnAppend.Enabled = MenuItemAdd.Enabled;
            btnModify.Enabled = MenuItemModify.Enabled;
            btnDisabled.Enabled = MenuItemDisable.Enabled;
            btnEnabled.Enabled = MenuItemEnable.Enabled;

            TreeViewTerminal.AllowDrop = MenuItemModify.Enabled;
        }

        private void fRoute_Load(object sender, EventArgs e)
        {
            Initial_Form();
            this.Text = this.Text + " (" + SajetCommon.g_sFileVersion + ")";   

            //Select Emp ID
            string sSQL = " SELECT EMP_ID,NVL(FACTORY_ID,0) FACTORY_ID "
                        + " FROM SAJET.SYS_EMP "
                        + " Where EMP_ID = '" + g_sUserID + "'";
            DataSet dsTemp = ClientUtils.ExecuteSQL(sSQL);
            if (dsTemp.Tables[0].Rows.Count == 0)
            {
                SajetCommon.Show_Message("Login User Error", 0);
                return;
            }           
            string sUserFacID = dsTemp.Tables[0].Rows[0]["FACTORY_ID"].ToString();

            //Select Factory
            combFactory.Items.Clear();
            sSQL = " SELECT FACTORY_ID,FACTORY_CODE,FACTORY_NAME "
                 + " FROM SAJET.SYS_FACTORY "
                 + " WHERE ENABLED='Y' "
                 + " ORDER BY FACTORY_CODE ";
            dsTemp = ClientUtils.ExecuteSQL(sSQL); 
            for (int i = 0; i <= dsTemp.Tables[0].Rows.Count - 1; i++)
            {
                string sData = dsTemp.Tables[0].Rows[i]["FACTORY_NAME"].ToString();
                combFactory.Items.Add(sData);
                if (sUserFacID == dsTemp.Tables[0].Rows[i]["FACTORY_ID"].ToString())
                {
                    combFactory.SelectedIndex = i;
                    combFactory.Enabled = false;
                }
            }
            if (sUserFacID == "0" && combFactory.Items.Count>0)
            {
                combFactory.SelectedIndex = 0;
                combFactory.Enabled = true;
            }                        

            ShowProcess();

            check_privilege(); 
            combShow.SelectedIndex = 0;                       
        }

        public void ShowProcess()
        {
            //Show Process
            TreeViewProcess.Nodes.Clear();
            string sStage = "";
            int iCnt = 0;
            string sSQL = " Select NVL(B.STAGE_CODE,'0') STAGE_CODE,B.STAGE_NAME,B.STAGE_ID "
                        + "       ,NVL(A.PROCESS_CODE,'0') PROCESS_CODE,A.PROCESS_NAME,A.PROCESS_ID "
                        + " From SAJET.SYS_PROCESS A "
                        + "      ,SAJET.SYS_STAGE B "
                        + " Where A.STAGE_ID = B.STAGE_ID "
                        + " and A.ENABLED = 'Y' "
                        + " and B.ENABLED = 'Y' "
                        + " Order By NVL(STAGE_CODE,'0'),STAGE_NAME,NVL(PROCESS_CODE,'0'),PROCESS_NAME ";
            DataSet DS = ClientUtils.ExecuteSQL(sSQL);

            for (int i = 0; i <= DS.Tables[0].Rows.Count - 1; i++)
            {
                if (sStage != DS.Tables[0].Rows[i]["STAGE_NAME"].ToString())
                {
                    sStage = DS.Tables[0].Rows[i]["STAGE_NAME"].ToString();
                    TreeNode Node1 = new TreeNode();
                    Node1.Text = sStage;
                    Node1.Tag = DS.Tables[0].Rows[i]["STAGE_ID"].ToString();
                    Node1.Name = Node1.Text;
                    Node1.ImageIndex = 1;
                    TreeViewProcess.Nodes.Add(Node1);
                    iCnt = iCnt + 1;
                }

                TreeNode NodeProcess = new TreeNode();
                NodeProcess.Text = DS.Tables[0].Rows[i]["PROCESS_NAME"].ToString();
                NodeProcess.Tag = DS.Tables[0].Rows[i]["PROCESS_ID"].ToString();
                NodeProcess.Name = NodeProcess.Text;
                NodeProcess.ImageIndex = 2;
                TreeViewProcess.Nodes[iCnt - 1].Nodes.Add(NodeProcess);
            }
        }

        public void ShowTerminal()
        {
            TreeViewTerminal.Nodes.Clear();
            if (string.IsNullOrEmpty(combLine.Text.Trim()))
                return;
            string sLine = combLine.Text;
            
            //Add Line
            TreeNode tNewNode = new TreeNode();
            tNewNode.Text = sLine;
            tNewNode.Tag = g_sLineID;
            tNewNode.ImageIndex = 0;
            TreeViewTerminal.Nodes.Add(tNewNode);

            string sPreStage = "";
            string sPreProcess = "";
            string sSQL = " Select B.PDLINE_NAME,C.STAGE_CODE,C.STAGE_NAME,D.PROCESS_CODE,D.PROCESS_NAME "
                        + "       ,A.TERMINAL_NAME,A.TGS_CONTROL, A.Enabled "
                        + "       ,A.PDLINE_ID,A.STAGE_ID,A.PROCESS_ID,A.TERMINAL_ID "
                        + "       ,A.MACHINE_LINK "
                        + " From SAJET.SYS_TERMINAL A, "
                        + " SAJET.SYS_PDLINE B, "
                        + " SAJET.SYS_STAGE C, "
                        + " SAJET.SYS_PROCESS D "
                        + " Where B.FACTORY_ID = '" + g_sFactoryID + "' "
                        + " and B.PDLINE_NAME = '" + sLine + "' "
                        + " and A.PDLINE_ID = B.PDLINE_ID "
                        + " and A.STAGE_ID = C.STAGE_ID "
                        + " and A.PROCESS_ID = D.PROCESS_ID "
                        + " and B.ENABLED = 'Y' "
                        + " and C.ENABLED = 'Y' "
                        + " and D.ENABLED = 'Y' ";
            if (combShow.SelectedIndex == 0)
                sSQL = sSQL + "and A.ENABLED = 'Y' ";
            else if (combShow.SelectedIndex == 1)
                sSQL = sSQL + "and A.ENABLED = 'N' ";
            sSQL = sSQL + " Order By PDLINE_NAME,STAGE_CODE,STAGE_NAME,PROCESS_CODE,PROCESS_NAME,TERMINAL_NAME ";

            DataSet DS = ClientUtils.ExecuteSQL(sSQL);
            if (DS.Tables[0].Rows.Count == 0)
                return;
            
            for (int i = 0; i <= DS.Tables[0].Rows.Count - 1; i++)
            {
                //Add Stage
                if (sPreStage != DS.Tables[0].Rows[i]["STAGE_NAME"].ToString())
                {
                    TreeNode tNewNodeStage = new TreeNode();
                    tNewNodeStage.Text = DS.Tables[0].Rows[i]["STAGE_NAME"].ToString();
                    tNewNodeStage.Tag = DS.Tables[0].Rows[i]["STAGE_ID"].ToString();
                    tNewNodeStage.ImageIndex = 1;
                    tNewNodeStage.Name = tNewNodeStage.Text;
                    TreeViewTerminal.Nodes[0].Nodes.Add(tNewNodeStage);
                }
                //Add Process
                if (sPreProcess != DS.Tables[0].Rows[i]["PROCESS_NAME"].ToString())
                {
                    TreeNode tNewNodeProcess = new TreeNode();
                    tNewNodeProcess.Text = DS.Tables[0].Rows[i]["PROCESS_NAME"].ToString();
                    tNewNodeProcess.Tag = DS.Tables[0].Rows[i]["PROCESS_ID"].ToString();
                    tNewNodeProcess.ImageIndex = 2;
                    tNewNodeProcess.Name = tNewNodeProcess.Text;
                    TreeViewTerminal.Nodes[0].LastNode.Nodes.Add(tNewNodeProcess);
                }
                //Add Terminal
                TreeNode tNewNodeTerminal = new TreeNode();
                tNewNodeTerminal.Text = DS.Tables[0].Rows[i]["TERMINAL_NAME"].ToString();
                tNewNodeTerminal.Tag = DS.Tables[0].Rows[i]["TERMINAL_ID"].ToString();
                if (DS.Tables[0].Rows[i]["TGS_CONTROL"].ToString() == "1")
                {
                    if (DS.Tables[0].Rows[i]["ENABLED"].ToString() == "Y")
                    {
                        tNewNodeTerminal.ImageIndex = 4;
                        if (DS.Tables[0].Rows[i]["MACHINE_LINK"].ToString() == "ATE")
                            tNewNodeTerminal.ImageIndex = 7;
                        if (DS.Tables[0].Rows[i]["MACHINE_LINK"].ToString() == "SMT")
                            tNewNodeTerminal.ImageIndex = 8;
                    }
                    else
                    {
                        tNewNodeTerminal.ImageIndex = 6;
                        if (DS.Tables[0].Rows[i]["MACHINE_LINK"].ToString() == "ATE")
                            tNewNodeTerminal.ImageIndex = 9;
                        if (DS.Tables[0].Rows[i]["MACHINE_LINK"].ToString() == "SMT")
                            tNewNodeTerminal.ImageIndex = 10;
                    }
                }
                else
                {
                    if (DS.Tables[0].Rows[i]["ENABLED"].ToString() == "Y")
                        tNewNodeTerminal.ImageIndex = 3;
                    else
                        tNewNodeTerminal.ImageIndex = 5;
                }                

                TreeViewTerminal.Nodes[0].LastNode.LastNode.Nodes.Add(tNewNodeTerminal);

                sPreStage = DS.Tables[0].Rows[i]["STAGE_NAME"].ToString();
                sPreProcess = DS.Tables[0].Rows[i]["PROCESS_NAME"].ToString();
            }
            TreeViewTerminal.ExpandAll();
        }

        protected void CopyToHistory(string sTerminalID)
        {
            string sSQL = " Insert Into SAJET.SYS_HT_TERMINAL "
                        + " Select * from SAJET.SYS_TERMINAL "
                        + " Where TERMINAL_ID = '" + sTerminalID + "' ";
            ClientUtils.ExecuteSQL(sSQL);;
        }        

        protected string GetProcessID(string sProcess)
        {
            string sSQL = "Select PROCESS_ID "
                        + "From SAJET.SYS_PROCESS "
                        + "Where PROCESS_NAME = '" + sProcess + "'";
            DataSet DS = ClientUtils.ExecuteSQL(sSQL);;
            if (DS.Tables[0].Rows.Count > 0)
            {
                return DS.Tables[0].Rows[0]["PROCESS_ID"].ToString();
            }
            else { return "0"; }
        }

        private void TreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void TreeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void TreeView_DragDrop(object sender, DragEventArgs e)
        {                                    
            TreeNode SrcNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode"); //來源Node
            if (SrcNode == null)
            { return; }

            //移stage
            if (SrcNode.Level == 0)
            {
                //此Stage在右方是否已存在
                if (TreeViewTerminal.Nodes[0].Nodes.Find(SrcNode.Text, false).Length > 0)
                    return;

                TreeViewTerminal.Nodes[0].Nodes.Add((TreeNode)SrcNode.Clone());
            }
            //移Process
            else if (SrcNode.Level == 1)
            {                
                //先找父節點(Stage)是否存在,若不存在則先建立Stage節點                
                TreeNode[] tFindStageNodes = TreeViewTerminal.Nodes[0].Nodes.Find(SrcNode.Parent.Text, false);
                TreeNode tNode;
                if (tFindStageNodes.Length == 0)
                {
                    TreeNode tStageNode = new TreeNode();
                    tStageNode.Text = SrcNode.Parent.Text;
                    tStageNode.Tag = SrcNode.Parent.Tag;
                    tStageNode.Name = tStageNode.Text;
                    tStageNode.ImageIndex = 1;
                    tStageNode.SelectedImageIndex = tStageNode.ImageIndex;
                    TreeViewTerminal.Nodes[0].Nodes.Add(tStageNode);
                    tNode = TreeViewTerminal.Nodes[0].LastNode;
                }
                else
                {
                    tNode = tFindStageNodes[0];
                }  
              
                //檢查節點(Process)是否已存在
                if (tNode.Nodes.Find(SrcNode.Text, false).Length > 0)
                    return;
                tNode.Nodes.Add((TreeNode)SrcNode.Clone());
            }
        }

        private void PopMenu1_Opening(object sender, CancelEventArgs e)
        {
            TreeNode SelectNode = TreeViewTerminal.SelectedNode;
            if (SelectNode == null)
            { return; }

            if (SelectNode.Level <= 2)
            {
                btnModify.Enabled = false;

                MenuItemAdd.Enabled = false;
                MenuItemModify.Enabled = false;
                MenuItemChange.Enabled = false;
                MenuItemShowTerminalID.Enabled = false;
                MenuItemHistory.Enabled = false;

                if (SelectNode.Level == 2)
                {
                    MenuItemAdd.Enabled = (g_tsEnabled.IndexOf("INSERT") > -1);
                    btnAppend.Enabled = MenuItemAdd.Enabled;
                }
            }
            else //點選Terminal層
            {
                btnAppend.Enabled = (g_tsEnabled.IndexOf("INSERT") > -1);
                btnModify.Enabled = (g_tsEnabled.IndexOf("UPDATE") > -1);

                MenuItemAdd.Enabled = btnAppend.Enabled;
                MenuItemModify.Enabled = btnModify.Enabled;
                MenuItemChange.Enabled = btnModify.Enabled;
                MenuItemShowTerminalID.Enabled = true;
                MenuItemHistory.Enabled = true;
            }                                                 
        }

        private void collapseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeViewProcess.CollapseAll();
        }

        private void expandToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeViewProcess.ExpandAll();
        }

        private void TreeViewTerminal_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeViewTerminal.SelectedNode.SelectedImageIndex = TreeViewTerminal.SelectedNode.ImageIndex;
        }

        private void combLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSQL = " select PDLINE_ID from sajet.sys_pdline "
                        + " where enabled='Y' "
                        + " and PDLINE_NAME = '" + combLine.Text + "' ";
            DataSet dsTemp = ClientUtils.ExecuteSQL(sSQL);;
            g_sLineID = dsTemp.Tables[0].Rows[0]["PDLINE_ID"].ToString();
            
            ShowTerminal();
        }

        private void MenuItemExpand_Click(object sender, EventArgs e)
        {            
            TreeViewTerminal.SelectedNode.Expand();
        }

        private void MenuItemCollapse_Click(object sender, EventArgs e)
        {            
            TreeViewTerminal.SelectedNode.Collapse();
        }

        private void MenuItemShowTerminalID_Click(object sender, EventArgs e)
        {
            if (TreeViewTerminal.SelectedNode == null)
                return;
            if (TreeViewTerminal.SelectedNode.Level != 3)
                return;

            string sTerminalName = TreeViewTerminal.SelectedNode.Text;
            string sTerminalId = TreeViewTerminal.SelectedNode.Tag.ToString();
            string sMsg1 = SajetCommon.SetLanguage("Terminal Name") + " : " + sTerminalName;
            string sMsg2 = SajetCommon.SetLanguage("Terminal ID") + " : " + sTerminalId;
            MessageBox.Show(sMsg1 + Environment.NewLine + sMsg2);
        }

        private void MenuItemChange_Click(object sender, EventArgs e)
        {
            if (TreeViewTerminal.SelectedNode == null)
                return;
            if (TreeViewTerminal.SelectedNode.Level != 3)
                return;

            string sTerminalId = TreeViewTerminal.SelectedNode.Tag.ToString();
            string sTGS_Control = "0";
            //改為For DCN
            if (sender == MenuItemChange)
            {
                string sMsg1 = SajetCommon.SetLanguage("Change this Terminal to DCN") + " ?";
                string sMsg2 = SajetCommon.SetLanguage("Terminal Name") + " : " + TreeViewTerminal.SelectedNode.Text;
                if (SajetCommon.Show_Message(sMsg1 + Environment.NewLine + sMsg2, 2) != DialogResult.Yes)
                    return;
                sTGS_Control = "1";
            }
            else
            {
                string sMsg1 = SajetCommon.SetLanguage("Change this Terminal to PC") + " ?";
                string sMsg2 = SajetCommon.SetLanguage("Terminal Name") + " : " + TreeViewTerminal.SelectedNode.Text;
                if (SajetCommon.Show_Message(sMsg1 + Environment.NewLine + sMsg2, 2) != DialogResult.Yes)
                    return;
                sTGS_Control = "0";
            }

            string sSQL = "Update SAJET.SYS_TERMINAL "
                        + "Set TGS_CONTROL = '" + sTGS_Control + "' "
                        + "   ,UPDATE_USERID = '" + g_sUserID + "' "
                        + "   ,UPDATE_TIME = SYSDATE "
                        + "Where TERMINAL_ID = '" + sTerminalId + "' ";
            ClientUtils.ExecuteSQL(sSQL);;
            CopyToHistory(sTerminalId);

            if (sTGS_Control == "1")
                TreeViewTerminal.SelectedNode.ImageIndex = 4;
            else
                TreeViewTerminal.SelectedNode.ImageIndex = 3;
            TreeViewTerminal.SelectedNode.SelectedImageIndex = TreeViewTerminal.SelectedNode.ImageIndex;
        }

        private void combShow_SelectedIndexChanged(object sender, EventArgs e)
        {
            //PopMenu1.Enabled = true;
            btnDisabled.Visible = false;
            btnEnabled.Visible = false;
            MenuItemDisable.Visible = false;
            MenuItemEnable.Visible = false;
            switch (combShow.SelectedIndex)
            {
                case 0:
                    MenuItemDisable.Visible = true;
            //        MenuItemAdd.Enabled = true;
            //        btnAppend.Enabled = true;
            //        TreeViewTerminal.AllowDrop = true;
                    btnDisabled.Visible = (g_tsEnabled.IndexOf("DISABLED") > -1);
                    break;
                case 1:
                    MenuItemEnable.Visible = true;
            //        MenuItemAdd.Enabled = false;
            //        btnAppend.Enabled = false;
            //        TreeViewTerminal.AllowDrop = false;
                    btnEnabled.Visible = (g_tsEnabled.IndexOf("ENABLED") > -1);
                    break;
                default:
            //        PopMenu1.Enabled = false;
            //        MenuItemAdd.Enabled = false;
            //        btnAppend.Enabled = false;                    
            //        TreeViewTerminal.AllowDrop = false;
                    break;
            }            
            ShowTerminal();
        }

        private void MenuItemHistory_Click(object sender, EventArgs e)
        {            
            if (TreeViewTerminal.SelectedNode == null)
                return;
            if (TreeViewTerminal.SelectedNode.Level != 3)
                return;

            string sFieldID = TreeViewTerminal.SelectedNode.Tag.ToString();
            string sSQL = " Select a.TERMINAL_NAME ,decode(TGS_CONTROL,'1','For DCN','For PC') TGS_CONTROL "
                        + "       ,c.PDLINE_NAME,d.STAGE_NAME,e.PROCESS_NAME,a.ENABLED,b.emp_name,a.UPDATE_TIME "
                        + "       ,a.machine_link,nvl(f.machine_code,'N/A') Machine "
                        + " from SAJET.SYS_HT_TERMINAL a "
                        + "     ,sajet.sys_emp b "
                        + "     ,sajet.sys_pdline c,sajet.sys_stage d,sajet.sys_process e "
                        + "     ,sajet.sys_machine f "
                        + " Where a.TERMINAL_ID = '" + sFieldID + "' "
                        + " and a.update_userid = b.emp_id "
                        + " and a.pdline_id = c.pdline_id "
                        + " and a.stage_id = d.stage_id "
                        + " and a.process_id = e.process_id "
                        + " and a.machine_id = f.machine_id(+) "
                        + " Order By a.Update_Time ";
            DataSet dsTemp = ClientUtils.ExecuteSQL(sSQL);;
            fHistory fHistory = new fHistory();
            fHistory.dgvHistory.DataSource = dsTemp;
            fHistory.dgvHistory.DataMember = dsTemp.Tables[0].ToString();
            fHistory.ShowDialog();
            fHistory.Dispose();
        }

        public void Maintain_Terminal(string sType)
        {
            TreeNode tSelectNode = TreeViewTerminal.SelectedNode;
            if (tSelectNode == null)
                return;
            if (tSelectNode.Level < 2)
                return;
            if (sType == "Modify" && tSelectNode.Level < 3)
                return;

            string sLine = "";
            string sProcess = "";
            string sLineID = "";
            string sStageID = "";
            string sProcessID = "";           
            switch (tSelectNode.Level)
            {
                case 2: //Process Level
                    sLine = tSelectNode.Parent.Parent.Text;
                    sLineID = tSelectNode.Parent.Parent.Tag.ToString();
                    sStageID = tSelectNode.Parent.Tag.ToString();
                    sProcess = tSelectNode.Text;
                    sProcessID = tSelectNode.Tag.ToString();                    
                    break;
                case 3:
                    sLine = tSelectNode.Parent.Parent.Parent.Text;
                    sLineID = tSelectNode.Parent.Parent.Parent.Tag.ToString();
                    sStageID = tSelectNode.Parent.Parent.Tag.ToString();
                    sProcess = tSelectNode.Parent.Text;
                    sProcessID = tSelectNode.Parent.Tag.ToString();                   
                    break;
            }

            string sOldMachineID = "0";
            fData fData = new fData();
            fData.LabLine.Text = sLine;
            fData.g_sLineID = sLineID;
            fData.LabProcess.Text = sProcess;
            fData.g_sUpdateType = sType;
            if (sType == "Append")
            {
                fData.g_sTerminalID = "0";
                fData.g_sProcessID = sProcessID;
                fData.Text = SajetCommon.SetLanguage("Append Terminal");
                fData.editTerminal.Text = GetMaxTerminalName(sProcess, sProcessID, sLineID);
                fData.g_sTerminalType = "0";
                fData.g_sATELink = "";
                fData.g_sMachineID = "0";
                fData.editMachine.Text = "";
                fData.g_sSideType = "N";                
            }
            else
            {
                fData.g_sTerminalID = tSelectNode.Tag.ToString();
                fData.g_sProcessID = sProcessID;
                fData.Text = SajetCommon.SetLanguage("Modify Terminal");
                fData.editTerminal.Text = tSelectNode.Text;

                sSQL = "Select a.*,b.machine_code "
                     + "from sajet.sys_terminal a,sajet.sys_machine b "
                     + "where terminal_id = '" + fData.g_sTerminalID + "'"
                     + "and a.machine_id = b.machine_id(+) ";
                DataSet dsTemp = ClientUtils.ExecuteSQL(sSQL);

                fData.g_sTerminalType = dsTemp.Tables[0].Rows[0]["TGS_CONTROL"].ToString();
                fData.g_sATELink = dsTemp.Tables[0].Rows[0]["MACHINE_LINK"].ToString();
                fData.g_sMachineID = dsTemp.Tables[0].Rows[0]["MACHINE_ID"].ToString();
                fData.g_sSideType = dsTemp.Tables[0].Rows[0]["SMT_SIDE_TYPE"].ToString();
                if (dsTemp.Tables[0].Rows[0]["MACHINE_CODE"].ToString() != "")
                {
                    if (fData.g_sATELink == "SMT")
                        fData.editSMTMachine.Text = dsTemp.Tables[0].Rows[0]["MACHINE_CODE"].ToString();
                    else
                    {
                        fData.g_sATELink = "ATE";
                        fData.editMachine.Text = dsTemp.Tables[0].Rows[0]["MACHINE_CODE"].ToString();
                    }
                }
                sOldMachineID = dsTemp.Tables[0].Rows[0]["MACHINE_ID"].ToString();
            }

            //Show
            if (fData.ShowDialog() != DialogResult.OK)
                return;

            int iImageIndex = 3;
            if (fData.g_sTerminalType == "1")
                iImageIndex = 4;

            if (fData.g_sATELink=="ATE")
                iImageIndex = 7;     
            else if (fData.g_sATELink=="SMT")
                iImageIndex = 8;    
            try
            {
                if (sType == "Append")
                {
                    string typename = string.Empty;
                    string strsql = "SELECT A.TYPE_NAME FROM SAJET.SYS_OPERATE_TYPE A,SAJET.SYS_PROCESS B WHERE A.OPERATE_ID=B.OPERATE_ID  AND B.PROCESS_ID='" + sProcessID + "'";
                    DataTable dt = ClientUtils.ExecuteSQL(strsql).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        typename = dt.Rows[0]["TYPE_NAME"].ToString();
                    }
                    //else
                    //{
                    //    SajetCommon.Show_Message("蜆秶最帤扢离秶最濬倰", 1);
                    //    return;
                    //}
                    //Insert
                    string sNewTerminalName = fData.editTerminal.Text;
                    string sNewTerminalID = SajetCommon.GetMaxID("SAJET.SYS_TERMINAL", "TERMINAL_ID", 8);
                    string sSQL="";
                    if (typename == "PDCA")
                    {
                        sSQL = " Insert Into SAJET.SYS_TERMINAL "
                             + " (CONTROL_ID,TERMINAL_ID,TERMINAL_NAME,PDLINE_ID,PROCESS_ID,STAGE_ID,UPDATE_USERID "
                             + " ,TGS_CONTROL,MACHINE_LINK,MACHINE_ID,SMT_SIDE_TYPE,AAB,AAB_QTY,TERMINAL_QTY) "
                             + " Values "
                             + " ('0','" + sNewTerminalID + "','" + sNewTerminalName + "','" + sLineID + "','" + sProcessID + "','" + sStageID + "','" + g_sUserID + "'"
                             + " ,'" + fData.g_sTerminalType + "','" + fData.g_sATELink + "','" + fData.g_sMachineID + "','" + fData.g_sSideType + "','Y','3','2') ";
                    }
                    else
                    {
                        sSQL = " Insert Into SAJET.SYS_TERMINAL "
                             + " (CONTROL_ID,TERMINAL_ID,TERMINAL_NAME,PDLINE_ID,PROCESS_ID,STAGE_ID,UPDATE_USERID "
                             + " ,TGS_CONTROL,MACHINE_LINK,MACHINE_ID,SMT_SIDE_TYPE) "
                             + " Values "
                             + " ('0','" + sNewTerminalID + "','" + sNewTerminalName + "','" + sLineID + "','" + sProcessID + "','" + sStageID + "','" + g_sUserID + "'"
                             + " ,'" + fData.g_sTerminalType + "','" + fData.g_sATELink + "','" + fData.g_sMachineID + "','" + fData.g_sSideType + "') ";
                    }
                    ClientUtils.ExecuteSQL(sSQL);;
                    CopyToHistory(sNewTerminalID);

                    TreeNode tNode = new TreeNode();
                    tNode.Text = sNewTerminalName;
                    tNode.Tag = sNewTerminalID;                    
                    tNode.ImageIndex = iImageIndex;
                    tNode.SelectedImageIndex =iImageIndex;                                    
                    if (tSelectNode.Level == 2)
                        tSelectNode.Nodes.Add(tNode);
                    else if (tSelectNode.Level == 3)
                        tSelectNode.Parent.Nodes.Add(tNode);
                }
                else if (sType == "Modify")
                {                                        
                    string sNewTerminalName = fData.editTerminal.Text;
                    string sNewTerminalID = tSelectNode.Tag.ToString();
                    string sSQL = "Update SAJET.SYS_TERMINAL "
                                + " set TERMINAL_NAME = '" + sNewTerminalName + "' "
                                + "    ,UPDATE_USERID = '" + g_sUserID + "' "
                                + "    ,UPDATE_TIME = SYSDATE "
                                + "    ,TGS_CONTROL = '" + fData.g_sTerminalType + "' "
                                + "    ,MACHINE_LINK = '" + fData.g_sATELink + "' "
                                + "    ,MACHINE_ID = '" + fData.g_sMachineID + "' "
                                + "    ,SMT_SIDE_TYPE = '" + fData.g_sSideType + "' "
                                + " where TERMINAL_ID = '" + sNewTerminalID + "' ";
                    ClientUtils.ExecuteSQL(sSQL);;
                    CopyToHistory(sNewTerminalID);

                    //當第一面關係解除時,若有第二面,出現提示並同時解除Machine關係
                    sSQL = "Select * from sajet.sys_terminal "
                         + "where machine_id = '" + sOldMachineID + "' "
                         + "and pdline_id = '" + g_sLineID + "' ";
                    DataSet dsCheck = ClientUtils.ExecuteSQL(sSQL);                   
                    if (dsCheck.Tables[0].Select("SMT_SIDE_TYPE='0'").Length == 0)
                    {
                        DataRow[] dr = dsCheck.Tables[0].Select("SMT_SIDE_TYPE='1'");
                        if (dr.Length > 0)
                        {
                            string sTerID = dr[0]["TERMINAL_ID"].ToString();
                            string sTerName = dr[0]["TERMINAL_NAME"].ToString();
                            sSQL = "Update SAJET.SYS_TERMINAL "
                                 + " set MACHINE_ID = '0' "
                                 + "    ,SMT_SIDE_TYPE = '' "
                                 + " where TERMINAL_ID = '" + sTerID + "' ";
                            ClientUtils.ExecuteSQL(sSQL); ;
                            CopyToHistory(sTerID);

                            string sMsg = SajetCommon.SetLanguage("Machine has Side 2,also remove link") + Environment.NewLine
                                        + sTerName;
                            SajetCommon.Show_Message(sMsg, 1);
                        }
                    }

                    tSelectNode.Text = sNewTerminalName;
                    tSelectNode.ImageIndex = iImageIndex;
                    tSelectNode.SelectedImageIndex = iImageIndex;
                    //SajetCommon.Show_Message(sType + " Terminal OK", -1);
                }                
            }
            catch (Exception ex)
            {
                SajetCommon.Show_Message("Exception:"+ex.Message.ToString(),0);
                return;
                
            }
            finally
            {
                fData.Dispose();
            }            
        }

        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            Maintain_Terminal("Append");
        }

        private void MenuItemModify_Click(object sender, EventArgs e)
        {
            Maintain_Terminal("Modify");
        }

        public string GetMaxTerminalName(string sProcess, string sProcessID, string sLineID)
        {
            string sResult = "";
            string sSQL = " Select NVL(MAX(TERMINAL_NAME),'N/A') TERMINAL_NAME "
                        + " From SAJET.SYS_TERMINAL "
                        + " Where PDLINE_ID = '" + sLineID + "' "
                        + " and PROCESS_ID = '" + sProcessID + "' "
                        + " and substr(TERMINAL_NAME,1,length('" + sProcess + "')) = '" + sProcess + "'";
            DataSet dsTemp = ClientUtils.ExecuteSQL(sSQL);;
            string sTerminalName = dsTemp.Tables[0].Rows[0]["TERMINAL_NAME"].ToString();
            if (sTerminalName == "N/A")
            {
                sResult = sProcess + "01";
            }
            else
            {
                string s1 = sTerminalName.Substring(0, sTerminalName.Length - 2);
                string s2 = sTerminalName.Substring(sTerminalName.Length - 2, 2);
                int iNo = 0;
                if (int.TryParse(s2, out iNo))
                {
                    iNo = Convert.ToInt32(s2) + 1;
                }
                else
                {
                    iNo = 1;
                }
                sResult = s1 + iNo.ToString("00");
            }
            return sResult;
        }

        private void MenuItemDisable_Click(object sender, EventArgs e)
        {
            TreeNode tSelectNode = TreeViewTerminal.SelectedNode;
            if (tSelectNode == null)
                return;
            string sType = "";
            string sEnabled = "";
            string sSQL = "";

            if (sender == MenuItemDisable || sender == btnDisabled)
            {
                sType = "Disable";
                sEnabled = "N";
            }
            else
            {
                sType = "Enable";
                sEnabled = "Y";
            }

            //Disable整個Line的Terminal            
            if (tSelectNode.Level == 0)
            {
                string sMsg1 = SajetCommon.SetLanguage(sType);
                string sMsg2 = SajetCommon.SetLanguage("all Terminal on this Production Line");
                string sMsg3 = SajetCommon.SetLanguage("Production Line") + " : " + tSelectNode.Text;
                if (SajetCommon.Show_Message(sMsg1 +" "+ sMsg2+" ?" + Environment.NewLine
                       + sMsg3, 2) != DialogResult.Yes)
                    return;

                string sLineID = tSelectNode.Tag.ToString();
                sSQL = " Update SAJET.SYS_TERMINAL "
                     + " Set ENABLED = '" + sEnabled + "' "
                     + "    ,UPDATE_USERID = '" + g_sUserID + "' "
                     + "    ,UPDATE_TIME = SYSDATE "                     
                     + "    ,MACHINE_ID = 0,SMT_SIDE_TYPE='' " //解除機台關係
                     + " Where PDLINE_ID = '" + sLineID + "' ";
                ClientUtils.ExecuteSQL(sSQL);;
                CopyToHistoryAll(0, sLineID, "", "");

                tSelectNode.Remove();
                return;
            }
            //Disable整個Stage的Terminal
            if (tSelectNode.Level == 1)
            {
                string sMsg1 = SajetCommon.SetLanguage(sType);
                string sMsg2 = SajetCommon.SetLanguage("all Terminal on this Stage");
                string sMsg3 = SajetCommon.SetLanguage("Stage Name") + " : " + tSelectNode.Text;

                if (SajetCommon.Show_Message(sMsg1 + " " + sMsg2 + " ?" + Environment.NewLine
                       + sMsg3, 2) != DialogResult.Yes)
                    return;

                string sLineID = tSelectNode.Parent.Tag.ToString();
                string sStageID = tSelectNode.Tag.ToString();
                sSQL = " Update SAJET.SYS_TERMINAL "
                     + " Set ENABLED = '" + sEnabled + "' "
                     + "    ,UPDATE_USERID = '" + g_sUserID + "' "
                     + "    ,UPDATE_TIME = SYSDATE "
                     + "    ,MACHINE_ID = 0,SMT_SIDE_TYPE='' " //解除機台關係
                     + " Where PDLINE_ID = '" + sLineID + "' "
                     + " and STAGE_ID = '" + sStageID + "' ";
                ClientUtils.ExecuteSQL(sSQL);;
                CopyToHistoryAll(1, sLineID, sStageID, "");

                tSelectNode.Remove();
                return;
            }
            //Disable整個Process的Terminal
            if (tSelectNode.Level == 2)
            {
                string sMsg1 = SajetCommon.SetLanguage(sType);
                string sMsg2 = SajetCommon.SetLanguage("all Terminal on this Process");
                string sMsg3 = SajetCommon.SetLanguage("Process Name") + " : " + tSelectNode.Text;
                if (SajetCommon.Show_Message(sMsg1 + " " + sMsg2 + " ?" + Environment.NewLine
                       + sMsg3, 2) != DialogResult.Yes)
                    return;                

                string sLineID = tSelectNode.Parent.Parent.Tag.ToString();
                string sProcessID = tSelectNode.Tag.ToString();
                sSQL = " Update SAJET.SYS_TERMINAL "
                     + " Set ENABLED = '" + sEnabled + "' "
                     + "    ,UPDATE_USERID = '" + g_sUserID + "' "
                     + "    ,UPDATE_TIME = SYSDATE "
                     + "    ,MACHINE_ID = 0,SMT_SIDE_TYPE='' " //解除機台關係
                     + " Where PDLINE_ID = '" + sLineID + "' "
                     + " and PROCESS_ID = '" + sProcessID + "' ";
                ClientUtils.ExecuteSQL(sSQL);;
                CopyToHistoryAll(2, sLineID, "", sProcessID);

                tSelectNode.Remove();
                return;
            }
            //Disable Terminal
            if (tSelectNode.Level == 3)
            {
                string sMsg1 = SajetCommon.SetLanguage(sType);
                string sMsg2 = SajetCommon.SetLanguage("this Terminal");
                string sMsg3 = SajetCommon.SetLanguage("Terminal Name") + " : " + tSelectNode.Text;
                if (SajetCommon.Show_Message(sMsg1 + " " + sMsg2 + " ?" + Environment.NewLine
                       + sMsg3, 2) != DialogResult.Yes)
                    return;                      
                
                string sTerminalID = tSelectNode.Tag.ToString();
                sSQL = " Update SAJET.SYS_TERMINAL "
                     + " Set ENABLED = '" + sEnabled + "' "
                     + "    ,UPDATE_USERID = '" + g_sUserID + "' "
                     + "    ,UPDATE_TIME = SYSDATE "
                     + "    ,MACHINE_ID = 0,SMT_SIDE_TYPE='' " //解除機台關係
                     + " Where TERMINAL_ID = '" + sTerminalID + "' ";                     
                ClientUtils.ExecuteSQL(sSQL);;
                CopyToHistory(sTerminalID);

                tSelectNode.Remove();
                return;
            }
        }

        public void CopyToHistoryAll(int iType, string sLine, string sStage, string sProcess)
        {

            string sSQL = " Insert Into SAJET.SYS_HT_TERMINAL "
                        + " select * from SAJET.SYS_TERMINAL "
                        + " Where PDLINE_ID = '" + sLine + "' ";
            if (iType==1)
                sSQL = sSQL + " and Stage_ID = '" + sStage + "' ";
            else if (iType==2)
                sSQL = sSQL + " and Process_ID = '" + sProcess + "' ";
            ClientUtils.ExecuteSQL(sSQL);;
        }

        private void combFactory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSQL = " select FACTORY_ID,FACTORY_CODE,FACTORY_DESC from sajet.sys_factory "
                        + " where enabled='Y' "
                        + " and Factory_NAME = '" + combFactory.Text + "' ";
            DataSet dsTemp = ClientUtils.ExecuteSQL(sSQL);;
            g_sFactoryID = dsTemp.Tables[0].Rows[0]["FACTORY_ID"].ToString();
            LabFactoryCode.Text = dsTemp.Tables[0].Rows[0]["FACTORY_CODE"].ToString();
            if (!string.IsNullOrEmpty(dsTemp.Tables[0].Rows[0]["FACTORY_DESC"].ToString()))
                LabFactoryCode.Text = LabFactoryCode.Text + "[" + dsTemp.Tables[0].Rows[0]["FACTORY_DESC"].ToString() + "]";
            //Select Line
            combLine.Items.Clear();
            sSQL = " select PDLINE_NAME from sajet.sys_pdline "
                 + " where enabled='Y' "
                 + " and Factory_ID = '" + g_sFactoryID + "' "
                 + " order by PDLINE_NAME ";
            dsTemp = ClientUtils.ExecuteSQL(sSQL);;
            for (int i = 0; i <= dsTemp.Tables[0].Rows.Count - 1; i++)
            {
                combLine.Items.Add(dsTemp.Tables[0].Rows[i]["PDLINE_NAME"].ToString());
            }

            ShowTerminal();
        }

        private void TreeViewProcess_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeViewProcess.SelectedNode.SelectedImageIndex = TreeViewProcess.SelectedNode.ImageIndex;
        }

        private void MenuItemChange_EnabledChanged(object sender, EventArgs e)
        {
            MenuItemPC.Enabled = MenuItemChange.Enabled;
        }

        private void MenuItemCollapseAll_Click(object sender, EventArgs e)
        {
            TreeViewTerminal.CollapseAll();
        }

        private void MenuItemExpandAll_Click(object sender, EventArgs e)
        {
            TreeViewTerminal.ExpandAll();
        }
        
        private void MenuItemTerminalType_Click(object sender, EventArgs e)
        {
            if (combLine.SelectedIndex == -1)
                return;

            fMachineLink f = new fMachineLink();
            f.g_sFactoryID = g_sFactoryID;
            f.g_sLineID = g_sLineID;
            f.g_sLineName = combLine.Text;
            f.ShowDialog();
        }        
    }
}


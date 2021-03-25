using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SajetClass;

namespace CRoute
{
    public partial class fMain : Form
    {        
        public fMain()
        {            
            InitializeComponent();
        }               

        public int g_iPrivilege = 0;
        public static String g_sUserID;
        public String g_sProgram, g_sFunction;
        bool bChange = false;
        String g_sOldRoute;

        public void check_privilege()
        {
            string sPrivilege = ClientUtils.GetPrivilege(g_sUserID, g_sFunction, g_sProgram).ToString();                        
            g_iPrivilege = ClientUtils.GetPrivilege(g_sUserID, g_sFunction, g_sProgram);

            btnSave.Enabled = SajetCommon.CheckEnabled("INSERT", sPrivilege);
            NecessaryToolStripMenuItem.Enabled = SajetCommon.CheckEnabled("UPDATE", sPrivilege);
            MenuItemInProcess.Enabled = SajetCommon.CheckEnabled("UPDATE", sPrivilege);   
            changeRouteNameToolStripMenuItem.Enabled = SajetCommon.CheckEnabled("UPDATE", sPrivilege);
            copyRoueFromToolStripMenuItem.Enabled = SajetCommon.CheckEnabled("INSERT", sPrivilege);
            deleteToolStripMenuItem.Enabled = SajetCommon.CheckEnabled("DELETE", sPrivilege);

            TreeViewRoute.AllowDrop = SajetCommon.CheckEnabled("INSERT", sPrivilege);
        }

        private void fRoute_Load(object sender, EventArgs e)
        {
            SajetCommon.SetLanguageControl(this);
            panel1.BackgroundImage = ClientUtils.LoadImage("ImgFilter.jpg");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackgroundImage = ClientUtils.LoadImage("ImgMain.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            g_sUserID = ClientUtils.UserPara1;
            g_sProgram = ClientUtils.fProgramName;
            g_sFunction = ClientUtils.fFunctionName;         
            this.Text = this.Text + " (" + SajetCommon.g_sFileVersion + ")";
            grpbDesc.Visible = false;
            editRouteDesc.BackColor = Color.White;
            //editRouteDesc.ReadOnly = true;

            //Select Route Name
            string sSQL = "select Route_Name from sajet.sys_route "
                 + "where enabled='Y' "
                 + "order by Route_Name ";
            DataSet dsTemp = ClientUtils.ExecuteSQL(sSQL);
            for (int i = 0; i <= dsTemp.Tables[0].Rows.Count - 1; i++)
            {
                combRoute.Items.Add(dsTemp.Tables[0].Rows[i]["ROUTE_NAME"].ToString());
            }

            //Show Process
            string sStage = "";
            int iCnt = 0;            
            sSQL = "Select STAGE_CODE,STAGE_NAME,PROCESS_CODE,PROCESS_NAME,Upper(TYPE_NAME) TYPE_NAME "
                 + " From SAJET.SYS_PROCESS A, "
                 + " SAJET.SYS_STAGE B, "
                 + " SAJET.SYS_OPERATE_TYPE C "
                 + " Where A.STAGE_ID = B.STAGE_ID "
                 + " and A.OPERATE_ID = C.OPERATE_ID(+) "
                 + " and A.ENABLED = 'Y' "
                 + " and B.ENABLED = 'Y' "
                 + " Order By STAGE_NAME,PROCESS_NAME ";
            DataSet DS = ClientUtils.ExecuteSQL(sSQL);         

            TreeViewProcess.Nodes.Clear();
            for (int i = 0; i <= DS.Tables[0].Rows.Count - 1; i++)
            {
                if (sStage != DS.Tables[0].Rows[i]["STAGE_NAME"].ToString())
                {
                    sStage = DS.Tables[0].Rows[i]["STAGE_NAME"].ToString();
                    TreeNode Node1 = new TreeNode();
                    Node1.Text = sStage;
                    Node1.ImageIndex =0;
                    Node1.SelectedImageIndex = Node1.ImageIndex;
                    TreeViewProcess.Nodes.Add(Node1);
                    iCnt = iCnt + 1;
                }

                TreeNode NodeProcess = new TreeNode();
                NodeProcess.Text = DS.Tables[0].Rows[i]["PROCESS_NAME"].ToString();
                if (DS.Tables[0].Rows[i]["TYPE_NAME"].ToString() == "REPAIR"
                    || DS.Tables[0].Rows[i]["TYPE_NAME"].ToString() == "SP-REPAIR")
                {
                    NodeProcess.ImageIndex = 2;
                }
                else
                {
                    NodeProcess.ImageIndex = 1;
                }
                NodeProcess.SelectedImageIndex = NodeProcess.ImageIndex;
                TreeViewProcess.Nodes[iCnt - 1].Nodes.Add(NodeProcess);
            }
            TreeViewProcess.ExpandAll();
            
            combRoute.Text= "";
            TreeViewRoute.Nodes.Clear();

            check_privilege();            
        }       

        public void GetRouteDetail(string sRoute, TreeView TV)
        {            
            string sProcess = "";
            string sStep = "";
            string sRepairProcess = "";
            int iCnt = 0;
            int iRepairCnt = 0;
            editRouteDesc.Text = string.Empty;
                                                       
            TV.Nodes.Clear();
            TreeNode NodeRoute = new TreeNode();   
            NodeRoute.Text = sRoute;                     
            TV.Nodes.Add(NodeRoute);
            string sSQL = "SELECT ROUTE_DESC "
                       + "  FROM SAJET.SYS_ROUTE "
                       + " WHERE ROUTE_NAME ='" + sRoute + "' "
                       + "   AND ROWNUM = 1 ";
            DataSet dsTemp = ClientUtils.ExecuteSQL(sSQL);
            if (dsTemp.Tables[0].Rows.Count > 0)
                editRouteDesc.Text = dsTemp.Tables[0].Rows[0]["ROUTE_DESC"].ToString();
            grpbDesc.Visible = (!string.IsNullOrEmpty(editRouteDesc.Text));

            sSQL = "Select C.PROCESS_NAME,D.PROCESS_NAME NEXT_PROCESS,TYPE_NAME,NECESSARY,B.RESULT,B.SEQ,B.STEP,D.Enabled "
                        + "From SAJET.SYS_ROUTE A, "
                        + "SAJET.SYS_ROUTE_DETAIL B ,"
                        + "SAJET.SYS_PROCESS C, "
                        + "SAJET.SYS_PROCESS D, "
                        + "SAJET.SYS_OPERATE_TYPE E "
                        + "Where A.ROUTE_NAME = '" + sRoute + "' "
                        + "AND A.ROUTE_ID = B.ROUTE_ID "
                        + "AND B.PROCESS_ID = C.PROCESS_ID(+) "
                        + "AND B.NEXT_PROCESS_ID = D.PROCESS_ID "
                        + "AND D.OPERATE_ID = E.OPERATE_ID(+) "
                        + "Order By B.STEP,B.SEQ ";
            DataSet DS = ClientUtils.ExecuteSQL(sSQL);
            for (int i = 0; i <= DS.Tables[0].Rows.Count - 1; i++)
            {
                //主流程
                sProcess = DS.Tables[0].Rows[i]["NEXT_PROCESS"].ToString(); 
                if (sStep != DS.Tables[0].Rows[i]["STEP"].ToString())
                {
                    sStep = DS.Tables[0].Rows[i]["STEP"].ToString();
                    TreeNode Node1 = new TreeNode();                    
                    Node1.Text = sProcess;
                    Node1.ImageIndex = 1;                    
                    if (DS.Tables[0].Rows[i]["NECESSARY"].ToString() == "N")
                    {
                        Node1.ImageIndex = 3;
                    }
                    if (DS.Tables[0].Rows[i]["Enabled"].ToString() == "N")
                    {
                        Node1.ImageIndex = 4;
                    }
                    Node1.SelectedImageIndex = Node1.ImageIndex;
                    TV.Nodes[0].Nodes.Add(Node1);
                    iCnt = iCnt + 1;
                    iRepairCnt = 0;
                }
                else
                {   //維修站
                    if (DS.Tables[0].Rows[i]["RESULT"].ToString() == "1")
                    {
                        TreeNode NodeRepair = new TreeNode();
                        NodeRepair.Text = sProcess;
                        NodeRepair.ImageIndex = 2;
                        if (DS.Tables[0].Rows[i]["Enabled"].ToString() == "N")
                        {
                            NodeRepair.ImageIndex = 5;
                        }
                        NodeRepair.SelectedImageIndex = NodeRepair.ImageIndex;
                        TV.Nodes[0].Nodes[iCnt - 1].Nodes.Add(NodeRepair);
                        iRepairCnt = iRepairCnt + 1;
                        sRepairProcess = sProcess;
                    }
                    //回流站
                    else if (sRepairProcess != "") 
                    {
                        if (sRepairProcess == DS.Tables[0].Rows[i]["PROCESS_NAME"].ToString())
                        {
                            TreeNode NodeReturn = new TreeNode();
                            NodeReturn.Text = sProcess;
                            NodeReturn.ImageIndex = 1;
                            if (DS.Tables[0].Rows[i]["Enabled"].ToString() == "N")
                            {
                                NodeReturn.ImageIndex = 4;
                            }
                            NodeReturn.SelectedImageIndex = NodeReturn.ImageIndex;
                            TV.Nodes[0].Nodes[iCnt - 1].Nodes[iRepairCnt - 1].Nodes.Add(NodeReturn);
                        }
                    }
                }
            }
            TV.Nodes[0].Expand();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (TreeViewRoute.Nodes.Count > 0)
            {
                if (TreeViewRoute.Nodes[0].Nodes.Count > 0)
                {


                    if (MessageBox.Show("�溜炱ㄣ璉�", "��佷奧綴俴", MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        SaveRoute(combRoute.Text);
                        routeDesc.Text = "";
                    }


                    
                }
            }
        }

        protected void SaveRoute(string sRouteName)
        {            
            string sRouteID;
            string sSysdate;            
            string sProcess;
            string sNextProcess;
            string sSQL;
            string sNecessary;
            int iStep;

            sSQL = " Select to_char(sysdate,'yyyy/mm/dd hh24:mi:ss') systime from dual ";
            DataSet dsTemp = ClientUtils.ExecuteSQL(sSQL);
            sSysdate = dsTemp.Tables[0].Rows[0]["systime"].ToString();            
            
            sRouteID = GetRouteID(sRouteName);
          //  string sRouteDesc = editRouteDesc.Text.Trim();
            try
            {                
                //Route Name==========================================
                if (sRouteID == "0") //新增Route Name
                {
                    sRouteID = SajetCommon.GetMaxID("SAJET.SYS_ROUTE", "ROUTE_ID", 8);                   
                    sSQL = " Insert Into SAJET.SYS_ROUTE "
                         + " (ROUTE_ID,ROUTE_NAME,ROUTE_DESC,UPDATE_USERID,UPDATE_TIME ) "
                         + " Values "
                         + " ('" + sRouteID + "','" + sRouteName + "','" + routeDesc.Text.Trim() + "','" + g_sUserID + "',to_date('" + sSysdate + "','yyyy/mm/dd hh24:mi:ss') ) ";
                    ClientUtils.ExecuteSQL(sSQL);
                }
                else //若已有Disabled的Route,改成Enabled
                {
                    sSQL = " Update SAJET.SYS_ROUTE "
                         + " Set Enabled='Y' "
                         //+ "    ,ROUTE_DESC ='" + routeDesc.Text.Trim()+ "' "
                         + "    ,UPDATE_USERID = '" + g_sUserID + "' "
                         + "    ,UPDATE_TIME = to_date('" + sSysdate + "','yyyy/mm/dd hh24:mi:ss') " 
                         + " where ROUTE_ID = '" + sRouteID + "' ";
                    ClientUtils.ExecuteSQL(sSQL);
                }

                //Route Detail=======================================
                //刪除原來的 Route
                sSQL = " Delete SAJET.SYS_ROUTE_DETAIL "
                     + " Where ROUTE_ID = '" + sRouteID + "' ";
                ClientUtils.ExecuteSQL(sSQL);
                
                //主流程
                int iSeq = 0;
                for (int i = 0; i <= TreeViewRoute.Nodes[0].Nodes.Count - 1; i++)
                {
                    iSeq = iSeq + 1;
                    if (i == 0)
                    { sProcess = "0"; }
                    else
                    { sProcess = GetProcessID(TreeViewRoute.Nodes[0].Nodes[i - 1].Text); }
                    sNextProcess = GetProcessID(TreeViewRoute.Nodes[0].Nodes[i].Text);                    
                    if (TreeViewRoute.Nodes[0].Nodes[i].ImageIndex == 3)
                    { sNecessary = "N"; }
                    else
                    { sNecessary = "Y"; }

                    InsertRouteDetail(sRouteID, sProcess, sNextProcess, "0", iSeq.ToString(), sNecessary, iSeq.ToString(), sSysdate);                    
                }

                //可過可不過的站
                for (int i = 0; i <= TreeViewRoute.Nodes[0].Nodes.Count - 1; i++)
                {
                    if (TreeViewRoute.Nodes[0].Nodes[i].ImageIndex == 3)
                    {
                        for (int j = i; j <= TreeViewRoute.Nodes[0].Nodes.Count - 2; j++)
                        {
                            iSeq = iSeq + 1;
                            if (i == 0)
                            { sProcess = "0"; }
                            else
                            { sProcess = GetProcessID(TreeViewRoute.Nodes[0].Nodes[i - 1].Text); }
                            sNextProcess = GetProcessID(TreeViewRoute.Nodes[0].Nodes[j + 1].Text);
                            if (TreeViewRoute.Nodes[0].Nodes[j + 1].ImageIndex == 3)
                            { sNecessary = "N"; }
                            else
                            { sNecessary = "Y"; }
                            if (i != 0)
                            { iStep = i; }
                            else
                            { iStep = 1; }
                            InsertRouteDetail(sRouteID, sProcess, sNextProcess, "0", iSeq.ToString(), sNecessary, iStep.ToString(), sSysdate);
                           
                            if (j < TreeViewRoute.Nodes[0].Nodes.Count - 1)
                            {
                                if (TreeViewRoute.Nodes[0].Nodes[j + 1].ImageIndex != 3)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                //維修站
                for (int i = 0; i <= TreeViewRoute.Nodes[0].Nodes.Count - 1; i++)
                {
                    for (int j = 0; j <= TreeViewRoute.Nodes[0].Nodes[i].Nodes.Count - 1; j++)
                    {
                        iSeq = iSeq + 1;
                        sProcess = GetProcessID(TreeViewRoute.Nodes[0].Nodes[i].Text);
                        sNextProcess = GetProcessID(TreeViewRoute.Nodes[0].Nodes[i].Nodes[j].Text);
                        iStep = i + 1;
                        InsertRouteDetail(sRouteID, sProcess, sNextProcess, "1", iSeq.ToString(), "Y", iStep.ToString(), sSysdate);
                       
                        //回流站
                        for (int k = 0; k <= TreeViewRoute.Nodes[0].Nodes[i].Nodes[j].Nodes.Count - 1; k++)
                        {
                            iSeq = iSeq + 1;
                            sProcess = GetProcessID(TreeViewRoute.Nodes[0].Nodes[i].Nodes[j].Text);
                            sNextProcess = GetProcessID(TreeViewRoute.Nodes[0].Nodes[i].Nodes[j].Nodes[k].Text);
                            iStep = i + 1;
                            InsertRouteDetail(sRouteID, sProcess, sNextProcess, "0", iSeq.ToString(), "Y", iStep.ToString(), sSysdate);                            
                        }
                    }
                }

                CopyToHistory(sRouteID);
                if (combRoute.FindString(sRouteName) == -1)
                {
                    combRoute.Items.Add(sRouteName);
                    combRoute.Sorted = true;
                }
                string sData = LabRouteName.Text + " : " + sRouteName;
                string sMsg = SajetCommon.SetLanguage("Save OK", 1) + Environment.NewLine + sData;
                SajetCommon.Show_Message(sMsg, -1);
                bChange = false;
            }
            catch (Exception ex)
            {
                SajetCommon.Show_Message("Exception:" + ex.Message, 0);
                return;
            }            
        }

        protected void InsertRouteDetail(string sRouteID, string sProcess, string sNextProcess, string sResult, string sSeq, string sNecessary, string sStep, string sDate)
        {
            string sSQL = " Insert Into SAJET.SYS_ROUTE_DETAIL "
                        + " (ROUTE_ID, PROCESS_ID, NEXT_PROCESS_ID, RESULT, SEQ, NECESSARY, STEP, UPDATE_USERID,UPDATE_TIME) "
                        + " Values "
                        + " ('" + sRouteID + "','" + sProcess + "','" + sNextProcess + "','" + sResult + "','" + sSeq + "','" + sNecessary + "','" + sStep + "','" + g_sUserID + "',to_date('" + sDate + "','yyyy/mm/dd hh24:mi:ss'))";
            ClientUtils.ExecuteSQL(sSQL);
        }

        protected void CopyToHistory(string sRouteID)
        {
            string sSQL = " Insert Into SAJET.SYS_HT_ROUTE "
                        + " Select * from SAJET.SYS_ROUTE "
                        + " Where ROUTE_ID = '"+sRouteID+"' ";
            ClientUtils.ExecuteSQL(sSQL);

            sSQL = " Insert Into SAJET.SYS_HT_ROUTE_DETAIL "
                 + " Select * from SAJET.SYS_ROUTE_DETAIL "
                 + " Where ROUTE_ID = '" + sRouteID + "' ";
            ClientUtils.ExecuteSQL(sSQL);
        }

        protected string GetRouteID(string RouteName)
        {            
            string sSQL = "Select ROUTE_ID,ENABLED "
                        + "From SAJET.SYS_ROUTE "
                        + "Where ROUTE_NAME = '" + RouteName + "'";
            DataSet DS = ClientUtils.ExecuteSQL(sSQL);         
            if (DS.Tables[0].Rows.Count > 0)
            {
                return DS.Tables[0].Rows[0]["ROUTE_ID"].ToString();
            }
            else { return "0"; }

        }        

        protected string GetProcessID(string sProcess)
        {            
            string sSQL = "Select PROCESS_ID "
                        + "From SAJET.SYS_PROCESS "
                        + "Where PROCESS_NAME = '" + sProcess + "'";
            DataSet DS = ClientUtils.ExecuteSQL(sSQL);
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
            /*
            TreeNode TNodeTemp = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
            if (TNodeTemp.TreeView == sender)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
             */ 
            e.Effect = DragDropEffects.Move;
        }

        private void TreeView_DragDrop(object sender, DragEventArgs e)
        {            
            bool bCheck;            
            TreeNode mNode;
            TreeNode SrcNode;                   

            SrcNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode"); //來源Node
            if (SrcNode.Level == 0)
            { return; }

            Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            mNode = ((TreeView)sender).GetNodeAt(pt);  //目的Node                                  

            if (mNode == null)
            { mNode = TreeViewRoute.TopNode; }

            TreeViewRoute.SelectedNode = mNode;
            TreeViewRoute.Focus();

            //Move
            if (SrcNode.TreeView == TreeViewRoute)
            {
                if (SrcNode.Level == 1 & mNode.Level == 1)
                {
                    string sMsg = SajetCommon.SetLanguage("Move",1)+" ? ";
                    string sMsg1 = SajetCommon.SetLanguage("From", 1) + " : " + SrcNode.Text;
                    string sMsg2 = SajetCommon.SetLanguage("To", 1) + " : " + mNode.Text;
                    if (SajetCommon.Show_Message(sMsg + Environment.NewLine + sMsg1 + Environment.NewLine + sMsg2, 2) == DialogResult.Yes)                    
                    {
                        int iIndex = mNode.Index;
                        mNode.Parent.Nodes.Insert(iIndex, (TreeNode)SrcNode.Clone());                        
                        SrcNode.Remove();
                        bChange = true;
                    }
                    return;
                }            
            }

            //維修站
            if (SrcNode.ImageIndex == 2)
            {
                //維修站不可放在正常流程或回流站下
                if (mNode.Level != 1)
                {
                    SajetCommon.Show_Message("Can not add Repair Process", 0);
                    return;
                }

                //不可有兩個維修站
                if (mNode.Nodes.Count > 0)
                {
                    SajetCommon.Show_Message("Can not have many Repair Process", 0);
                    return;
                }
            }
            else
            //非維修站:不可當維修站 & 不可放在回流站下
            {
                if (mNode.Level == 1)
                {
                    //非維修站
                    SajetCommon.Show_Message("Not Repair Process", 0);
                    return;
                }
                if (mNode.Level >= 3)
                {
                    //只能加入維修站
                    SajetCommon.Show_Message("Must add Repair Process", 0);
                    return;
                }
            }
            //回流站不是正常流程中的一站
            if (mNode.Level == 2)
            {
                bCheck = false;
                for (int j = 0; j <= TreeViewRoute.Nodes[0].Nodes.Count - 1; j++)
                {
                    if (SrcNode.Text == TreeViewRoute.Nodes[0].Nodes[j].Text)
                    {
                        bCheck = false;
                        break;
                    }
                    else
                    {
                        bCheck = true;
                    }
                }
                if (bCheck == true)
                {
                    //回流站不是正常流程中的一站
                    SajetCommon.Show_Message("Return Process is not at this Route",0);
                    return;                    
                }
            }
            //process不可重複
            bCheck = false;
            for (int j = 0; j <= mNode.Nodes.Count - 1; j++)
            {
                if (SrcNode.Text == mNode.Nodes[j].Text)
                {
                    bCheck = true;
                    break;
                }
            }
            if (bCheck == true)
            {
                SajetCommon.Show_Message("Process Duplicate",0);
                return;    
            }
            
            TreeNode Node1 = new TreeNode();
            Node1.Text = SrcNode.Text;
            if (SrcNode.ImageIndex == 2)
            {
                Node1.ImageIndex = 2;
            }
            else
            {
                Node1.ImageIndex = 1;
            }
            Node1.SelectedImageIndex = Node1.ImageIndex;
            mNode.Nodes.Add(Node1);
            mNode.Expand();
            bChange = true;
        }

        private void collapseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeViewRoute.CollapseAll();
        }

        private void expandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeViewRoute.ExpandAll();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (combRoute.Text.Trim() == "")
                return;
            if (TreeViewRoute.SelectedNode == null || combRoute.FindString(combRoute.Text) == -1)
                return;


            if (TreeViewRoute.SelectedNode.Level == 0)
            {
                string sMsg = SajetCommon.SetLanguage("Delete Route", 1) + " ? ";
                if (SajetCommon.Show_Message(sMsg + Environment.NewLine + TreeViewRoute.SelectedNode.Text, 2) == DialogResult.Yes)
                {
                    string sRouteID = GetRouteID(TreeViewRoute.SelectedNode.Text);

                    string sSQL = " Update SAJET.SYS_ROUTE "
                                + " Set Enabled='N' "
                                + "    ,UPDATE_USERID = '" + g_sUserID + "' "
                                + "    ,UPDATE_TIME = SYSDATE "
                                + " where ROUTE_ID = '" + sRouteID + "' ";
                    ClientUtils.ExecuteSQL(sSQL);

                    sSQL = " Delete SAJET.SYS_ROUTE_DETAIL "
                         + " Where ROUTE_ID = '" + sRouteID + "' ";
                    ClientUtils.ExecuteSQL(sSQL);

                    combRoute.Items.Remove(combRoute.Text);
                    combRoute.Text = "";
                    TreeViewRoute.Nodes.Clear();
                    bChange = false;
                }
            }
            else
            {
                string sMsg = SajetCommon.SetLanguage("Delete Process", 1) + " ? ";
                if (SajetCommon.Show_Message(sMsg + Environment.NewLine + TreeViewRoute.SelectedNode.Text, 2) == DialogResult.Yes)
                {
                    TreeViewRoute.SelectedNode.Remove();
                    bChange = true;
                }
            }
            
        }

        private void necessaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TreeViewRoute.SelectedNode == null)
            { 
                return; 
            }
            if (TreeViewRoute.SelectedNode.Level != 1)
            {
                return;
            }
                     
            if (TreeViewRoute.SelectedNode.ImageIndex == 1)
            {
                TreeViewRoute.SelectedNode.ImageIndex = 3;
                TreeViewRoute.SelectedNode.SelectedImageIndex = TreeViewRoute.SelectedNode.ImageIndex;  
            }
            else if (TreeViewRoute.SelectedNode.ImageIndex == 3)
            {
                TreeViewRoute.SelectedNode.ImageIndex = 1;
                TreeViewRoute.SelectedNode.SelectedImageIndex = TreeViewRoute.SelectedNode.ImageIndex;     
            }
        }

        private void PopMenu1_Opening(object sender, CancelEventArgs e)
        {
            TreeNode SelectNode = TreeViewRoute.SelectedNode;            
            if (SelectNode == null)
            { return; }

            if (g_iPrivilege >= 1)
            {
                if (SelectNode.Level > 1 || SelectNode.Level == 0)
                    NecessaryToolStripMenuItem.Enabled = false;
                else
                    NecessaryToolStripMenuItem.Enabled = true;

                if (SelectNode.Level == 1)
                    MenuItemInProcess.Enabled = true;
                else
                    MenuItemInProcess.Enabled = false;
            }

            if (SelectNode.ImageIndex == 3)
            {
                NecessaryToolStripMenuItem.Visible = true;
                unNecessaryToolStripMenuItem.Visible = false;   
            }
            else 
            {
                NecessaryToolStripMenuItem.Visible = false;
                unNecessaryToolStripMenuItem.Visible = true;   
        
            }            
        }

        private void combRoute_TextChanged(object sender, EventArgs e)
        {
            if (ConformSave(g_sOldRoute))
            {
                SaveRoute(g_sOldRoute);                
            }
            GetRouteDetail(combRoute.Text,TreeViewRoute);
            bChange = false;
            g_sOldRoute = combRoute.Text;
        }

        private void historyLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = TreeViewRoute.Nodes[0].Text;
            fHistory fHistory = new fHistory();
            fHistory.txtName.Text = s;  
            fHistory.ShowDialog();
            fHistory.Dispose();             
        }

        private void changeRouteNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TreeViewRoute.SelectedNode == null)
            {
                return;
            }

            if (combRoute.FindString(combRoute.Text) == -1)
            {
                return;
            }

            int iIndex = combRoute.FindString(combRoute.Text);
            string sOldName = combRoute.Text;
            string sRouteDesc = editRouteDesc.Text;
            fChangeRoute fNewRoute = new fChangeRoute();
            try
            {
                fNewRoute.g_sOldRoute = sOldName;
                fNewRoute.g_sRouteDesc = sRouteDesc;
                if (fNewRoute.ShowDialog() != DialogResult.OK)
                    return;
                sRouteDesc = fNewRoute.g_sRouteDesc;
            }
            finally
            {
                fNewRoute.Dispose();
            }

            string sNewName = fNewRoute.g_sNewRoute;
            
            string sSQL = "";

            if (sNewName.Trim() == "")
            {
                sNewName = sOldName;
//                return;
            }
            sSQL = "select route_id from sajet.sys_route "
                 + "where route_name = '" + sNewName + "' "
                 + "and route_id <> (select route_id from sajet.sys_route "
                 + "where route_name = '" + sOldName + "' ) ";
            DataSet dsTemp = ClientUtils.ExecuteSQL(sSQL);
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                string sData = LabRouteName.Text + " : " + sNewName;
                string sMsg = SajetCommon.SetLanguage("Data Duplicate", 2) + Environment.NewLine + sData;
                SajetCommon.Show_Message(sMsg, 0);                
                return;
            }
            sSQL = "Update sajet.sys_route "
                 + "Set route_name = '" + sNewName + "' "
                 + "    ,ROUTE_DESC ='"+sRouteDesc+"' "
                 + "   ,UPDATE_USERID = '" + g_sUserID + "' "
                 + "   ,update_time = sysdate "
                 + "where route_name = '" + sOldName + "' ";

            ClientUtils.ExecuteSQL(sSQL);
            //add by rita 2009/12/04
            sSQL = " Insert Into SAJET.SYS_HT_ROUTE "
                        + " Select * from SAJET.SYS_ROUTE "
                        + " Where ROUTE_NAME = '" + sNewName + "' ";
            ClientUtils.ExecuteSQL(sSQL);

            TreeViewRoute.Nodes[0].Text = sNewName;
            combRoute.Items[iIndex] = sNewName;
            combRoute.SelectedIndex = iIndex;
            editRouteDesc.Text = sRouteDesc;
            grpbDesc.Visible = (!string.IsNullOrEmpty(editRouteDesc.Text));

        }

        private void TreeViewRoute_MouseDown(object sender, MouseEventArgs e)
        {           
            if (e.Button == MouseButtons.Right)
            {                
                if (TreeViewRoute.Nodes.Count==0)
                    PopMenu1.Enabled = false;
                else
                {
                  if (TreeViewRoute.SelectedNode == null)
                      TreeViewRoute.SelectedNode = TreeViewRoute.Nodes[0];

                  PopMenu1.Enabled = true;
                }                    
            }
        }

        private void copyRoueFromToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            fCopyRoute fCopyRoute = new fCopyRoute();
            fCopyRoute.sCopyRouteName = combRoute.Text;            
            if (fCopyRoute.ShowDialog() == DialogResult.OK)
            {
                combRoute.Text = fCopyRoute.sCopyRouteName;
                TreeViewRoute.Nodes.Clear();
                GetRouteDetail(fCopyRoute.sCopyFrom, TreeViewRoute);
                TreeViewRoute.Nodes[0].Text = fCopyRoute.sCopyRouteName;

                bChange = true;
            }
            fCopyRoute.Dispose();
        }

        private void collapseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeViewProcess.CollapseAll();
        }

        private void expandToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeViewProcess.ExpandAll();
        }

        private void TreeViewRoute_DragOver(object sender, DragEventArgs e)
        {
            //當移動到節點上,該節點會Focus並成藍色
            TreeNode DropNode = new TreeNode();            
            Point Position = TreeViewRoute.PointToClient(new Point(e.X, e.Y));
            DropNode = TreeViewRoute.GetNodeAt(Position);            
            if (DropNode != null)
            {
                TreeViewRoute.Focus();
                TreeViewRoute.SelectedNode = DropNode;                
            }  
        }

        private void NecessaryToolStripMenuItem_EnabledChanged(object sender, EventArgs e)
        {
            unNecessaryToolStripMenuItem.Enabled = NecessaryToolStripMenuItem.Enabled;
        }

        private bool ConformSave(string sRoute)
        {
            // 有異動時離開要提示是否儲存
            if (bChange)
            {
                string sMsg = SajetCommon.SetLanguage("Route had Changed,Save", 1) + " ?"
                            + Environment.NewLine + " \"" + sRoute + "\"";
                if (SajetCommon.Show_Message(sMsg , 2) == DialogResult.Yes)
                    return true;
            }
            return false;
        }        

        private void fMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ConformSave(g_sOldRoute))
            {
                SaveRoute(g_sOldRoute);
            }
        }      
            
        private void MenuItemInProcess_Click(object sender, EventArgs e)
        {
            string sSQL = "";
            DataSet dsTemp = null;
            //將此站設定為預設投入站
            if (TreeViewRoute.SelectedNode == null)            
                return;
            if (TreeViewRoute.SelectedNode.Level != 1 )
                return;

            string sProcessName = TreeViewRoute.SelectedNode.Text;
            string sConfirmMsg = SajetCommon.SetLanguage("Setup Default Input Process", 1) + " ?"
                               + Environment.NewLine
                               +sProcessName;
            if (SajetCommon.Show_Message(sConfirmMsg, 2) != DialogResult.Yes)
                return;
            string sProcessID = GetProcessID(sProcessName);
            string sRouteID = GetRouteID(combRoute.Text);

            try
            {
                sSQL = "Select rowid from SAJET.SYS_ROUTE_DETAIL "
                     + "Where ROUTE_ID = '" + sRouteID + "' "
                     + "and next_process_id = '" + sProcessID + "' "
                     + "and result = '0' "
                     + "Order By Seq ";
                dsTemp = ClientUtils.ExecuteSQL(sSQL);
                string sRowid = dsTemp.Tables[0].Rows[0]["rowid"].ToString();

                sSQL = "Update SAJET.SYS_ROUTE_DETAIL "
                     + "Set DEFAULT_INPROCESS = 'N' "
                     + "Where ROUTE_ID = '" + sRouteID + "' ";
                dsTemp = ClientUtils.ExecuteSQL(sSQL);

                sSQL = "Update SAJET.SYS_ROUTE_DETAIL "
                     + "Set DEFAULT_INPROCESS = 'Y' "
                     + "Where Rowid = '" + sRowid + "' ";
                dsTemp = ClientUtils.ExecuteSQL(sSQL);

                SajetCommon.Show_Message("Setup OK", -1);                
            }
            catch (Exception ex)
            {
                SajetCommon.Show_Message("Exception:" + ex.Message, 0);
            }        
        }              
    }
}


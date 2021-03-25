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
    public partial class fHistory : Form
    {
        public fHistory()
        {
            InitializeComponent();
        }
        
        public string gsRouteID;

        private void fHistory_Load(object sender, EventArgs e)
        {
            SajetCommon.SetLanguageControl(this);
            ShowRoute();          
        }

        private void ShowRoute()
        {
            string sSQL;
            combTime.Items.Clear();
            TV.Nodes.Clear();

            if (chkbID.Checked == true)
            {
                sSQL = " Select ROUTE_ID "
                    + " From SAJET.SYS_ROUTE "
                    + " Where ROUTE_NAME = '" + txtName.Text + "' ";
                DataSet dtTemp = ClientUtils.ExecuteSQL(sSQL);
                if (dtTemp.Tables[0].Rows.Count == 0)
                    return;
                gsRouteID = dtTemp.Tables[0].Rows[0]["ROUTE_ID"].ToString();
                sSQL = " Select ROUTE_NAME, to_char(Update_Time,'yyyy/mm/dd hh24:mi:ss') Update_Time "
                     +" From SAJET.SYS_HT_ROUTE A "
                     +" Where A.ROUTE_ID = '" + gsRouteID + "' "
                     +" Order By Update_Time ";
            }
            else
            {
                sSQL = " Select ROUTE_NAME, to_char(Update_Time,'yyyy/mm/dd hh24:mi:ss') Update_Time "
                     +" From SAJET.SYS_HT_ROUTE A "
                     +" Where A.ROUTE_NAME = '"+txtName.Text+"' "
                     +" Order By Update_Time ";
            }
            DataSet dtTemp1 = ClientUtils.ExecuteSQL(sSQL);
            for (int i = 0; i <= dtTemp1.Tables[0].Rows.Count - 1; i++)
            {
                combTime.Items.Add(dtTemp1.Tables[0].Rows[i]["Update_Time"].ToString());
            }

            if (combTime.Items.Count>0)
            {
                combTime.SelectedIndex=0;
            }
        }

        private void combTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSQL;
            string sProcess = "";
            string sStep = "";
            string sRepairProcess = "";
            int iCnt = 0;
            int iRepairCnt = 0;

            TV.Nodes.Clear();      
            sSQL =" Select A.ROUTE_NAME,C.PROCESS_NAME,D.PROCESS_NAME NEXT_PROCESS,E.TYPE_NAME "
                 +"       ,B.NECESSARY,B.RESULT,B.SEQ,B.STEP,D.Enabled "
                 +" From SAJET.SYS_HT_ROUTE A, "
                 +" SAJET.SYS_HT_ROUTE_DETAIL B, "
                 +" SAJET.SYS_PROCESS C, "
                 +" SAJET.SYS_PROCESS D, "
                 +" SAJET.SYS_OPERATE_TYPE E ";
            if (chkbID.Checked == true)
                sSQL = sSQL + " Where A.ROUTE_ID = '" + gsRouteID + "' ";
            else
                sSQL = sSQL + " Where A.ROUTE_NAME = '"+txtName.Text+"' ";
            sSQL = sSQL
                 +" and to_char(A.UPDATE_TIME,'yyyy/mm/dd hh24:mi:ss') = '"+combTime.Text+"' "
                 +" and to_char(B.UPDATE_TIME,'yyyy/mm/dd hh24:mi:ss') = '"+combTime.Text+"' "
                 +" and A.ROUTE_ID = B.ROUTE_ID "
                 +" and B.PROCESS_ID = C.PROCESS_ID(+) "
                 +" and B.NEXT_PROCESS_ID = D.PROCESS_ID "
                 +" and D.OPERATE_ID = E.OPERATE_ID(+) "
                 +" Order By B.STEP,B.SEQ ";
            DataSet DS = ClientUtils.ExecuteSQL(sSQL);

            //根節點
            TreeNode NodeRoute = new TreeNode();
            if (DS.Tables[0].Rows.Count == 0)
                NodeRoute.Text = txtName.Text;
            else
                NodeRoute.Text = DS.Tables[0].Rows[0]["ROUTE_NAME"].ToString();
            TV.Nodes.Add(NodeRoute);

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
                TV.Nodes[0].Expand();
            }            
        }

        private void chkbID_Click(object sender, EventArgs e)
        {
            chkbID.Checked = true;
            chkbName.Checked = false;
            ShowRoute();
        }

        private void chkbName_Click(object sender, EventArgs e)
        {
            chkbID.Checked = false;
            chkbName.Checked = true;
            ShowRoute();
        }            
    }  

}
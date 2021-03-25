using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PickListAC
{
    public partial class PickListLabel : Form
    {
        private string strShipmentID = "";
        public PickListLabel(string _strShipmentID)
        {
            InitializeComponent();

            this.strShipmentID = _strShipmentID;
        }

        private void PickListLabel_Load(object sender, EventArgs e)
        {
            InitComboBox();
        }

        private void InitComboBox()
        {
            cmbSmid.Items.Clear();
            string strSql = @"SELECT DISTINCT SHIPMENT_ID FROM NONEDIPPS.T_order_INFO WHERE STATUS IN ('WP','IP') and  person_flag='Y'
                            and shipment_id in(select shipment_id from NONEDIPPS.t_shipment_info where status in('WP','IP'))
                            ORDER BY SHIPMENT_ID ASC";
            DataSet dts = ClientUtils.ExecuteSQL(strSql);
            if (dts != null && dts.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dts.Tables[0].Rows.Count; i++)
                {
                    cmbSmid.Items.Add(dts.Tables[0].Rows[i]["SHIPMENT_ID"].ToString().Trim());
                }
                if (cmbSmid.Items.Contains(this.strShipmentID))
                {
                    cmbSmid.SelectedItem = this.strShipmentID;
                }
                else
                {
                    cmbSmid.SelectedIndex = 0;
                }
            }
            else
            {
                cmbSmid.DataSource = null;
            }
        }

        private void cmbSmid_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowMsg("", -1);

            InitComboBoxPallet(this.cmbSmid.Text.Trim());


        }
        private void InitComboBoxPallet(String strShipmentID)
        {
            cmbPallet.Items.Clear();
            string strSql = @"SELECT tsp.PALLET_NO FROM NONEDIPPS.T_SHIPMENT_PALLET tsp where Shipment_id='" + strShipmentID+"'";

            DataSet dts = ClientUtils.ExecuteSQL(strSql);
            if (dts != null && dts.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dts.Tables[0].Rows.Count; i++)
                {
                    cmbPallet.Items.Add(dts.Tables[0].Rows[i]["PALLET_NO"].ToString().Trim());
                }
               
                    cmbPallet.SelectedIndex = 0;
               
            }
            else
            {
                cmbPallet.DataSource = null;
            }
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            string strSQL = string.Format(@"
                                             select aa.shipment_id,
              aa.shipping_time,
              aa.region,
              aa.pallet_no,
              aa.location_no,
              aa.trolley_line_no,
              aa.tdm,
              listagg(aa.pointno, '*') within group(order by aa.pointno) as pointno
         from (select distinct a.shipment_id,
                               b.shipping_time,
                               a.region,
                               decode(g.pack_pallet_no,
                                      null,
                                      c.pallet_no,
                                      g.pack_pallet_no) pallet_no,
                               '' ictpn,
                               '' mpn,
                               d.location_no,
                               e.sides_no,
                               e.level_no,
                               e.seq_no,
                               e.pointno,
                               f.trolley_line_no,
                               decode(b.tdm,
                                      null,
                                      '',
                                      to_char(b.tdm, 'yyyy-mm-dd hh24:mi')) tdm
                 from NONEDIPPS.t_order_info a
                inner join NONEDIPPS.t_shipment_info b
                   on a.shipment_id = b.shipment_id
                inner join NONEDIPPS.t_pallet_order c
                   on a.delivery_no = c.delivery_no
                  and a.line_item = c.line_item
                  and a.ictpn = c.ictpn
                  and a.shipment_id = c.shipment_id
                inner join NONEDIPPS.vw_person_log d
                   on a.delivery_no = d.delivery_no
                  and a.line_item = d.line_item
                  and a.ictpn = d.part_no
                inner join NONEDIPPS.t_trolley_sn_status e
                   on d.customer_sn = e.custom_sn
                 join NONEDIPPS.t_trolley_line_info f
                   on e.trolley_no = f.trolley_no
                  and e.sides_no = f.sides_no
                  and e.level_no = f.level_no
                  and e.seq_no = f.seq_no
                 left join NONEDIPPS.t_sn_ppart g
                   on e.carton_no = g.carton_no
                where a.shipment_id = '{0}'
                  and c.pallet_no = '{1}'
                  and f.trolley_no not in ('ICT-00-00-000')
                order by d.location_no, f.trolley_line_no, e.pointno) aa
        where aa.pallet_no ='{2}'
        group by aa.shipment_id,
                 aa.shipping_time,
                 aa.region,
                 aa.pallet_no,
                 aa.location_no,
                 aa.trolley_line_no,
                 aa.tdm
        order by aa.location_no asc, aa.trolley_line_no asc
            ", this.cmbSmid.Text.Trim(), this.cmbPallet.Text.Trim(), this.cmbPallet.Text.Trim());

            DataTable dtTemp = ClientUtils.ExecuteSQL(strSQL).Tables[0];
            if ((dtTemp == null) || (dtTemp.Rows.Count <= 0))
            {
                ShowMsg("未找到该集货单号对应的P-part库存信息", 0);
                return;
            }
            PickPalletLabel ppl = new PickPalletLabel();
            if (ppl.PrintPickListLabel(dtTemp,54))
            {
                ShowMsg("打印OK", -1);
            }
            else
            {
                ShowMsg("打印连接出了问题", 0);
            }
        }

        public DialogResult ShowMsg(string sText, int iType)
        {
            TextMsg.Text = sText;
            switch (iType)
            {
                case 0: //Error                
                    TextMsg.ForeColor = Color.Red;
                    TextMsg.BackColor = Color.Yellow;
                    return DialogResult.None;
                case 1: //Warning                        
                    TextMsg.ForeColor = Color.Blue;
                    TextMsg.BackColor = Color.White;
                    return DialogResult.None;
                case 2: //Confirm
                    return MessageBox.Show(sText, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                default:
                    TextMsg.ForeColor = Color.Black;
                    TextMsg.BackColor = Color.Blue;
                    return DialogResult.None;
            }
        }

        private void cmbPallet_SelectedIndexChanged(object sender, EventArgs e)
        {
            PickListBll plb = new PickListBll();
            string strReult = string.Empty;
            string strSid = string.Empty;
            strSid = this.cmbSmid.Text.Trim();
            string strOuterrmsg = string.Empty;
            //strReult = plb.PPartPreAssinPalletNO(strSid, out strOuterrmsg);


            string strSQL = string.Format(@"
                                                select aa.shipment_id,
              aa.shipping_time,
              aa.region,
              aa.pallet_no,
              aa.location_no,
              aa.trolley_line_no,
              aa.tdm,
              listagg(aa.pointno, '*') within group(order by aa.pointno) as pointno
         from (select distinct a.shipment_id,
                               b.shipping_time,
                               a.region,
                               decode(g.pack_pallet_no,
                                      null,
                                      c.pallet_no,
                                      g.pack_pallet_no) pallet_no,
                               '' ictpn,
                               '' mpn,
                               d.location_no,
                               e.sides_no,
                               e.level_no,
                               e.seq_no,
                               e.pointno,
                               f.trolley_line_no,
                               decode(b.tdm,
                                      null,
                                      '',
                                      to_char(b.tdm, 'yyyy-mm-dd hh24:mi')) tdm
                 from NONEDIPPS.t_order_info a
                inner join NONEDIPPS.T_SHIPMENT_INFO b
                   on a.shipment_id = b.shipment_id
                inner join NONEDIPPS.T_PALLET_ORDER c
                   on a.delivery_no = c.delivery_no
                  and a.line_item = c.line_item
                  and a.ictpn = c.ictpn
                  and a.shipment_id = c.shipment_id
                inner join NONEDIPPS.vw_person_log d
                   on a.delivery_no = d.delivery_no
                  and a.line_item = d.line_item
                  and a.ictpn = d.part_no
                inner join NONEDIPPS.t_trolley_sn_status e
                   on d.customer_sn = e.custom_sn
                 join NONEDIPPS.t_trolley_line_info f
                   on e.trolley_no = f.trolley_no
                  and e.sides_no = f.sides_no
                  and e.level_no = f.level_no
                  and e.seq_no = f.seq_no
                 left join NONEDIPPS.t_sn_ppart g
                   on e.carton_no = g.carton_no
                where a.shipment_id = '{0}'
                  and c.pallet_no = '{1}'
                  and f.trolley_no not in ('ICT-00-00-000')
                order by d.location_no, f.trolley_line_no, e.pointno) aa
        where aa.pallet_no ='{2}'
        group by aa.shipment_id,
                 aa.shipping_time,
                 aa.region,
                 aa.pallet_no,
                 aa.location_no,
                 aa.trolley_line_no,
                 aa.tdm
        order by aa.location_no asc, aa.trolley_line_no asc
            ", this.cmbSmid.Text.Trim(), this.cmbPallet.Text.Trim(), this.cmbPallet.Text.Trim());


            //        select aa.SHIPPING_TIME,
            //                   aa.REGION,
            //                   aa.ICTPN,
            //                   aa.MPN,
            //                   aa.LOCATION_NO,
            //                   aa.TROLLEY_LINE_NO,
            //                   aa.TDM,
            //                   LISTAGG(aa.pointno, '*') WITHIN GROUP(ORDER BY aa.pointno) as pointno
            //              from (SELECT distinct A.SHIPMENT_ID,
            //                                    B.SHIPPING_TIME,
            //                                    A.REGION,
            //                                    C.PALLET_NO,
            //                                    A.ICTPN,
            //                                    A.MPN,
            //                                    D.LOCATION_NO,
            //                                    E.SIDES_NO,
            //                                    E.LEVEL_NO,
            //                                    E.SEQ_NO,
            //                                    E.POINTNO,
            //                                    F.TROLLEY_LINE_NO,
            //                                    DECODE(B.TDM,
            //                                           NULL,
            //                                           '',
            //                                           TO_CHAR(B.TDM, 'yyyy-mm-dd hh24:mi')) TDM
            //                      FROM NONEDIPPS.t_order_info A
            //                     INNER JOIN NONEDIPPS.T_SHIPMENT_INFO B
            //                        ON A.SHIPMENT_ID = B.SHIPMENT_ID
            //                     INNER JOIN NONEDIPPS.T_PALLET_ORDER C
            //                        ON A.DELIVERY_NO = C.DELIVERY_NO
            //                       AND A.LINE_ITEM = C.LINE_ITEM
            //                       AND A.ICTPN = C.ICTPN
            //                       AND A.SHIPMENT_ID = C.SHIPMENT_ID
            //                     INNER JOIN NONEDIPPS.VW_PERSON_LOG D
            //                        ON A.DELIVERY_NO = D.DELIVERY_NO
            //                       AND A.LINE_ITEM = D.LINE_ITEM
            //                       AND A.ICTPN = D.PART_NO
            //                     INNER JOIN NONEDIPPS.T_TROLLEY_SN_STATUS E
            //                        ON D.CUSTOMER_SN = E.CUSTOM_SN
            //                      JOIN NONEDIPPS.T_TROLLEY_LINE_INFO F
            //                        ON E.TROLLEY_NO = F.TROLLEY_NO
            //                       AND E.SIDES_NO = F.SIDES_NO
            //                       AND E.LEVEL_NO = F.LEVEL_NO
            //                       AND E.SEQ_NO = F.SEQ_NO
            //                     WHERE A.SHIPMENT_ID = '{0}'
            //                       and C.Pallet_no = '{1}'
            //                       and f.trolley_no not in ('ICT-00-00-000')
            //                     order by D.LOCATION_NO, F.TROLLEY_LINE_NO, E.POINTNO) aa
            //             group by aa.SHIPPING_TIME,
            //                      aa.REGION,
            //                      aa.ICTPN,
            //                      aa.MPN,
            //                      aa.LOCATION_NO,
            //                      aa.TROLLEY_LINE_NO,
            //                      aa.TDM
            DataTable dtTemp = ClientUtils.ExecuteSQL(strSQL).Tables[0];
            if (dtTemp.Rows.Count > 0)
            {
                dgvPalletInfo.DataSource = dtTemp;
            }
            else
            {
                dgvPalletInfo.DataSource = null;
            }
        }


    }
}

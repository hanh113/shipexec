using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Write_ReaderTxt
{
    public class Utility
    {
        /// <summary>
        /// 根据shipmentID获取DN DN_Line 信息
        /// </summary>
        /// <param name="shipmentID"></param>
        /// <param name="DN"></param>
        /// <param name="DN_Line"></param>
        public void GetDNInfo(string shipmentID, out string DN, out string DN_Line)
        {
            DN = string.Empty;
            DN_Line = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append(@"select DN,DN_LINE from ppsuser.g_ds_pick_t t");
            DataTable dt = ClientUtils.ExecuteSQL(sb.ToString()).Tables[0];

            if (dt != null && dt.Rows.Count > 0)
            {
                DN = dt.Rows[0]["DN"].ToString();
                DN_Line = dt.Rows[0]["DN_LINE"].ToString();
            }
        }

        /// <summary>
        /// 根据Carrier 获取Carrier Path
        /// </summary>
        /// <param name="carrier"></param>
        /// <returns></returns>

        public bool WriteFile(string carrier, string DN, string DN_LINE, string path, string ssm_id,string cartonNo= "")
        {
            //if (comboBox1.Text == null || string.IsNullOrEmpty(comboBox1.Text.ToString()))
            //{
            //    MessageBox.Show("请选择", "提示框", MessageBoxButtons.OK);
            //    return;
            //}
            string filename = string.Empty;

            try
            {
                if (carrier == ExpressType.XCPEL.ToString())
                {
                    filename = "cnp" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                    new CNP_Write().cnp_Write(DN, DN_LINE, path, filename,cartonNo);

                }

                if (carrier == ExpressType.XDHLE.ToString() || carrier == ExpressType.XDHLKR.ToString())
                {
                    //打印mother档根据shipment_id判断
                    //string SDHLSQL = @"select sum(cartons) sunn from ppsuser.g_ds_pick_cartons WHERE SHIPMENT_ID = '" + ssm_id + "' AND DN='" + DN + "'";
                    string SDHLSQL = @"select sum(cartons) sunn from ppsuser.g_ds_pick_cartons WHERE SHIPMENT_ID = '" + ssm_id + "'";
                    DataSet SDHLSQLL = ClientUtils.ExecuteSQL(SDHLSQL);
                    string sldn = SDHLSQLL.Tables[0].Rows[0]["sunn"].ToString();
                    int CONII = Convert.ToInt32(sldn);

                    //string CONUT = @"select count(*) con from PPSUSER.ICT_LPS_line where mother_child_tag = 'C' and AC_DN = '" + DN + "'";
                    string CONUT = @"select count(*) con from PPSUSER.ICT_LPS_line where mother_child_tag = 'C' and SHIPMENT_ID = '" + ssm_id + "'";
                    DataSet CON = ClientUtils.ExecuteSQL(CONUT);
                    string COIN = CON.Tables[0].Rows[0]["con"].ToString();
                    int CONI = Convert.ToInt32(COIN);
                    //int CONN = CONI - 1;

                    if (CONII == CONI)
                    {
                        filename = "mother" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".sps";
                        new DHL_M_Write().dhl_m_Write(DN, DN_LINE, path, filename);
                    }


                    filename = "baby" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".sps";
                    new DHL_Baby_Write().dhl_Write(DN, DN_LINE, path, filename,cartonNo);

                }
                string XDHLWPS = "1060025331";
                string XDHLWPX = "1060023973";
                string DHLWPX = "1060032962";
                if (carrier == XDHLWPX || carrier == DHLWPX)
                {
                    filename = "wpx" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".sps";
                    new DHL_WPX_Write().wpx_Write(DN, DN_LINE, path, filename, cartonNo);

                }
                if (carrier == XDHLWPS) //DHL快递公司 EXC规格的打印
                {
                    filename = "ecx" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".sps";
                    new DHL_ECX_Write().DhlExcWrite(DN, DN_LINE, path, filename, cartonNo);

                }
                if (carrier == ExpressType.XSF.ToString())
                {
                    filename = "sf" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                    new SF_Write().sf_Write(DN, DN_LINE, path, filename,cartonNo);

                }
                string XTNTS = "1060019776";
                if (carrier == ExpressType.XTNTA.ToString() || carrier == ExpressType.XTNTKR.ToString() || carrier == ExpressType.XTNTTW.ToString() || carrier == XTNTS)
                {
                    filename = "tnt" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                    new TNT_Write().tnt_Write(DN, DN_LINE, path, filename,cartonNo);

                }
                string XUPSS = "1060022352";
                if (carrier == ExpressType.XUPSC.ToString() || carrier == ExpressType.XUPSM.ToString() || carrier == ExpressType.XUPSN.ToString() || carrier == XUPSS)
                {
                    filename = "ups" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".ups";
                    new UPS_Write().ups_Write(DN, DN_LINE, path, filename,cartonNo);

                }
                if (carrier == ExpressType.XYMT.ToString() || carrier == ExpressType.XYMTSG.ToString())
                {
                    filename = "ymt" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".dat";
                    new YMT_Write().ymt_Write(DN, DN_LINE, path, filename, cartonNo);

                }
            }
            catch (Exception )
            {
                return false;
            }

            return true;
        }

    }
}

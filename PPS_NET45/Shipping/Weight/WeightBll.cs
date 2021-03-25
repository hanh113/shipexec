using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Weight
{
    public class WeightBll
    {
        /// <summary>
        /// 获取check站点进度信息
        /// </summary>
        /// <param name="strPalletNo">栈板号</param>
        /// <returns>信息(10/100%)</returns>
        public static string getCheckStatInfo(string strPalletNo, out bool flag, out string errorMessage)
        {
            flag = true;
            errorMessage = string.Empty;
            WeightDal weightDal = new WeightDal();
            //获取shippment
            string shipmnetId = weightDal.getShipmentIdByPalletNo(strPalletNo);
            if (string.IsNullOrEmpty(shipmnetId))
            {
                flag = false;
                errorMessage = "栈板号错误";
                return string.Empty;
            }
            //获取shipmentId的数量
            float palletNum = weightDal.getPalletQtyByShipmentId(shipmnetId);
            //获取scan数量
            float scanNum = 0F;
            DataTable dataTable = weightDal.getScanQtyByShipmentId(shipmnetId);
            if (dataTable.Rows.Count > 0)
            {
                string qty = dataTable.Rows[0][0].ToString();
                scanNum = float.Parse(qty);
            }
            else
            {
                flag = false;
                errorMessage = "获取站点信息错误";
            }
            int percen = (int)(scanNum / palletNum * 100);
            string resultStr = scanNum.ToString() + '/' + percen.ToString() + '%';
            return resultStr;
        }

        public string PPSInsertWorkLogBy(string insn, string inwc, string macaddress, out string errmsg)
        {

            errmsg = string.Empty;
            WeightDal wd = new WeightDal();
            string strRB = wd.PPSInsertWorkLogByProcedure(insn, inwc, macaddress, out errmsg);
            if (strRB.Equals("NG"))
            {
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }

        public bool CheckShipmentIDHold(string insn, out string errmsg)
        {
            //insn 是PACKPALLETNO
            WeightDal wd = new WeightDal();
            string strRB = wd.CheckShipmentIDHoldByProcedure(insn, out errmsg);
            if (strRB.Equals("NG"))
            {
                return false;
            }
            errmsg = "OK";
            return true;
        }
        public bool CheckExistSSCC18Label(string strPalletNo)
        {
            if (string.IsNullOrEmpty(strPalletNo)) { return false; }
            WeightDal wd = new WeightDal();
            DataSet dataSet = wd.CheckExistSSCC18LabelBySQL(strPalletNo);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckPalletandSsccMatch(string strPalletNo, string strSSCC)
        {
            if (string.IsNullOrEmpty(strPalletNo) || string.IsNullOrEmpty(strSSCC)) { return false; }
            WeightDal wd = new WeightDal();
            DataSet dataSet = wd.CheckExistSSCC18LabelBySQL(strPalletNo, strSSCC);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void CheckShipmentWeightStatus(string inpalletno, out string strregion, out string errmsg)
        {
            WeightDal wd = new WeightDal();
            string strRB = wd.CheckShipmentWeightStatusSP(inpalletno, out strregion, out errmsg);

        }

        public void CheckPalletNoSAWB(string insn, out string outregion, out string errmsg)
        {
            //insn 是PACKPALLETNO
            WeightDal wd = new WeightDal();
            string strRB = wd.CheckPalletNoSAWBByProcedure(insn, out outregion, out errmsg);

        }

        public void CheckPalletNoSAWB(string insn, out string errmsg)
        {
            //insn 是PACKPALLETNO
            WeightDal wd = new WeightDal();
            string strRB = wd.CheckPalletNoSAWBByProcedure(insn, out errmsg);

        }
        public void initLocalBalance(int BalanceType)
        {
            //1:WagonBalance
            //2:BluetoothBalance
            //3.COMWagonBalance
            string strStartupPath = System.Windows.Forms.Application.StartupPath;
            string strShippingPath = strStartupPath + @"\Shipping";
            string strWeightfile = Path.GetFullPath(strShippingPath) + @"\" + "WeightBalance.ini";
            StreamWriter writer = null;
            using (writer = new StreamWriter(strWeightfile, false, Encoding.UTF8))
            {
                writer.WriteLine((BalanceType).ToString());
                writer.Flush();
                writer.Close();
            }

        }
        public int getLocalBalance()
        {
            //1:WagonBalance
            //2:BluetoothBalance
            //3.COMWagonBalance
            string strStartupPath = System.Windows.Forms.Application.StartupPath;
            string strShippingPath = strStartupPath + @"\Shipping";
            string strWeightfile = Path.GetFullPath(strShippingPath) + @"\" + "WeightBalance.ini";

            int BalanceType;


            //读
            string sData = string.Empty;
            StreamWriter writer = null;
            if (!File.Exists(strWeightfile))
            {
                BalanceType = 1;
                using (writer = new StreamWriter(strWeightfile, false, Encoding.UTF8))
                {
                    writer.WriteLine((BalanceType).ToString());
                    writer.Flush();
                    writer.Close();
                }
            }
            else
            {
                using (StreamReader _sr = new StreamReader(strWeightfile))
                {
                    sData = _sr.ReadLine();
                }


                BalanceType = Convert.ToInt32(sData);
            }
            return BalanceType;
        }

        public string changeSNtoShipmentID(string changeSNtoShipmentID, out string errmsg)
        {
            //必须是所有栈板全部做完称重 
            string sql = string.Format(@"Select distinct a.shipment_id 
                                        from ppsuser.t_shipment_pallet a  
                                        where a.pallet_no='{0}' or a.real_pallet_no='{1}' or a.shipment_id='{2}' "
                                   , changeSNtoShipmentID, changeSNtoShipmentID, changeSNtoShipmentID);

            DataTable dt_change = new DataTable();
            try
            {
                dt_change = ClientUtils.ExecuteSQL(sql).Tables[0];
            }
            catch (Exception ex)
            {
                errmsg = "NG" + ex.ToString();
                return "";
            }


            if (dt_change.Rows.Count > 0)
            {
                //如果输入的时real_pallet_no 或者时print_pallet_no 
                //转换位pallet_no 来处理
                changeSNtoShipmentID = dt_change.Rows[0]["shipment_id"].ToString();
                errmsg = "OK";
                return changeSNtoShipmentID;
            }
            else
            {
                errmsg = "NG";
                return "";
            }

        }


        public void ShowShipmentPalletInfo(string palletno, DataGridView dtPallet)
        {
            if (string.IsNullOrEmpty(palletno)) { return; }
            dtPallet.DataSource = null;
            dtPallet.Rows.Clear();
            WeightDal wd = new WeightDal();
            DataTable dataSet = wd.ShowShipmentPalletInfoDataTable(palletno).Tables[0];
            if (dataSet.Rows.Count > 0)
            {
                for (int i = 0; i < dataSet.Rows.Count; i++)
                {
                    //创建行
                    DataGridViewRow dr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in dtPallet.Columns)
                    {
                        dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    //累加序号
                    dr.HeaderCell.Value = (i + 1).ToString();
                    dr.Cells[0].Value = dataSet.Rows[i]["集货单号"].ToString();
                    dr.Cells[1].Value = dataSet.Rows[i]["栈板号"].ToString();
                    dr.Cells[2].Value = dataSet.Rows[i]["箱数"].ToString();
                    dr.Cells[3].Value = dataSet.Rows[i]["重量"].ToString();
                    dr.Cells[4].Value = dataSet.Rows[i]["PICK箱数"].ToString();
                    dr.Cells[5].Value = dataSet.Rows[i]["PACK箱数"].ToString();
                    dr.Cells[6].Value = dataSet.Rows[i]["CHECK结果"].ToString();
                    dr.Cells[7].Value = dataSet.Rows[i]["SECURITY"].ToString();
                    dr.Cells[8].Value = dataSet.Rows[i]["栈板规格"].ToString();
                    try
                    {
                        dtPallet.Invoke((MethodInvoker)delegate ()
                        {
                            dtPallet.Rows.Add(dr);
                        });
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }
            }
        }

        public DataTable GetPalletLabelDataTableBLL(string inpalletno, string isSAWB, string inregion)
        {
            if (string.IsNullOrEmpty(inpalletno)) { return null; }
            WeightDal wd = new WeightDal();
            DataSet dataSet = wd.GetPalletLabelDataTableDAL(inpalletno, isSAWB, inregion);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public string DelPrefixCartonSN(string insn)
        {
            if (insn.Length == 20 && insn.Substring(0, 2).Equals("00"))
            { insn = insn.Substring(2); }
            else if (insn.StartsWith("3S"))
            { insn = insn.Substring(2); }
            else if (insn.StartsWith("S"))
            { insn = insn.Substring(1); }

            return insn;

        }
    }
}

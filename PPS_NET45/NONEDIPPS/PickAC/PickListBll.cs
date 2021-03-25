using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace PickListAC
{
    class PickListBll
    {
        public void ShowStockCarInfo(string ictpartno,string palletno, DataGridView dtStock)
        {
            if (string.IsNullOrEmpty(ictpartno)) { return; }
            dtStock.DataSource = null;
            dtStock.Rows.Clear();
            PickListDal PickDal = new PickListDal();
            DataTable dataSet = PickDal.GetStockCarInfoDataTable(ictpartno,palletno).Tables[0];
            if (dataSet.Rows.Count > 0)
            {
                for (int i = 0; i < dataSet.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(dataSet.Rows[i]["车行号"].ToString()))
                    {
                        dtStock.Columns[1].HeaderText = "箱数";
                    }
                    else
                    {
                        dtStock.Columns[1].HeaderText = "CSN数";
                    }
                    //创建行
                    DataGridViewRow dr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in dtStock.Columns)
                    {
                        dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    //累加序号
                    dr.HeaderCell.Value = (i + 1).ToString();
                    //dr.Cells[0].Value = dataSet.Rows[i]["料号"].ToString();
                    dr.Cells[0].Value = dataSet.Rows[i]["库位"].ToString();
                    dr.Cells[1].Value = dataSet.Rows[i]["箱数"].ToString();
                    dr.Cells[2].Value = dataSet.Rows[i]["车行号"].ToString();

                    try
                    {
                        dtStock.Invoke((MethodInvoker)delegate ()
                        {
                            dtStock.Rows.Add(dr);
                        });
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }
            }

        }

        public DataTable GetSNInfoDataTableBLL(string inputsno)
        {
            if (string.IsNullOrEmpty(inputsno)) { return null; }
            PickListDal PickDal = new PickListDal();
            DataSet dataSet = PickDal.GetSNInfoDataTableDAL(inputsno);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public DataTable GetShipmentTypeBll(string inputsno)
        {
            if (string.IsNullOrEmpty(inputsno)) { return null; }
            PickListDal PickDal = new PickListDal();
            DataSet dataSet = PickDal.GetShipmentTypeDAL(inputsno);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public DataTable GetDataTableBLL(string inputsno)
        {
            if (string.IsNullOrEmpty(inputsno)) { return null; }
            PickListDal PickDal = new PickListDal();
            DataSet dataSet = PickDal.GetDataTableDAL(inputsno);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }


        public string PPSInsertWorkLogBy(string insn, string inwc, string macaddress, out string errmsg)
        {

            errmsg = string.Empty;
            PickListDal pl = new PickListDal();
            string strRB = pl.PPSInsertWorkLogByProcedure(insn, inwc, macaddress, out errmsg);
            if (strRB.Equals("NG"))
            {
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }

        public string DelPrefixCartonSN(string insn)
        {
            if (insn.Length == 20 && insn.Substring(0, 2).Equals("00"))
            { insn = insn.Substring(2); }
            //else if (insn.StartsWith("3S"))
            //{ insn = insn.Substring(2); }
            //else if (insn.StartsWith("S"))
            //{ insn = insn.Substring(1); }

            return insn;
            
        }

        public string GetDBType(string inparatype, out string outparavalue, out string errmsg)
        {

            errmsg = string.Empty;
            PickListDal pl = new PickListDal();
            string strRB = pl.GetDBTypeBySP( inparatype, out  outparavalue, out  errmsg);
            if (strRB.Equals("NG"))
            {
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }
    }
}

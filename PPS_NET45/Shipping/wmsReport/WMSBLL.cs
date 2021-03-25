using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing;

namespace wmsReport
{
    class WMSBLL
    {
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

        public void fillCmb(string strSQL, string colName, ComboBox cmb)
        {

            DataSet dts = ClientUtils.ExecuteSQL(strSQL);
            if (dts.Tables[0].Rows.Count > 0)
            {
                cmb.DataSource = dts.Tables[0];
                cmb.ValueMember = "id";
                cmb.DisplayMember = "name";
            }
            else
            {
                cmb.Items.Clear();
            }
        }

        public void ShowStockInfo(string carlineno, DataGridView dtStock)
        {
            if (string.IsNullOrEmpty(carlineno)) { return; }
            dtStock.DataSource = null;
            dtStock.Rows.Clear();
            WMSDLL wd = new WMSDLL();
            DataTable dataSet = wd.GetStockInfoDataTable(carlineno).Tables[0];
            if (dataSet.Rows.Count > 0)
            {

                for (int i = 0; i < dataSet.Rows.Count; i++)
                {
                    //创建行

                    DataGridViewRow dr = new DataGridViewRow();
                    dtStock.Invoke((MethodInvoker)delegate ()
                    {
                        foreach (DataGridViewColumn c in dtStock.Columns)
                        {
                            dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                        }
                    });
                    try
                    {
                        //DataGridViewRow dr = new DataGridViewRow();
                        //foreach (DataGridViewColumn c in dtStock.Columns)
                        //{
                        //    dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                        //}
                        //累加序号
                        dr.HeaderCell.Value = (i + 1).ToString();
                        dr.Cells[0].Value = dataSet.Rows[i]["location_no"].ToString();
                        dr.Cells[1].Value = dataSet.Rows[i]["custom_sn"].ToString();
                        dr.Cells[2].Value = dataSet.Rows[i]["trolley_line_no"].ToString();
                        dr.Cells[3].Value = dataSet.Rows[i]["pointno"].ToString();
                        dr.Cells[4].Value = dataSet.Rows[i]["group_code"].ToString();
                        dr.Cells[5].Value = dataSet.Rows[i]["delivery_no"].ToString();
                        dr.Cells[6].Value = dataSet.Rows[i]["line_item"].ToString();
                        dr.Cells[7].Value = dataSet.Rows[i]["shipment_id"].ToString();
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
        public void ShowStockInfo2(string carlineno, DataGridView dtStock, string incsn)
        {
            if (string.IsNullOrEmpty(carlineno)) { return; }
            dtStock.Invoke((MethodInvoker)delegate
            {
                dtStock.DataSource = null;
                dtStock.Rows.Clear();
            });
            
            WMSDLL wd = new WMSDLL();
            DataTable dataSet = wd.GetStockInfoDataTable(carlineno).Tables[0];

            if (dataSet.Rows.Count > 0)
            {
                DataGridViewRow dr1 = new DataGridViewRow();
                foreach (DataGridViewColumn c in dtStock.Columns)
                {
                    dr1.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                }
                for (int i = 0; i < dataSet.Rows.Count; i++)
                {
                    //创建行
                    DataGridViewRow dr = dr1.Clone()as DataGridViewRow;
                    
                    //累加序号
                    dr.HeaderCell.Value = (i + 1).ToString();
                    dr.Cells[0].Value = dataSet.Rows[i]["location_no"].ToString();
                    dr.Cells[1].Value = dataSet.Rows[i]["custom_sn"].ToString();
                    dr.Cells[2].Value = dataSet.Rows[i]["trolley_line_no"].ToString();
                    dr.Cells[3].Value = dataSet.Rows[i]["pointno"].ToString();
                    dr.Cells[4].Value = dataSet.Rows[i]["group_code"].ToString();
                    dr.Cells[5].Value = dataSet.Rows[i]["delivery_no"].ToString();
                    dr.Cells[6].Value = dataSet.Rows[i]["line_item"].ToString();
                    dr.Cells[7].Value = dataSet.Rows[i]["shipment_id"].ToString();

                    if (dataSet.Rows[i]["custom_sn"].ToString().Contains(incsn))
                    {
                        dr.DefaultCellStyle.BackColor = Color.Yellow;
                    }

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
        public string PPSInsertWorkLogBy(string incarlineno, string incsn, out string errmsg)
        {

            errmsg = string.Empty;
            WMSDLL wd = new WMSDLL();
            string strRB = wd.WMSPpartCheckByProcedure(incarlineno, incsn, out errmsg);
            if (errmsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }
        }
        public string WMSPpartTrans(string incarlinenofrom, string incarlinenoto, string incsn, out string errmsg)
        {

            errmsg = string.Empty;
            WMSDLL wd = new WMSDLL();
            string strRB = wd.WMSPpartTransByProcedure(incarlinenofrom, incarlinenoto, incsn, out errmsg);
            if (errmsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }
        }



        public string GetCarlinenoByCSN(string incsn, out string carlineno, out string errmsg)
        {

            errmsg = string.Empty;
            WMSDLL wd = new WMSDLL();
            string strRB = wd.GetCarlinenoByCSNByProcedure(incsn, out carlineno, out errmsg);
            if (errmsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }
        }

        public void GetCarlinenoByAdvise(string incsn, out string carlineno)
        {
            WMSDLL wd = new WMSDLL();
            wd.GetCarlinenoByAdviseByProcedure(incsn, out carlineno);

        }
        public string WMSTrolleyMove(string incar, string incarlinenoto, out string errmsg)
        {

            errmsg = string.Empty;
            WMSDLL wd = new WMSDLL();
            string strRB = wd.WMSTrolleyMoveByProcedure(incar, incarlinenoto, out errmsg);
            if (errmsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }
        }
        public void ShowCarStockInfo(string stcar, DataGridView dtStock)
        {
            if (string.IsNullOrEmpty(stcar)) { return; }
            dtStock.DataSource = null;
            dtStock.Rows.Clear();
            WMSDLL wd = new WMSDLL();
            DataTable dataSet = wd.GetCarInfoDataTable(stcar).Tables[0];
            if (dataSet.Rows.Count > 0)
            {
                for (int i = 0; i < dataSet.Rows.Count; i++)
                {
                    //创建行
                    DataGridViewRow dr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in dtStock.Columns)
                    {
                        dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    //累加序号
                   
                    dr.HeaderCell.Value = (i + 1).ToString();
                    dr.Cells[0].Value = dataSet.Rows[i]["trolley_no"].ToString();
                    dr.Cells[1].Value = dataSet.Rows[i]["trolley_line_no"].ToString();
                    dr.Cells[2].Value = dataSet.Rows[i]["pointno"].ToString();
                    dr.Cells[3].Value = dataSet.Rows[i]["pallet_no"].ToString();
                    dr.Cells[4].Value = dataSet.Rows[i]["carton_no"].ToString();
                    dr.Cells[5].Value = dataSet.Rows[i]["custom_sn"].ToString();
                    dr.Cells[6].Value = dataSet.Rows[i]["delivery_no"].ToString();
                    dr.Cells[7].Value = dataSet.Rows[i]["line_item"].ToString();
                    dr.Cells[8].Value = dataSet.Rows[i]["location_no"].ToString();

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
    }
}

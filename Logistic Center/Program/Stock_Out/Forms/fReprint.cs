using ClientUtilsDll;
using ClientUtilsDll.Forms;
using Stock_Out.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock_Out.Forms
{
    public partial class fReprint : PPSForm
    {
        corebridge core;
        ExecutionResult exeRes;
        public fReprint()
        {
            core = new corebridge();
            exeRes = new ExecutionResult();
            InitializeComponent();
        }

        private void tbLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            exeRes = new ExecutionResult();
            exeRes = this.PrintLabel();
            ShowMSG(exeRes.Message, exeRes.Status ? 3 : 0);
            tbLocation.SelectAll();
            tbLocation.Focus();
        }

        private ExecutionResult PrintLabel()
        {
            exeRes = new ExecutionResult();
            try
            {
                //获取栈板信息
                exeRes = core.GetLabelInfo(tbLocation.Text.Trim().ToUpper());
                if (exeRes.Status)
                {
                    DataTable dt = (DataTable)exeRes.Anything;
                    //打印标签
                    string strStartPath = Application.StartupPath;
                    //label btw path
                    string strLabelPath = strStartPath + @"\Shipping\Transfer_PalletPartLabel.btw";
                    //label dat path
                    string strDatPath = strStartPath + @"\Shipping\Transfer_PalletPartLabel.dat";
                    //label lst path
                    string strLstPath = strStartPath + @"\Shipping\Transfer_PalletPartLabel.lst";
                    //delete histroy file
                    if (File.Exists(strLstPath))
                        File.Delete(strLstPath);
                    //read header params
                    StreamReader _sr = new StreamReader(strDatPath);
                    string strLabelHeader = _sr.ReadLine();//paramters
                    string strLabelParams = tbLocation.Text.Trim().ToUpper() + "|";
                    //add label params
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string palletNo = dt.Rows[i]["pallet_no"].ToString();
                        string partNo = dt.Rows[i]["part_no"].ToString();
                        string MPN_ = dt.Rows[i]["MPN"].ToString();
                        string perQty = dt.Rows[i]["PER_QTY"].ToString();
                        strLabelParams += palletNo + "|" + MPN_ + "|" + partNo + "|" + perQty + "|";
                    }
                    //create & write lst file
                    File.AppendAllText(strLstPath, strLabelHeader + Environment.NewLine + strLabelParams, Encoding.Default);
                    ClientUtils.PrintBartender(strLabelPath, strLstPath);
                    exeRes = new ExecutionResult { Status = true, Message = tbLocation.Text.Trim().ToUpper() + "Print OK." };
                }
            }
            catch (Exception ex)
            {
                exeRes = new ExecutionResult { Status = false, Message = ex.Message };
            }
            return exeRes;
        }


    }
}

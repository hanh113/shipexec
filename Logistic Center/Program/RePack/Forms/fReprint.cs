using ClientUtilsDll;
using ClientUtilsDll.Forms;
using RePack.Core.Common;
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

namespace RePack.Forms
{
    public partial class fReprint : PPSForm
    {
        #region fields & properties
        private CommonProcessExcute core;
        private ExecutionResult exeRes;
        private string _strPallet;
        #endregion
        #region construe method
        public fReprint()
        {
            exeRes = new ExecutionResult();
            core = new CommonProcessExcute();
            InitializeComponent();
        }
        public fReprint(string strPallet)
        {
            _strPallet = strPallet;
            core = new CommonProcessExcute();
            exeRes = new ExecutionResult();
            InitializeComponent();
        }
        #endregion
        #region form events
        private void fReprint_Load(object sender, EventArgs e)
        {
            this.prgTitle.Text = "Print Label";
            tbPallet.Text = string.IsNullOrEmpty(_strPallet) ? "" : _strPallet;
            tbPallet.SelectAll();
            tbPallet.Focus();
            cbType.SelectedIndex = 1;//默认传入类型为PALLET
        }

        private void tbPallet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            exeRes = this.CheckAndPrintLabel();

            ShowMSG(exeRes.Message, exeRes.Status ? 3 : 0);
            if (exeRes.Status)
            {
                this.Close();
            }
        }
        #endregion
        #region inner method
        private ExecutionResult CheckAndPrintLabel()
        {

            try
            {
                if (cbType.Text.Equals("PALLET"))
                {
                    #region  PALLET REPRINT
                    string strPallet_no, strKeypart, strQty;
                    exeRes = core.GetPrintPalletBySN(tbPallet.Text.Trim(), out strPallet_no, out strKeypart, out strQty);
                    if (!exeRes.Status)
                        return exeRes;
                    string labelparams = string.Format("PALLET_NO,PART_NO,QTY\r\n{0},{1},{2}", strPallet_no, strKeypart, strQty);
                    string strLstPath = System.Windows.Forms.Application.StartupPath + @"\Repack\Label\RePack_Pallet_Label.lst";
                    if (File.Exists(strLstPath))
                    {
                        File.Delete(strLstPath);
                    }
                    File.AppendAllText(strLstPath, labelparams, Encoding.Default);
                    string strBtwPath = System.Windows.Forms.Application.StartupPath + @"\Repack\Label\RePack_Pallet_Label.btw";
                    ClientUtils.PrintBartender(strBtwPath, strLstPath);
                    exeRes.Status = true;
                    exeRes.Message = "Print Success";
                    #endregion
                }
                else if (cbType.Text.Equals("CARTON"))
                {
                    exeRes = core.GetCartonLabel(tbPallet.Text.Trim());
                    if (exeRes.Status)
                    {
                        fMain fm = new fMain();
                        fm.PrintCartonLabel((DataTable)exeRes.Anything);
                    }
                }
                else
                {
                    exeRes.Status = false;
                    exeRes.Message = "Unknow Reprint Type.";
                }
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;
            }
            return exeRes;
        }
        #endregion
    }
}

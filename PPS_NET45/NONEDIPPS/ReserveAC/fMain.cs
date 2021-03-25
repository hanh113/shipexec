using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReverseAC
{
    public partial class fMain : Form
    {
        public string InShipmentId { get; set; }
        public string InCartonNo { get; set; }
        public fMain()
        {
            InitializeComponent();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    #region 检查Hold例子
        //    string errorMessage = string.Empty;
        //    bool isExistHold = ReserveBll.CheckHold("ID", out errorMessage);
        //    if (isExistHold)
        //    {
        //        MessageBox.Show(errorMessage);
        //    }
        //    #endregion
        //}

        #region Event/fMain_Load
        private void fMain_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(InShipmentId))
            {
                txtShipmentId.Text = InShipmentId;
                KeyPressEventArgs keyPress = new KeyPressEventArgs((char)Keys.Enter);
                txtShipmentId_KeyPress(txtShipmentId, keyPress);
            }
            else if (!string.IsNullOrEmpty(InCartonNo))
            {
                TxtCartonNo.Text = InCartonNo;
                KeyPressEventArgs keyPress = new KeyPressEventArgs((char)Keys.Enter);
                TxtCartonNo_KeyPress(TxtCartonNo, keyPress);
            }
            ShowDataGridView();
        }
        #endregion

        #region Event/DgvInfo_CellClick
        private void DgvInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //点击行改变SEL字段值
            int currentRowIndex = DgvInfo.CurrentRow.Index;
            int colorCal = currentRowIndex + 1;
            Color perColor = SystemColors.Window;
            float mod = colorCal % 2;
            if (mod == 0)
            {
                perColor = SystemColors.GradientActiveCaption;
            }
            if (DgvInfo.Rows[currentRowIndex].Cells["SEL"].Value.ToString() == "True")
            {
                DgvInfo.Rows[currentRowIndex].Cells["SEL"].Value = false;
                DgvInfo.Rows[currentRowIndex].DefaultCellStyle.BackColor = perColor;
            }
            else
            {
                DgvInfo.Rows[currentRowIndex].Cells["SEL"].Value = true;
                DgvInfo.Rows[currentRowIndex].DefaultCellStyle.BackColor = Color.Aquamarine;
            }
        }
        #endregion

        #region Event/BtUnHold_Click
        private void BtUnHold_Click(object sender, EventArgs e)
        {
            ReverseBll reserverBll = new ReverseBll();
            DataTable dtInfo = DgvInfo.DataSource as DataTable;
            string strMessage = string.Empty;
            bool flag = reserverBll.SetSelectedUnHold(dtInfo, out strMessage);
            if (!flag)
            {
                ShowMsg(strMessage, 0);
            }
            else
            {
                ShowMsg("UnHold 成功", 3);
            }
            //重新绑定
            ShowDataGridView();
        }
        #endregion

        #region Event/txtShipmentId_KeyPress
        private void txtShipmentId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(txtShipmentId.Text.Trim()) ||
                e.KeyChar != (char)Keys.Enter)
            {
                return;
            }
            //清空dgvShipmentInfo
            DataTable dtHoldDataTalbe = (DataTable)DgvInfo.DataSource;
            if (dtHoldDataTalbe != null)
            {
                dtHoldDataTalbe.Rows.Clear();
                DgvInfo.DataSource = dtHoldDataTalbe;
            }
            //获取Info
            ReverseBll reverseBll = new ReverseBll();
            dtHoldDataTalbe = reverseBll.GetInfoByShipmentId(txtShipmentId.Text.Trim());
            DgvInfo.DataSource = dtHoldDataTalbe;
        }
        #endregion

        #region Event/TxtCartonNo_KeyPress
        private void TxtCartonNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtCartonNo.Text.Trim()) ||
                e.KeyChar != (char)Keys.Enter)
            {
                return;
            }
            //清空dgvShipmentInfo
            DataTable dtHoldDataTalbe = (DataTable)DgvInfo.DataSource;
            if (dtHoldDataTalbe != null)
            {
                dtHoldDataTalbe.Rows.Clear();
                DgvInfo.DataSource = dtHoldDataTalbe;
            }
            //获取Info
            ReverseBll reverseBll = new ReverseBll();
            dtHoldDataTalbe = reverseBll.GetInfoByCartonNo(TxtCartonNo.Text.Trim());
            DgvInfo.DataSource = dtHoldDataTalbe;
        }
        #endregion

        #region Event/UnShip_Click
        private void UnShip_Click(object sender, EventArgs e)
        {
            fCheckReverse fcheck = new fCheckReverse();
            if (fcheck.ShowDialog() != DialogResult.OK)
            {
                ShowMsg("账号权限不正确", 0);
                return;
            }

            try
            {
                ReverseBll reserverBll = new ReverseBll();
                DataTable dtInfo = DgvInfo.DataSource as DataTable;
                string errorMessage = string.Empty;
                DataRow[] unShipArry = dtInfo.Select("SEL = True");
                if (unShipArry.Count() <= 0)
                {
                    ShowMsg("请选择UnShip的ShipmentId", 0);
                    return;
                }
                if (DialogResult.Yes != ShowMsg("是否Unshi选中的行项目", 2))
                {
                    return;
                }
                bool flag = reserverBll.SetSelectedUnShip(dtInfo, out errorMessage);
                if (!flag)
                {
                    ShowMsg(errorMessage, 0);
                }
                else
                {
                    ShowMsg("UnShip OK", 3);
                }
                ShowDataGridView();
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message.ToString(), 0);
            }
        }
        #endregion

        #region Event/TxtMaterial_KeyPress
        private void TxtMaterial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtMaterial.Text.Trim()) ||
               e.KeyChar != (char)Keys.Enter)
            {
                return;
            }
            ReverseBll reverseBll = new ReverseBll();
            //清空DgvInfoTran
            DataTable dtDgvInfo = (DataTable)DgvInfoTran.DataSource;
            if (dtDgvInfo != null)
            {
                dtDgvInfo.Rows.Clear();
                DgvInfoTran.DataSource = dtDgvInfo;
            }
            DataTable dtCartonInfo = reverseBll.GetCartonByMaterial(TxtMaterial.Text.Trim());
            if (dtCartonInfo != null)
            {
                DgvInfoTran.DataSource = dtCartonInfo;
                TxtLocation.Focus();
            }
        }
        #endregion

        #region Event/TxtCarton_KeyPress
        private void TxtCarton_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtCarton.Text.Trim()) ||
               e.KeyChar != (char)Keys.Enter)
            {
                return;
            }
            ShowMsg("", -1);
            string errorMessage = string.Empty;
            ReverseBll reverseBll = new ReverseBll();
            //检查是否在正常单据中
            bool isAlready = reverseBll.CheckCartonAlready(TxtCarton.Text.Trim(), out errorMessage);
            if (!isAlready)
            {
                ShowMsg(errorMessage, 0);
                return;
            }
            //检查料号是否与仓位匹配
            bool isMatchLocation = reverseBll.CheckCartonLocation(TxtCarton.Text.Trim(), TxtLocation.Text.Trim(), out errorMessage);
            if (!isMatchLocation)
            {
                ShowMsg(errorMessage, 0);
                return;
            }
            //插入列
            DataTable dtCartonInfoDataSou = (DataTable)DgvInfoTran.DataSource;
            bool addFlag = reverseBll.AddDgvCartonInfo(TxtCarton.Text.Trim(), ref dtCartonInfoDataSou, out errorMessage);
            if (!addFlag)
            {
                ShowMsg(errorMessage, 0);
                return;
            }
            else
            {
                DgvInfoTran.DataSource = dtCartonInfoDataSou;
            }
            TxtCarton.SelectAll();
            TxtCarton.Focus();
        }
        #endregion

        #region Event/TxtLocation_KeyPress
        private void TxtLocation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtLocation.Text.Trim()) ||
                e.KeyChar != (char)Keys.Enter)
            {
                return;
            }
            ReverseBll reverseBll = new ReverseBll();
            string errorMessage = string.Empty;
            bool isExistLocation = reverseBll.CheckLocationExist(TxtLocation.Text.Trim(), out errorMessage);
            if (!isExistLocation)
            {
                ShowMsg(errorMessage, 0);
                TxtLocation.SelectAll();
                TxtLocation.Focus();
            }
            else
            {
                TxtCarton.SelectAll();
                TxtCarton.Focus();
            } 
        }
        #endregion

        #region Event/BtnClear_Click
        private void BtnClear_Click(object sender, EventArgs e)
        {
            //清空DgvInfoTran
            DataTable dtDgvInfo = (DataTable)DgvInfoTran.DataSource;
            if (dtDgvInfo != null)
            {
                dtDgvInfo.Rows.Clear();
                DgvInfoTran.DataSource = dtDgvInfo;
            }
        }
        #endregion

        #region Event/BtnTran_Click
        private void BtnTran_Click(object sender, EventArgs e)
        {
            DataTable dtDgvInfo = (DataTable)DgvInfoTran.DataSource;
            if (dtDgvInfo != null && dtDgvInfo.Rows.Count > 0)
            {
                if (DialogResult.Yes == ShowMsg("确定转移储位？", 2))
                {
                    ReverseBll reverseBll = new ReverseBll();
                    string errorMessage = string.Empty;
                    for (int i = 0; i < dtDgvInfo.Rows.Count; i++)
                    {

                        bool flag = reverseBll.UpdateLocation(dtDgvInfo.Rows[i]["CARTON_NO"].ToString(), TxtLocation.Text.Trim(), out errorMessage);
                        if (!flag)
                        {
                            ShowMsg(errorMessage, 0);
                            return;
                        }
                        //修改更新后的储位
                        DgvInfoTran.Rows[i].Cells["tbcPcNo"].Value = TxtLocation.Text;
                    }
                    string mess = "转移仓位: " + dtDgvInfo.Rows.Count + " 条记录";
                    ShowMsg(mess, -1);
                }
            }
            else
            {
                ShowMsg("无记录,请扫描箱号", 0);
            }
        }
        #endregion

        #region Function
        /// <summary>
        /// 错误/警告/确认 消息显示
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public DialogResult ShowMsg(string strText, int intType)
        {
            txtMessage.Text = strText;
            switch (intType)
            {
                case 0: //Error     
                    LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                    txtMessage.ForeColor = Color.Red;
                    txtMessage.BackColor = Color.Silver;
                    return DialogResult.None;
                case 1: //Warning                        
                    txtMessage.ForeColor = Color.Blue;
                    txtMessage.BackColor = Color.FromArgb(255, 255, 128);
                    return DialogResult.None;
                case 2: //Confirm
                    return MessageBox.Show(strText, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                default:
                    txtMessage.ForeColor = Color.Green;
                    txtMessage.BackColor = Color.White;
                    return DialogResult.None;
            }
        }

        /// <summary>
        /// 绑定DataGridView
        /// </summary>
        private void ShowDataGridView()
        {
            //清空DataGridView
            DataTable dtHoldDataTable = (DataTable)DgvInfo.DataSource;
            if (dtHoldDataTable != null)
            {
                dtHoldDataTable.Rows.Clear();
                DgvInfo.DataSource = dtHoldDataTable;
            }
            //如果没有入参则显示所有的Hold信息
            if (string.IsNullOrEmpty(txtShipmentId.Text.Trim()) &&
                string.IsNullOrEmpty(TxtCartonNo.Text.Trim()))
            {
                ReverseBll reserverBll = new ReverseBll();
                DataTable dtHoldInfo = reserverBll.GetHoldByAll();
                DgvInfo.DataSource = dtHoldInfo;
            }
            else if (!string.IsNullOrEmpty(txtShipmentId.Text.Trim()))
            {
                ReverseBll reserverBll = new ReverseBll();
                DataTable dtHoldInfo = reserverBll.GetInfoByShipmentId(txtShipmentId.Text.Trim());
                DgvInfo.DataSource = dtHoldInfo;
            }
            else if (!string.IsNullOrEmpty(txtShipmentId.Text))
            {
                ReverseBll reserverBll = new ReverseBll();
                DataTable dtHoldInfo = reserverBll.GetInfoByCartonNo(TxtCartonNo.Text.Trim());
                DgvInfo.DataSource = dtHoldInfo;
            }
        }

        #endregion

    }
}

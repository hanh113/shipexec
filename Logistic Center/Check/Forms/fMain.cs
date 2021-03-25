using ClientUtilsDll.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Check.Forms;
using Check.Beans;
using Check.Core.Common;
using ClientUtilsDll;
using System.IO.Ports;

namespace Check
{
    public partial class fMain : PPSForm
    {
        #region fields & properties
        private CommonProcessExcute core;
        private ExecutionResult exeRes;
        private baseinfo bean;
        private int ComCount = 0;
        #endregion

        #region construe method
        public fMain()
        {
            bean = new baseinfo();
            core = new CommonProcessExcute();
            exeRes = new ExecutionResult();
            InitializeComponent();
        }
        #endregion

        #region    Form Events
        private void setupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fSetup fSetup = new fSetup();
            fSetup.ShowDialog();
            if (string.IsNullOrEmpty(SetupInfo.strLine) || string.IsNullOrEmpty(SetupInfo.strStation))
            {
                ShowMSG("Chose the Line and  Station by Setup Button first. ", 0);
            }
            else
            {
                this.prgTitle.Text = string.Format("Line: {0}  Station: {1} ver:{2}", SetupInfo.strLine, SetupInfo.strStation, this.ProductVersion);
                ShowMSG("Input the Mo Number pls. ", 3);
            }
        }

        private void tbMo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            if (!string.IsNullOrEmpty(SetupInfo.strLine) && !string.IsNullOrEmpty(SetupInfo.strStation))
            {
                this.prgTitle.Text = string.Format("Line: {0}  Station: {1} ver:{2}", SetupInfo.strLine, SetupInfo.strStation, this.ProductVersion);
            }
            bean = new baseinfo();
            bean.strMo = tbMo.Text.Trim();
            exeRes = core.GetMoInfo(bean);
            if (exeRes.Status)
            {
                lbPart.Text = bean.strKeyPart;
                lbSTDWeight.Text = "N/A";
                tbCarton.Enabled = true;
                tbMo.Enabled = false;
                ShowMSG("Scan the Carton pls.", 3);
                tbCarton.Text = "";
                tbCarton.Focus();
            }
            else
            {
                RefreshData();
                ShowMSG(exeRes.Message, 0);
            }
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SetupInfo.strLine) && !string.IsNullOrEmpty(SetupInfo.strStation))
            {
                this.prgTitle.Text = string.Format("Line: {0}  Station: {1} ver:{2}", SetupInfo.strLine, SetupInfo.strStation, this.ProductVersion);
            }
            RefreshData();
        }

        private void tbCarton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            if (!string.IsNullOrEmpty(SetupInfo.strLine) && !string.IsNullOrEmpty(SetupInfo.strStation))
            {
                this.prgTitle.Text = string.Format("Line: {0}  Station: {1} ver:{2}", SetupInfo.strLine, SetupInfo.strStation, this.ProductVersion);
            }
            if (tbCarton.Text.Trim().ToUpper().Equals("UNDO"))
            {
                RefreshData(); return;
            }
            bean.strCarton = tbCarton.Text.Trim().ToUpper();
            bean.strMo = tbMo.Text.Trim().ToUpper();
            exeRes = core.GetCartonInfo(bean);
            if (exeRes.Status)
            {
                lbSTDWeight.Text = string.Format("{0} ~ {1}", bean.dblLowerWeight, bean.dblUpperWeight);
                dgvSnInfo.DataSource = bean.dtSnInfo;
                tbCarton.Enabled = false;
                tbSN.Enabled = true;
                tbSN.SelectAll();
                tbSN.Focus();
            }
            else
            {
                DataTable dtSN = (DataTable)dgvSnInfo.DataSource;
                if (dtSN != null)
                {
                    dtSN.Rows.Clear();
                    dgvSnInfo.DataSource = dtSN;
                }
                lbSTDWeight.Text = "N/A";
                tbCarton.SelectAll();
                tbCarton.Focus();

            }
            ShowMSG(exeRes.Message, exeRes.Status ? 3 : 0);
        }

        private void tbSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            try
            {
                if (!string.IsNullOrEmpty(SetupInfo.strLine) && !string.IsNullOrEmpty(SetupInfo.strStation))
                {
                    this.prgTitle.Text = string.Format("Line: {0}  Station: {1} ver:{2}", SetupInfo.strLine, SetupInfo.strStation, this.ProductVersion);
                }
                if (tbSN.Text.Trim().ToUpper().Equals("UNDO"))
                {
                    RefreshData(); return;
                }

                bean.strCarton = tbCarton.Text.Trim().ToUpper();
                bean.strMo = tbMo.Text.Trim().ToUpper();
                bean.strSN = tbSN.Text.Trim().ToUpper();
                //去前缀部分可以分离出去
                if (bean.strSN.Length == 20 && bean.strSN.Substring(0, 2).Equals("00"))
                {
                    bean.strSN = bean.strSN.Substring(2, 18);
                }
                if (bean.strSN.StartsWith("3S"))
                {
                    bean.strSN = bean.strSN.Substring(2);
                }
                if (bean.strSN.StartsWith("S"))
                {
                    bean.strSN = bean.strSN.Substring(1);
                }
                //检查dgv中是否包含扫描SN 
                int idgvRowOK = 0;
                for (int i = 0; i < dgvSnInfo.Rows.Count; i++)
                {
                    //已扫描的跳过检查
                    if (dgvSnInfo.Rows[i].DefaultCellStyle.BackColor.Equals(Color.Green))
                    {
                        idgvRowOK++;
                        continue;
                    }
                    else if (dgvSnInfo.Rows[i].Cells["CUSTOMER_SN"].Value.Equals(bean.strSN))
                    {
                        idgvRowOK++;
                        dgvSnInfo.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                        exeRes.Status = true;
                    }
                    //遍历后无匹配项报错
                    if (i == dgvSnInfo.Rows.Count - 1 && !exeRes.Status)
                    {
                        ShowMSG(string.Format("This SN [{0}] not belong to Carton [{1}]", bean.strSN, bean.strCarton), 0);
                        return;
                    }
                }
                //所有CSN都已扫描 检查完成，执行过站
                if (dgvSnInfo.Rows.Count.Equals(idgvRowOK))
                {
                    exeRes = core.PassStation(bean, tbWeight.Text);
                    if (exeRes.Status)
                    {
                        DataTable dtSN = (DataTable)dgvSnInfo.DataSource;
                        if (dtSN != null)
                        {
                            dtSN.Rows.Clear();
                            dgvSnInfo.DataSource = dtSN;
                        }
                        //过站成功
                        tbSN.Text = "";
                        tbSN.Enabled = false;
                        lbWeight.Text = "0.0";
                        tbWeight.Text = "0.0";
                        tbCarton.Enabled = true;
                        tbCarton.Text = "";
                        tbCarton.Focus();
                    }
                    else
                    {
                        //过站失败
                        tbSN.SelectAll();
                        tbSN.Focus();
                    }
                }
                else
                {
                    tbSN.SelectAll();
                    tbSN.Focus();
                    exeRes = new ExecutionResult { Status = true, Message = "Scan Next One pls." };
                }
            }
            catch (Exception ex)
            {
                exeRes = new ExecutionResult { Status = false, Message = ex.Message };
            }
            ShowMSG(exeRes.Message, exeRes.Status ? 3 : 0);
        }

        private void cbManual_CheckedChanged(object sender, EventArgs e)
        {
            if (cbManual.Checked)
            {
                lbWeight.Visible = false;
                tbWeight.Visible = true;
            }
            else
            {
                lbWeight.Visible = true;
                tbWeight.Visible = false;
            }
        }

        private void tbWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.tbSN_KeyDown(sender, e);
            }
        }
        #endregion
        #region inner method
        /// <summary>
        /// refresh controls
        /// </summary>
        private void RefreshData()
        {
            DataTable dtSN = (DataTable)dgvSnInfo.DataSource;
            if (dtSN != null)
            {
                dtSN.Rows.Clear();
                dgvSnInfo.DataSource = dtSN;
            }

            lbPart.Text = "N/A";
            lbWeight.Text = "0.0";
            tbWeight.Text = "0.0";
            lbSTDWeight.Text = "N/A";
            tbSN.Text = "";
            tbSN.Enabled = false;
            tbCarton.Text = "";
            tbCarton.Enabled = false;
            tbMo.Enabled = true;
            tbMo.SelectAll();
            tbMo.Focus();
            if (!cbManual.Checked)
                ComPortOpen();

            // ChangeSerialPortStatus();
        }

        /// <summary>
        /// Changes the serial port status from 1-4. 
        /// </summary>
        private void ComPortOpen()
        {
            if (ComPort.IsOpen) { }
            else
            {
                try
                {
                    ComPort.PortName = "COM1";
                    if (!ComPort.IsOpen)
                    {
                        ComPort.Open();
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        ComPort.PortName = "COM2";
                        if (!ComPort.IsOpen)
                        {
                            ComPort.Open();
                        }
                    }
                    catch (Exception exx)
                    {
                        try
                        {
                            ComPort.PortName = "COM3";
                            if (!ComPort.IsOpen)
                            {
                                ComPort.Open();
                            }
                        }
                        catch (Exception exxx)
                        {
                            try
                            {
                                ComPort.PortName = "COM4";
                                if (!ComPort.IsOpen)
                                {
                                    ComPort.Open();
                                }
                            }
                            catch (Exception exxxx)
                            {
                                MessageBox.Show(exxxx.Message, "Open COM Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Changes the serial port status.
        /// </summary>
        public void ChangeSerialPortStatus()
        {
            int comLength;
            string[] arrayCom;
            string comName;

            try
            {
                comLength = SerialPort.GetPortNames().Length;
                arrayCom = new string[comLength];
                if (ComCount == comLength)
                    ComCount = 0;

                arrayCom = SerialPort.GetPortNames();
                comName = arrayCom[ComCount].ToString();
                ComCount = ComCount + 1;
                if (ComPort.IsOpen)
                    ComPort.Close();
                ComPort.PortName = comName;
                ComPort.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Open COM Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// port received data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string tDataFromComPort = "";
            try
            {
                tDataFromComPort = ComPort.ReadLine();
                if (!string.IsNullOrEmpty(tDataFromComPort))
                {
                    // 不同 电子秤 传入数据格式不同  如果有前后缀 可进行截取
                    string Show_Data = tDataFromComPort.Trim();
                    lbWeight.Text = Show_Data;
                    //如果非手输 则同时赋值到tb中
                    if (!cbManual.Checked)
                    {
                        tbWeight.Text = Show_Data;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Open COM Error Again!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


    }
}

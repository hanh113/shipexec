using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientUtilsDll.Forms
{
    public partial class PPSForm : Form
    {
        public PPSForm()
        {
            InitializeComponent();
        }


        private void frmmenu_Click(object sender, EventArgs e)
        {
            base.WindowState = FormWindowState.Normal;
            base.Activate();
        }

        /// <summary>
        /// 根据信息类型显示信息
        /// 0：报错，1：警示，2：弹窗提示，其他为成功提示
        /// </summary>
        /// <param name="Msg">提示信息</param>
        /// <param name="MsgType">信息类型 </param>
        public virtual void ShowMSG(string Msg, int MsgType)
        {
            switch (MsgType)
            {
                case 0: ErrorMSG(Msg); break;
                case 1: WarningMSG(Msg); break;
                case 2: MessageBox.Show(Msg, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question); break;
                default: SuccessMSG(Msg); break;
            }
        }

        /// <summary>
        /// 成功提示
        /// </summary>
        /// <param name="successMSG">信息</param>
        public virtual void SuccessMSG(string successMSG)
        {
            this.prgMSG.ForeColor = Color.Blue;
            this.prgMSG.Text = successMSG;
        }

        public virtual void SuccessMSG(string msgCode, string successMSG)
        {
            string msg = "";
            if (string.IsNullOrEmpty(msgCode))
            {
                msg = successMSG;
            }
            this.SuccessMSG(msgCode + ": " + msg);
        }
        /// <summary>
        /// 报错提示
        /// </summary>
        /// <param name="errorMSG">信息</param>
        public virtual void ErrorMSG(string errorMSG)
        {
            this.prgMSG.ForeColor = Color.Red;
            this.prgMSG.Text = errorMSG;
        }

        public virtual void ErrorMSG(string msgCode, string errorMSG)
        {
            string msg = "";
            if (string.IsNullOrEmpty(msgCode))
            {
                msg = errorMSG;
            }
            this.ErrorMSG(msgCode + ": " + msg);
        }

        /// <summary>
        /// 警示提示
        /// </summary>
        /// <param name="warningMSG">信息</param>
        public virtual void WarningMSG(string warningMSG)
        {
            this.prgMSG.ForeColor = Color.Yellow;
            this.prgMSG.Text = warningMSG;
        }

        public virtual void WarningMSG(string msgCode, string warningMSG)
        {
            string msg = "";
            if (string.IsNullOrEmpty(msgCode))
            {
                msg = warningMSG;
            }
            this.WarningMSG(msgCode + ": " + msg);
        }

        private void prgMSG_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(prgMSG, prgMSG.Text);
        }

        private void prgTitle_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(prgTitle, prgTitle.Text);
        }
    }
}

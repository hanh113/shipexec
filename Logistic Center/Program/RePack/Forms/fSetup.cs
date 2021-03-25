using ClientUtilsDll;
using ClientUtilsDll.Forms;
using Repack.Beans;
using RePack.Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RePack.Forms
{
    public partial class fSetup : SetupForm
    {
        #region fields & properties
        CommonProcessExcute commCore;
        ExecutionResult exeRes;
        #endregion

        #region construe method
        public fSetup()
        {
            commCore = new CommonProcessExcute();
            InitializeComponent();
        }
        #endregion

        #region FormEvents



        private void fSetup_Load(object sender, EventArgs e)
        {
            exeRes = new ExecutionResult();
            exeRes = commCore.GetLineInfo();
            if (exeRes.Status)
            {
                string[] arrLine = ((DataTable)exeRes.Anything).AsEnumerable().Select(x => x.Field<string>("LINE_NAME")).ToArray();
                cbLine.Items.AddRange(arrLine);
                cbLine.DisplayMember = "LINE_NAME";
                cbLine.ValueMember = "LINE_NAME";
                this.cbLine.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show(exeRes.Message);
            }
        }



        private void cbLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iCurrentIndex = this.cbLine.SelectedIndex;
            if (iCurrentIndex < 0) return;
            SetupInfo.strLine = cbLine.Text.Trim();
            exeRes = new ExecutionResult();
            exeRes = commCore.GetStationByLine(SetupInfo.strLine);
            if (exeRes.Status)
            {
                cbStation.DataSource = (DataTable)exeRes.Anything;
                cbStation.DisplayMember = "STATION_NAME";
                cbStation.ValueMember = "STATION_NAME";
                this.cbStation.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show(exeRes.Message);
            }
        }

        private void cbStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iCurrentIndex = this.cbStation.SelectedIndex;
            if (iCurrentIndex < 0) return;
            SetupInfo.strStation = cbStation.Text.Trim();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SetupInfo.strStation = cbStation.Text.Trim();
            SetupInfo.strLine = cbLine.Text.Trim();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SajetClass;
using System.Collections.Specialized;

namespace SajetFilter
{
    public partial class fFilter : Form
    {
        public fFilter()
        {
            InitializeComponent();
        }
        
        public string sSQL;
        StringCollection g_tsField = new StringCollection();

        private void dgvData_DoubleClick(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count > 0 && dgvData.CurrentRow != null)
                DialogResult = DialogResult.OK;
        }

        private void fFilter_Load(object sender, EventArgs e)
        {            
            panel2.BackgroundImage = ClientUtils.LoadImage("ImgFilter.jpg");
            panel1.BackgroundImage = ClientUtils.LoadImage("ImgButton.jpg");            

            combField.Items.Clear();
            DataSet dsSearch = ClientUtils.ExecuteSQL(sSQL);
            dgvData.DataSource = dsSearch;
            dgvData.DataMember = dsSearch.Tables[0].ToString();

            for (int i = 0; i <= dsSearch.Tables[0].Columns.Count - 1; i++)
            {
                combField.Items.Add(dsSearch.Tables[0].Columns[i].ToString());
                g_tsField.Add(dsSearch.Tables[0].Columns[i].ToString());
            }
            if (combField.Items.Count > 0)
                combField.SelectedIndex = 0;

            if (dgvData.Rows.Count > 0)
                dgvData.CurrentCell = dgvData.Rows[0].Cells[0];

            SajetCommon.SetLanguageControl(this);
            editValue.Focus();            
        }

        private void editValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Return)
                return;
            if (dgvData.Rows.Count == 0)
                return;

            for (int i = 0; i <= dgvData.RowCount - 1; i++)
            {
                string sData = dgvData.Rows[i].Cells[g_tsField[combField.SelectedIndex]].Value.ToString();
                if (sData.Length > editValue.Text.Length)
                    sData = sData.Substring(0, editValue.Text.Length);
                
                if (sData == editValue.Text)
                {
                    dgvData.CurrentCell = dgvData.Rows[i].Cells[0];
                    break;
                }
            }
        }
    }
}
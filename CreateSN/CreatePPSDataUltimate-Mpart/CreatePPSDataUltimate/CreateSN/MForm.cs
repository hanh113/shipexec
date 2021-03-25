using System;
using System.Windows.Forms;

namespace CreateSN
{
    public partial class MForm : Form
    {
        public MForm()
        {
            InitializeComponent();
        }

        private void btnMpart_Click(object sender, EventArgs e)
        {
            CreatePPSData.MainForm MpartForm = new CreatePPSData.MainForm();
            //     this.Hide(); 
            this.WindowState = FormWindowState.Minimized;
            MpartForm.Show();
        }

        private void btnPpart_Click(object sender, EventArgs e)
        {
            CreatePPSDataPpart.MainForm PpartForm = new CreatePPSDataPpart.MainForm();
            //    this.Hide();
            this.WindowState = FormWindowState.Minimized;
            PpartForm.Show();
        }

        private void btnAC_Click(object sender, EventArgs e)
        {
            CreatePPSDataAC.MainForm3 ACForm = new CreatePPSDataAC.MainForm3();
            //     this.Hide();
            this.WindowState = FormWindowState.Minimized;
            ACForm.Show();
        }
    }
}

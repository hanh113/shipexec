using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SajetClass;

namespace CRoute
{
    public partial class fChangeRoute : Form
    {
        public fChangeRoute()
        {
            InitializeComponent();
        }

        public string g_sOldRoute, g_sNewRoute,g_sRouteDesc;

        private void btnOK_Click(object sender, EventArgs e)
        {
            g_sNewRoute = editNewRoute.Text.Trim();
            g_sRouteDesc = editRouteDesc.Text.Trim();
            DialogResult = DialogResult.OK;
        }

        private void fChangeRoute_Load(object sender, EventArgs e)
        {
            SajetCommon.SetLanguageControl(this);
            panel2.BackgroundImage = ClientUtils.LoadImage("ImgButton.jpg");
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackgroundImage = ClientUtils.LoadImage("ImgMain.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            LabOldRoute.Text = g_sOldRoute;
            editRouteDesc.Text = g_sRouteDesc;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }        
    }
}
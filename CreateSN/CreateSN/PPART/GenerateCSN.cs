using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CreatePPSDataPpart
{
    public partial class GenerateCSN : Form
    {
        public GenerateCSN()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            geneCSNfunc gcf = new geneCSNfunc();

            gcf.CheckStrLen(txtPPP, 3);
            gcf.CheckStrLen(txtBu, 1);
            gcf.CheckStrLen(txtCC, 2);
            gcf.CheckStrLen(txtBl, 1);
            gcf.CheckStrLen(txtEEEE, 4);
            gcf.CheckStrLen(txtEl, 1);

            string PPP;
            PPP = txtPPP.Text;

            string Bu;
            Bu = txtBu.Text;

            string CC;
            CC = txtCC.Text;

            string Bl;
            Bl = txtBl.Text;

            string EEEE;
            EEEE = txtEEEE.Text;

            string El;
            El = txtEl.Text;

            //string X;
            //X = "X";


            int n;
            n = Convert.ToInt32(TextBox2.Text);

            // TextBox1.Text = PPP + Bu + CC + Bl + SSSS + EEEE + El + X
            lbxCSN.Items.Clear();
            string tt;
            tt = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();

            string SSSS;

            for (int i = 1; i <= n ; i++)
            {
                //GetNumtoWv
                string aa;
                aa = "000" + gcf.GetNumtoWvV((i).ToString(), "A", "-");
                SSSS = aa.Substring(aa.Length - 4, 4);

                string CSN;
                CSN = PPP + Bu + CC + Bl + SSSS + EEEE + El;
                CSN = CSN + gcf.CheckSum(CSN);

                lbxCSN.Items.Add(CSN);

                string sFile = tt + "log.txt";
                File.AppendAllText(sFile, CSN+"\r\n", Encoding.Default);
               
            }

        TextBox2.Text = "1";
        }
   
        private void button1_Click(object sender, EventArgs e)
        {
            geneCSNfunc gcf = new geneCSNfunc();
            string a = gcf.GetNumtoWvV((100).ToString(), "A", "-");
            MessageBox.Show(a);
        }
    }
}

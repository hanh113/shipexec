using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClientUtilsDll;

namespace Shipment
{
    public partial class fCheck : Form
    {
        public fCheck()
        {
            InitializeComponent();
            ClientUtils.runBackground((Form)this);
        }

      

        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(emp.Text.Trim()) || string.IsNullOrEmpty(password.Text.Trim()))
                {
                    MessageBox.Show("账号或密码不能为空".TL());
                }
                if (CheckEmp(emp.Text.Trim(), password.Text.Trim()))
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    DialogResult = DialogResult.Cancel;
                }

            }
        }

        private bool CheckEmp(string emp, string psw)
        {
            if (string.IsNullOrEmpty(emp)||string.IsNullOrEmpty(psw))

            {
                return false;
            }

            string sql = "SELECT EMP_ID FROM SAJET.SYS_EMP WHERE EMP_NO=:EMP_NO AND PASSWD = SAJET.PASSWORD.ENCRYPT (:PWD)";
            object[][] param = new object[2][];
            param[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "EMP_NO", emp };
            param[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PWD", psw };

           DataSet ds= ClientUtils.ExecuteSQL(sql,param);
           if (ds.Tables[0].Rows.Count > 0)
           {
               string empid = ds.Tables[0].Rows[0]["EMP_ID"].ToString();
               if (empid == "10000353")
               {
                   return true;
               }
               return false;
           }
           else
           {
               return false;
           }
               
        }

        private void emp_KeyDown(object sender, KeyEventArgs e)
        {if(e.KeyCode!=Keys.Enter){
            return;
        }
            
            if(string.IsNullOrEmpty(emp.Text.Trim()))
                return;
            
            password.Focus();
        }

        private void fCheck_Load(object sender, EventArgs e)
        {

        }
    }
}

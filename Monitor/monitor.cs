using MESModel;
using Newtonsoft.Json;
using OperationWCF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class monitor : Form
    {
        Dictionary<string, string> keyValues;
        public monitor()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //  ClientUtils.ServerUrl = "http://10.171.16.201:8090/WCF_RemoteObject";
            if (ConfigurationManager.AppSettings.HasKeys())
            {
                keyValues = new Dictionary<string, string>();
                foreach (string theKey in ConfigurationManager.AppSettings.Keys)
                {
                    keyValues.Add(theKey, ConfigurationManager.AppSettings.Get(theKey));
                }
                cbHost.Items.AddRange(keyValues.Values.ToArray());
                cbHost.Text = cbHost.Items[0].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lbcount.Text = "0";
            //   GetUserList();
            AsyncGetUserList();
        }

        private async void AsyncGetUserList()
        {
            var ResFromConsumingMethod = ConsumingMethod();
            Dictionary<string, List<ClientObject>> keyValuePairs = JsonConvert.DeserializeObject<Dictionary<string, List<ClientObject>>>(await ResFromConsumingMethod);
            DataTable dt = new DataTable();
            dt.Columns.Add("computerName");
            dt.Columns.Add("userNo");
            dt.Columns.Add("userName");
            dt.Columns.Add("loginTime");

            DataRow dr = dt.NewRow();
            foreach (var values in keyValuePairs.Values)
            {
                foreach (var item in values)
                {
                    dr = dt.NewRow();
                    dr[0] = item.computerName;
                    dr[1] = item.userNo;
                    dr[2] = item.userName;
                    dr[3] = item.loginTime;
                    dt.Rows.Add(dr);
                }
            }
            dataGridView1.DataSource = dt;
            lbcount.Text = dt.Rows.Count.ToString();
        }

        private Task<string> ConsumingMethod()
        {
            var task = Task.Run(() =>
            {
                return ClientUtils.GetUserList();
            });
            task.Wait();
            return task;
        }

        public async void GetUserList()
        {
            Dictionary<string, List<ClientObject>> keyValuePairs = new Dictionary<string, List<ClientObject>>();
            DataTable dt = new DataTable();
            await CallWcf(keyValuePairs, dt);
        }

        private Task CallWcf(Dictionary<string, List<ClientObject>> keyValuePairs, DataTable dt)
        {
            var task = Task.Run(() =>
            {
                keyValuePairs = JsonConvert.DeserializeObject<Dictionary<string, List<ClientObject>>>(ClientUtils.GetUserList());
                dt.Columns.Add("computerName");
                dt.Columns.Add("userNo");
                dt.Columns.Add("userName");
                dt.Columns.Add("loginTime");

                DataRow dr = dt.NewRow();
                foreach (var values in keyValuePairs.Values)
                {
                    foreach (var item in values)
                    {
                        dr = dt.NewRow();
                        dr[0] = item.computerName;
                        dr[1] = item.userNo;
                        dr[2] = item.userName;
                        dr[3] = item.loginTime;
                        dt.Rows.Add(dr);
                    }
                }
            });
            task.Wait();
            dataGridView1.DataSource = dt;
            return task;
        }

        public int rowindex = -1;
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                contextMenuStrip1.Show(Control.MousePosition.X, Control.MousePosition.Y);
                rowindex = e.RowIndex;
            }
            else if (e.Button == MouseButtons.Left && e.RowIndex >= 0 && dataGridView2.Rows.Count > 0)
            {
                DataTable dt = (DataTable)this.dataGridView2.DataSource;
                dt.Rows.Clear();
                dataGridView2.DataSource = dt;
                lbqty.Text = "0";
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //rerownload client all dll
            IClientHost ws = HttpChannel.Get<IClientHost>(string.Format("http://{0}:8901/ClientObject", dataGridView1.Rows[rowindex].Cells[0].Value.ToString() + ".luxshare.com.cn"));
            ws.ProcessMsg("ReDownload");
            this.button1_Click(sender, e);
        }

        private void cbHost_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (keyValues.Count > 0)
            {
                //change the server url
                tbDesc.Text = keyValues.FirstOrDefault(g => g.Value == cbHost.Text).Key;
                ClientUtils.ServerUrl = string.Format("http://{0}/WCF_RemoteObject", cbHost.Text);
            }
        }


        private void ToolStripMenuItem_QueryClick(object sender, EventArgs e)
        {
            try
            {
                lbqty.Text = "0";
                //get Client dll ver info
                IClientHost ws = HttpChannel.Get<IClientHost>(string.Format("http://{0}:8901/ClientObject", dataGridView1.Rows[rowindex].Cells[0].Value.ToString() + ".luxshare.com.cn"));
                //get Server dll ver info
                List<FileObject> lsClient = ws.GetFileLists();
                List<FileObject> lsServer = ClientUtils.GetFileList("LoadClient");
                DataTable dt3 = new DataTable();
                dt3.Columns.Add("name");
                dt3.Columns.Add("server_version");
                dt3.Columns.Add("client_version");
                var query = from qClient in lsClient.AsEnumerable().Distinct()
                            from qServer in lsServer.AsEnumerable().Distinct()
                            where qClient.fileName == qServer.fileName
                            select new
                            {
                                name = qClient.fileName,
                                _server = qServer.version ?? qServer.fileAge.ToString(),
                                _client = qClient.version ?? qClient.fileAge.ToString()
                            };
                foreach (var item in query)
                {
                    if (item.name.ToString().ToUpper().EndsWith("DLL") && !item._server.Equals(item._client))
                    {
                        dt3.Rows.Add(item.name, item._server, item._client);
                    }
                    //else if ((!item.name.ToString().ToUpper().EndsWith("DLL")
                    //    && !item.name.ToString().ToUpper().EndsWith("EXE")) 
                    //    && DateTime.Parse(item._server) > DateTime.Parse(item._client))
                    //{
                    //    dt3.Rows.Add(item.name, item._server, item._client);
                    //}
                }
                dataGridView2.DataSource = dt3;
                lbqty.Text = dt3.Rows.Count.ToString();
            }
            catch { }

        }
    }





    [DataContract]
    public class ClientObject
    {
        [DataMember]
        public string computerName;

        [DataMember]
        public string userNo;

        [DataMember]
        public string userName;

        [DataMember]
        public DateTime loginTime;

        [DataMember]
        public int Port;

        public ClientObject(string computerName, string userNo, string userName, DateTime loginTime, int Port)
        {
            this.computerName = computerName;
            this.userNo = userNo;
            this.userName = userName;
            this.loginTime = loginTime;
            this.Port = Port;
        }
    }
}

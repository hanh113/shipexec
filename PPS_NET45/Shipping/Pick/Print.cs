using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace PickList
{
    public class Print
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sn">shipment_id</param>
        /// <returns></returns>
        public bool Print_Label(string sn)
        {
            System.Windows.Forms.ListBox ListParam = new ListBox();
            System.Windows.Forms.ListBox ListData = new ListBox();
            string g_sExeName = ClientUtils.fCurrentProject;
            string assyPath = "";
            string sFileFix = "";
            string sLabelType = "PICKPRINT";
            string sMessage = string.Empty;
            assyPath = Assembly.GetExecutingAssembly().Location;

            assyPath = assyPath.Substring(0, assyPath.LastIndexOf('\\')) + "\\";
            PrintLabel.Setup PrintLabelDll = new PrintLabel.Setup();
            PrintLabel_Bitland.Setup stup = new PrintLabel_Bitland.Setup();
            string labelName = "Pick_DEFAULT";
            string labelPath = assyPath + labelName + ".btw";
            try
            {
                ListParam.Items.Clear();
                ListData.Items.Clear();
                ListData.Items.Add(sn);
                if (!stup.GetPrintData(sLabelType, ref ListParam, ref ListData, out sMessage)) //获得列印数据
                {
                    return false;
                }

                PrintLabelDll.Print_Bartender_DataSource_Single(g_sExeName, sLabelType, sFileFix, labelName, 1, "BARTENDER", "DATASOURCE", ListParam, ListData, out sMessage);
                if (sMessage != "OK")
                {
                    return false;
                }
                //Thread.Sleep(3000);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }

        }
    }
}

using ClientUtilsDll.Forms;
using CRReport.CRfrom;
using Spire.Pdf.General.Render.Decode.Jpeg2000.j2k.image;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Packingparcel
{
    public partial class Printer : PPSForm
    {
        public Printer()
        {
            InitializeComponent();
        }

        private void Printer_Load(object sender, EventArgs e)
        {
            PrintDocument prtdoc = new PrintDocument();
            string strDefaultPrinter = prtdoc.PrinterSettings.PrinterName;//获取默认的打印机名

            foreach (string ss in PrinterSettings.InstalledPrinters)
            {
                ///在列表框中列出所有的打印机,
                this.cbPrinter.Items.Add(ss);
                if (ss == strDefaultPrinter)//把默认打印机设为缺省值
                {
                    cbPrinter.SelectedIndex = cbPrinter.Items.IndexOf(ss);
                }
                SuccessMSG("Chose Printer first pls.");
            }

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            fMain.Printer = cbPrinter.Text;
            var printerName = fMain.Printer;
            XmlDocument doc = new XmlDocument();
            doc.Load(Application.StartupPath + Path.DirectorySeparatorChar.ToString() + "PPS.xml");
            XmlNode node = doc.DocumentElement.SelectSingleNode("/EDIPPS/LC_UPSPrinter");
            if (node == null)
            {

                XmlElement elm = doc.CreateElement("LC_UPSPrinter");
                elm.SetAttribute("Name", "UPS_PRINTER_NAME");
                elm.SetAttribute("Value", printerName);
                doc.DocumentElement.AppendChild(elm.Clone());
                doc.Save(Application.StartupPath + Path.DirectorySeparatorChar.ToString() + "PPS.xml");
            }
            else
            {
                var docx = System.Xml.Linq.XDocument.Load(Application.StartupPath + Path.DirectorySeparatorChar.ToString() + "PPS.xml");
                XElement ele = docx.Descendants("LC_UPSPrinter").Where(x => x.Attribute("Name").Value.Equals("UPS_PRINTER_NAME")).First();
                ele.Attribute("Value").Value = printerName;
                docx.Save(Application.StartupPath + Path.DirectorySeparatorChar.ToString() + "PPS.xml");
            }
            this.Close();
        }
    }
}

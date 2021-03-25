using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wmsReportAC
{
    class PrintInfo
    {
        public PrintInfo()
        {

        }

        public PrintInfo(string palletNo,string partNo,string qty)
        {
            this.palletNo = palletNo; 
            this.partNo = partNo;
            this.qty = qty;
        }
        private string palletNo;
        private string partNo;
        private string qty;
        public string PalletNo
        {
            get { return palletNo; }
            set { palletNo = value; }
        }
        public string PartNo
        {
            get { return partNo; }
            set { partNo = value; }
        }
        public string Qty
        {
            get { return qty; }
            set { qty = value; }
        }

    }
}

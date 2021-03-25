using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Packingparcel.Entitys
{
    class LabelInfo
    {
        //记录labelString的前半部分
        public string CarrierName { get; set; }
        public string COC { get; set; }//poe
        public string Ctry { get; set; }// shiptocontry
        public string ReturnTo1 { get; set; }
        public string ReturnTo2 { get; set; }
        public string ReturnTo3 { get; set; }
        public string ReturnTo4 { get; set; }
        public string Tel { get; set; }
        public string ShipTo1 { get; set; }
        public string ShipTo2 { get; set; }
        public string ShipTo3 { get; set; }
        public string ShipTo4 { get; set; }
        public string ShipTo5 { get; set; }
        public string ShipTo6 { get; set; }
        public string ShipTo7 { get; set; }
        public string ShipTo8 { get; set; }
        public string Origin { get; set; }
        public string DelDate { get; set; }
        public string Special { get; set; }
        public string Cartons { get; set; }
        public string ShipDate { get; set; }
        public string HAWB { get; set; }
        public string Sale { get; set; }
        public string DeliveryNO { get; set; }//PO 等于  deliveryNo
        public string WEB { get; set; }
        public string UUIShipId { get; set; }
        public string UUIShipIdValue { get; set; }
        public string SSCC { get; set; }
        public string KnBoxNo { get; set; }
    }
}

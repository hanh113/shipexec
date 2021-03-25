using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackLogic
{
    class ShipmentInfo
    {
        public string ShipmentID { get; set; }

        public string PalletNo { get; set; }

        public string POE { get; set; }

        public string ShipmentType { get; set; }

        public string Region { get; set; }

        public string Type { get; set; }

        public string ICTPN { get; set; }

        public string Transport { get; set; }

        public string ServiceLevel { get; set; }

        public string CarrierCode { get; set; }

        public string CarrierName { get; set; }

        public string CarrierSCACCode { get; set; }

        public bool IsMix { get; set; }

        public string DeliveryNo { get; set; }

        public string LineItem { get; set; }

        public string ShipPlant { get; set; }

        public string DSGS1Flag { get; set; }

        public string PackCode { get; set; }

        public string ShippingLabelType { get; set; }

        public string GS1Label { get; set; }

        public string UUI { get; set; }

        public string ComsumerPackingList { get; set; }

        public string ChannelPackingList { get; set; }

        public string DeliveryNote { get; set; }
    }
}

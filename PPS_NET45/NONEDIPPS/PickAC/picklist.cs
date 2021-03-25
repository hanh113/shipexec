using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PickListAC
{
    class picklist
    {
        private string seq;

        public string Seq
        {
            get { return seq; }
            set { seq = value; }
        }


        private string sn;//项次

        public string Sn
        {
            get { return sn; }
            set { sn = value; }
        }
        private string region;//地区

        public string Region
        {
            get { return region; }
            set { region = value; }
        }
        private string shipment_id;//集货单号

        public string Shipment_id
        {
            get { return shipment_id; }
            set { shipment_id = value; }
        }
        private string dnNumber;//DNNumber

        public string DnNumber
        {
            get { return dnNumber; }
            set { dnNumber = value; }
        }
        private string dn_Line;//DN_Line

        public string Dn_Line
        {
            get { return dn_Line; }
            set { dn_Line = value; }
        }
        private string hawb;//HAWB

        public string Hawb
        {
            get { return hawb; }
            set { hawb = value; }
        }
        private string country;//国家

        public string Country
        {
            get { return country; }
            set { country = value; }
        }
        private string pallet_type;//栈板类型

        public string Pallet_type
        {
            get { return pallet_type; }
            set { pallet_type = value; }
        }
        private string poe;//入口港

        public string Poe
        {
            get { return poe; }
            set { poe = value; }
        }
        private string pallet_no;//栈板号

        public string Pallet_no
        {
            get { return pallet_no; }
            set { pallet_no = value; }
        }
        private string ictpn;//料号

        public string Ictpn
        {
            get { return ictpn; }
            set { ictpn = value; }
        }
        private string suggest_store;//储位

        public string Suggest_store
        {
            get { return suggest_store; }
            set { suggest_store = value; }
        }
        private string qty;//出货数量

        public string Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        private string out_qty;//出货箱数

        public string Out_qty
        {
            get { return out_qty; }
            set { out_qty = value; }
        }
        private string pick_qty;//Pick数量

        public string Pick_qty
        {
            get { return pick_qty; }
            set { pick_qty = value; }
        }
        private string pack_type;//包装类型

        public string Pack_type
        {
            get { return pack_type; }
            set { pack_type = value; }
        }
        private string transport;//运输方式

        public string Transport
        {
            get { return transport; }
            set { transport = value; }
        }
        private string type;//类型

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private string qtys;//库存数量

        public string Qtys
        {
            get { return qtys; }
            set { qtys = value; }
        }
        private string carrier;//货代

        public string Carrier
        {
            get { return carrier; }
            set { carrier = value; }
        }
        private string shipping_time;//出货日期

        public string Shipping_time
        {
            get { return shipping_time; }
            set { shipping_time = value; }
        }
        private string lineNo; //站点

        public string LineNo
        {
            get { return lineNo; }
            set { lineNo = value; }
        }



        private string p_status;//状态

        public string P_status
        {
            get { return p_status; }
            set { p_status = value; }
        }


    }
}

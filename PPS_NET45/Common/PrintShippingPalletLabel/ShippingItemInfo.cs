using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

namespace ShippingScan
{
    public class ShippingItemInfo
    {
        private string rowid;

        public string Rowid
        {
            get { return rowid; }
            set { rowid = value; }
        }
        private string shipping_id;

        public string Shipping_id
        {
            get { return shipping_id; }
            set { shipping_id = value; }
        }

        private string dn_No;

        public string Dn_No
        {
            get { return dn_No; }
            set { dn_No = value; }
        }

        private string shipping_item;

        
        public string Shipping_item
        {
            get { return shipping_item; }
            set { shipping_item = value; }
        }
        private string part_no;

        public string Part_no
        {
            get { return part_no; }
            set { part_no = value; }
        }

        private string part_id;

        public string Part_id
        {
            get { return part_id; }
            set { part_id = value; }
        }
        private string part_spec;

        public string Part_spec
        {
            get { return part_spec; }
            set { part_spec = value; }
        }
        private int qty;

        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        private int usedqty;

        public int Usedqty
        {
            get { return usedqty; }
            set { usedqty = value; }
        }
        private int cqty;

        public int Cqty
        {
            get { return cqty; }
            set { cqty = value; }
        }
        private int usedcqty;

        public int Usedcqty
        {
            get { return usedcqty; }
            set { usedcqty = value; }
        }
        private string ship_to;

        public string Ship_to
        {
            get { return ship_to; }
            set { ship_to = value; }
        }
        private string vehicle_no;

        public string Vehicle_no
        {
            get { return vehicle_no; }
            set { vehicle_no = value; }
        }

        private string version;

        public string Version
        {
            get { return version; }
            set { version = value; }
        }

        private int endCqty;

        public int EndCqty
        {
            get { return endCqty; }
            set { endCqty = value; }
        }
        private string sscc;

        public string Sscc
        {
            get { return sscc; }
            set { sscc = value; }
        }

        private string carrier;

        public string Carrier
        {
            get { return carrier; }
            set { carrier = value; }
        }
        private string coc;

        public string Coc
        {
            get { return coc; }
            set { coc = value; }
        }
        private string ctry;

        public string Ctry
        {
            get { return ctry; }
            set { ctry = value; }
        }
        private string return_to;

        public string Return_to
        {
            get { return return_to; }
            set { return_to = value; }
        }

        private string tel;

        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }
        private string origin;

        public string Origin
        {
            get { return origin; }
            set { origin = value; }
        }
        private string cartons;

        public string Cartons
        {
            get { return cartons; }
            set { cartons = value; }
        }
        private string ship_date;

        public string Ship_date
        {
            get { return ship_date; }
            set { ship_date = value; }
        }
        private string hawb;

        public string Hawb
        {
            get { return hawb; }
            set { hawb = value; }
        }
        private string po_no;

        public string Po_no
        {
            get { return po_no; }
            set { po_no = value; }
        }
        private string mpn;

        public string Mpn
        {
            get { return mpn; }
            set { mpn = value; }
        }
        private string erp_shipping_no;

        public string Erp_shipping_no
        {
            get { return erp_shipping_no; }
            set { erp_shipping_no = value; }
        }


        private string region;

        public string Region
        {
            get { return region; }
            set { region = value; }
        }

            private string invoiceno;

public string Invoiceno
{
  get { return invoiceno; }
  set { invoiceno = value; }
}

private string mixFlag;

public string MixFlag
{
    get { return mixFlag; }
    set { mixFlag = value; }
}

private string mixPallets;

public string MixPallets
{
    get { return mixPallets; }
    set { mixPallets = value; }
}
private string updateMixFlag;

public string UpdateMixFlag
{
    get { return updateMixFlag; }
    set { updateMixFlag = value; }
}

private string lineItem;

public string LineItem
{
    get { return lineItem; }
    set { lineItem = value; }
}

public ShippingItemInfo Clone() 
{
    ShippingItemInfo item = new ShippingItemInfo();
    item.Carrier = this.Carrier;
    item.Cartons = this.Cartons;
    item.Coc = this.Coc;
    item.Cqty = this.Cqty;
    item.Ctry = this.Ctry;
    item.Dn_No = this.Dn_No;
    item.EndCqty = this.EndCqty;
    item.Erp_shipping_no = this.Erp_shipping_no;
    item.Hawb = this.Hawb;
    item.Invoiceno = this.Invoiceno;
    item.LineItem = this.LineItem;
    item.MixFlag = this.MixFlag;
    item.MixPallets = this.MixPallets;
    item.Mpn = this.Mpn;
    item.Origin = this.Origin;
    item.Part_id = this.Part_id;
    item.Part_no = this.Part_no;
    item.Part_spec = this.Part_spec;
    item.Po_no = this.Po_no;
    item.Qty = this.Qty;
    item.Region = this.Region;
    item.Return_to = this.Return_to;
    item.Rowid = this.Rowid;
    item.Ship_date = this.Ship_date;
    item.Ship_to = this.Ship_to;
    item.Shipping_id = this.Shipping_id;
    item.Shipping_item = this.Shipping_item;
    item.Sscc = this.Sscc;
    item.Tel = this.Tel;
    item.UpdateMixFlag = this.UpdateMixFlag;
    item.Usedcqty = this.Usedcqty;
    item.Usedqty = this.Usedqty;
    item.Vehicle_no = this.Vehicle_no;
    item.Version = this.Version;

    return item;
}

    }


}

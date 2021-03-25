using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRReport.CRfrom
{
    public partial class ConsumerPackingList_EMEA_H
    {
        public ConsumerPackingList_EMEA_H(String acDn)
        {
            setDataSoure(acDn);
        }

        private void setDataSoure(String acDn)
        {
            String strSql = string.Format(@"    select a.deliveryno AC_DN,
                               A.WEBORDER AS WO_NUM,
                               a.orderdate as AC_PO_DATE,
                               a.soldtoname as SO_NAME,
                               a.soldtocompany as SO_COMPANY,
                               a.soldtoaddress as SO_ADDR1,
                               a.soldtoaddress2 as SO_ADDR2,
                               '' as SO_ADDR3,
                               a.soldtocity AS SO_CITY,
                               a.regiondesc AS SO_REGION,
                               '' AS SO_POSTAL,
                               a.soldtocountry as SO_COUNTRY_CODE,
                               a.shiptoname as ST_NAME,
                               a.shiptocompany as ST_COMPANY,
                               a.shiptoaddress as ST_ADDR1,
                               a.shiptoaddress2 as ST_ADDR2,
                               a.shiptoaddress3 as ST_ADDR3,
                               a.shiptocity AS ST_CITY,
                               a.regiondesc AS ST_REGION,
                               '' AS ST_POSTAL,
                               a.shiptocountry as ST_COUNTRY_CODE,
                               B.HAWB,
                               B.SHIPPING_TIME AS SHIP_DATE,
                               '' AS PER_TERM,
                               a.CustSONo as AC_SO,
                               B.DELIVERY_NO AS AC_SO,
                               b.carrier_code AS SHIP_VIA,
                               '' AS OR_PO,
                               a.euheadtext1 AS HEADER_TEXT,
                               '' AS ENTIYT,
                               '' AS AC_ECPON,
                               '' AS AC_SO,
                               a.deliveryno AS AC_DN,
                               a.custdelitem AS AC_DN_LINE,
                               a.mpn AS AC_PN,
                               '' AS MATE_DESC,
                               a.qty AS QTY,
                               '' AS COUNTRY_ORIG,
                               '' AS UPC_EAN,
                               '' AS CID,
                               '' AS CMPN,
                               '' AS CID,
                               '' AS CMPN,
                               '' AS ITEM_TEXT,
                               '' AS ITEM_NO,
                               b.delivery_no AS PON
                               from ppsuser.t_940_unicode   a  left  join 
                               ppsuser.t_order_info    b
                               on a.deliveryno = TRIM(b.delivery_no)
                               and a.custdelitem = b.line_item    
                               where a.deliveryno = '{0}'", acDn);
            DataSet action = new DataSet();
            action.Tables.Add(Util.getDataTaleC(strSql, "ConsumerPacking"));
            Util.CreateDataTable(Constant.ConsumerPackingList_EMEA_H_URL, action);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRReport.CRfrom
{
    public partial class ACHandoverManifest
    {
        public ACHandoverManifest(string shipmentId, string strpalletno, bool PRINTERorPDF, bool ISFIRST, string strPath, string strnull, string strnull2)
        {
            //CarBillList
            setDataSoure4(shipmentId, strpalletno, PRINTERorPDF, ISFIRST, strPath);
        }

        private void setDataSoure4(string shipmentId, string strpalletno, bool PRINTERorPDF, bool ISFIRST, string strPath)
        {
            DataSet ds = new DataSet();
            #region linesql
            string lineSql = string.Format(@"
                                            select d.hawb,
                                   f.mpn as part_no,
                                   f.pallet_no as pallet_id,
                                   sum(f.assign_qty) as shipment_qty
                              from nonedioms.oms_load_car a
                             inner join nonedipps.t_shipment_info d
                                on a.shipment_id = d.shipment_id
                             inner join nonedipps.t_shipment_pallet e
                                on a.shipment_id = e.shipment_id
                               and a.pallet_no = e.pallet_no
                             inner join nonedipps.t_pallet_order f
                                on a.shipment_id = f.shipment_id
                               and a.pallet_no = f.pallet_no
                             where (a.car_no, trunc(a.expectedtime)) in
                                   (select distinct b.car_no, trunc(y.shipping_time)
                                      from nonedioms.oms_load_car b
                                     inner join nonedipps.t_shipment_info y
                                        on b.shipment_id = y.shipment_id
                                        and trunc(b.expectedtime) =trunc(y.shipping_time)
                                     inner join nonedipps.t_shipment_pallet c
                                        on b.shipment_id = c.shipment_id
                                        and b.pallet_no =c.pallet_no
                                     where c.shipment_id = '{0}'
                                        or ( c.pallet_no = '{1}'
                                        or c.real_pallet_no = '{2}'))
                             group by d.hawb, f.mpn, f.pallet_no
                             order by d.hawb, f.pallet_no, f.mpn asc
                                            ", shipmentId, strpalletno, strpalletno);
            #endregion

            #region  headerSql
            string headerSql = string.Format(@"
                select to_char(max(a.expectedtime), 'YYYY/MM/DD') as shipdate,
                       count(distinct a.pallet_no) as totalpallets,
                       sum(e.qty) as totalqty,
                       max(a.car_no) as carno,
                       max(a.drivername) as drivername,
                       max(driverphone) as drivertel
                  from nonedioms.oms_load_car a
                 inner join nonedipps.t_shipment_pallet e
                    on a.shipment_id = e.shipment_id
                   and a.pallet_no = e.pallet_no
                 where (a.car_no, trunc(a.expectedtime)) in
                       (select distinct b.car_no, trunc(y.shipping_time)
                          from nonedioms.oms_load_car b
                         inner join nonedipps.t_shipment_info y
                            on b.shipment_id = y.shipment_id
                            and trunc(b.expectedtime) =trunc(y.shipping_time)
                         inner join nonedipps.t_shipment_pallet c
                            on b.shipment_id = c.shipment_id
                            and b.pallet_no =c.pallet_no
                         where c.shipment_id = '{0}'
                                        or ( c.pallet_no = '{1}'
                                        or c.real_pallet_no = '{2}'))
                ", shipmentId, strpalletno, strpalletno);
            #endregion

            ds.Tables.Clear();

            DataTable dtHeader = Util.getDataTaleC(headerSql, "Header");
            if ((dtHeader == null) || (dtHeader.Rows.Count <= 0))
            {
                throw new Exception("未维护装车信息或装车日期与集货单出货日期不符,请检查0！");
            }
            DataTable dtLine = Util.getDataTaleC(lineSql, "Line");
            if ((dtLine == null) || (dtLine.Rows.Count <= 0))
            {
                throw new Exception("未维护装车信息或装车日期与集货单出货日期不符,请检查1！");
            }
            ds.Tables.Add(dtHeader);
            ds.Tables.Add(dtLine);
            //ds.WriteXmlSchema(@"C:\aa.xml");
            string strCRReportName = string.Empty;
            strCRReportName = Constant.CarManifest_URL;
            int iisfirst = 0;
            if (ISFIRST)
            {
                iisfirst = 3;
            }
            else
            {
                iisfirst = 1;
            }

            if (PRINTERorPDF)
            {

                Util.CreateDataTableADDcount(strCRReportName, ds, iisfirst);
                string completeDiskPath = "";
                DateTime d = DateTime.Now;
                if (ISFIRST)
                {
                    completeDiskPath = Application.StartupPath + "\\PDF\\" + "CBL_" + shipmentId + "_" + d.ToString("yyyyMMddHHmmss") + ".pdf";
                }
                else
                {
                    completeDiskPath = Application.StartupPath + "\\PDF\\" + "CBLRE_" + shipmentId + "_" + d.ToString("yyyyMMddHHmmss") + ".pdf";
                }
                if (!string.IsNullOrEmpty(strPath))
                {
                    completeDiskPath = strPath;
                }
                Util.printPDFCrystalReportV2(strCRReportName, ds, completeDiskPath);

                /**Summary:printPDFCrystalReport此方法用于将CR报表导出为PDF，并保存在指定的文件中
                *Paramater List:  ①.Crystal Report 报表rpt文件路径（在Constant中）  ②.获取的Dataset 
                *                 ③.保存PDF文件的路径
                *Returns :   void     ---By Lk 2018/07/08  **/
            }
            else
            {
                //MessageBox.Show("PDF打印");
                string completeDiskPath = "";
                DateTime d = DateTime.Now;
                completeDiskPath = Application.StartupPath + "\\PDF\\" + "CBL_" + shipmentId + "_" + d.ToString("yyyyMMddHHmmss") + ".pdf";
                if (!string.IsNullOrEmpty(strPath))
                {
                    completeDiskPath = strPath;
                }
                Util.printPDFCrystalReportV2(strCRReportName, ds, completeDiskPath);

                /**Summary:printPDFCrystalReport此方法用于将CR报表导出为PDF，并保存在指定的文件中
                *Paramater List:  ①.Crystal Report 报表rpt文件路径（在Constant中）  ②.获取的Dataset 
                *                 ③.保存PDF文件的路径
                *Returns :   void     ---By Lk 2018/07/08  **/

            }
        }
    }
}

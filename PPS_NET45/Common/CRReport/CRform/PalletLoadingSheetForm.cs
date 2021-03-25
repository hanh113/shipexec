using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRReport.CRfrom
{
    public partial class PalletLoadingSheetForm
    {
        public PalletLoadingSheetForm(string strPalletNo, bool PRINTERorPDF, bool ISFIRST, string strPath)
        {
            setDataSoure(strPalletNo, PRINTERorPDF, ISFIRST, strPath);
        }
        private void setDataSoure(String strPalletNo, bool PRINTERorPDF, bool ISFIRST, string strPath)
        {
            //判断是否为SAWB
            string strPalletNOSAWB = string.Empty;
            string strRPTNAME = string.Empty;
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "packpalletno", strPalletNo };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "outregion", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            DataSet ds0 = ClientUtils.ExecuteProc("PPSUSER.SP_WEIGHT_CHECKPALLETSAWB2", procParams);
            string strResult = ds0.Tables[0].Rows[0]["errmsg"].ToString();
            string strRegion = ds0.Tables[0].Rows[0]["outregion"].ToString();

            DotNetBarcode dnb = new DotNetBarcode();
            string strQRpath = string.Format(@"QRCodeBmp.bmp");
            dnb.Type = DotNetBarcode.Types.QRCode;
            dnb.PrintChar = true;
            //保存QRCode
            dnb.QRSave(strPalletNo, strQRpath, 4);

            //读取QRCodeBmp 
            FileStream fs = new FileStream(strQRpath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            int ilength = (int)br.BaseStream.Length;
            byte[] bt2 = br.ReadBytes(ilength);
            br.Close();
            fs.Close();
            //初始化 DataSet
            DataSet ds = new DataSet();
            string headerSql = string.Empty;

            #region  headerSql
            headerSql = string.Format(@" 
                                select a.pallet_no palletno,
                                       a.real_pallet_no printpalletno,
                                       (select aa.palletseq
                                          from (select tsp2.pallet_no, rownum as palletseq
                                                  from ppsuser.t_shipment_pallet tsp2
                                                 where tsp2.shipment_id in
                                                       (select shipment_id
                                                          from ppsuser.t_shipment_pallet
                                                         where pallet_no = '{0}')
                                                 order by tsp2.pallet_no) aa
                                         where aa.pallet_no = a.pallet_no) curpallet,
                                       (select count(distinct tsp.pallet_no)
                                          from ppsuser.t_shipment_pallet tsp
                                         where tsp.shipment_id = a.shipment_id) totalpallet,
                                       to_char(b.shipping_time, 'dd/mm/yyyy') deliverydate,
                                       '' ref,
                                       case
                                         when b.shipment_type = 'DS' then
                                          b.carrier_code
                                         else
                                          (select distinct scaccode
                                             from pptest.oms_carrier_tracking_prefix d
                                            where trim(d.carriercode) = b.carrier_code
                                              and d.shipmode = b.transport
                                              and d.isdisabled = '0'
                                              and d.type = 'HAWB')
                                       end carrier,
                                       b.hawb hawb,
                                         to_char(a.weight,'FM999990.00') weight,
                                       a.empty_carton + a.carton_qty fullcartonqty,
                                       a.empty_carton emptycartonqty,
                                       ppsuser.t_pallet_bottomdesc(a.pallet_no) mixdesc,
                                       b.hawb SAWB,
                                       a.qty totalqty  
                                  from ppsuser.t_shipment_pallet a
                                  join ppsuser.t_shipment_info b
                                    on a.shipment_id = b.shipment_id
                                 where a.pallet_no = '{1}'
                                            ", strPalletNo, strPalletNo);
            #endregion

            string lineSql = string.Empty;
            if (strResult.Equals("OK-NSAWB"))
            {
                strPalletNOSAWB = "NSAWB";
                if (PRINTERorPDF)
                {
                    strRPTNAME = Constant.PalletLoadingSheet_URL;
                }
                else
                {
                    strRPTNAME = Constant.PalletLoadingSheet_URL_ByGW;
                }
            }
            else if (strResult.Equals("OK-SAWB"))
            {
                strPalletNOSAWB = "SAWB";
                if (PRINTERorPDF)
                {
                    strRPTNAME = Constant.PalletLoadingSheetSAWB_URL;
                }
                else
                {
                    strRPTNAME = Constant.PalletLoadingSheetSAWB_URL_ByGW;
                }
            }
            else
            {
                return;
            }

            lineSql = GetPalletLabelDataTableDAL(strPalletNo, strPalletNOSAWB, strRegion);

            ds.Tables.Clear();

            //ds.Tables.Add(Util.getDataTaleC(headerSql, "Header", sqlparams));
            DataTable dt = Util.getDataTaleC(headerSql, "Header");
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("PALLETNO", System.Type.GetType("System.String"));
            dtNew.Columns.Add("PRINTPALLETNO", System.Type.GetType("System.String"));
            dtNew.Columns.Add("CURPALLET", System.Type.GetType("System.String"));
            dtNew.Columns.Add("TOTALPALLET", System.Type.GetType("System.String"));
            dtNew.Columns.Add("REF", System.Type.GetType("System.String"));
            dtNew.Columns.Add("DELIVERYDATE", System.Type.GetType("System.String"));
            dtNew.Columns.Add("CARRIER", System.Type.GetType("System.String"));
            dtNew.Columns.Add("HAWB", System.Type.GetType("System.String"));
            dtNew.Columns.Add("WEIGHT", System.Type.GetType("System.String"));
            dtNew.Columns.Add("FULLCARTONQTY", System.Type.GetType("System.String"));
            dtNew.Columns.Add("EMPTYCARTONQTY", System.Type.GetType("System.String"));
            dtNew.Columns.Add("MIXDESC", System.Type.GetType("System.String"));
            dtNew.Columns.Add("SAWB", System.Type.GetType("System.String"));
            dtNew.Columns.Add("TOTALQTY", System.Type.GetType("System.String"));
            //dtNew.Columns.Add(new DataColumn("PALLETNOCODE", typeof(byte[])));
            dtNew.Columns.Add(new DataColumn("PALLETNOCODE", System.Type.GetType("System.Byte[]")));



            DataRow dr = dtNew.NewRow();
            dr[0] = dt.Rows[0]["PALLETNO"].ToString();
            dr[1] = dt.Rows[0]["PRINTPALLETNO"].ToString();
            dr[2] = dt.Rows[0]["CURPALLET"].ToString();
            dr[3] = dt.Rows[0]["TOTALPALLET"].ToString();
            dr[4] = dt.Rows[0]["REF"].ToString();
            dr[5] = dt.Rows[0]["DELIVERYDATE"].ToString();
            dr[6] = dt.Rows[0]["CARRIER"].ToString();
            dr[7] = dt.Rows[0]["HAWB"].ToString();
            dr[8] = dt.Rows[0]["WEIGHT"].ToString();
            dr[9] = dt.Rows[0]["FULLCARTONQTY"].ToString();
            dr[10] = dt.Rows[0]["EMPTYCARTONQTY"].ToString();
            dr[11] = dt.Rows[0]["MIXDESC"].ToString();
            dr[12] = dt.Rows[0]["SAWB"].ToString();
            dr[13] = dt.Rows[0]["TOTALQTY"].ToString();
            //dr[14] = br.ReadBytes((int)br.BaseStream.Length);
            dr[14] = bt2;
            dtNew.Rows.Add(dr);
            dtNew.TableName = "Header";
            ds.Tables.Add(dtNew);
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));


            if (PRINTERorPDF)
            {
                //
                if (ISFIRST)
                {
                    Util.CreateDataTableADDcount(strRPTNAME, ds, 1);
                }
                else
                {
                    Util.CreateDataTableADDcount(strRPTNAME, ds, 1);
                }

                //MessageBox.Show("PDF打印");
                string completeDiskPath = "";
                DateTime d = DateTime.Now;
                if (ISFIRST)
                {
                    completeDiskPath = Application.StartupPath + "\\PDF\\" + "PLS_" + strPalletNo + "_" + d.ToString("yyyyMMddHHmmss") + ".pdf";
                }
                else
                {
                    completeDiskPath = Application.StartupPath + "\\PDF\\" + "PLSRE_" + strPalletNo + "_" + d.ToString("yyyyMMddHHmmss") + ".pdf";
                }
                Util.printPDFCrystalReportV2(strRPTNAME, ds, completeDiskPath);
                /**Summary:printPDFCrystalReport此方法用于将CR报表导出为PDF，并保存在指定的文件中
                *Paramater List:  ①.Crystal Report 报表rpt文件路径（在Constant中）  ②.获取的Dataset 
                *                 ③.保存PDF文件的路径
                *Returns :   void     ---By Lk 2018/07/08  **/

            }
            else
            {
                string completeDiskPath = "";
                DateTime d = DateTime.Now;
                completeDiskPath = Application.StartupPath + "\\PDF\\" + "PLS_" + strPalletNo + "_" + d.ToString("yyyyMMddHHmmss") + ".pdf";
                if (!string.IsNullOrEmpty(strPath))
                {
                    completeDiskPath = strPath;
                }
                Util.printPDFCrystalReportV2(strRPTNAME, ds, completeDiskPath);

                /**Summary:printPDFCrystalReport此方法用于将CR报表导出为PDF，并保存在指定的文件中
    *Paramater List:  ①.Crystal Report 报表rpt文件路径（在Constant中）  ②.获取的Dataset 
    *                 ③.保存PDF文件的路径
    *Returns :   void     ---By Lk 2018/07/08  **/

            }
        }

        public string GetPalletLabelDataTableDAL(string inpalletno, string isSAWB, string inregion)
        {
            string sql = string.Empty;
            //20190827
            //1如果是SAWB继续用SAWB的模板
            //2.1 剩余的如果是AMR 和EMEIA 用简约版
            //2.2 剩余的如果是PAC则保持不变
            if (inregion.Equals("AMR") || inregion.Equals("EMEIA"))
            {
                if (isSAWB.Equals("SAWB"))
                {
                    #region SAWB
                    sql = string.Format(@"select DISTINCT bb.pallet_no AS PALLETNO,
                                                  bb.real_pallet_no,
                                                  case  
                                                      when aa.shipment_type = 'DS' then
                                                        aa.carrier_code 
                                                    else  
                                                (SELECT distinct SCACCODE
                                             FROM PPTEST.OMS_CARRIER_TRACKING_PREFIX D
                                            where trim(D.carriercode) = aa.carrier_code
                                              and D.ShipMode = aa.transport
                                              and D.isdisabled = '0'
                                              and D.type = 'HAWB') end carrier_code,
                                                  aa.hawb,aa.poe AS GATEWAY,
                                                  bb.weight,  
                                                  bb.empty_carton + bb.carton_qty fullcartonqty,  
                                                  bb.empty_carton,  
                                                  bb.qty totalqty,
                                                  to_char(aa.shipping_time, 'dd/mm/yyyy') cdt,  
                                                  ppsuser.t_pallet_bottomdesc(bb.pallet_no) mix_desc,  
                                                  aa.shipment_id,  
                                                  '' AS PO ,  
                                                  '' AS POITEM,  
                                                  '' AS DN,  
                                                  '' AS ITEM,  
                                                  decode(aa.shipment_type, 'FD', 'HUB', aa.shipment_type) AS HUBDS,  
                                                  cc.MPN,cc.ictpn,  
                                                  cc.assign_qty AS QTY,  
                                                  cc.GCCN,  
                                                  cc.poe AS POECOC
                                    from ppsuser.t_shipment_info aa
                                    join ppsuser.t_shipment_pallet bb
                                      on aa.shipment_id = bb.shipment_id
                                    join(select a.pallet_no,
                                                 b.hawb as gccn,
                                                 b.mpn,
                                                 b.poe, b.ictpn,
                                                 sum(a.assign_qty) as assign_qty
                                            from ppsuser.t_pallet_order a
                                            join ppsuser.t_order_info b
                                         on a.delivery_no = b.delivery_no
                                        and a.line_item = b.line_item
                                             and a.ictpn = b.ictpn
                                            join ppsuser.t_shipment_sawb c
                                              on b.shipment_id = c.shipment_id
                                             and a.shipment_id = c.sawb_shipment_id
                                           where a.pallet_no = '{0}'
                                           group by a.pallet_no, b.hawb, b.mpn, b.poe, b.ictpn) cc
                                      on bb.pallet_no = cc.pallet_no
                                    join(select *
                                            from(select pallet_no, weight, cdt
                                                    from ppsuser.t_pallet_weight_log
                                                  where pallet_no = '{1}'
                                                     AND PASS = '1'
                                                   order by cdt desc)
                                           where rownum = 1) dd
                                      on bb.pallet_no = dd.pallet_no
                                    join ppsuser.t_pallet_order ee
                                      on dd.pallet_no = ee.pallet_no
                                    left join ppsuser.t_940_unicode ff
                                      on ee.delivery_no = ff.deliveryno
                                     and ee.line_item = ff.custdelitem  
                               order by  cc.ictpn asc,
                                      cc.gccn asc,
                                      cc.poe asc
                                        ", inpalletno, inpalletno);
                    #endregion
                }
                else
                {
                    #region NOSAWB 简约版
                    sql = string.Format(@"select pallet_no AS PALLETNO,
                                                           real_pallet_no,
                                                           carrier_code,
                                                           hawb,
                                                           weight,
                                                           fullcartonqty,
                                                           empty_carton,
                                                           totalqty,
                                                           cdt,
                                                           mix_desc,
                                                           shipment_id,
                                                           itemcustpo AS PO,
                                                           itemcustpoline AS POITEM,
                                                           delivery_no AS DN,
                                                           line_item AS ITEM,
                                                           MPN,
                                                           ictpn,
                                                           poe AS POECOC,
                                                           GATEWAY,
                                                           GCCN,
                                                           shipmenttype AS HUBDS,
                                                           sum(assign_qty) AS QTY
                                                      from (select b.pallet_no,
                                                                   b.real_pallet_no,
                                                                   case
                                                                     when a.shipment_type = 'DS' then
                                                                      a.carrier_code
                                                                     else
                                                                      (SELECT distinct SCACCODE
                                                                         FROM PPTEST.OMS_CARRIER_TRACKING_PREFIX D
                                                                        where trim(D.carriercode) = a.carrier_code
                                                                          and D.ShipMode = a.transport
                                                                          and D.isdisabled = '0'
                                                                          and D.type = 'HAWB')
                                                                   end carrier_code,
                                                                   a.hawb,
                                                                   b.weight,
                                                                   case
                                                                     when a.region = 'EMEIA' and (a.carrier_code like '%DHL%' or
                                                                          a.carrier_name like '%DHL%') and
                                                                          a.service_level = 'WPX' then
                                                                      b.carton_qty
                                                                     else
                                                                      b.empty_carton + b.carton_qty
                                                                   end fullcartonqty,
                                                                   case
                                                                     when a.region = 'EMEIA' and (a.carrier_code like '%DHL%' or
                                                                          a.carrier_name like '%DHL%') and
                                                                          a.service_level = 'WPX' then
                                                                      '0'
                                                                     else
                                                                      to_char(b.empty_carton)
                                                                   end empty_carton,
               
                                                                   b.qty totalqty,
                                                                   to_char(a.shipping_time, 'dd/mm/yyyy') cdt,
                                                                   case
                                                                     when a.region = 'EMEIA' and UPPER(e.SHIPPLANT) LIKE 'MIT%' then
                                                                      'MIT'
                                                                     when pallet_type = '001' then
                                                                      'DO NOT BREAK BULK'
                                                                     when pallet_type = '999' then
                                                                      'CONSOLIDATED'
                                                                   end mix_desc,
                                                                   c.shipment_id,
                                                                   decode(a.shipment_type, 'DS', '', e.itemcustpo) itemcustpo,
                                                                   decode(a.shipment_type, 'DS', '', e.itemcustpoline) itemcustpoline,
                                                                   decode(a.shipment_type, 'DS', '', c.delivery_no) delivery_no,
                                                                   decode(a.shipment_type, 'DS', '', c.line_item) line_item,
                                                                   c.mpn,
                                                                   c.ictpn,
                                                                   c.assign_qty,
                                                                   case
                                                                     when a.shipment_type = 'DS' and a.region = 'EMEIA' and
                                                                          a.poe = 'SA' then
                                                                      e.PORTOFENTRY
                                                                     else
                                                                      a.poe
                                                                   end poe,
                                                                   '' gateway,
                                                                   '' gccn,
                                                                   decode(a.shipment_type, 'FD', 'HUB', a.shipment_type) shipmenttype
                                                              from ppsuser.t_shipment_info a
                                                              join ppsuser.t_shipment_pallet b
                                                                on a.shipment_id = b.shipment_id
                                                              join ppsuser.t_pallet_order c
                                                                on b.pallet_no = c.pallet_no
                                                              join (select *
                                                                     from (select pallet_no, weight, cdt
                                                                             from ppsuser.t_pallet_weight_log
                                                                            where pallet_no = '{0}'
                                                                              AND PASS = '1'
                                                                            order by cdt desc)
                                                                    where rownum = 1) d
                                                                on c.pallet_no = d.pallet_no
                                                              left join (select decode(itemcustpo,
                                                                                      '',
                                                                                      weborderno,
                                                                                      null,
                                                                                      weborderno,
                                                                                      itemcustpo) itemcustpo,
                                                                               itemcustpoline,
                                                                               PORTOFENTRY,
                                                                               deliveryno,
                                                                               custdelitem,
                                                                               SHIPPLANT
                                                                          from ppsuser.t_940_unicode) e
                                                                on c.delivery_no = e.deliveryno
                                                               and c.line_item = e.custdelitem
                                                             where b.pallet_no = '{1}') aa
                                                     group by pallet_no,
                                                              real_pallet_no,
                                                              carrier_code,
                                                              hawb,
                                                              weight,
                                                              fullcartonqty,
                                                              empty_carton,
                                                              totalqty,
                                                              cdt,
                                                              mix_desc,
                                                              shipment_id,
                                                              itemcustpo,
                                                              itemcustpoline,
                                                              delivery_no,
                                                              line_item,
                                                              mpn,
                                                              ictpn,
                                                              poe,
                                                              gateway,
                                                              gccn,
                                                              shipmenttype
                                                     order by ictpn asc ", inpalletno, inpalletno);
                    #endregion
                }
            }
            else
            {
                #region PAC 复杂版
                sql = string.Format(@"

                                select distinct b.pallet_no as palletno,
                                                b.real_pallet_no,
                                                case
                                                  when a.shipment_type = 'DS' then
                                                   a.carrier_code
                                                  else
                                                   (select distinct scaccode
                                                      from pptest.oms_carrier_tracking_prefix d
                                                     where trim(d.carriercode) = a.carrier_code
                                                       and d.shipmode = a.transport
                                                       and d.isdisabled = '0'
                                                       and d.type = 'HAWB')
                                                end carrier_code,
                                                a.hawb,
                                                b.weight,
                                                b.empty_carton + b.carton_qty fullcartonqty,
                                                b.empty_carton,
                                                b.qty totalqty,
                                                to_char(a.shipping_time, 'dd/mm/yyyy') cdt,
                                                ppsuser.t_pallet_bottomdesc(b.pallet_no) mix_desc,
                                                c.shipment_id,
                                                e.itemcustpo as po,
                                                e.itemcustpoline as poitem,
                                                e.deliveryno as dn,
                                                e.custdelitem as item,
                                                c.mpn,
                                                c.ictpn,
                                                c.assign_qty as qty,
                                                case
                                                  when a.shipment_type = 'DS' and a.region = 'EMEIA' and
                                                       a.poe = 'SA' then
                                                   e.portofentry
                                                  else
                                                   a.poe
                                                end as poecoc,
                                                '' gateway,
                                                '' gccn,
                                                decode(a.shipment_type, 'FD', 'HUB', a.shipment_type) as hubds
                                  from ppsuser.t_shipment_info a
                                  join ppsuser.t_shipment_pallet b
                                    on a.shipment_id = b.shipment_id
                                  join ppsuser.t_pallet_order c
                                    on b.pallet_no = c.pallet_no
                                  join (select *
                                          from (select pallet_no, weight, cdt
                                                  from ppsuser.t_pallet_weight_log
                                                 where pallet_no = '{0}'
                                                   and pass = '1'
                                                 order by cdt desc)
                                         where rownum = 1) d
                                    on c.pallet_no = d.pallet_no
                                  left join (select decode(itemcustpo,
                                                           '',
                                                           weborderno,
                                                           null,
                                                           weborderno,
                                                           itemcustpo) itemcustpo,
                                                    itemcustpoline,
                                                    portofentry,
                                                    deliveryno,
                                                    custdelitem,
                                                    shipplant
                                               from ppsuser.t_940_unicode) e
                                    on c.delivery_no = e.deliveryno
                                   and c.line_item = e.custdelitem
                                 where b.pallet_no = '{1}'
                                 order by e.deliveryno asc, e.custdelitem asc
                            ", inpalletno, inpalletno);
                #endregion
            }
            return sql;
        }


        public PalletLoadingSheetForm(string strPalletNo, string strCrystalFullPath, bool isCustom, bool isPDF, string strPDFPath, int nCopies, bool isOnlyGetDS, out DataSet dtCrystal, out string serverFullLabelName)
        {
            setDataSoure2(strPalletNo, strCrystalFullPath, isCustom, isPDF, strPDFPath, nCopies, isOnlyGetDS, out dtCrystal, out serverFullLabelName);
        }
        private void setDataSoure2(String strPalletNo, string strCrystalFullPath, bool isCustom, bool isPDF, string strPDFPath, int nCopies, bool isOnlyGetDS, out DataSet dtCrystal, out string serverFullLabelName)
        {
            //判断是否为SAWB
            string strPalletNOSAWB = string.Empty;
            string strRPTNAME = string.Empty;
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "packpalletno", strPalletNo };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "outregion", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            DataSet ds0 = ClientUtils.ExecuteProc("PPSUSER.SP_WEIGHT_CHECKPALLETSAWB2", procParams);
            string strResult = ds0.Tables[0].Rows[0]["errmsg"].ToString();
            string strRegion = ds0.Tables[0].Rows[0]["outregion"].ToString();

            DotNetBarcode dnb = new DotNetBarcode();
            string strQRpath = string.Format(@"QRCodeBmp.bmp");
            dnb.Type = DotNetBarcode.Types.QRCode;
            dnb.PrintChar = true;
            //保存QRCode
            dnb.QRSave(strPalletNo, strQRpath, 4);


            //读取QRCodeBmp 
            FileStream fs = new FileStream(strQRpath, FileMode.Open, FileAccess.Read);
            //byte[] imageDataTemp = new byte[fs.Length + 1];
            //fs.Read(imageDataTemp, 0, System.Convert.ToInt32(fs.Length));
            BinaryReader br = new BinaryReader(fs);
            int ilength = (int)br.BaseStream.Length;
            byte[] bt2 = br.ReadBytes(ilength);
            br.Close();
            fs.Close();

            //初始化 DataSet
            DataSet ds = new DataSet();
            string headerSql = string.Empty;

            #region  headerSql
            headerSql = string.Format(@" 
                                select a.pallet_no palletno,
                                       a.real_pallet_no printpalletno,
                                       (select aa.palletseq
                                          from (select tsp2.pallet_no, rownum as palletseq
                                                  from ppsuser.t_shipment_pallet tsp2
                                                 where tsp2.shipment_id in
                                                       (select shipment_id
                                                          from ppsuser.t_shipment_pallet
                                                         where pallet_no = '{0}')
                                                 order by tsp2.pallet_no) aa
                                         where aa.pallet_no = a.pallet_no) curpallet,
                                       (select count(distinct tsp.pallet_no)
                                          from ppsuser.t_shipment_pallet tsp
                                         where tsp.shipment_id = a.shipment_id) totalpallet,
                                       to_char(b.shipping_time, 'dd/mm/yyyy') deliverydate,
                                       '' ref,
                                       case
                                         when b.shipment_type = 'DS' then
                                          b.carrier_code
                                         else
                                          (select distinct scaccode
                                             from pptest.oms_carrier_tracking_prefix d
                                            where trim(d.carriercode) = b.carrier_code
                                              and d.shipmode = b.transport
                                              and d.isdisabled = '0'
                                              and d.type = 'HAWB')
                                       end carrier,
                                       b.hawb hawb,
                                         to_char(a.weight,'FM999990.00') weight,
                                       a.empty_carton + a.carton_qty fullcartonqty,
                                       a.empty_carton emptycartonqty,
                                       ppsuser.t_pallet_bottomdesc(a.pallet_no) mixdesc,
                                       b.hawb SAWB,
                                       a.qty totalqty  
                                  from ppsuser.t_shipment_pallet a
                                  join ppsuser.t_shipment_info b
                                    on a.shipment_id = b.shipment_id
                                 where a.pallet_no = '{1}'
                                            ", strPalletNo, strPalletNo);
            #endregion

            string lineSql = string.Empty;
            if (string.IsNullOrEmpty(strCrystalFullPath))
            {
                if (strResult.Equals("OK-NSAWB"))
                {
                    strPalletNOSAWB = "NSAWB";
                    if (!isCustom) //不是关务用默认值
                    {
                        strCrystalFullPath = Constant.PalletLoadingSheet_URL;
                    }
                    else
                    {
                        strCrystalFullPath = Constant.PalletLoadingSheet_URL_ByGW;
                    }

                }
                else if (strResult.Equals("OK-SAWB"))
                {
                    strPalletNOSAWB = "SAWB";
                    if (!isCustom) //不是关务用默认值
                    {
                        strCrystalFullPath = Constant.PalletLoadingSheetSAWB_URL;
                    }
                    else
                    {
                        strCrystalFullPath = Constant.PalletLoadingSheetSAWB_URL_ByGW;
                    }

                }
                else
                {
                    //       MessageBox.Show(strResult + "检查SAWB异常。");
                    serverFullLabelName = "";
                    dtCrystal = null;
                    return;
                }

            }
            lineSql = GetPalletLabelDataTableDAL(strPalletNo, strPalletNOSAWB, strRegion);

            ds.Tables.Clear();

            //ds.Tables.Add(Util.getDataTaleC(headerSql, "Header", sqlparams));
            DataTable dt = Util.getDataTaleC(headerSql, "Header");
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("PALLETNO", System.Type.GetType("System.String"));
            dtNew.Columns.Add("PRINTPALLETNO", System.Type.GetType("System.String"));
            dtNew.Columns.Add("CURPALLET", System.Type.GetType("System.String"));
            dtNew.Columns.Add("TOTALPALLET", System.Type.GetType("System.String"));
            dtNew.Columns.Add("REF", System.Type.GetType("System.String"));
            dtNew.Columns.Add("DELIVERYDATE", System.Type.GetType("System.String"));
            dtNew.Columns.Add("CARRIER", System.Type.GetType("System.String"));
            dtNew.Columns.Add("HAWB", System.Type.GetType("System.String"));
            dtNew.Columns.Add("WEIGHT", System.Type.GetType("System.String"));
            dtNew.Columns.Add("FULLCARTONQTY", System.Type.GetType("System.String"));
            dtNew.Columns.Add("EMPTYCARTONQTY", System.Type.GetType("System.String"));
            dtNew.Columns.Add("MIXDESC", System.Type.GetType("System.String"));
            dtNew.Columns.Add("SAWB", System.Type.GetType("System.String"));
            dtNew.Columns.Add("TOTALQTY", System.Type.GetType("System.String"));
            //dtNew.Columns.Add(new DataColumn("PALLETNOCODE", typeof(byte[])));
            dtNew.Columns.Add(new DataColumn("PALLETNOCODE", System.Type.GetType("System.Byte[]")));


            DataRow dr = dtNew.NewRow();
            dr[0] = dt.Rows[0]["PALLETNO"].ToString();
            dr[1] = dt.Rows[0]["PRINTPALLETNO"].ToString();
            dr[2] = dt.Rows[0]["CURPALLET"].ToString();
            dr[3] = dt.Rows[0]["TOTALPALLET"].ToString();
            dr[4] = dt.Rows[0]["REF"].ToString();
            dr[5] = dt.Rows[0]["DELIVERYDATE"].ToString();
            dr[6] = dt.Rows[0]["CARRIER"].ToString();
            dr[7] = dt.Rows[0]["HAWB"].ToString();
            dr[8] = dt.Rows[0]["WEIGHT"].ToString();
            dr[9] = dt.Rows[0]["FULLCARTONQTY"].ToString();
            dr[10] = dt.Rows[0]["EMPTYCARTONQTY"].ToString();
            dr[11] = dt.Rows[0]["MIXDESC"].ToString();
            dr[12] = dt.Rows[0]["SAWB"].ToString();
            dr[13] = dt.Rows[0]["TOTALQTY"].ToString();
            //dr[14] = br.ReadBytes((int)br.BaseStream.Length);
            dr[14] = bt2;
            dtNew.Rows.Add(dr);
            dtNew.TableName = "Header";
            ds.Tables.Add(dtNew);
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));
            dtCrystal = ds;
            if (!isOnlyGetDS)
            {
                if (isPDF)
                {
                    if (string.IsNullOrEmpty(strPDFPath))
                    {
                        strPDFPath = AppDomain.CurrentDomain.BaseDirectory + @"\PDF\CR_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".PDF";
                    }
                    Util.printPDFCrystalReportV3(strCrystalFullPath, ds, strPDFPath, out serverFullLabelName);
                }
                else
                {
                    Util.CreateDataTableADDcount(strCrystalFullPath, ds, nCopies, out serverFullLabelName);
                    if (string.IsNullOrEmpty(strPDFPath))
                    {
                        strPDFPath = AppDomain.CurrentDomain.BaseDirectory + @"\PDF\CR_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".PDF";
                    }
                    Util.printPDFCrystalReportV3(strCrystalFullPath, ds, strPDFPath, out serverFullLabelName);
                }
            }
            else
            {
                string reportPath = Util.checkCRReportVersion(strCrystalFullPath, out serverFullLabelName);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;

namespace wmsReport
{
    class WMSDLL
    {
        public DataSet GetStockInfoDataTable(string carlineno, string incsn = "")
        {
            string sql = string.Empty;
            //copy from jx source by wenxing 20200813
            //虚拟储位加载太多资料,导致产线作业比较卡 Modified By KyLinQiu 20190808
            if (carlineno == "ICT-00-00-SYS-SYS")
            {
                sql = string.Format(@"select aa.location_no,
                               aa.custom_sn,
                               aa.trolley_line_no,
                               aa.pointno,
                               aa.group_code,
                               aa.delivery_no,
                               aa.line_item,
                               LISTAGG(aa.shipment_id, ',') WITHIN GROUP(ORDER BY aa.shipment_id) as shipment_id
                          from (select distinct d.location_no,
                                                a.custom_sn,
                                                e.trolley_line_no,
                                                a.pointno,
                                                a.group_code,
                                                b.delivery_no,
                                                b.line_item,
                                                c.shipment_id
                                  from ppsuser.t_trolley_sn_status a
                                  join ppsuser.vw_person_log b
                                    on a.custom_sn = b.customer_sn
                                  left join ppsuser.t_order_info c
                                    on b.delivery_no = c.delivery_no
                                   and b.line_item = c.line_item
                                   --and b.PART_NO = c.ictpn
                                  join ppsuser.t_sn_status d
                                    on a.custom_sn = d.customer_sn
                                  join ppsuser.t_trolley_line_info e
                                    on a.trolley_no = e.trolley_no
                                   and a.sides_no = e.sides_no
                                   and a.level_no = e.level_no
                                   and a.seq_no = e.seq_no
                                 where e.trolley_line_no = '{0}'
                                   and e.trolley_line_no <> 'ICT-00-00-000-0000'
                                   AND (A.CUSTOM_SN='{1}' OR A.CARTON_NO='{1}')
                                   and (c.shipment_id not in
                                       (select shipment_id from ppsuser.t_shipment_sawb) or
                                       c.shipment_id is null)) aa
                         group by aa.location_no,
                                  aa.custom_sn,
                                  aa.trolley_line_no,
                                  aa.pointno,
                                  aa.group_code,
                                  aa.delivery_no,
                                  aa.line_item
                         order by aa.pointno asc", carlineno, incsn);
            }
            else
            {
                sql = string.Format(@"select aa.location_no,
                               aa.custom_sn,
                               aa.trolley_line_no,
                               aa.pointno,
                               aa.group_code,
                               aa.delivery_no,
                               aa.line_item,
                               LISTAGG(aa.shipment_id, ',') WITHIN GROUP(ORDER BY aa.shipment_id) as shipment_id
                          from (select distinct d.location_no,
                                                a.custom_sn,
                                                e.trolley_line_no,
                                                a.pointno,
                                                a.group_code,
                                                b.delivery_no,
                                                b.line_item,
                                                c.shipment_id
                                  from ppsuser.t_trolley_sn_status a
                                  join ppsuser.vw_person_log b
                                    on a.custom_sn = b.customer_sn
                                  left join ppsuser.t_order_info c
                                    on b.delivery_no = c.delivery_no
                                   and b.line_item = c.line_item
                                   and b.PART_NO = c.ictpn
                                  join ppsuser.t_sn_status d
                                    on a.custom_sn = d.customer_sn
                                  join ppsuser.t_trolley_line_info e
                                    on a.trolley_no = e.trolley_no
                                   and a.sides_no = e.sides_no
                                   and a.level_no = e.level_no
                                   and a.seq_no = e.seq_no
                                 where e.trolley_line_no = '{0}'
                                   and e.trolley_line_no <> 'ICT-00-00-000-0000'
                                   and (c.shipment_id not in
                                       (select shipment_id from ppsuser.t_shipment_sawb) or
                                       c.shipment_id is null)) aa
                         group by aa.location_no,
                                  aa.custom_sn,
                                  aa.trolley_line_no,
                                  aa.pointno,
                                  aa.group_code,
                                  aa.delivery_no,
                                  aa.line_item
                         order by aa.pointno asc", carlineno);
            }
            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }

        public string WMSPpartCheckByProcedure(string incarlineno, string incsn, out string errmsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incarlineno", incarlineno };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incsn", incsn };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_WMS_PPARTCHECK", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            return errmsg;
            //if (errmsg.Equals("OK"))
            //{
            //    return "OK";
            //}
            //else
            //{
            //    return "NG";
            //}

        }

        public string WMSPpartTransByProcedure(string incarlinenofrom, string incarlinenoto, string incsn, out string errmsg)
        {
            object[][] procParams = new object[4][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incarlinenofrom", incarlinenofrom };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incarlinenoto", incarlinenoto };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incsn", incsn };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_WMS_PPARTTRANS", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            return errmsg;
        }


        public string GetCarlinenoByCSNByProcedure(string incsn, out string carlineno, out string errmsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incsn", incsn };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "carlineno", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_WMS_GETCARLINENOBYCSN", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            carlineno = ds.Tables[0].Rows[0]["carlineno"].ToString();
            return errmsg;
        }
        //P_PROPOSAL_TROLLEY(vCartonNo        in varchar2, vTROLLEY_LINE_NO out varchar2)

        public void GetCarlinenoByAdviseByProcedure(string incsn, out string carlineno)
        {
            object[][] procParams = new object[2][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "vCartonNo", incsn };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "vTROLLEY_LINE_NO", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.P_PROPOSAL_TROLLEY", procParams);
            carlineno = ds.Tables[0].Rows[0]["vTROLLEY_LINE_NO"].ToString();

        }
        public string WMSTrolleyMoveByProcedure(string incar, string inlocationto, out string errmsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incar", incar };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inlocationto", inlocationto };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_WMS_MOVETROLLEY", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            return errmsg;

        }
        public DataSet GetCarInfoDataTable(string strcar)
        {
            string sql = string.Empty;
            sql = string.Format(@"
                               select a.trolley_no,
                                a.trolley_line_no,
                                a.pointno,
                                a.pallet_no,
                                a.carton_no,
                                a.custom_sn,
                                a.delivery_no,
                                a.line_item,
                                b.location_no
                           from ppsuser.t_trolley_sn_status a
                           join ppsuser.t_sn_status b
                             on a.custom_sn = b.customer_sn
                          where a.trolley_no = '{0}'
                          order by a.trolley_line_no asc, a.pointno asc
                            ", strcar);
            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }

        public DataTable GetShipmentInfo()
        {
            #region Modified by wenxing 2020-08-13
            //            return ClientUtils.ExecuteSQL(@"
            //SELECT m.CAR_NO,
            //       m.SHIPMENT_ID,
            //       m.TYPE,
            //       m.carrier_name,
            //       m.REGION,
            //       m.UPLOADFLAG,
            //       m.PALLETCOUNT,
            //       m.STATUS,
            //       n.PALLETCOUNTALL,
            //       n.TIME_S
            //  FROM (SELECT nvl(g.CAR_NO, ' ') AS CAR_NO,
            //               g.SHIPMENT_ID,
            //               g.carrier_name,
            //               g.type,
            //               g.region,
            //               g.UPLOADFLAG,
            //               g.PALLETCOUNT,
            //               CASE
            //                 WHEN g.STATUS = 'UF' OR g.STATUS = 'LF' THEN
            //                  '已装车'
            //                 WHEN g.WeightCount = 0 THEN
            //                  '已称重'
            //                 WHEN g.PICKCount > 0 THEN
            //                  '作业中'
            //                 ELSE
            //                  '未Pick'
            //               END AS STATUS,
            //               g.TIME_S
            //          FROM (SELECT c.car_no,
            //                       a.SHIPMENT_ID,
            //                       a.carrier_name,
            //                       a.type,
            //                       a.region,
            //                       CASE
            //                         WHEN a.status = 'UF' THEN
            //                          'Y'
            //                         ELSE
            //                          'N'
            //                       END AS UPLOADFLAG,
            //                       count(DISTINCT b.PALLET_NO) AS PalletCount,
            //                       a.STATUS,
            //                       sum(CASE
            //                             WHEN b.PICK_STATUS = 'IP' OR b.PICK_STATUS = 'FP' THEN
            //                              1
            //                             ELSE
            //                              0
            //                           END) AS PICKCount,
            //                       sum(CASE
            //                             WHEN nvl(b.WEIGHT, '0') = '0' THEN
            //                              1
            //                             ELSE
            //                              0
            //                           END) AS WeightCount,
            //                       min(f.time_s) AS TIME_S
            //                  FROM ppsuser.T_SHIPMENT_INFO a
            //                 INNER JOIN ppsuser.T_SHIPMENT_PALLET b
            //                    ON a.SHIPMENT_ID = b.SHIPMENT_ID
            //                 INNER JOIN ppsuser.T_SHIPMENT_PALLET_PART d
            //                    ON b.PALLET_NO = d.PALLET_NO
            //                 INNER JOIN PPTEST.OMS_PARTMAPPING e
            //                    ON d.ICTPN = e.PART
            //                  LEFT JOIN pptest.oms_load_car c
            //                    ON a.SHIPMENT_ID = c.SHIPMENT_ID
            //                   AND b.PALLET_NO = c.PALLET_NO
            //                  LEFT JOIN (SELECT DISTINCT x.region, x.model, x.time_s
            //                              FROM PPTEST.OMS_TDM x
            //                             WHERE x.TDMTYPE = 'PGI') f
            //                    ON a.REGION = f.region
            //                   AND e.custmodel = f.model
            //                 WHERE TRUNC(a.SHIPPING_TIME) = TRUNC(SYSDATE)
            //                   AND a.STATUS not in ('WS', 'SF')
            //                 GROUP BY c.car_no,
            //                          a.SHIPMENT_ID,
            //                          a.carrier_name,
            //                          a.type,
            //                          a.region,
            //                          a.STATUS) g) m
            // INNER JOIN (SELECT nvl(g.CAR_NO, ' ') AS CAR_NO,
            //                    sum(g.PalletCount) AS PalletCountALL,
            //                    min(g.TIME_S) AS TIME_S
            //               FROM (SELECT c.car_no,
            //                            a.SHIPMENT_ID,
            //                            count(DISTINCT b.PALLET_NO) AS PalletCount,
            //                            a.STATUS,
            //                            sum(CASE
            //                                  WHEN b.PICK_STATUS = 'IP' OR
            //                                       b.PICK_STATUS = 'FP' THEN
            //                                   1
            //                                  ELSE
            //                                   0
            //                                END) AS PICKCount,
            //                            sum(CASE
            //                                  WHEN nvl(b.WEIGHT, '0') = '0' THEN
            //                                   1
            //                                  ELSE
            //                                   0
            //                                END) AS WeightCount,
            //                            min(f.time_s) AS TIME_S
            //                       FROM ppsuser.T_SHIPMENT_INFO a
            //                      INNER JOIN ppsuser.T_SHIPMENT_PALLET b
            //                         ON a.SHIPMENT_ID = b.SHIPMENT_ID
            //                      INNER JOIN ppsuser.T_SHIPMENT_PALLET_PART d
            //                         ON b.PALLET_NO = d.PALLET_NO
            //                      INNER JOIN PPTEST.OMS_PARTMAPPING e
            //                         ON d.ICTPN = e.PART
            //                       LEFT JOIN pptest.oms_load_car c
            //                         ON a.SHIPMENT_ID = c.SHIPMENT_ID
            //                        AND b.PALLET_NO = c.PALLET_NO
            //                       LEFT JOIN (SELECT DISTINCT x.region, x.model, x.time_s
            //                                   FROM PPTEST.OMS_TDM x
            //                                  WHERE x.TDMTYPE = 'PGI') f
            //                         ON a.REGION = f.region
            //                        AND e.custmodel = f.model
            //                      WHERE TRUNC(a.SHIPPING_TIME) = TRUNC(SYSDATE)
            //                        AND a.STATUS not in ('WS', 'SF')
            //                      GROUP BY c.car_no, a.SHIPMENT_ID, a.STATUS) g
            //              GROUP BY g.CAR_NO) n
            //    ON m.CAR_NO = n.CAR_NO
            // ORDER BY m.CAR_NO, m.SHIPMENT_ID, m.TYPE, m.REGION, m.UPLOADFLAG ASC
            //").Tables[0];
            #endregion
            return ClientUtils.ExecuteSQL(@"SELECT
	m.CAR_NO,
	m.SHIPMENT_ID,
	m.TYPE,
	m.carrier_name,
	m.REGION,
	m.UPLOADFLAG,
	m.PALLETCOUNT,
	m.STATUS,
	n.PALLETCOUNTALL,
	n.TIME_S 
FROM
	(
	SELECT
		nvl( g.CAR_NO, ' ' ) AS CAR_NO,
		g.SHIPMENT_ID,
		g.carrier_name,
		g.TYPE,
		g.region,
		g.UPLOADFLAG,
		g.PALLETCOUNT,
	CASE
			
			WHEN g.STATUS = 'UF' 
			OR g.STATUS = 'LF' THEN
				'已装车' 
				WHEN g.WeightCount = 0 THEN
				'已称重' 
				WHEN g.PICKCount > 0 THEN
				'作业中' ELSE '未Pick' 
			END AS STATUS,
			g.TIME_S 
		FROM
			(
			SELECT
				c.car_no,
				a.SHIPMENT_ID,
				a.carrier_name,
				a.TYPE,
				a.region,
			CASE
					
					WHEN a.status = 'UF' THEN
					'Y' ELSE 'N' 
				END AS UPLOADFLAG,
				count( DISTINCT b.PALLET_NO ) AS PalletCount,
				a.STATUS,
				sum( CASE WHEN b.PICK_STATUS = 'IP' OR b.PICK_STATUS = 'FP' THEN 1 ELSE 0 END ) AS PICKCount,
				sum( CASE WHEN nvl( b.WEIGHT, '0' ) = '0' THEN 1 ELSE 0 END ) AS WeightCount,
				min( f.time_s ) AS TIME_S 
			FROM
				ppsuser.T_SHIPMENT_INFO a
				INNER JOIN ppsuser.T_SHIPMENT_PALLET b ON a.SHIPMENT_ID = b.SHIPMENT_ID
				INNER JOIN ppsuser.T_SHIPMENT_PALLET_PART d ON b.PALLET_NO = d.PALLET_NO
				INNER JOIN PPTEST.OMS_PARTMAPPING e ON d.ICTPN = e.PART
				LEFT JOIN pptest.oms_load_car c ON a.SHIPMENT_ID = c.SHIPMENT_ID 
				AND b.PALLET_NO = c.PALLET_NO
				LEFT JOIN ( SELECT DISTINCT x.region, x.model, x.time_s FROM PPTEST.OMS_TDM x WHERE x.TDMTYPE = 'PGI' ) f ON a.REGION = f.region 
				AND e.custmodel = f.model 
			WHERE
				TRUNC( a.SHIPPING_TIME ) = TRUNC( SYSDATE ) 
				AND a.STATUS NOT IN ( 'WS', 'SF' ) 
			GROUP BY
				c.car_no,
				a.SHIPMENT_ID,
				a.carrier_name,
				a.TYPE,
				a.region,
				a.STATUS 
				
				UNION
				
			SELECT
				cc.car_no,
				aa.SHIPMENT_ID || '-AC' SHIPMENT_ID,
				aa.carrier_name,
				aa.TYPE,
				aa.region,
			CASE
					
					
					WHEN aa.status = 'UF' THEN
					'Y' ELSE 'N' 
				END AS UPLOADFLAG,
				count( DISTINCT bb.PALLET_NO ) AS PalletCount,
				aa.STATUS,
				sum( CASE WHEN bb.PICK_STATUS = 'IP' OR bb.PICK_STATUS = 'FP' THEN 1 ELSE 0 END ) AS PICKCount,
				sum( CASE WHEN nvl( bb.WEIGHT, '0' ) = '0' THEN 1 ELSE 0 END ) AS WeightCount,
				( SELECT nvl( min( aaaa.PARA_VALUE ), ' ' ) FROM ppsuser.T_BASICPARAMETER_INFO aaaa WHERE aaaa.PARA_TYPE = 'SHIPMENTTDMAC' ) AS TIME_S 
			FROM
				NONEDIPPS.T_SHIPMENT_INFO aa
				INNER JOIN NONEDIPPS.T_SHIPMENT_PALLET bb ON aa.SHIPMENT_ID = bb.SHIPMENT_ID
				INNER JOIN NONEDIPPS.T_SHIPMENT_PALLET_PART dd ON bb.PALLET_NO = dd.PALLET_NO
				INNER JOIN NONEDIOMS.OMS_PARTMAPPING ee ON dd.ICTPN = ee.PART
				LEFT JOIN NONEDIOMS.oms_load_car cc ON aa.SHIPMENT_ID = cc.SHIPMENT_ID 
				AND bb.PALLET_NO = cc.PALLET_NO 
			WHERE
				TRUNC( aa.SHIPPING_TIME ) = TRUNC( SYSDATE ) 
				AND aa.STATUS NOT IN ( 'WS', 'SF' ) 
			GROUP BY
				cc.car_no,
				aa.SHIPMENT_ID,
				aa.carrier_name,
				aa.TYPE,
				aa.region,
				aa.STATUS 
			) g 
		) m
		INNER JOIN (
		SELECT
			nvl( g2.CAR_NO, ' ' ) AS CAR_NO,
			sum( g2.PalletCount ) AS PalletCountALL,
			min( g2.TIME_S ) AS TIME_S 
		FROM
			(
			SELECT
				c2.car_no,
				a2.SHIPMENT_ID,
				count( DISTINCT b2.PALLET_NO ) AS PalletCount,
				a2.STATUS,
				sum( CASE WHEN b2.PICK_STATUS = 'IP' OR b2.PICK_STATUS = 'FP' THEN 1 ELSE 0 END ) AS PICKCount,
				sum( CASE WHEN nvl( b2.WEIGHT, '0' ) = '0' THEN 1 ELSE 0 END ) AS WeightCount,
				min( f2.time_s ) AS TIME_S 
			FROM
				ppsuser.T_SHIPMENT_INFO a2
				INNER JOIN ppsuser.T_SHIPMENT_PALLET b2 ON a2.SHIPMENT_ID = b2.SHIPMENT_ID
				INNER JOIN ppsuser.T_SHIPMENT_PALLET_PART d2 ON b2.PALLET_NO = d2.PALLET_NO
				INNER JOIN PPTEST.OMS_PARTMAPPING e2 ON d2.ICTPN = e2.PART
				LEFT JOIN pptest.oms_load_car c2 ON a2.SHIPMENT_ID = c2.SHIPMENT_ID 
				AND b2.PALLET_NO = c2.PALLET_NO
				LEFT JOIN ( SELECT DISTINCT x2.region, x2.model, x2.time_s FROM PPTEST.OMS_TDM x2 WHERE x2.TDMTYPE = 'PGI' ) f2 ON a2.REGION = f2.region 
				AND e2.custmodel = f2.model 
			WHERE
				TRUNC( a2.SHIPPING_TIME ) = TRUNC( SYSDATE ) 
				AND a2.STATUS NOT IN ( 'WS', 'SF' ) 
			GROUP BY
				c2.car_no,
				a2.SHIPMENT_ID,
				a2.STATUS UNION
			SELECT
				cc2.car_no,
				aa2.SHIPMENT_ID || '-AC' SHIPMENT_ID,
				count( DISTINCT bb2.PALLET_NO ) AS PalletCount,
				aa2.STATUS,
				sum( CASE WHEN bb2.PICK_STATUS = 'IP' OR bb2.PICK_STATUS = 'FP' THEN 1 ELSE 0 END ) AS PICKCount,
				sum( CASE WHEN nvl( bb2.WEIGHT, '0' ) = '0' THEN 1 ELSE 0 END ) AS WeightCount,
				( SELECT nvl( min( aaaa2.PARA_VALUE ), ' ' ) FROM ppsuser.T_BASICPARAMETER_INFO aaaa2 WHERE aaaa2.PARA_TYPE = 'SHIPMENTTDMAC' ) AS TIME_S 
			FROM
				NONEDIPPS.T_SHIPMENT_INFO aa2
				INNER JOIN NONEDIPPS.T_SHIPMENT_PALLET bb2 ON aa2.SHIPMENT_ID = bb2.SHIPMENT_ID
				INNER JOIN NONEDIPPS.T_SHIPMENT_PALLET_PART dd2 ON bb2.PALLET_NO = dd2.PALLET_NO
				INNER JOIN NONEDIOMS.OMS_PARTMAPPING ee2 ON dd2.ICTPN = ee2.PART
				LEFT JOIN NONEDIOMS.oms_load_car cc2 ON aa2.SHIPMENT_ID = cc2.SHIPMENT_ID 
				AND bb2.PALLET_NO = cc2.PALLET_NO 
			WHERE
				TRUNC( aa2.SHIPPING_TIME ) = TRUNC( SYSDATE ) 
				AND aa2.STATUS NOT IN ( 'WS', 'SF' ) 
			GROUP BY
				cc2.car_no,
				aa2.SHIPMENT_ID,
				aa2.STATUS 
			) g2 
		GROUP BY
			g2.CAR_NO 
		) n ON m.CAR_NO = n.CAR_NO 
	ORDER BY
		m.CAR_NO,
		m.SHIPMENT_ID,
		m.TYPE,
	m.REGION,
	m.UPLOADFLAG ASC").Tables[0];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace InPaShippingLabel
{
    public class DS_Cartons
    {
        public bool DS_CartonsFunc(string shipment_id)
        {
            try
            {
                bool result = false;
                //获取总箱数
                string total_Cartons = GetTotal_Cartons(shipment_id);  //pick list总共需要多少carton
                //获取插入的数据
                DataTable insertValue = GetInsertValue(shipment_id);
                if (insertValue == null) {
                    throw new Exception("无数据");
                }
                int start_carton = 0;
                int end_carton = 0;
                for (int i = 0; i < insertValue.Rows.Count; i++)
                {
                    string dn = insertValue.Rows[i]["dn"].ToString();
                    string pack_code = insertValue.Rows[i]["pack_code"].ToString();
                    string pallet_no = insertValue.Rows[i]["pallet_no"].ToString();
                    string sn_qty = insertValue.Rows[i]["sn_qty"].ToString();
                    string cartons = insertValue.Rows[i]["cartons"].ToString();
                    string total_cartons = total_Cartons;
                    if (i == 0)
                    {
                        start_carton = 1;
                        end_carton = Convert.ToInt32(insertValue.Rows[i]["cartons"].ToString());
                    }
                    else
                    {
                        start_carton = end_carton + 1;
                        end_carton = start_carton + Convert.ToInt32(insertValue.Rows[i]["cartons"].ToString()) - 1;
                    }
                    InsertData(shipment_id, dn, pack_code, pallet_no, sn_qty, cartons, total_cartons, start_carton.ToString(), end_carton.ToString());
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //获取总箱数
        private string GetTotal_Cartons(string so_no)
        {
            string sql = string.Format(@"select sum(a.qty/(select x.sn_qty from ppsuser.g_Ds_Packinfo_t x where x.pack_code = a.pack_code )) as totalcartons
                                         from ppsuser.g_ds_pick_t a
                                         where a.shipment_id = '{0}'
                                         group by a.shipment_id
                                         order by a.shipment_id", so_no);
            DataTable total_Cortons = ClientUtils.ExecuteSQL(sql).Tables[0];
            if (total_Cortons.Rows.Count > 0)
            {
                return total_Cortons.Rows[0]["totalcartons"].ToString();
            }
            else
            {
                return "";
            }
            
        }

        //获取插入数据
        private DataTable GetInsertValue(string so_no)
        {
            string sql = string.Format(@"select m.shipment_id,m.dn,m.pack_code,m.pallet_no, y.sn_qty,
                                         trunc(sum(m.qty)/y.sn_qty)+case mod(sum(m.qty),y.sn_qty) when 0 then 0 else 1 end as cartons
                                         from ppsuser.g_Ds_Packinfo_t y,ppsuser.g_ds_pick_t m
                                         where y.pack_code = m.pack_code
                                         and m.shipment_id = '{0}'
                                         and not exists(select a.shipment_id,a.dn,a.pallet_no  from ppsuser.G_DS_PICK_CARTONS a where a.shipment_id = m.shipment_id and a.dn = m.dn)
                                         group by m.shipment_id,m.dn,m.pack_code,m.pallet_no,y.sn_qty
                                         order by m.pallet_no,cartons desc,m.shipment_id,m.dn", so_no);
            DataTable insertValue = ClientUtils.ExecuteSQL(sql).Tables[0];

            return insertValue;
        }

        //插入数据
        private void InsertData(string shipment_id, string dn, string pack_code, string pallet_no, string sn_qty, string cartons, string total_cartons, string start_carton, string end_carton)
        {
            string sql = string.Format(@"insert into ppsuser.G_DS_PICK_CARTONS(shipment_id,dn,pack_code,pallet_no,sn_qty,cartons,totalcartons,star_cartons,end_cartons)
                                          values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", shipment_id, dn, pack_code, pallet_no, sn_qty, cartons, total_cartons, start_carton, end_carton);
            ClientUtils.ExecuteSQL(sql);
        }
    }
}

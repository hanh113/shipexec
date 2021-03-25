using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace InPaShippingLabel
{
    public class FD_Cartons
    {
        public bool FD_CartonsFunc(string so_no)
        {
            try
            {
                bool result = false;
                //获取总箱数
                string total_Cartons = GetTotal_Cartons(so_no);
                //获取插入的数据
                DataTable insertValue = GetInsertValue(so_no);
                if (insertValue == null) {
                    throw new Exception("无数据");
                }
                int start_carton = 0;
                int end_carton = 0;
                for (int i = 0; i < insertValue.Rows.Count; i++)
                {
                    string po_no = insertValue.Rows[i]["po_no"].ToString();
                    string mix_pallets = insertValue.Rows[i]["mix_pallets"].ToString();
                    string pack_code = insertValue.Rows[i]["pack_code"].ToString();
                    string sn_qty = insertValue.Rows[i]["sn_qty"].ToString();
                    string qty = insertValue.Rows[i]["qty"].ToString();
                    string cartons = insertValue.Rows[i]["Cartons"].ToString();
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
                    InsertData(so_no,po_no, mix_pallets, pack_code, sn_qty, qty, cartons, total_cartons, start_carton.ToString(), end_carton.ToString());
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
            string sql = string.Format( @"select sum(a.qty/(select d.sn_qty from sajet.sys_part b,ppsuser.g_ds_partinfo_t c,ppsuser.g_Ds_Packinfo_t d
                                          where  a.part_id = b.part_id
                                          and    b.part_no = c.ictpn
                                          and    c.pack_code = d.pack_code)) as SO_TOTALCARTONS
                                          from ppsuser.g_shipping_detail_t a
                                          where a.so_no = '{0}'
                                          group by a.so_no",so_no);
            DataTable total_Cortons = ClientUtils.ExecuteSQL(sql).Tables[0];

            return total_Cortons.Rows[0]["SO_TOTALCARTONS"].ToString();
        }

        //获取插入数据
        private DataTable GetInsertValue(string so_no)
        {
            string sql = string.Format(@"select t.so_no,t.po_no,t.mix_pallets, n.pack_code,n.sn_qty,sum(t.qty) qty,
                                         trunc(sum(t.qty)/n.sn_qty)+case mod(sum(t.qty),n.sn_qty) when 0 then 0 else 1 end as Cartons
                                         from ppsuser.g_shipping_detail_t t,sajet.sys_part a,ppsuser.g_ds_partinfo_t d,ppsuser.g_Ds_Packinfo_t n
                                         where  t.part_id = a.part_id
                                         and    a.part_no = d.ictpn
                                         and    d.pack_code = n.pack_code
                                         and    not exists(select o.po_no,o.mix_pallets from ppsuser.g_shipping_detail_cartons o where o.po_no = t.po_no and t.so_no = o.so_no)
                                         and    t.so_no = '{0}'
                                         group by t.so_no,t.po_no,t.mix_pallets, n.pack_code,n.sn_qty
                                         order by t.so_no,t.mix_pallets,t.po_no", so_no);
            DataTable insertValue = ClientUtils.ExecuteSQL(sql).Tables[0];

            return insertValue;
        }

        //插入数据
        private void InsertData(string so_no, string po_no, string mix_pallets, string pack_code, string sn_qty, string qty, string cartons, string total_cartons, string start_carton, string end_carton)
        {
            string sql = string.Format(@"insert into ppsuser.g_shipping_detail_cartons(so_no,po_no,mix_pallets,pack_code,sn_qty,qty,cartons,total_cartons,start_carton,end_carton)
                                          values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", so_no, po_no, mix_pallets, pack_code, sn_qty, qty, cartons, total_cartons, start_carton, end_carton);
            ClientUtils.ExecuteSQL(sql);
        }
    }
}

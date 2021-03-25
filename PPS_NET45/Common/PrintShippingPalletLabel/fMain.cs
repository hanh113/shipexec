using System;
using System.Data;
using System.Windows.Forms;
using HShippingLabel;
using System.IO;
using SajetClass;
using System.Linq;
using System.Data.OracleClient;
using ShippingScan;
using System.ComponentModel;
using System.Text;
using System.Globalization;
using System.Drawing;
using System.Threading;
using System.Text.RegularExpressions;

namespace PrintShippingPalletLabel
{
  public partial class fMain : Form
  {
    public PrintLabel sPrintLabel;
    public string g_sExeName;
    public string g_sProgram;
    public string g_sFunction;
    public string g_sFunctionType;
    public string g_sUserID;
    public string g_sUserNo;
    public string g_sUserName;
    public string g_sTerminalID = string.Empty;
    public string g_sProcessID = string.Empty;
    public string g_sStageID = string.Empty;
    public string g_sPDLineID = string.Empty;
    public string sPdlineName, sProcessName, sTerminalName, sStageName, sShiftName, sMessage;
    public string g_sIniFile = Application.StartupPath + "\\sajet.ini";
    /// <summary>
    /// 空栈板总量
    /// </summary>

    public string palletWeight = "";
    private BindingList<ShippingItemInfo> bindDNItemList = new BindingList<ShippingItemInfo>();
    private double fullCartonWeight;
    public fMain()
    {
      InitializeComponent();
    }



    public BindingList<ShippingItemInfo> GetShippingItemList(string sscc)
    {
      BindingList<ShippingItemInfo> bshipItemList = new BindingList<ShippingItemInfo>();
      string strSql = @"SELECT A.ROWID ID, C.DN_NO,
                               A.SHIPPING_ID,
                               A.SHIPPING_ITEM,
                               A.PART_ID,
                               B.PART_NO,
                               B.SPEC1,
                               A.VERSION,
                               NVL(A.QTY,0) QTY,
                               A.USEDQTY,
                               NVL(A.CQTY,0) CQTY,
                               A.USEDCQTY,
                               A.SHIP_TO,
                               A.VEHICLE_NO,
                               NVL(A.END_CARTONS,0) END_CARTONS,
                               A.SSCC,
                               A.CARRIER,
                               A.COC,
                               A.CTRY,
                               A.RETURN_TO,
                               A.TEL,
                               A.ORIGIN,
                               A.CARTONS,
                               A.SHIP_DATE,
                               A.HAWB,
                               A.PO_NO,
                               A.DN_ITEM,
                               A.MPN,
                               A.SHIPPING_NO,
                               A.REGION,
                               A.INVOICENO,
                                A.MIX_FLAG,
                                A.MIX_PALLETS,
                                A.UPDATE_MIX_FLAG,
                                A.LINE_ITEM,
                               C.PROFIT_CENTER,
                               C.OPERATIONS_CENTER
                          FROM PPSUSER.G_SHIPPING_DETAIL_T A, SAJET.SYS_PART B, PPSUSER.G_DN_BASE C
                         WHERE A.SSCC = :SSCC
                           AND A.SHIPPING_STATUS <> 0
                           AND A.PART_ID = B.PART_ID
                           AND A.SHIPPING_ID = C.DN_ID
                         ORDER BY A.PO_NO, A.MIX_PALLETS,A.SHIPPING_ITEM";
      object[][] sqlParams = new object[1][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "SSCC", sscc } };
      DataTable dt = ClientUtils.ExecuteSQL(strSql, sqlParams).Tables[0];


      if (dt.Rows.Count > 0)
      {

        foreach (DataRow dr in dt.Rows)
        {
          ShippingItemInfo shipItem = new ShippingItemInfo();
          shipItem.Rowid = dr["ID"].ToString();
          shipItem.Dn_No = dr["DN_NO"].ToString();
          shipItem.Shipping_id = dr["SHIPPING_ID"].ToString();
          shipItem.Shipping_item = dr["SHIPPING_ITEM"].ToString();
          shipItem.Part_no = dr["PART_NO"].ToString();
          shipItem.Part_id = dr["PART_ID"].ToString();
          shipItem.LineItem = dr["LINE_ITEM"].ToString();
          shipItem.Part_spec = dr["SPEC1"].ToString();
          shipItem.Version = dr["VERSION"].ToString();
          shipItem.Qty = int.Parse(dr["QTY"].ToString());
          shipItem.Usedqty = int.Parse(dr["USEDQTY"].ToString());
          shipItem.Cqty = int.Parse(dr["CQTY"].ToString());
          shipItem.Usedcqty = int.Parse(dr["USEDCQTY"].ToString());
          shipItem.Ship_to = dr["SHIP_TO"].ToString();
          shipItem.Vehicle_no = dr["VEHICLE_NO"].ToString();
          shipItem.EndCqty = int.Parse(dr["END_CARTONS"].ToString());
          shipItem.Sscc = dr["SSCC"].ToString();
          shipItem.Carrier = dr["CARRIER"].ToString();
          shipItem.Coc = dr["COC"].ToString();
          shipItem.Ctry = dr["CTRY"].ToString();
          shipItem.Return_to = dr["RETURN_TO"].ToString();
          shipItem.Tel = dr["TEL"].ToString();
          shipItem.Origin = dr["ORIGIN"].ToString();
          shipItem.Cartons = dr["CARTONS"].ToString();
          shipItem.Ship_date = dr["SHIP_DATE"].ToString();
          shipItem.Hawb = dr["HAWB"].ToString();
          shipItem.Po_no = dr["PO_NO"].ToString();
          shipItem.Mpn = dr["MPN"].ToString();
          shipItem.Erp_shipping_no = dr["SHIPPING_NO"].ToString();
          shipItem.Region = dr["REGION"].ToString();
          shipItem.Invoiceno = dr["INVOICENO"].ToString();
          shipItem.MixFlag = dr["MIX_FLAG"].ToString();
          shipItem.MixPallets = dr["MIX_PALLETS"].ToString();
          shipItem.UpdateMixFlag = dr["UPDATE_MIX_FLAG"].ToString();
          bshipItemList.Add(shipItem);
        }

      }
      //            string sql = @"SELECT a.*  FROM ppsuser.ed_PPSDN_DETAIL a
      //                           WHERE a.DN_NO =:DN_NO";
      //            object[][] sqlParams1 = new object[1][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "DN_NO", txtDNNo.Text.Trim() } };
      //            DataTable dt1 = ClientUtils.ExecuteSQL(sql, sqlParams1).Tables[0];
      //if (dt1.Rows.Count > 0)
      //{
      //    dataGridView1.DataSource = dt1;
      //}
      return bshipItemList;
    }

    private bool DNMappingMultiLocation(DataTable dttemp)
    {
      int palletcount = dttemp.Rows.Count;
      //查找容量最近的储位号
      if (palletcount == 0) return true;
      DataTable dt = new DataTable();
      string sqlstr = @"SELECT LOCATION_NO,CAPACITY,MIX FROM(SELECT LOCATION_NO,CAPACITY, :PALLETCOUNT-CAPACITY MIX FROM PPSUSER.G_SHIPPING_LOCATION WHERE USEDFLAG='N' ORDER BY (:PALLETCOUNT-CAPACITY)) WHERE ROWNUM=1";
      object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "PALLETCOUNT", palletcount } };
      DataTable dtlocationinfo = ClientUtils.ExecuteSQL(sqlstr, sqlparams).Tables[0];
      int locationcapacity = int.Parse(dtlocationinfo.Rows[0]["CAPACITY"].ToString());
      string locationno = dtlocationinfo.Rows[0]["LOCATION_NO"].ToString();
      foreach (DataColumn itemc in dttemp.Columns)
      {
        DataColumn dc = new DataColumn(itemc.ColumnName, itemc.DataType);
        dt.Columns.Add(dc);
      }

      for (int i = 0; i < palletcount; i++)
      {
        if (i < locationcapacity)
        {
          sqlstr = @"INSERT INTO PPSUSER.G_SHIPPING_PALLET_LOCATION(DN_NO,PALLET_ID,LOCATION_NO) VALUES(:DN_NO,:MIX_PALLETS,:LOCATIONNO)";
          object[][] sqlparmasi = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "LOCATIONNO", locationno },
              new object[] { ParameterDirection.Input, OracleType.VarChar, "MIX_PALLETS", dttemp.Rows[i]["MIX_PALLETS"].ToString() },
              new object[]{ ParameterDirection.Input, OracleType.VarChar, "DN_NO", dttemp.Rows[i]["SO_NO"].ToString()}};

          ClientUtils.ExecuteSQL(sqlstr, sqlparmasi);

        }
        else
        {
          DataRow dr = dt.NewRow();
          for (int j = 0; j < dt.Columns.Count; j++)
          {
            dr[j] = dttemp.Rows[i][j];

          }
          dt.Rows.Add(dr);

        }

        if (i == locationcapacity - 1)
        {
          sqlstr = "UPDATE PPSUSER.G_SHIPPING_LOCATION SET USEDFLAG='Y' WHERE LOCATION_NO=:LOCATIONNO AND ROWNUM=1";
          object[][] sqlparamsu = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "LOCATIONNO", locationno } };
          ClientUtils.ExecuteSQL(sqlstr, sqlparamsu);
        }

        if (i == palletcount - 1)
        {
          DNMappingMultiLocation(dt);
        }
      }

      return false;
    }


    private string GetPalletWeight(string dnno,string sscc)
    {
      try
      {
        string sqlstr = "SELECT ALLPALLET_WEIGHT FROM PPSUSER.G_PALLETEDI_INFO WHERE SO_NO=:SO_NO AND SSCC=:SSCC AND ROWNUM=1";
      
        object[][]sqlparams=new object[][]{new object[]{ParameterDirection.Input,OracleType.VarChar,"SO_NO",dnno},
        new object[]{ParameterDirection.Input,OracleType.VarChar,"SSCC",sscc}};
        DataTable dt=ClientUtils.ExecuteSQL(sqlstr,sqlparams).Tables[0];
        if(dt.Rows.Count>0)
        {
          return dt.Rows[0][0].ToString();
        }else
        {
          return string.Empty;
        }
      }
      catch (Exception ex) 
      {
        ShowMsg(ex.Message, 0);
        return string.Empty;
      }
    }

    private bool GetPalletLocation(string mixpalletid, out string locationno)
    {
      string sqlstr = "SELECT LOCATION_NO FROM PPSUSER.G_SHIPPING_PALLET_LOCATION WHERE PALLET_ID=:MIX_PALLETS AND ROWNUM=1";
      object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "MIX_PALLETS", mixpalletid } };
      DataTable dt = ClientUtils.ExecuteSQL(sqlstr, sqlparams).Tables[0];
      if (dt.Rows.Count > 0)
      {
        locationno = dt.Rows[0]["LOCATION_NO"].ToString();
        return true;
      }
      else
      {
        //查询栈板号所对应的出货中所有栈板
        sqlstr = @"SELECT DISTINCT SO_NO,MIX_PALLETS FROM PPSUSER.G_SHIPPING_DETAIL_T WHERE SHIPPING_ID = (SELECT SHIPPING_ID
                        FROM PPSUSER.G_SHIPPING_DETAIL_T
                       WHERE MIX_PALLETS = :MIX_PALLETS AND ROWNUM = 1)";
        DataTable dtpallets = ClientUtils.ExecuteSQL(sqlstr, sqlparams).Tables[0];

        int dnpalletcount = dtpallets.Rows.Count;

        //查找直接满足栈板总数的储位
        string sqlstrs = @"SELECT LOCATION_NO,CAPACITY FROM (SELECT LOCATION_NO,CAPACITY FROM PPSUSER.G_SHIPPING_LOCATION WHERE CAPACITY>=:PALLETCOUNT AND USEDFLAG='N' ORDER BY CAPACITY) WHERE ROWNUM=1";
        object[][] sqlparamss = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "PALLETCOUNT", dnpalletcount } };
        DataTable dtlocation = ClientUtils.ExecuteSQL(sqlstrs, sqlparamss).Tables[0];
        if (dtlocation.Rows.Count > 0)
        {
          locationno = dtlocation.Rows[0]["LOCATION_NO"].ToString();
          sqlstrs = @"INSERT INTO PPSUSER.G_SHIPPING_PALLET_LOCATION SELECT DISTINCT SO_NO,MIX_PALLETS,:LOCATIONNO FROM PPSUSER.G_SHIPPING_DETAIL_T
                        WHERE SHIPPING_ID = (SELECT SHIPPING_ID
                        FROM PPSUSER.G_SHIPPING_DETAIL_T
                       WHERE MIX_PALLETS = :MIX_PALLETS AND ROWNUM = 1)";
          object[][] sqlparmasi = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "LOCATIONNO", locationno },
              new object[] { ParameterDirection.Input, OracleType.VarChar, "MIX_PALLETS", mixpalletid }};

          ClientUtils.ExecuteSQL(sqlstrs, sqlparmasi);

          sqlstrs = "UPDATE  PPSUSER.G_SHIPPING_LOCATION SET USEDFLAG='Y' WHERE LOCATION_NO=:LOCATIONNO AND ROWNUM=1";
          object[][] sqlparamsu = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "LOCATIONNO", locationno } };

          ClientUtils.ExecuteSQL(sqlstrs, sqlparamsu);
          return true;
        }
        else
        {
          //查询满足栈板总数的多个储位
          sqlstrs = @"SELECT COUNT(*) LOCATIONCOUNT FROM PPSUSER.G_SHIPPING_LOCATION WHERE USEDFLAG='N' HAVING SUM(CAPACITY)>=:PALLETCOUNT ";
          dtlocation = ClientUtils.ExecuteSQL(sqlstrs, sqlparamss).Tables[0];
          if (dtlocation.Rows.Count > 0)
          {
            if (!DNMappingMultiLocation(dtpallets))
            {
              sqlstr = "SELECT LOCATION_NO FROM PPSUSER.G_SHIPPING_PALLET_LOCATION WHERE PALLET_ID=:MIX_PALLETS AND ROWNUM=1";
              dt = ClientUtils.ExecuteSQL(sqlstr, sqlparams).Tables[0];
              if (dt.Rows.Count > 0)
              {
                locationno = dt.Rows[0]["LOCATION_NO"].ToString();
                return true;
              }
              else
              {
                locationno = "";
                return false;
              }
            }
          }
          else //没有可用储位时
          {

          }
        }

      }
      locationno = "";
      return false;
    }

    private bool CheckPalletHold(string palletid)
    {
      try
      {
        string sqlstr = "SELECT DN_NO,WORK_FLAG FROM PPSUSER.G_DN_BASE WHERE DN_ID=(SELECT SHIPPING_ID FROM PPSUSER.G_SHIPPING_DETAIL_T WHERE PALLET_ID=:PALLET_ID AND ROWNUM=1)";
        object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "PALLET_ID", palletid } };
        DataTable dt = ClientUtils.ExecuteSQL(sqlstr, sqlparams).Tables[0];
        if (dt.Rows.Count > 0)
        {
          string workflag = dt.Rows[0]["WORK_FLAG"].ToString();
          string dn = dt.Rows[0]["DN_NO"].ToString();
          if (workflag == "H"||workflag=="A")
          {
            ShowMsg("出货单Hold或已取消", 0);
            return false;
          }
          sqlstr = "SELECT OGA01 FROM SZFD.OGA_FILE WHERE OGA01=:DN";
          sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "DN", dn } };
          dt = ClientUtils.ExecuteSQL(sqlstr, sqlparams).Tables[0];
          if (dt.Rows.Count == 0)
          {
            sqlstr = "UPDATE PPSUSER.G_DN_BASE SET WORK_FLAG='A' WHERE DN_NO=:DN";
            ShowMsg("出货单TT已取消", 0);
            return false;
          }

          sqlstr = @"SELECT DISTINCT CARTON_NO
  FROM PPSUSER.G_SN_STATUS A
 WHERE  A.HOLD_FLAG='Y' AND A.CARTON_NO IN (SELECT INPUT_DATA
                         FROM PPSUSER.G_SHIPPING_PICK_DITAIL B
                        WHERE B.MIX_PALLETS =
                                 (SELECT MIX_PALLETS
                                    FROM PPSUSER.G_SHIPPING_DETAIL_T
                                   WHERE PALLET_ID = :PALLET_ID AND ROWNUM=1))";
          sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "PALLET_ID", palletid } };
          dt = ClientUtils.ExecuteSQL(sqlstr, sqlparams).Tables[0];
          if (dt.Rows.Count > 0) 
          {
            LibHelper.MediasHelper.PlaySoundAsync(Application.StartupPath + "\\Shipping\\Hold.wav");
            ShowMsg("箱号有Hold产品" + dt.Rows[0]["CARTON_NO"].ToString(), 0);
            return false;
          }


          return true;
        }
        ShowMsg("pallet ID 错误", 0);
        return false;
      }
      catch (Exception ex)
      {
        ShowMsg(ex.Message, 0);
        return false;

      }
    }

    string currSSCC = string.Empty;
    private void txt_PalletId_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == (char)Keys.Enter)
      {
        bindDNItemList.Clear();
        string palletno = txt_PalletId.Text.Trim();
        string sscc = string.Empty;

        if (!CheckPalletHold(palletno))
        {
          txt_PalletId.SelectAll();
          txt_PalletId.Focus();
          return;
        }

        if (!CheckSSCC(palletno, out sscc))
        {
          txt_PalletId.SelectAll();
          txt_PalletId.Focus();
          return;
        }
        currSSCC = sscc;
        try
        {
          bindDNItemList = GetShippingItemList(currSSCC);
          palletActualWeitht = getweight(tblayer.Text.Trim(), txt_EmptyCartons.Text.Trim(), (Convert.ToInt32(txt_TotalCartons.Text.Trim()) - Convert.ToInt32(txt_EmptyCartons.Text.Trim())).ToString());

          if (string.IsNullOrEmpty(palletActualWeitht))
          {
            txt_PalletId.SelectAll();
            txt_PalletId.Focus();
            return;
          }
          txtStandardWeitht.Text = palletActualWeitht;
          if (bindDNItemList != null && bindDNItemList.Count > 0)
          {
            txtWeight.Text = GetPalletWeight(bindDNItemList[0].Dn_No, bindDNItemList[0].Sscc);
          }

          txtDeviation.Text = "+/- 3%";
          txtUpperlimit.Text = (Convert.ToDouble(palletActualWeitht) + Convert.ToDouble(palletActualWeitht) * 0.03).ToString("F");
          txtLowerlimit.Text = (Convert.ToDouble(palletActualWeitht) - Convert.ToDouble(palletActualWeitht) * 0.03).ToString("F");

          //生成装车储位号
          string location = string.Empty;
          txtLocation.Text = location;
          if (GetPalletLocation(bindDNItemList[0].MixPallets, out location))
          {
            txtLocation.Text = location;
          }
          else
          {
            txtLocation.Text = "没有可用储位！";
          }

          ShowMsg("Pallet ID OK", 3);
          btnPrintH.Enabled = true;
          btnPrintPDF.Enabled = true;
          //txtWeight.Clear();

          if (cmbWeightType.SelectedIndex == 1)
          {
            txtWeight.Enabled = true;
            txtWeight.SelectAll();
            txtWeight.Focus();
            txtWeightRecive.Enabled = false;
          }
          else 
          {
            txtWeight.Enabled = false;
            txtWeightRecive.Enabled = true;
            txtWeightRecive.SelectAll();
            txtWeightRecive.Focus();
          }
        }
        catch (Exception ex) 
        {
          ShowMsg(ex.Message, 0);
        }
      }

    }

    public DialogResult ShowMsg(string sText, int iType)
    {
      TextMsg.Text = sText;
      switch (iType)
      {
        case 0: //Error                
          TextMsg.ForeColor = Color.Red;
          TextMsg.BackColor = Color.Silver;
          return DialogResult.None;
        case 1: //Warning                        
          TextMsg.ForeColor = Color.Blue;
          TextMsg.BackColor = Color.FromArgb(255, 255, 128);
          return DialogResult.None;
        case 2: //Confirm
          return MessageBox.Show(sText, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        default:
          TextMsg.ForeColor = Color.Green;
          TextMsg.BackColor = Color.White;
          return DialogResult.None;
      }
    }


    DateTime shipDate = new DateTime();
    private bool CheckSSCC(string palletid, out string sSSCC)//产生栈板号
    {
      string sSQL = @"SELECT A.SO_NO,A.INVOICENO,A.HAWB,A.REGION,A.CARRIER,A.SSCC,A.SHIP_DATE,SUBSTR(A.CARTONS,INSTR(A.CARTONS,'/')+1) TOTAL_CARTONS
                            FROM PPSUSER.G_SHIPPING_DETAIL_T  A WHERE A.PALLET_ID=:PALLET_ID AND ROWNUM=1";
      object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "PALLET_ID", palletid } };
      DataSet sDataSet = ClientUtils.ExecuteSQL(sSQL, sqlparams);
      if (sDataSet.Tables[0].Rows.Count > 0)
      {
        txt_Invoice.Text = sDataSet.Tables[0].Rows[0]["INVOICENO"].ToString();
        txt_Carrier.Text = sDataSet.Tables[0].Rows[0]["CARRIER"].ToString();
        txt_Hawb.Text = sDataSet.Tables[0].Rows[0]["HAWB"].ToString();
        if (txt_Carrier.Text == "KN")
        {
          string sqlstrhawb = "SELECT HAWB FROM PPSUSER.G_SHIPPING_DETAIL WHERE SO_NO=:SO_NO AND ROWNUM=1 ";
          object[][] sqlparamshawb = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "SO_NO", sDataSet.Tables[0].Rows[0]["SO_NO"].ToString() } };
          DataTable dthawb = ClientUtils.ExecuteSQL(sqlstrhawb, sqlparamshawb).Tables[0];
          if (dthawb.Rows.Count > 0)
          {
            txt_Hawb.Text = dthawb.Rows[0]["HAWB"].ToString();
          }
        }
      
       
        txt_Region.Text = sDataSet.Tables[0].Rows[0]["REGION"].ToString();
       
        sSSCC = sDataSet.Tables[0].Rows[0]["SSCC"].ToString();
        DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
        dtFormat.ShortDatePattern = "yyyy/MM/dd";
        shipDate = Convert.ToDateTime(sDataSet.Tables[0].Rows[0]["SHIP_DATE"].ToString(), dtFormat);
      }
      else
      {
        ShowMsg("Pallet ID 错误或不存在", 0);
        sSSCC = string.Empty;
        return false;
      }
      #region SQL
      string SQL = @"SELECT (SELECT COUNT (*)
          FROM (SELECT DISTINCT MIX_PALLETS
                  FROM PPSUSER.G_SHIPPING_DETAIL_T
                 WHERE SHIPPING_ID = (SELECT DISTINCT SHIPPING_ID
                                  FROM PPSUSER.G_SHIPPING_DETAIL_T
                                 WHERE SSCC = :SSCC)))
          TOTAL_PALLET,
       (SELECT ROWNO
          FROM (SELECT ROWNUM ROWNO, SSCC
                  FROM (  SELECT SSCC
                            FROM PPSUSER.G_SHIPPING_DETAIL_T
                           WHERE     SHIPPING_ID =
                                        (SELECT DISTINCT SHIPPING_ID
                                           FROM PPSUSER.G_SHIPPING_DETAIL_T
                                          WHERE SSCC = :SSCC)
                        GROUP BY SSCC,MIX_PALLETS
                        ORDER BY MIX_PALLETS)) R
         WHERE R.SSCC = :SSCC)
          START_PALLET
  FROM DUAL ";
      #endregion
      object[][] Params = new object[1][];
      Params[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "SSCC", sSSCC };
      DataSet dt = ClientUtils.ExecuteSQL(SQL, Params);
      if (dt.Tables[0].Rows.Count > 0)
      {
        txt_Start.Text = dt.Tables[0].Rows[0]["START_PALLET"].ToString();
        txt_End.Text = dt.Tables[0].Rows[0]["TOTAL_PALLET"].ToString();
      }
      else
      {
        sSSCC = string.Empty;
        ShowMsg("Pallet ID错误或不存在", 0);
        return false;
      }
      /*
      string sql = @" SELECT CEIL ( (C.QTY / A.CARTON_COUNTS) / A.COUNTS) * A.COUNTS AS CARTON_QTY,
         CEIL ( (C.QTY / A.CARTON_COUNTS) / A.COUNTS) * A.COUNTS
       - (C.QTY / A.CARTON_COUNTS)
          AS EMPTY_CARTON_QTY,
       CEIL ( (C.QTY / A.CARTON_COUNTS) / A.COUNTS) AS LEVEL_QTY
  FROM PPSUSER.G_CARTON_INFO A,
       SAJET.SYS_PART B,
       (SELECT SSCC,SUM(QTY)QTY FROM PPSUSER.G_SHIPPING_DETAIL_T WHERE SSCC=:SSCC GROUP BY SSCC )C,(SELECT PART_ID,SSCC FROM PPSUSER.G_SHIPPING_DETAIL_T WHERE SSCC=:SSCC AND ROWNUM=1) D
 WHERE A.MPN = B.PART_NO AND B.PART_ID = D.PART_ID AND C.SSCC = :SSCC AND C.SSCC=D.SSCC";*/
      string sql = @"SELECT CEIL (C.qty / A.LEVEL_QTY) * (A.LEVEL_QTY / A.SN_QTY) AS CARTON_QTY,
         CEIL (C.qty / A.LEVEL_QTY) * (A.LEVEL_QTY / A.SN_QTY)
       - (C.QTY / A.sn_qty)
          AS EMPTY_CARTON_QTY,
       CEIL ( C.QTY  / A.LEVEL_QTY) AS LEVELS_QTY
  FROM PPSUSER.G_DS_PACKINFO_T A,
       PPSUSER.G_DS_PARTINFO_T E,
       SAJET.SYS_PART B,
       (  SELECT SSCC, SUM (CQTY) CQTY, SUM (qty) qty
            FROM PPSUSER.G_SHIPPING_DETAIL_T
           WHERE SSCC = :SSCC
        GROUP BY SSCC) C,
       (SELECT PART_ID, SSCC
          FROM PPSUSER.G_SHIPPING_DETAIL_T
         WHERE SSCC = :SSCC AND ROWNUM = 1) D
 WHERE     A.PACK_CODE = E.PACK_CODE
       AND E.ICTPN = B.PART_NO
       AND B.PART_ID = D.PART_ID
       AND C.SSCC = :SSCC
       AND C.SSCC = D.SSCC";

      object[][] Pa = new object[1][];
      Pa[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "SSCC", sSSCC };
      DataSet ds = ClientUtils.ExecuteSQL(sql, Pa);
      if (ds.Tables[0].Rows.Count > 0)
      {
        txt_TotalCartons.Text = ds.Tables[0].Rows[0]["CARTON_QTY"].ToString();
        txt_EmptyCartons.Text = ds.Tables[0].Rows[0]["EMPTY_CARTON_QTY"].ToString();
        tblayer.Text = ds.Tables[0].Rows[0]["LEVELS_QTY"].ToString();
      }
      else
      {
        sSSCC = string.Empty;
        ShowMsg("层数和空箱数计算错误", 0);
        return false;
      }
      return true;
      #region 生成栈板号
      /*
            string sqls = "SELECT DISTINCT MODEL_NAME FROM PPSUSER.G_SHIPPING_DETAIL_T A ,PPSUSER.G_DS_PARTINFO_T B,SAJET.SYS_PART C WHERE A.SSCC=:SSCC AND A.PART_ID=C.PART_ID AND C.PART_NO=B.ICTPN AND A.MPN=B.MPN";
            DataTable dtt = ClientUtils.ExecuteSQL(sqls, Pa).Tables[0];
            if (dtt.Rows.Count != 1)
            {
                ShowMsg("Model Error ",0);
                txt_sscc.Focus();
                txt_sscc.SelectAll();
                return false;

            }
            string modelname = dtt.Rows[0]["MODEL_NAME"].ToString();
            //DateTime sDateTime = ClientUtils.GetSysDate();
            string sPalletNO = modelname + txt_sscc.Text.Substring(8, 9) + shipDate.Year.ToString() + shipDate.Month.ToString().PadLeft(2, '0') + shipDate.Day.ToString().PadLeft(2, '0');
            txt_sscc.Text = sPalletNO;
            */
      #endregion
    }

    private void fMain_Load(object sender, EventArgs e)
    {
      sPrintLabel = new PrintLabel();
      g_sProgram = ClientUtils.fProgramName;
      this.BackgroundImage = ClientUtils.LoadImage("ImgMain.jpg");
      this.BackgroundImageLayout = ImageLayout.Stretch;
      this.WindowState = FormWindowState.Maximized;
      this.Text = this.Text + "(" + SajetCommon.g_sFileVersion + ")";
      btnPrintH.Enabled = false;
      cmbWeightType.SelectedIndex = 0;
    }
    private string CheckDirectory()
    {
      string sDirectory = Application.StartupPath + @"\" + g_sProgram + @"\Label";
      if (!Directory.Exists(sDirectory))
        Directory.CreateDirectory(sDirectory);
      return sDirectory;
    }
    private void PrintRecord()
    {
      try
      {
        string sql = @"SELECT a.* FROM   PPSUSER.G_SHIPPING_PALLET_LABEL_PRINT a
                      WHERE a.sscc =:SSCC";
        object[][] Pa = new object[1][];
        Pa[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "SSCC", currSSCC };
        DataSet ds = ClientUtils.ExecuteSQL(sql, Pa);
        if (ds.Tables[0].Rows.Count > 0)
        {
          return;
        }
        object[][] Params = new object[11][];
        Params[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "PALLET_NO", txt_PalletId.Text.Trim() };
        Params[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "INVOICE", txt_Invoice.Text.Trim() };
        Params[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "HAWB", txt_Hawb.Text.Trim() };
        Params[3] = new object[] { ParameterDirection.Input, OracleType.VarChar, "PALLET", txt_Start.Text.Trim() + " OF " + txt_End.Text.Trim() };
        Params[4] = new object[] { ParameterDirection.Input, OracleType.VarChar, "REGION", txt_Region.Text.Trim() };
        Params[5] = new object[] { ParameterDirection.Input, OracleType.VarChar, "CARRIER", txt_Carrier.Text.Trim() };
        Params[6] = new object[] { ParameterDirection.Input, OracleType.Number, "TOTAL_CARTONS", txt_TotalCartons.Text.Trim() };
        Params[7] = new object[] { ParameterDirection.Input, OracleType.Number, "EMPTY_CARTONS", txt_EmptyCartons.Text.Trim() };
        Params[8] = new object[] { ParameterDirection.Input, OracleType.Number, "UPDATE_USERID", ClientUtils.UserPara1 };
        Params[9] = new object[] { ParameterDirection.Input, OracleType.DateTime, "UPDATE_TIME", ClientUtils.GetSysDate() };
        Params[10] = new object[] { ParameterDirection.Input, OracleType.VarChar, "SSCC", currSSCC };
        string sSQL = @"INSERT INTO PPSUSER.G_SHIPPING_PALLET_LABEL_PRINT(
                            PALLET_NO,
                            INVOICE,
                            HAWB,
                            PALLET,
                            REGION,
                            CARRIER,
                            TOTAL_CARTONS,
                            EMPTY_CARTONS,
                            UPDATE_USERID,
                            UPDATE_TIME,
                            SSCC
                            ) VALUES
                           (:PALLET_NO,
                           :INVOICE,
                           :HAWB,
                           :PALLET,
                           :REGION,
                           :CARRIER,
                           :TOTAL_CARTONS,
                           :EMPTY_CARTONS,
                           :UPDATE_USERID,
                           :UPDATE_TIME,
                           :SSCC)";
        ClientUtils.ExecuteSQL(sSQL, Params);
      }
      catch (Exception ex)
      {
        ShowMsg(ex.Message, 0);
      }

    }



    private void ClearText()
    {
      foreach (Control sControl in this.Controls)
      {
        if (sControl is TextBox)
        {
          sControl.Text = string.Empty;

        }
      }
      
    }

    private string GetPoeStr(ShippingItemInfo item)
    {
      try
      {
        //这两张表没用同步，只能使用DBlink 进行处理
        string sqlstr = "SELECT DISTINCT COC,POE FROM WMUSER.AC_TMS_REQ_HEADER@DGEDI  WHERE REQ_NUM IN (SELECT REQ_NUM FROM WMUSER.AC_TMS_REQ_LINE@DGEDI WHERE AC_PO=:PO AND AC_PO_LINE=:PO_LINE)";
        object[][] sqlparams = new object[][]{new object[]{ParameterDirection.Input,OracleType.VarChar,"PO",item.Po_no},
                new object[]{ParameterDirection.Input,OracleType.VarChar,"PO_LINE",item.LineItem}};
        DataTable dt = ClientUtils.ExecuteSQL(sqlstr, sqlparams).Tables[0];
        if (dt.Rows.Count == 1)
        {
          if (item.Region == "EMEIA")
          {
            return dt.Rows[0]["COC"].ToString();
          }
          else
          {
            return dt.Rows[0]["POE"].ToString();
          }
        }
        else
        {
          ShowMsg("POE COUNT ERROR", 0);
          return string.Empty;
        }
      }
      catch (Exception ex)
      {
        ShowMsg(ex.Message, 0);
        return string.Empty;
      }

    }

    private void PrintPalletLoadingSheet()
    {

      if (bindDNItemList.Count == 0)
      {
        return;
      }
      foreach (Control sControl in this.Controls)
      {
        if (sControl is TextBox)
        {
          if (string.IsNullOrEmpty(sControl.Text))
          {
            ShowMsg("有为空项", 0);
            return;
          }
        }
      }
      string weight = ActualWeight;//getweight(tblayer.Text.Trim(), txt_EmptyCartons.Text.Trim(), (Convert.ToInt32(txt_TotalCartons.Text.Trim()) - Convert.ToInt32(txt_EmptyCartons.Text.Trim())).ToString());
      if (string.IsNullOrEmpty(weight))
      {
        ShowMsg("获取栈板重量错误", 0);
        return;
      }
      if (!insertpalletedi(weight))
      {
        ShowMsg("insert栈板重量错误", 0);
        return;
      }
      try
      {
        string sMessage = string.Empty;
        string sTrText = txt_PalletId.Text.Trim() + "|" + txt_Invoice.Text.Trim() + "|" + txt_Hawb.Text.Trim() + "|" + txt_Start.Text.Trim() + "|" + txt_End.Text.Trim() + "|" + txt_Region.Text.Trim() + "|" + txt_Carrier.Text.Trim() + "|" + txt_TotalCartons.Text.Trim() + "|" + txt_EmptyCartons.Text.Trim();

        StringBuilder spodata = new StringBuilder();
        spodata.Append("|");
        DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
        dtFormat.ShortDatePattern = "yyyy/MM/dd";
        spodata.Append(Convert.ToDateTime(bindDNItemList[0].Ship_date, dtFormat).ToString("dd/MM/yyyy") + "|" + weight + "|");
        if (bindDNItemList[0].MixFlag == "999")
        {
          spodata.Append("CONSOLIDATED|");
        }
        else
        {
          spodata.Append("DO NOT BREAK BULK|");
        }
        foreach (var item in bindDNItemList)
        {
          spodata.Append(item.Po_no + "|" + item.LineItem + "|" + item.Mpn + "|" + item.Qty + "|" + GetPoeStr(item) + "|" + "HUB" + "|");
        }
        sTrText += spodata.ToString();

        

        if (sPrintLabel.Print_Bartender_DataSource("PalletSheetlabel", CheckDirectory(), sTrText, 1, out sMessage))
        {
          ShowMsg("打印OK", 3);
          PrintRecord();
          ClearText();
        }
        else
        {
          ShowMsg(sMessage, 0);
        }
      }
      catch (Exception ex)
      {
        ShowMsg(ex.Message, 0);
      }
    }
    string palletActualWeitht = string.Empty;
    private void btnPrint_Click(object sender, EventArgs e)
    {
      PrintPalletLoadingSheet();
    }


    private void PrintHandoverManifest() 
    {
      try 
      {
        string sqlstr = @"SELECT DISTINCT SSCC
  FROM PPSUSER.G_SHIPPING_DETAIL_T
 WHERE SO_NO = :SO_NO
MINUS
SELECT DISTINCT B.SSCC
  FROM PPSUSER.G_SHIPPING_PALLET_LABEL_PRINT A, PPSUSER.G_SHIPPING_DETAIL_T B
 WHERE     A.SSCC = B.SSCC
       AND A.PALLET_NO = B.PALLET_ID
       AND A.SSCC = B.SSCC
       AND B.SO_NO = :SO_NO";
        object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "SO_NO", bindDNItemList[0].Dn_No } };
        DataTable dt = ClientUtils.ExecuteSQL(sqlstr, sqlparams).Tables[0];
        if (dt.Rows.Count == 0) //整个出货单全部称重完成
        {
          CRReport.CRMain cr = new CRReport.CRMain();
          //HandoverManifest 
          cr.FDReport(bindDNItemList[0].Dn_No);
          System.Threading.Thread.Sleep(2000);

          cr.FDRegion(bindDNItemList[0].Dn_No, bindDNItemList[0].Po_no, bindDNItemList[0].Region,bindDNItemList[0].Ctry, false, Application.StartupPath.Substring(0, 2) + @"\PLIST\");
          ShowMsg("出货单称重完成，打印HandoverManifest", 1);
        }
      }
      catch(Exception ex) 
      {
        MessageBox.Show(ex.Message);
      }
    }
    
    public string getweight(string layer, string emptycontainer, string fullcartoncount)
    {
      try
      {
        double layercount = Convert.ToDouble(layer);
        double emptycartoncount = Convert.ToDouble(emptycontainer);
        double fullcarton = Convert.ToDouble(fullcartoncount);
        string allpalletweight = "";
        object[][] pa = new object[1][];
        string sql = @"SELECT A.MPN,
                           A.AREA,
                           A.TRUNKFUL_WEIGHT,
                           A.TRUNKFUL_COUNT,
                           A.EMPTYCONTAINER_WEIGHT,
                           A.EMPTYCONTAINER_COUNT,
                           A.ALLCONTAINER_WEIGHT,
                           A.ALLEMPTYCONTAINER_WEIGHT,
                           A.VERTICALANGLELAYER_WEIGHT,
                           A.LAYER_QTY,
                           A.VERTICALANGLE_WEIGHT,
                           A.UPPERCORNERPLATE_WEIGHT,
                           A.WORLDCOVER_WEIGHT,
                           A.PALLET_WEIGHT,
                           A.OTHER_WEIGHT,
                           A.ALLPALLET_WEIGHT,
                           A.TOLERANCE
                      FROM PPSUSER.G_SHIPPING_DETAIL_T B,
                           PPSUSER.SYS_PALLET_INFO   A,
                           SAJET.SYS_PART            C
                     WHERE A.MPN = C.PART_NO
                       AND C.PART_ID = B.PART_ID
                       AND B.SSCC=:SSCC";
        pa[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "SSCC", currSSCC };
        DataSet ds = ClientUtils.ExecuteSQL(sql, pa);
        if (ds.Tables[0].Rows.Count > 0)
        {
          double y = Convert.ToDouble(ds.Tables[0].Rows[0]["OTHER_WEIGHT"].ToString());//Other
          double q = Convert.ToDouble(ds.Tables[0].Rows[0]["TRUNKFUL_WEIGHT"].ToString());//满箱单重
          double v = Convert.ToDouble(ds.Tables[0].Rows[0]["UPPERCORNERPLATE_WEIGHT"].ToString());//上角板重量
          double w = Convert.ToDouble(ds.Tables[0].Rows[0]["WORLDCOVER_WEIGHT"].ToString());//天地盖重量
          double x = Convert.ToDouble(ds.Tables[0].Rows[0]["PALLET_WEIGHT"].ToString());//空栈板重量
          double o = Convert.ToDouble(ds.Tables[0].Rows[0]["EMPTYCONTAINER_WEIGHT"].ToString());//空箱单重
          double s = Convert.ToDouble(ds.Tables[0].Rows[0]["VERTICALANGLELAYER_WEIGHT"].ToString());//竖角板每层重

          double u = s * layercount;//竖角板总重
          double r = o * emptycartoncount;//空箱总重
          double f = fullcarton * q;//满箱总重

          double z = u + r + f + v + w + x + y;//竖角板总重+空箱总重+满箱总重+上角板重量+天地盖重量+空栈板重量+Other
          allpalletweight = z.ToString("0.00");
          palletWeight = x.ToString();
          fullCartonWeight = f;
          return allpalletweight;
        }
        else 
        {
          ShowMsg("缺少料号对应的栈板数据", 0);
          return string.Empty;
        }
      }
      catch (Exception ex)
      {
        ShowMsg(ex.Message, 0);
        return string.Empty;
      }
    }

    public bool insertpalletedi(string allpallet_weight)
    {
      try
      {

        double a = Convert.ToDouble(palletWeight);
        double b = Convert.ToDouble(allpallet_weight);
        double e = Convert.ToDouble(tblayer.Text);
        //double c = b - a;
        string carton_weight = fullCartonWeight.ToString("0.00");

        double d = 12.3 + 1 + 0.5 + e * 14.3;
        string pallet_height = d.ToString("0.0");

        string Sql = @"SELECT a.*  FROM  PPSUSER.G_PALLETEDI_INFO a
                               WHERE a.sscc=:SSCC";
        object[][] para = new object[1][];
        para[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "SSCC", currSSCC };
        DataSet ds = ClientUtils.ExecuteSQL(Sql, para);
        if (ds.Tables[0].Rows.Count == 0)// 不存在insert
        {

          object[][] pa = new object[7][];
          string sql = @"INSERT INTO PPSUSER.G_PALLETEDI_INFO  a
                                (a.so_no,a.pallet_weight,a.allpallet_weight,a.carton_weight,a.sscc,a.pallet_length,a.pallet_width,a.pallet_height,a.layer,A.STANDARD_WEIGHT)
                                SELECT DISTINCT b.so_no,:pallet_weight,:allpallet_weight,:carton_weight,:SSCC,'115','85',:pallet_height,:layer,:STANDARD_WEIGHT
                                FROM ppsuser.g_shipping_detail_T b
                                WHERE b.sscc =:SSCC";
          pa[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "SSCC", currSSCC };
          pa[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "pallet_weight", palletWeight };
          pa[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "allpallet_weight", allpallet_weight };
          pa[3] = new object[] { ParameterDirection.Input, OracleType.VarChar, "carton_weight", carton_weight };
          pa[4] = new object[] { ParameterDirection.Input, OracleType.VarChar, "pallet_height", pallet_height };
          pa[5] = new object[] { ParameterDirection.Input, OracleType.VarChar, "layer", tblayer.Text };
          pa[6] = new object[] { ParameterDirection.Input, OracleType.VarChar, "STANDARD_WEIGHT", palletActualWeitht };
          ClientUtils.ExecuteSQL(sql, pa);
          return true;
        }
        else
        {
          object[][] pa = new object[6][];
          string sql = @"UPDATE PPSUSER.G_PALLETEDI_INFO  a
                                  SET a.pallet_weight=:pallet_weight,a.allpallet_weight=:allpallet_weight,
                                      a.carton_weight=:carton_weight,a.pallet_height=:pallet_height,STANDARD_WEIGHT=:STANDARD_WEIGHT
                                  WHERE a.sscc =:SSCC";
          pa[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "SSCC", currSSCC };
          pa[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "pallet_weight", palletWeight };
          pa[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "allpallet_weight", allpallet_weight };
          pa[3] = new object[] { ParameterDirection.Input, OracleType.VarChar, "carton_weight", carton_weight };
          pa[4] = new object[] { ParameterDirection.Input, OracleType.VarChar, "pallet_height", pallet_height };
          pa[5] = new object[] { ParameterDirection.Input, OracleType.VarChar, "STANDARD_WEIGHT", palletActualWeitht };
          ClientUtils.ExecuteSQL(sql, pa);
          return true;
        }
      }
      catch (Exception ex)
      {
        ShowMsg(ex.Message, 0);
        return false;
      }
    }


    private void txt_PalletId_TextChanged(object sender, EventArgs e)
    {
      foreach (var item in this.Controls)
      {
        if (item is TextBox)
        {
          if (((TextBox)item).Name == "txt_PalletId")
          {
            continue;
          }
          else
          {
            ((TextBox)item).Clear();

          }
        }
      }

      currSSCC = string.Empty;
      txtWeight.Enabled = false;
      btnPrintH.Enabled = false;
      btnPrintPDF.Enabled = false;

    }

    /// <summary>
    /// 栈板实际总量
    /// </summary>
    string ActualWeight = string.Empty;
    private void txtWeight_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Enter) return;
      if (string.IsNullOrEmpty(txtWeight.Text))
      {
        return;
      }
      double actualweight = 0.0;
      double upperweight = 0.0;
      double lowerweight = 0.0;
      try
      {
        actualweight = Convert.ToDouble(txtWeight.Text);
        upperweight = Convert.ToDouble(txtUpperlimit.Text);
        lowerweight = Convert.ToDouble(txtLowerlimit.Text);
        if (actualweight >= lowerweight && actualweight <= upperweight)
        {
          ActualWeight = actualweight.ToString();
          PrintPalletLoadingSheet();
          //打印HandoverManifest
          PrintHandoverManifest();
          bindDNItemList.Clear();
          txt_PalletId.Clear();
          txt_PalletId.Focus();
        }
        else
        {
          txtWeight.SelectAll();
          txtWeight.Focus();
          ShowMsg("实际重量不在范围内", 0);
        }

      }

      catch (Exception ex)
      {
        txtWeight.SelectAll();
        txtWeight.Focus();
        
        ShowMsg(ex.Message, 0);

      }
      finally
      {
        txtWeightRecive.Clear();
        reciveFlag = true;
      }
    }

    private bool CheckCartonAndPalletId(string sscc, string cartonno)
    {
      try
      {
        string sqlstr = "SELECT DISTINCT SHIPPING_RECID FROM PPSUSER.G_SHIPPING_SN WHERE CARTON_NO=:CARTON_NO";
        object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "CARTON_NO", cartonno } };
        DataTable dt = ClientUtils.ExecuteSQL(sqlstr, sqlparams).Tables[0];
        if (dt.Rows.Count > 0)
        {
          string psscc = dt.Rows[0]["SHIPPING_RECID"].ToString();
          if (psscc != sscc)
          {
            ShowMsg("该箱号不属于当前栈板", 0);
            return false;
          }
          else
          {
            return true;
          }

        }
        else
        {
          ShowMsg("该箱号没有进行Pack,找不到栈板信息", 0);
          return false;
        }
      }
      catch (Exception ex)
      {
        ShowMsg(ex.Message, 0);
        return false;
      }

    }
    
    
    /*
    private void txtCartonNo_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Enter) return;
      string cartonno = txtCartonNo.Text.Trim();
      if (string.IsNullOrEmpty(cartonno)) { return; }
      if (!CheckCartonAndPalletId(currSSCC, cartonno))
      {
        txtCartonNo.SelectAll();
        txtCartonNo.Focus();
        return;
      }
      ShowMsg("箱号检查OK,属于此栈板", 3);
      txtWeight.Enabled = true;
      txtWeight.Clear();
      txtWeight.Focus();

    }
    */
    private void btnPrintH_Click(object sender, EventArgs e)
    { 
      #region 检查权限
      fCheck fcheck = new fCheck();
      if (fcheck.ShowDialog() != DialogResult.OK)
      {
        ShowMsg(SajetCommon.SetLanguage("账号权限不正确"), 1);
        return;
      }
      #endregion

      btnPrintH.Enabled = false;
      //打印HandoverManifest水晶报表
      CRReport.CRMain cr = new CRReport.CRMain();
      cr.FDReport(bindDNItemList[0].Dn_No);
      btnPrintH.Enabled = true;
      //System.Threading.Thread.Sleep(2000);
      //cr.FDRegion(bindDNItemList[0].Dn_No, bindDNItemList[0].Po_no, bindDNItemList[0].Region, false, Application.StartupPath.Substring(0, 2) + @"\PLIST\");
      //Thread t = new Thread(PrintHandoverManifest);
      //t.IsBackground = true;
     // t.Start();
      //CRReport.CRfrom.EMEIAPLLayoutForm crpl = new CRReport.CRfrom.EMEIAPLLayoutForm(bindDNItemList[0].Invoiceno, @"D:\PLIST\");
      //CRReport.CRfrom.EMEIAInvoiceLayoutForm crinvoice = new CRReport.CRfrom.EMEIAInvoiceLayoutForm(bindDNItemList[0].Invoiceno, @"D:\PLIST\");
    
    }

    private void btnPrintPDF_Click(object sender, EventArgs e)
    {
      #region 检查权限
      fCheck fcheck = new fCheck();
      if (fcheck.ShowDialog() != DialogResult.OK)
      {
        ShowMsg(SajetCommon.SetLanguage("账号权限不正确"), 1);
        return;
      }
      #endregion
      btnPrintPDF.Enabled = false;
      CRReport.CRMain cr = new CRReport.CRMain();
      cr.FDRegion(bindDNItemList[0].Dn_No, bindDNItemList[0].Po_no, bindDNItemList[0].Region,bindDNItemList[0].Ctry, false, Application.StartupPath.Substring(0, 2) + @"\PLIST\");
      btnPrintPDF.Enabled = true;
    }
    bool reciveFlag = true;
    private void txtWeightRecive_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Enter) 
      {
        return;
      }
      if (!reciveFlag) { txtWeightRecive.Clear(); return; }
      string recivedata = txtWeightRecive.Text.ToUpper();
      if (reciveFlag&&recivedata.Contains("0000")&&Regex.Matches(recivedata,"KG").Count==3)
      {
        if (Regex.IsMatch(recivedata, "[0-9]{1,}[.][0-9]*"))
        {
          reciveFlag = false;
          string weight = Regex.Matches(recivedata, "[0-9]{1,}[.][0-9]*")[0].Value;
          txtWeight.Text = weight;
          txtWeight_KeyDown(null, new KeyEventArgs(Keys.Enter));
        }
        else 
        {

          return;
        }
        
      }
    }

    private void cmbWeightType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (cmbWeightType.SelectedIndex == 0)
      {
        txtWeightRecive.Visible = true;
      }
      else 
      {
        txtWeightRecive.Visible = false;
      }
    }
  }
}

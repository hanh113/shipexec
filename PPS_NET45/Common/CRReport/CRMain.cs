/*
 * Data:2018-07-01
 * User:Admin
 * Content:根据条件查询对应的报表 
 * Explanatory：更改逻辑简单，只需要再对于的地方NEW一个F0RM对象
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRReport.CRfrom;
using System.Data;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CRReport
{
    public class CRMain
    {
        /// <summary>
        /// DS调用报表的方法
        /// </summary>
        /// <param name="shipTypePar"></param> 
        /// <param name="regionPar"></param> 
        /// <param name="stCountryCodePar"></param>
        /// <param name="dic"></param>
        public void printCR(string shipTypePar, string regionPar, string stCountryCodePar, Dictionary<string, string> dic)
        {
            if (regionPar == "AMR")
            {
                if (stCountryCodePar == "CA")
                {
                    new ConsumerPkListBilingualCanadaForm(dic["AC_DN"], "", "", true);
                }
                else if (stCountryCodePar == "MX")
                {
                    new Ltr_PkList_MEX(dic["AC_DN"], "", "", true, Application.StartupPath + "\\PDF\\");
                }
                else
                {
                    new ConsumerPackingList613_6647_A4(dic["AC_DN"], true, Application.StartupPath + "\\PDF\\");
                }
            }
            else if (regionPar == "EMEIA")
            {
                new ConsumerPackingList_EMEA_G("", "", "");
            }
            else if (regionPar == "PAC")
            {
                if (stCountryCodePar == "CN")
                {
                    new P1ConsumerPackingListChina(dic["AC_DN"], "", "", true);
                }
                else if (stCountryCodePar == "KR")
                {
                    new ConsumerPackingListKorea(dic["AC_DN"], "", "", true, Application.StartupPath + "\\PDF\\");
                }
                else if (stCountryCodePar == "HK")
                {
                    new P1ConsumerPackingListHK(dic["AC_DN"], "", "", true);
                }
                else if (stCountryCodePar == "JP")
                {
                    new P1ConsumerPackingListJP(dic["AC_DN"], "", "", true);
                }
                else if (stCountryCodePar == "TW")
                {
                    new P1ConsumerPackingListTW(dic["AC_DN"], "", "", true);
                }
                else if (stCountryCodePar == "TH")
                {
                    new ThaiLandPKFormcs(dic["AC_DN"], "", "", true);
                }
                else
                {
                    new P1ConsumerPackingListGlobal(dic["AC_DN"], "", "", true);
                }
            }
            else
            {
                //  MessageBox.Show("找不到对应报表！");  
            }
        }

        public void ACHanDoveMan2(string strsid, string strpalletno, bool PRINTERorPDF, bool isFirst, string strPath, string strnull, string strnull2)
        {
            //20200511 修复一个集货单分多辆车的bug
            new ACHandoverManifest(strsid, strpalletno, PRINTERorPDF, isFirst, strPath, strnull, strnull2);
        }

        public void EmailCRreportSyn(string shipTypePar, string regionPar, string stCountryCodePar, Dictionary<string, string> dic, bool print)
        {
            List<string> filePath = new List<string>();
            #region AMR
            if (regionPar.Equals("AMR"))
            {
                if (stCountryCodePar.Equals("US") || stCountryCodePar.Equals("PE"))
                {
                    #region
                    try
                    {
                        ConsolAMRLayoutUSPR ConsolAMRLayoutUSPR = new ConsolAMRLayoutUSPR(dic["SHIPMENT_ID"], print);
                        string diskPath = ConsolAMRLayoutUSPR.diskCompletePath;
                        filePath.Add(diskPath);
                    }
                    catch
                    {
                    }
                    Thread.Sleep(2000);
                    try
                    {
                        ConsolCIAMRLayoutUSAndPeru ConsolCIAMRLayoutUSAndPeru = new ConsolCIAMRLayoutUSAndPeru(dic["SHIPMENT_ID"], Application.StartupPath + "\\PDF\\", print);
                        string diskPath = ConsolCIAMRLayoutUSAndPeru.diskCompletePath;
                        string diskPath1 = ConsolCIAMRLayoutUSAndPeru.diskCompelteSerialPath;
                        filePath.Add(diskPath);
                        filePath.Add(diskPath1);
                    }
                    catch
                    {

                    }
                    #endregion
                }
                else if (stCountryCodePar.Equals("CA"))
                {
                    #region
                    try
                    {
                        ConsolAMRLayoutCAN ConsolAMRLayoutCAN = new ConsolAMRLayoutCAN(dic["SHIPMENT_ID"], print);
                        string diskPath = ConsolAMRLayoutCAN.diskCompletePath;
                        filePath.Add(diskPath);
                    }
                    catch
                    {
                    }
                    Thread.Sleep(2000);
                    try
                    {
                        ConsolCIAMRLayoutCanada ConsolCIAMRLayoutCanada = new ConsolCIAMRLayoutCanada(dic["SHIPMENT_ID"], print);
                        string diskPath = ConsolCIAMRLayoutCanada.diskCompletePath;
                        string diskPath1 = ConsolCIAMRLayoutCanada.diskCompelteSerialPath;
                        filePath.Add(diskPath);
                        filePath.Add(diskPath1);
                    }
                    catch
                    {
                    }
                    #endregion
                }
                else if (stCountryCodePar.Equals("MX"))
                {
                    #region
                    try
                    {
                        ConsolAMRLayoutMexico ConsolAMRLayoutMexico = new ConsolAMRLayoutMexico(dic["SHIPMENT_ID"], print);
                        string diskPath = ConsolAMRLayoutMexico.diskCompletePath;
                        filePath.Add(diskPath);
                    }
                    catch { }
                    Thread.Sleep(2000);
                    try
                    {
                        ConsolCIAMRLayoutMexicoForm ConsolCIAMRLayoutMexicoForm = new ConsolCIAMRLayoutMexicoForm(dic["SHIPMENT_ID"], print);
                        string diskPath = ConsolCIAMRLayoutMexicoForm.diskCompletePath;
                        string diskPath1 = ConsolCIAMRLayoutMexicoForm.diskCompelteSerialPath;
                        filePath.Add(diskPath);
                        filePath.Add(diskPath1);
                    }
                    catch { }
                    #endregion
                }
                else if (stCountryCodePar.Equals("BR"))
                {
                    #region
                    try
                    {
                        ConsolAMRLayoutBrazil ConsolAMRLayoutBrazil = new ConsolAMRLayoutBrazil(dic["SHIPMENT_ID"], print);
                        string diskPath = ConsolAMRLayoutBrazil.diskCompletePath;
                        filePath.Add(diskPath);
                    }
                    catch { }
                    Thread.Sleep(2000);
                    try
                    {
                        ConsolCIAMRLayoutBrazilForm ConsolCIAMRLayoutBrazilForm = new ConsolCIAMRLayoutBrazilForm(dic["SHIPMENT_ID"], print);
                        string diskPath = ConsolCIAMRLayoutBrazilForm.diskCompletePath;
                        filePath.Add(diskPath);
                    }
                    catch { }
                    #endregion
                }
                else if (stCountryCodePar.Equals("CH"))
                {
                    #region
                    try
                    {
                        ConsolAMRLayoutChile ConsolAMRLayoutChile = new ConsolAMRLayoutChile(dic["SHIPMENT_ID"], print);
                        string diskPath = ConsolAMRLayoutChile.diskCompletePath;
                        filePath.Add(diskPath);
                    }
                    catch { }
                    Thread.Sleep(2000);
                    try
                    {
                        ConsolCIAMRLayoutChileForm ConsolCIAMRLayoutChileForm = new ConsolCIAMRLayoutChileForm(dic["SHIPMENT_ID"], print);
                        string diskPath = ConsolCIAMRLayoutChileForm.diskCompletePath;
                        filePath.Add(diskPath);
                    }
                    catch
                    { }
                    #endregion
                }
                else if (stCountryCodePar.Equals("CO"))
                {
                    #region
                    try
                    {
                        ConsolAMRLayoutColumbia ConsolAMRLayoutColumbia = new ConsolAMRLayoutColumbia(dic["SHIPMENT_ID"], print);
                        string diskPath = ConsolAMRLayoutColumbia.diskCompletePath;
                        filePath.Add(diskPath);
                    }
                    catch { }
                    Thread.Sleep(2000);
                    try
                    {
                        ConsolCIAMRLayoutCOForm ConsolCIAMRLayoutCOForm = new ConsolCIAMRLayoutCOForm(dic["SHIPMENT_ID"], print);
                        string diskPath = ConsolCIAMRLayoutCOForm.diskCompletePath;
                        string diskPath1 = ConsolCIAMRLayoutCOForm.diskCompelteSerialPath;
                        filePath.Add(diskPath);
                        filePath.Add(diskPath1);
                    }
                    catch { }
                    #endregion
                }
            }
            #endregion
            #region EMEIA
            else if (regionPar.Equals("EMEIA"))
            {
                #region CPL
                if (stCountryCodePar.Equals("IN"))
                {
                    #region
                    try
                    {
                        EMEA_OEM_PK_INDIAForm EMEA_OEM_PK_INDIAForm = new EMEA_OEM_PK_INDIAForm(dic["SHIPMENT_ID"], print);
                        string diskPath = EMEA_OEM_PK_INDIAForm.diskCompletePath;
                        filePath.Add(diskPath);
                    }
                    catch { }
                    #endregion
                }
                else if (stCountryCodePar.Equals("RU"))
                {
                    #region
                    try
                    {
                        EMEA_OEM_PK_RussiaForm EMEA_OEM_PK_RussiaForm = new EMEA_OEM_PK_RussiaForm(dic["SHIPMENT_ID"], print);
                        string diskPath = EMEA_OEM_PK_RussiaForm.diskCompletePath;
                        filePath.Add(diskPath);
                    }
                    catch { }
                    #endregion
                }
                else if (stCountryCodePar.Equals("TR"))
                {
                    #region
                    try
                    {
                        EMEA_OEM_PK_TurkeyForm EMEA_OEM_PK_TurkeyForm = new EMEA_OEM_PK_TurkeyForm(dic["SHIPMENT_ID"], print);
                        string diskPath = EMEA_OEM_PK_TurkeyForm.diskCompletePath;
                        filePath.Add(diskPath);
                    }
                    catch
                    { }
                    #endregion
                }
                else
                {
                    if (shipTypePar.Equals("DSP") || shipTypePar.Equals("DSB"))
                    {
                        #region
                        try
                        {
                            EMEA_OEM_PK_DirectShipForm EMEA_OEM_PK_DirectShipForm = new EMEA_OEM_PK_DirectShipForm(dic["SHIPMENT_ID"], print);
                            string diskPath = EMEA_OEM_PK_DirectShipForm.diskCompletePath;
                            filePath.Add(diskPath);
                        }
                        catch { }
                        #endregion
                    }
                    else if (shipTypePar.Equals("DSS"))
                    {
                        #region
                        try
                        {
                            EMEA_OEM_PK_STOForm EMEA_OEM_PK_STOForm = new EMEA_OEM_PK_STOForm(dic["SHIPMENT_ID"], print);
                            string diskPath = EMEA_OEM_PK_STOForm.diskCompletePath;
                            filePath.Add(diskPath);
                        }
                        catch { }
                        #endregion
                    }
                    else if (shipTypePar.Equals("DSM"))
                    {
                        #region
                        try
                        {
                            EMEA_OEM_PK_MITForm EMEA_OEM_PK_MITForm = new EMEA_OEM_PK_MITForm(dic["SHIPMENT_ID"], print);
                            string diskPath = EMEA_OEM_PK_MITForm.diskCompletePath;
                            filePath.Add(diskPath);
                        }
                        catch { }
                        #endregion
                    }
                }
                #endregion
                Thread.Sleep(2000);
                #region CCI
                if (stCountryCodePar.Equals("CZ"))
                {
                    #region
                    try
                    {
                        EMEAOEMCommercialInvocieCzechForm EMEAOEMCommercialInvocieCzechForm = new EMEAOEMCommercialInvocieCzechForm(dic["SHIPMENT_ID"], Application.StartupPath + "\\PDF\\", print);
                        string diskPath = EMEAOEMCommercialInvocieCzechForm.diskCompletePath;
                        filePath.Add(diskPath);
                    }
                    catch { }
                    #endregion
                }
                else if (stCountryCodePar.Equals("IT"))
                {
                    #region
                    try
                    {
                        EMEAOEMCommercialInvocieItalyForm EMEAOEMCommercialInvocieItalyForm = new EMEAOEMCommercialInvocieItalyForm(dic["SHIPMENT_ID"], Application.StartupPath + "\\PDF\\", print);
                        string diskPath = EMEAOEMCommercialInvocieItalyForm.diskCompletePath;
                        filePath.Add(diskPath);
                    }
                    catch { }
                    #endregion
                }
                else if (stCountryCodePar.Equals("NL"))
                {
                    #region
                    try
                    {
                        EMEAOEMCommercialInvocieNetherlandsForm EMEAOEMCommercialInvocieNetherlandsForm = new EMEAOEMCommercialInvocieNetherlandsForm(dic["SHIPMENT_ID"], Application.StartupPath + "\\PDF\\", print);
                        string diskPath = EMEAOEMCommercialInvocieNetherlandsForm.diskCompletePath;
                        filePath.Add(diskPath);
                    }
                    catch { }
                    #endregion
                }
                else if (stCountryCodePar.Equals("GB"))
                {
                    #region
                    try
                    {
                        EMEAOEMCommercialInvocieUKForm EMEAOEMCommercialInvocieUKForm = new EMEAOEMCommercialInvocieUKForm(dic["SHIPMENT_ID"], Application.StartupPath + "\\PDF\\", print);
                        string diskPath = EMEAOEMCommercialInvocieUKForm.diskCompletePath;
                        filePath.Add(diskPath);
                    }
                    catch { }
                    #endregion
                }
                else if (stCountryCodePar.Equals("AE"))
                {
                    #region
                    try
                    {
                        EMEAOEMCommercialInvocieUAEFTZForm EMEAOEMCommercialInvocieUAEFTZForm = new EMEAOEMCommercialInvocieUAEFTZForm(dic["SHIPMENT_ID"], Application.StartupPath + "\\PDF\\", print);
                        string diskPath = EMEAOEMCommercialInvocieUAEFTZForm.completeDiskPath;
                        filePath.Add(diskPath);
                    }
                    catch { }
                    #endregion
                }
                else if (stCountryCodePar.Equals("TR"))
                {
                    #region
                    try
                    {
                        EMEAOEMCommercialInvocieTurkeyForm EMEAOEMCommercialInvocieTurkeyForm = new EMEAOEMCommercialInvocieTurkeyForm(dic["SHIPMENT_ID"], Application.StartupPath + "\\PDF\\", print);
                        string diskPath = EMEAOEMCommercialInvocieTurkeyForm.diskCompletePath;
                        filePath.Add(diskPath);
                    }
                    catch { }
                    #endregion
                }
                else
                {
                    if (shipTypePar.Equals("DSP") || shipTypePar.Equals("DSB"))
                    {
                        #region
                        try
                        {
                            EMEAOEMCommercialInvocieDirectShipForm EMEAOEMCommercialInvocieDirectShipForm = new EMEAOEMCommercialInvocieDirectShipForm(dic["SHIPMENT_ID"], Application.StartupPath + "\\PDF\\", print);
                            string diskPath = EMEAOEMCommercialInvocieDirectShipForm.diskCompletePath;
                            filePath.Add(diskPath);
                        }
                        catch { }
                        #endregion
                    }
                    else if (shipTypePar.Equals("DSS"))
                    {
                        #region
                        try
                        {
                            EMEAOEMCommercialInvocieSTOForm EMEAOEMCommercialInvocieSTOForm = new EMEAOEMCommercialInvocieSTOForm(dic["SHIPMENT_ID"], Application.StartupPath + "\\PDF\\", print);
                            string diskPath = EMEAOEMCommercialInvocieSTOForm.diskCompletePath;
                            filePath.Add(diskPath);
                        }
                        catch { }
                        #endregion
                    }
                    else if (shipTypePar.Equals("DSM"))
                    {
                        #region
                        try
                        {
                            EMEAOEMCommercialInvocieMITForm EMEAOEMCommercialInvocieMITForm = new EMEAOEMCommercialInvocieMITForm(dic["SHIPMENT_ID"], Application.StartupPath + "\\PDF\\", print);
                            string diskPath = EMEAOEMCommercialInvocieMITForm.diskCompletePath;
                            filePath.Add(diskPath);
                        }
                        catch { }
                        #endregion
                    }
                }
                #endregion
            }
            #endregion
            #region APAC
            else if (regionPar.Equals("PAC"))
            {
                try
                {
                    P2_ConsolPackingList P2_ConsolPackingList = new P2_ConsolPackingList(dic["SHIPMENT_ID"], print);
                    string diskPath = P2_ConsolPackingList.completeDiskPath;
                    filePath.Add(diskPath);
                }
                catch { }
                Thread.Sleep(2000);
                try
                {
                    TPIForm TPIForm = new TPIForm(dic["SHIPMENT_ID"], print);
                    string diskPath = TPIForm.completeDiskPath;
                    filePath.Add(diskPath);
                }
                catch
                {

                }
                //Thread.Sleep(2000);
                //try
                //{
                //    ShippingInvoiceForm ShippingInvoiceForm = new ShippingInvoiceForm(dic["SHIPMENT_ID"], print);
                //    string diskPath = ShippingInvoiceForm.completeDiskPath;
                //    filePath.Add(diskPath);
                //}
                //catch
                //{

                //}
            }
            #endregion
            else
            {
                //MessageBox.Show("没有找到对应的报表！");
            }
            Thread.Sleep(10000);
            if (filePath.Count >= 2)
            {
                SendEmail.Send(filePath, dic["SHIPMENT_ID"], "DS");
            }
            else
            {
                // MessageBox.Show("CCI或者CPL缺失，发送不成功！");
            }
        }
        public void BulkCRAMR(string StrCountry, Dictionary<string, string> dic)
        {
            if (StrCountry.Equals("MX"))
            {
                new MexicoChannelPackingList(dic["AC_DN"], "", "", true, Application.StartupPath + "\\PDF\\");
            }
            else if (StrCountry.Equals("US"))
            {
                new ChannelPackingListUS(dic["AC_DN"], "", "", true, Application.StartupPath + "\\PDF\\");
            }
            else if (StrCountry.Equals("CA"))
            {
                new ChannelPackingListCanada(dic["AC_DN"], "", "", true, Application.StartupPath + "\\PDF\\");
            }
            else if (StrCountry.Equals("BR"))
            {
                new ChannelPackingListBrazil(dic["AC_DN "], "", "", true, Application.StartupPath + "\\PDF\\");
            }
            else if (StrCountry.Equals("CH"))
            {
                new ChileChannelPackingList(dic["AC_DN "], "", "", true, Application.StartupPath + "\\PDF\\");
            }
            else if (StrCountry.Equals("CO"))
            {
                new ChannelColumbiaForm(dic["AC_DN "], "", "", true, Application.StartupPath + "\\PDF\\");
            }
            else if (StrCountry.Equals("PE"))
            {
                new ChannelPackingListPeru(dic["AC_DN "], "", "", true, Application.StartupPath + "\\PDF\\");
            }
        }
        public void BulkCREMEIA(string StrCountry, Dictionary<string, string> dic)
        {
            new NewDeliveryNoteGForm(dic["SHIPMENT_ID"], dic["AC_DN"], true);
        }
        public void BulkCRPAC(string StrCountry, Dictionary<string, string> dic)
        {
            new CRReport.CRfrom.D1_DeliveryNote(dic["AC_DN"], "", "", false, "");
        }
        public void HanDoveMan(string shimpid, bool PRINTERorPDF, bool isFirst)
        {
            //如果是第一次做，打印2张A4纸
            //如果是补印的就只打印1张
            new HandoverManifestDirectShip(shimpid, PRINTERorPDF, isFirst);
        }

        public void HanDoveMan(string shimpid, bool PRINTERorPDF, bool isFirst, string strPath)
        {
            //如果是第一次做，打印2张A4纸
            //如果是补印的就只打印1张
            new HandoverManifestDirectShip(shimpid, PRINTERorPDF, isFirst, strPath);
        }

        public void EOSHanDoveManPAC(string strSID, string strCrystalFullPath, bool isPDF, string strPDFPath, int nCopies, bool isOnlyGetDS, out DataSet dtCrystal, out string serverFullLabelName)
        {
            //new HandoverManifestDirectShip(shimpid, PRINTERorPDF, isFirst, strPath);
            new HandoverManifestDirectShip(strSID, strCrystalFullPath, isPDF, strPDFPath, nCopies, isOnlyGetDS, out dtCrystal, out serverFullLabelName);
        }
        public void EOSHanDoveMan(string strSID, string strCrystalFullPath, bool isPDF, string strPDFPath, int nCopies, bool isOnlyGetDS, out DataSet dtCrystal, out string serverFullLabelName)
        {
            //new HandoverManifest(shimpid, PRINTERorPDF, isFirst, strStation, strPath);
            new HandoverManifest(strSID, strCrystalFullPath, isPDF, strPDFPath, nCopies, isOnlyGetDS, out dtCrystal, out serverFullLabelName);
        }

        public void HanDoveMan2(string strsid, string strpalletno, bool PRINTERorPDF, bool isFirst, string strPath, string strnull, string strnull2)
        {
            //20200402 修复一个集货单分多辆车的bug
            new HandoverManifest(strsid, strpalletno, PRINTERorPDF, isFirst, strPath, strnull, strnull2);
        }
        public void HanDoveMan2(string shimpid, bool PRINTERorPDF, bool isFirst)
        {
            //20191010 CarBillList
            new HandoverManifest(shimpid, PRINTERorPDF, isFirst);
        }
        public void HanDoveMan2(string shimpid, bool PRINTERorPDF, bool isFirst, string strPath, string strnull, string strnull2)
        {
            //20191010 CarBillList  ADD PDF backup
            new HandoverManifest(shimpid, PRINTERorPDF, isFirst, strPath, strnull, strnull2);
        }
        public void HanDoveMan2(string shimpid, bool PRINTERorPDF, bool isFirst, string strStation)
        {
            //20190801新写 汇总的HM
            new HandoverManifest(shimpid, PRINTERorPDF, isFirst, strStation);
        }

        public void HanDoveMan2(string shimpid, bool PRINTERorPDF, bool isFirst, string strStation, string strPath)
        {
            //20190801新写 汇总的HM
            new HandoverManifest(shimpid, PRINTERorPDF, isFirst, strStation, strPath);
        }



        public void PalletLoadingSheet(string strPalletNo, bool PRINTERorPDF, bool ISFIRST, string strPath)
        {
            new PalletLoadingSheetForm(strPalletNo, PRINTERorPDF, ISFIRST, strPath);
        }
        public void PalletLoadingSheet(string strPalletNo, string strCrystalFullPath, bool isCustom, bool isPDF, string strPDFPath, int nCopies, bool isOnlyGetDS, out DataSet dtCrystal, out string serverFullLabelName)
        {
            new PalletLoadingSheetForm(strPalletNo, strCrystalFullPath, isCustom, isPDF, strPDFPath, nCopies, isOnlyGetDS, out dtCrystal, out serverFullLabelName);
        }



        public void FDReport(string ICT_DN)
        {
            new HanManCRFrom(ICT_DN);
        }
        public void FDAMRReport(string ICT_DN, string PO_NO, string REGION, bool print, string filedir)
        {
            FDHubGlobalLayoutForm FDHubGlobalLayoutForm = new FDHubGlobalLayoutForm(PO_NO, ICT_DN, filedir, print);
        }
        /// <summary>
        /// FD报表打印方法
        /// </summary>
        /// <param name="ICT_DN"></param> ICT_DN
        /// <param name="REGION"></param> 区域
        /// <param name="print"></param> 打印方式
        /// <param name="filedir"></param> 文件地址
        public void FDEmailRegionSyn(string ICT_DN, string PO_NO, string REGION, string stCountryCodePar, bool print, string filedir)
        {
            List<string> filePath = new List<string>();
            #region PAC
            if (REGION.Equals("PAC"))
            {
                try
                {
                    FDAPACShippingInoviceForm InoviceForm = new FDAPACShippingInoviceForm(ICT_DN, filedir, print);
                    string diskPath = InoviceForm.diskCompletePath;
                    filePath.Add(diskPath);
                }
                catch
                {
                    //防止程式报错无法继续执行下一个，所有无返回值。
                }
                Thread.Sleep(2000);
                try
                {
                    CRfrom.FDAPACPackingListForm ListForm = new FDAPACPackingListForm(ICT_DN, filedir, print);
                    string diskPath = ListForm.diskCompletePath;
                    filePath.Add(diskPath);
                }
                catch
                {
                }
            }
            #endregion 
            #region EMEIA
            else if (REGION.Equals("EMEIA"))
            {
                try
                {
                    EMEIAInvoiceLayoutForm EMEIAInvoiceLayoutForm = new EMEIAInvoiceLayoutForm(ICT_DN, filedir, print, stCountryCodePar);
                    string diskPath = EMEIAInvoiceLayoutForm.diskCompletePath;
                    filePath.Add(diskPath);
                }
                catch
                {
                }
                Thread.Sleep(2000);
                try
                {
                    EMEIAPLLayoutForm EMEIAPLLayoutForm = new EMEIAPLLayoutForm(ICT_DN, filedir, print, stCountryCodePar);
                    string diskPath = EMEIAPLLayoutForm.diskCompletePath;
                    filePath.Add(diskPath);
                }
                catch
                {
                }
            }
            #endregion
            #region AMR
            else if (REGION.Equals("AMR"))
            {
                try
                {
                    if (stCountryCodePar.Equals("CA"))
                    {
                        FDConsolPLAMRLayoutCANForm FDConsolPLAMRLayoutCANForm = new FDConsolPLAMRLayoutCANForm(ICT_DN, print);
                        string diskPath = FDConsolPLAMRLayoutCANForm.diskCompletePath;
                        filePath.Add(diskPath);
                    }
                    else if (stCountryCodePar.Equals("MX"))
                    {
                        FDConsolPLAMRLayoutMEXForm FDConsolPLAMRLayoutMEXForm = new FDConsolPLAMRLayoutMEXForm(ICT_DN, print);
                        string diskPath = FDConsolPLAMRLayoutMEXForm.diskCompletePath;
                        filePath.Add(diskPath);
                    }
                    else if (stCountryCodePar.Equals("BR"))
                    {
                        FDConsolPLAMRLayoutBZForm FDConsolPLAMRLayoutBZForm = new FDConsolPLAMRLayoutBZForm(ICT_DN, print);
                        string diskPath = FDConsolPLAMRLayoutBZForm.diskCompletePath;
                        filePath.Add(diskPath);
                    }
                    else if (stCountryCodePar.Equals("CL"))
                    {
                        FDConsolPLAMRLayoutCLForm FDConsolPLAMRLayoutCLForm = new FDConsolPLAMRLayoutCLForm(ICT_DN, print);
                        string diskPath = FDConsolPLAMRLayoutCLForm.diskCompletePath;
                        filePath.Add(diskPath);
                    }
                    else if (stCountryCodePar.Equals("CO"))
                    {
                        FDConsolPLAMRLayoutCOForm FDConsolPLAMRLayoutCOForm = new FDConsolPLAMRLayoutCOForm(ICT_DN, print);
                        string diskPath = FDConsolPLAMRLayoutCOForm.diskCompletePath;
                        filePath.Add(diskPath);
                    }
                    else
                    {
                        FDConsolPLAMRLayoutALLUSAndPEForm FDConsolPLAMRLayoutALLUSAndPEForm = new FDConsolPLAMRLayoutALLUSAndPEForm(ICT_DN, print);
                        string diskPath = FDConsolPLAMRLayoutALLUSAndPEForm.diskCompletePath;
                        filePath.Add(diskPath);
                    }
                }
                catch
                {
                }
                Thread.Sleep(2000);
                try
                {
                    AMRFDCCILayoutForm AMRFDCCILayoutForm = new AMRFDCCILayoutForm(ICT_DN, filedir, print);
                    string diskPath = AMRFDCCILayoutForm.diskCompletePath;
                    filePath.Add(diskPath);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
            #endregion
            Thread.Sleep(10000);
            if (print == false && filePath.Count >= 2)
            {
                SendEmail.Send(filePath, ICT_DN, "FD");
            }
            else
            {
                //MessageBox.Show("CCI或者CPL缺失，发送不成功！");
            }
        }
        /// <summary>
        /// 线程异步执行
        /// </summary>
        public void FDRegion(string ICT_DN, string PO_NO, string REGION, string stCountryCodePar, bool print, string filedir)
        {
            Thread aa = new Thread(delegate () { FDEmailRegionSyn(ICT_DN, PO_NO, REGION, stCountryCodePar, print, filedir); });
            aa.Start();

        }
        public void EmailCRreport(string shipTypePar, string regionPar, string stCountryCodePar, Dictionary<string, string> dic, bool print)
        {
            Thread aa = new Thread(delegate () { EmailCRreportSyn(shipTypePar, regionPar, stCountryCodePar, dic, print); });
            aa.Start();
        }


    }
}

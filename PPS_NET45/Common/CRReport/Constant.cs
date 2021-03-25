/*
 * Data:2018-07-01
 * User:Admin
 * Content:调用报表的路径
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRReport
{
    class Constant
    {
        //生产环境BASE路径
        public static string BASE_RUL = AppDomain.CurrentDomain.BaseDirectory + "rpt\\";
        //生产环境BASE路径
        public static string A4ConsumerPackingList_613_664_URL = BASE_RUL + "6136647A4ConsumerPackingList.rpt";
        public static string ConsumerPackingList_EMEA_H_URL = BASE_RUL + "ConsumerPackingList_EMEA_H.rpt";
        public static string ConsumerPacklingList_EMEA_C_URL = BASE_RUL + "ConsumerPacklingList_EMEA_C.rpt";
        public static string HandoverManifest_URL = BASE_RUL + "HandoverManifestNEW.rpt";
        public static string HandoverManifest_URL2 = BASE_RUL + "HandoverManifestNEW2.rpt";
        public static string HandoverManifestDirectShip_URL = BASE_RUL + "HandoverManifestDirectShip.rpt";
        public static string HandoverManifestDS_URL = BASE_RUL + "HandoverManifestDS.rpt";
        public static string CarManifest_URL = BASE_RUL + "DriverBillList.rpt";
        public static string PalletLoadingSheet_URL = BASE_RUL + "PalletLoadingSheet.rpt";
        public static string PalletLoadingSheet_URL_ByGW = BASE_RUL + "PalletLoadingSheet_songti.rpt";
        public static string PalletLoadingSheetSAWB_URL = BASE_RUL + "PalletLoadingSheetSAWB.rpt";
        public static string PalletLoadingSheetSAWB_URL_ByGW = BASE_RUL + "PalletLoadingSheetSAWB_songti.rpt";
        public static string INVOICE_URL = BASE_RUL + "INVOICE.rpt";
        public static string Ltr_PkList_MEX_URL = BASE_RUL + "Ltr_PkList_MEX.rpt";
        public static string Ltr_PkList_MEX_URL_ByGW = BASE_RUL + "Ltr_PkList_MEX_songti.rpt";
        public static string P1ConsumerPackingListChina_URL = BASE_RUL + "P1ConsumerPackingListChina.rpt";
        public static string P1ConsumerPackingListChina_URL_ByGW = BASE_RUL + "P1ConsumerPackingListChina_songti.rpt";
        public static string P1ConsumerPackingListGlobal_URL = BASE_RUL + "P1ConsumerPackingListGlobal.rpt";
        public static string P1ConsumerPackingListGlobal_URL_ByGW = BASE_RUL + "P1ConsumerPackingListGlobal_songti.rpt";
        public static string P1ConsumerPackingListHongKong_URL = BASE_RUL + "P1ConsumerPackingListHK.rpt";
        public static string P1ConsumerPackingListHongKong_URL_ByGW = BASE_RUL + "P1ConsumerPackingListHK_songti.rpt";
        public static string P1ConsumerPackingListJapan_URL = BASE_RUL + "P1ConsumerPackingListJP.rpt";
        public static string P1ConsumerPackingListJapan_URL_ByGW = BASE_RUL + "P1ConsumerPackingListJP_songti.rpt";
        public static string P1ConsumerPackingListTW_URL = BASE_RUL + "P1ConsumerPackingListTW.rpt";
        public static string P1ConsumerPackingListTW_URL_ByGW = BASE_RUL + "P1ConsumerPackingListTW_songti.rpt";
        public static string packingList_URL = BASE_RUL + "PackingList.rpt";
        public static string EMEIA_SIGNLE_DeliveryNote_URL = BASE_RUL + "EMEIA_SIGNLE_DeliveryNote_LayOut.rpt";
        public static string EMEIA_SIGNLE_DeliveryNote_URL_ByGW = BASE_RUL + "EMEIA_SIGNLE_DeliveryNote_LayOut_songti.rpt";
        public static string EMEIA_MULTI_DeliveryNote_URL = BASE_RUL + "EMEIA_MULTI_DeliveryNote_LayOut.rpt";
        public static string EMEIA_MULTI_DeliveryNote_URL_ByGW = BASE_RUL + "EMEIA_MULTI_DeliveryNote_LayOut_songti.rpt";
        public static string EMEIA_BUY_DeliveryNote_URL = BASE_RUL + "EMEIA_BUY_MULTI_DeliveryNote_LayOut.rpt";
        public static string EMEIA_BUY_DeliveryNote_URL_ByGW = BASE_RUL + "EMEIA_BUY_MULTI_DeliveryNote_LayOut_songti.rpt";
        public static string EMEIA_SELL_DeliveryNote_URL = BASE_RUL + "EMEIA_SELL_DeliveryNote_LayOut.rpt";
        public static string EMEIA_SELL_DeliveryNote_URL_ByGW = BASE_RUL + "EMEIA_SELL_DeliveryNote_LayOut_songti.rpt";
        public static string DeliveryNoteG_URL = BASE_RUL + "DeliveryNoteG.rpt";
        public static string DeliveryNoteG_HEADER_URL = BASE_RUL + "DeliveryNoteG_HEADER.rpt";
        public static string DeliveryNoteC_URL = BASE_RUL + "DeliveryNoteC.rpt";
        public static string PAC_DeliveryNote_URL = BASE_RUL + "PAC_DeliveryNote.rpt";
        public static string PAC_DeliveryNote_URL2 = BASE_RUL + "PAC_DeliveryNote2.rpt";
        public static string PAC_DeliveryNote_URL_ByGW = BASE_RUL + "PAC_DeliveryNote_songti.rpt";
        public static string PAC_DeliveryNote_URL2_ByGW = BASE_RUL + "PAC_DeliveryNote2_songti.rpt";
        public static string AmrDsShippingLabel940_URL = BASE_RUL + "AmrDsShippingLabel940.rpt";
        public static string ConsumerPackingList_EMEA_G_URL = BASE_RUL + "ConsumerPackingList_EMEA_G.rpt";
        public static string ConsumerPackingList_EMEA_G_URL_ByGW = BASE_RUL + "ConsumerPackingList_EMEA_G_songti.rpt";
        public static string ConsumerPkListBilingualCanada_URL = BASE_RUL + "ConsumerPkListBilingualCanadaCR.rpt";
        public static string ConsumerPkListBilingualCanada_URL_ByGW = BASE_RUL + "ConsumerPkListBilingualCanadaCR_songti.rpt";
        public static string Ltr_PkList_bilingual613_6685_URL = BASE_RUL + "Ltr_PkList_bilingual613_6685.rpt";
        public static string ThaiLandPK_URL = BASE_RUL + "ThaiLandPK.rpt";
        public static string ThaiLandPK_URL_ByGW = BASE_RUL + "ThaiLandPK_songti.rpt";
        public static string ShipLabelSTO_URL = BASE_RUL + "STO_label.rpt";
        public static string P2ConsolPackingList_URL = BASE_RUL + "P2ConsolPackingList.rpt";
        public static string MITShipLabel_URL = BASE_RUL + "MIT_label.rpt";
        public static string KoreaPackingList_URL = BASE_RUL + "P1ConsumerPackingListKA.rpt";
        public static string KoreaPackingList_URL_ByGW = BASE_RUL + "P1ConsumerPackingListKA_songti.rpt";
        public static string ALLETERPK_URL = BASE_RUL + "A_LetterPackingList_f.rpt";
        public static string DirectShipLabel_URL = BASE_RUL + "DirectShip_label.rpt";
        public static string ConsignedShipLabel_URL = BASE_RUL + "HUBConsigned_label.rpt";
        public static string INDIAShipLabel_URL = BASE_RUL + "INDIA_label.rpt";
        public static string MexicoChannelPK_URL = BASE_RUL + "ChannelAMRLayoutAllMX.rpt";
        public static string MexicoChannelPK_URL_ByGW = BASE_RUL + "ChannelAMRLayoutAllMX_songti.rpt";
        public static string ColumbiaChannelPK_URL = BASE_RUL + "Channel_AMR_Layout_All_Columbia.rpt";
        public static string ColumbiaChannelPK_URL_ByGW = BASE_RUL + "Channel_AMR_Layout_All_Columbia_songti.rpt";
        public static string CHANNELBRAZIL_URL = BASE_RUL + "Channel_AMR_LayoutBrazil.rpt";
        public static string CHANNELBRAZIL_URL_ByGW = BASE_RUL + "Channel_AMR_LayoutBrazil_songti.rpt";
        public static string PeruChannelPK_URL = BASE_RUL + "Channel_AMR_Layout_All_Peru.rpt";
        public static string PeruChannelPK_URL_ByGW = BASE_RUL + "Channel_AMR_Layout_All_Peru_songti.rpt";
        public static string ConsolAMRBR_URL = BASE_RUL + "Consol_PL_AMR_Layout_ALL_Brazil.rpt";
        public static string ConsolAMRCAN_URL = BASE_RUL + "Consol_PL_AMR_Layout_ALL_CAN.rpt";
        public static string ConsolAMRChile_URL = BASE_RUL + "Consol_PL_AMR_Layout_ALL_Chilie.rpt";
        public static string CHANNELCHILE_URL = BASE_RUL + "Channel_AMR_Layout_All_Chilie.rpt";
        public static string CHANNELCHILE_URL_ByGW = BASE_RUL + "Channel_AMR_Layout_All_Chilie_songti.rpt";
        public static string ConsolAMRCO_URL = BASE_RUL + "CCIColumbiaRPT.rpt";
        public static string ConsolAMRMexico_URL = BASE_RUL + "Consol_PL_AMR_Layout_ALL_Mexico.rpt";
        public static string ConsolAMRUSPR_URL = BASE_RUL + "Consol_PL_AMR_Layout_ALL_US_P.rpt";
        public static string EMEADirectShip_URL = BASE_RUL + "EMEA_OEM_C_PL_ALL_Direct_Ship.rpt";
        public static string EMEASTO_URL = BASE_RUL + "EMEA_OEM_C_PL_ALL_STO.rpt";
        public static string EMEAINDIA_URL = BASE_RUL + "EMEA_OEM_C_PL_ALL_India.rpt";
        public static string EMEAMIT_URL = BASE_RUL + "EMEA_OEM_C_PL_ALL_MIT.rpt";
        public static string EMEATurkey_URL = BASE_RUL + "EMEA_OEM_C_PL_ALL_Turkey.rpt";
        public static string EMEARussia_URL = BASE_RUL + "EMEA_OEM_C_PL_ALL_Russia.rpt";
        public static string ChannePKCAN_URL = BASE_RUL + "Channel_AMR_Layout_All_CAN.rpt";
        public static string ChannePKCAN_URL_ByGW = BASE_RUL + "Channel_AMR_Layout_All_CAN_songti.rpt";
        public static string CHANNELUS_URL = BASE_RUL + "Channel_AMR_Layout_All_US.rpt";
        public static string CHANNELUS_URL_ByGW = BASE_RUL + "Channel_AMR_Layout_All_US_songti.rpt";
        public static string CCILayoutUSAndPeru_URL = BASE_RUL + "CCIUSAndPeru.rpt";
        public static string CCILayoutUSAndPeruSerial_URL = BASE_RUL + "CCIUSAndPeruSeriallRPT.rpt";
        public static string CCILayoutCAN_URL = BASE_RUL + "CCICAN.rpt";
        public static string CCILayoutCANSerial_URL = BASE_RUL + "CCICANSeriallRPT.rpt";
        public static string CCILayoutMexico_URL = BASE_RUL + "CCIMexicoRPT.rpt";
        public static string CCILayoutMexicoSerial_URL = BASE_RUL + "CCIMexicoSerialRPT.rpt";
        public static string CCILayoutBZ_URL = BASE_RUL + "CCIBrazilRPT.rpt";
        public static string CCILayoutBZSERIAL_URL = BASE_RUL + "CCIBrazilSerialRPT.rpt";
        public static string CCILayoutChile_URL = BASE_RUL + "CCIChileRPT.rpt";
        public static string CCILayoutCO_URL = BASE_RUL + "CCIColumbiaRPT.rpt";
        public static string CCILayoutCOSERIAL_URL = BASE_RUL + "CCIColumbiaSerialRPT.rpt";
        //fd
        public static string FDAPACPackingList_URL = BASE_RUL + "FDAPACPackingListRPT.rpt";
        public static string FDAPACShippingInvoice_URL = BASE_RUL + "FDAPACShippingInoviceRPT.rpt";
        public static string AMRFDCCILayout_URL = BASE_RUL + "AMRCCI.rpt";
        public static string FDAMRCCILayout_URL = BASE_RUL + "FDCCIRPT.rpt";
        public static string EMEACommercialCzech_URL = BASE_RUL + "EMEA_OEM_L_ALL_CZECH.rpt";
        public static string EMEACommercialDirectShip_URL = BASE_RUL + "EMEA_OEM_L_ALL_Direct_Ship.rpt";
        public static string EMEACommercialHub_URL = BASE_RUL + "EMEAOEMLALLHubRPT.rpt";

        public static string EMEACommercialItaly_URL = BASE_RUL + "EMEAOEMLALLItalyRPT.rpt";
        public static string EMEACommercialMIT_URL = BASE_RUL + "EMEA_OEM_L_ALL_MIT.rpt";
        public static string EMEACommercialNetherlands_URL = BASE_RUL + "EMEAOEMLALLNetherlandsRPT.rpt";
        public static string EMEACommercialSTO_URL = BASE_RUL + "EMEAOEMLALLSTORPT.rpt";
        public static string EMEACommercialTurkey_URL = BASE_RUL + "EMEAOEMCommercialinvoiceTurkey.rpt";
        public static string EMEACommercialUAEFTZ_URL = BASE_RUL + "EMEAOEMLALLUAEFTZRPT.rpt";

        public static string EMEACommercialUAE_URL = BASE_RUL + "EMEAOEMLALLUAERPT.rpt";
        public static string EMEACommercialUK_URL = BASE_RUL + "EMEAOEMLALLUKRPT.rpt";
        public static string EMEIAInvoiceLayout_URL = BASE_RUL + "EMEIAInvocieLayout.rpt";
        public static string EMEIAInvoiceLayoutAE_URL = BASE_RUL + "AEEMEIAInvocieLayout.rpt";
        public static string FDHubPKGlobal_URL = BASE_RUL + "Hub_850_Global_Layout_All.rpt";
        public static string FDHubPKGlobal_URL_ByGW = BASE_RUL + "Hub_850_Global_Layout_All_songti.rpt";
        public static string EMEIAPLLayout_URL = BASE_RUL + "EMEIAPLLayoutRPT.rpt";
        public static string EMEIAPLLayoutAE_URL = BASE_RUL + "EMEIAPLLayoutAERPT.rpt";
        public static string EMEIAPLLayoutIT_URL = BASE_RUL + "EMEIAPLLayoutITRPT.rpt";

        public static string FDAMRCPLUSAndPeru_URL = BASE_RUL + "FDConsolPLAMRLayoutALLUSPE.rpt";
        public static string FDAMRCPLBZ_URL = BASE_RUL + "FDConsolPLAMRLayoutBrazil.rpt";
        public static string FDAMRCPLChileURL = BASE_RUL + "FDConsolPLAMRLayoutChile.rpt";
        public static string FDAMRCPLColumbia_URL = BASE_RUL + "FDConsolPLAMRLayoutCO.rpt";

        public static string DSTPI_URL = BASE_RUL + "TPI-format.rpt";
        public static string DSShippingInvoice_URL = BASE_RUL + "ShippingInvoiceRpt.rpt";

        public static string NewDeliveryNote_URL = BASE_RUL + "NewDeliveryNoteGRPT.rpt";


    }
}

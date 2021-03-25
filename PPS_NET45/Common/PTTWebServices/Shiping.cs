using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using PTTWebServices.Models;

namespace PTTWebServices
{
    public class Shipping
    {
        /// <summary>
        /// 合并HUB  SHIP出货单的xml文件
        /// </summary>
        /// <param name="U_Doc"></param>
        /// <returns></returns>
        public ResponseModel HUnitShip(U_Doc U_Doc)
        {
            var TemplateText_OgaFiles = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/Template/UnitShip_OgaFile.xml");

            var OgaFilesXmlSum = "";
            //为xml文件oga进行赋值
            foreach (var item in U_Doc.U_OgaFiles)
            {
                var OgaFilesXml = TemplateText_OgaFiles;
                OgaFilesXml = OgaFilesXml.Replace("{oga01}", item.oga01);
                OgaFilesXmlSum = OgaFilesXmlSum + OgaFilesXml;
            }

            var TemplateText_OgcFile = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/Template/UnitShip_OgcFile.xml");
            var OgcFileXmlSum = "";
            //为xml文件ogc进行赋值
            foreach (var item in U_Doc.U_OgcFiles)
            {
                var OgcFileXml = TemplateText_OgcFile;
                OgcFileXml = OgcFileXml.Replace("{ogc03}", item.ogc03)
                                       .Replace("{ogc09}", item.ogc09)
                                       .Replace("{ogc091}",item.ogc091)
                                       .Replace("{ogc12}", item.ogc12)
                                       .Replace("{ogc15}", item.ogc15);
                OgcFileXmlSum = OgcFileXmlSum + OgcFileXml;
            }

            var TemplateText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/Template/UnitShip.xml");
            //对xml文件UnitShip进行赋值
            var XmlStr = TemplateText.Replace("{user}", U_Doc.User.Acount)
                        .Replace("{password}", U_Doc.User.PassWord)
                        .Replace("{application}", U_Doc.User.Application)
                        .Replace("{source}", U_Doc.User.IP)
                        .Replace("{Organization}", U_Doc.User.Organization)
                        .Replace("{Profit}", U_Doc.User.Profit)
                        .Replace("{language}", U_Doc.User.language)
                        .Replace("{ASNtype}", U_Doc.ASNtype)
                        .Replace("{oea01}", U_Doc.oea01)
                        .Replace("{ogc_file}", OgcFileXmlSum);

           TTWebServiceFrom.TIPTOPServiceGateWayPortTypeClient ef = new TTWebServiceFrom.TIPTOPServiceGateWayPortTypeClient();
            var r = ef.CreateASN(XmlStr);
            return ProcReturnXml(r);

        }
        /// <summary>
        /// 合并DIRECT  SHIP出货单出货单的xml文件
        /// </summary>
        /// <param name="U_Doc"></param>
        /// <returns></returns>
        public ResponseModel DUnitShip(DU_Doc DU_Doc)
        {
            var TemplateText_OgaFiles = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/Template/UnitShip_OgaFile.xml");
            //对xml文件oga进行赋值
            var OgaFilesXmlSum = "";

            foreach (var item in DU_Doc.OgaFiles)
            {
                var OgaFilesXml = TemplateText_OgaFiles;
                OgaFilesXml = OgaFilesXml.Replace("{oga01}", item.oga01);
                OgaFilesXmlSum = OgaFilesXmlSum + OgaFilesXml;
            }

            var TemplateText_OgbFile = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/Template/UnitShip_OgbFile.xml");
            //对xml文件ogb进行赋值
            var OgbFileXmlSum = "";
            foreach (var item in DU_Doc.OgbFiles)
            {
                var OgbFileXml = TemplateText_OgbFile;
                OgbFileXml = OgbFileXml.Replace("{ogb03}", item.ogb03)
                                       .Replace("{ogb12}", item.ogb12);
                OgbFileXmlSum = OgbFileXmlSum + OgbFileXml;
            }

            //对xml文件DUnitShip进行赋值
            var TemplateText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/Template/DUnitShip.xml");
            var XmlStr = TemplateText.Replace("{ASNtype}",DU_Doc.ASNtype)
                        .Replace("{EDItype}", DU_Doc.EDItype)
                        .Replace("{SHIPMENT_ID}", DU_Doc.SHIPMENT_ID)
                        .Replace("{user}", DU_Doc.User.Acount)
                        .Replace("{password}", DU_Doc.User.PassWord)
                        .Replace("{application}", DU_Doc.User.Application)
                        .Replace("{source}", DU_Doc.User.IP)
                        .Replace("{Organization}", DU_Doc.User.Organization)
                        .Replace("{Profit}", DU_Doc.User.Profit)
                        .Replace("{language}", DU_Doc.User.language)
                        .Replace("{oea01}", OgaFilesXmlSum)
                        .Replace("{ogb_file}", OgbFileXmlSum);

            TTWebServiceFrom.TIPTOPServiceGateWayPortTypeClient ef = new TTWebServiceFrom.TIPTOPServiceGateWayPortTypeClient();
            var r = ef.CreateASN(XmlStr);
            return ProcReturnXml(r);

        }
        /// <summary>
        /// 对Moreship模板文件进行赋值
        /// </summary>
        /// <param name="Docs"></param>
        /// <returns></returns>
        public ResponseModel MoreShip(M_Doc Docs)
        {

            var TemplateText_Doc= System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/Template/MoreShip_Doc.xml");
            var TemplateText_OgcFile = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/Template/MoreShip_OgcFile.xml");
            var DocXmlSum = "";
            var i = 1;
            //对xml文件ogc进行赋值
            foreach (var item in Docs.M_Ships)
            {
                var OgcFileXmlSum = "";
                foreach (var ogc in item.OgcFiles)
                {
                    var OgcFileXml = TemplateText_OgcFile;
                    OgcFileXml = OgcFileXml.Replace("{ogc03}", ogc.ogc03)
                                           .Replace("{ogc09}", ogc.ogc09)
                                           .Replace("{ogc091}", ogc.ogc091)
                                           .Replace("{ogc12}", ogc.ogc12)
                                           .Replace("{ogc15}", ogc.ogc15);
                    OgcFileXmlSum = OgcFileXmlSum + OgcFileXml;
                }

                var DocXml = TemplateText_Doc;
                DocXml = DocXml.Replace("{Id}", i.ToString())
                               .Replace("{oga01}", item.oga01)
                               .Replace("{ogc_file}",OgcFileXmlSum);

                DocXmlSum = DocXmlSum + DocXml;
                i += 1;

            }
            var TemplateText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/Template/MoreShip.xml");
            //对xml文件中的Acces和Parameter进行赋值
            var XmlStr = TemplateText.Replace("{ASNtype}",Docs.ASNtype)
                        .Replace("{user}", Docs.User.Acount)
                        .Replace("{password}", Docs.User.PassWord)
                        .Replace("{application}", Docs.User.Application)
                        .Replace("{source}", Docs.User.IP)
                        .Replace("{Organization}", Docs.User.Organization)
                        .Replace("{Profit}", Docs.User.Profit)
                        .Replace("{language}", Docs.User.language)     
                        .Replace("{Document}", DocXmlSum);

            TTWebServiceFrom.TIPTOPServiceGateWayPortTypeClient ef = new TTWebServiceFrom.TIPTOPServiceGateWayPortTypeClient();
            var r = ef.BatchPostASN(XmlStr);
            return ProcReturnXml(r);

        }
        public ResponseModel DMoreShip(DM_Doc Docs)
        {

            var TemplateText_Doc = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/Template/DMoreShip_Doc.xml");
            var TemplateText_OgbFile = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/Template/MoreShip_OgbFile.xml");
            var DocXmlSum = "";
            var i = 1;
            //对xml文件ogb进行赋值
            foreach (var item in Docs.DM_Ships)
            {
                var OgbFileXmlSum = "";
                foreach (var ogb in item.OgbFiles)
                {
                    var OgbFileXml = TemplateText_OgbFile;
                    OgbFileXml = OgbFileXml.Replace("{ogb03}", ogb.ogb03)
                                           .Replace("{ogb12}", ogb.ogb12);
                    OgbFileXmlSum = OgbFileXmlSum + OgbFileXml;
                }

                var DocXml = TemplateText_Doc;
                DocXml = DocXml.Replace("{Id}", i.ToString())
                               .Replace("{oga01}", item.oga01)
                               .Replace("{ogb_file}", OgbFileXmlSum);

                DocXmlSum = DocXmlSum + DocXml;
                i += 1;

            }

            var TemplateText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/Template/DMoreShip.xml");
            //对xml文件中的Acces和Parameter进行赋值
            var XmlStr = TemplateText.Replace("{ASNtype}", Docs.ASNtype)
                        .Replace("{EDItype}", Docs.EDItype)
                        .Replace("{SHIPMENT_ID}", Docs.SHIPMENT_ID)
                        .Replace("{user}", Docs.User.Acount)
                        .Replace("{password}", Docs.User.PassWord)
                        .Replace("{application}", Docs.User.Application)
                        .Replace("{source}", Docs.User.IP)
                        .Replace("{Organization}", Docs.User.Organization)
                        .Replace("{Profit}", Docs.User.Profit)
                        .Replace("{language}", Docs.User.language)
                        .Replace("{Document}", DocXmlSum);

            TTWebServiceFrom.TIPTOPServiceGateWayPortTypeClient ef = new TTWebServiceFrom.TIPTOPServiceGateWayPortTypeClient();
            var r = ef.BatchPostASN(XmlStr);
            return ProcReturnXml(r);

        }

        private ResponseModel ProcReturnXml(string XmlText)
        {
            ResponseModel respons = new ResponseModel();

            XmlDocument myXmlDoc = new XmlDocument();
            myXmlDoc.LoadXml(XmlText);
            XmlNode statusNode = myXmlDoc.SelectSingleNode("Response/Execution/Status");
            //通过Attributes获得属性名为name的属性
            if (statusNode.Attributes["code"].Value == "0")
            {
                respons.YN = true;
                respons.FromNo = statusNode.Attributes["description"].Value;
            }
            else if (statusNode.Attributes["code"].Value == "-1")
            {
                respons.YN = false;
                respons.errDesc = statusNode.Attributes["description"].Value;
            }
            else
            {
                XmlNode ErrorNode = myXmlDoc.SelectSingleNode("Response/Execution/Error");
                respons.YN = false;
                respons.type = ErrorNode.Attributes["type"].Value;
                respons.errcode = ErrorNode.Attributes["errcode"].Value;
                respons.errDesc = ErrorNode.Attributes["description"].Value;
            }
            return respons;
        }
    }
}

using CarrierWCF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierWCF.Model
{
    //v2.0 修改格式
    #region request model
    /// <summary>
    /// Ship Request Model
    /// </summary>
    public class ShipModel : Credentials
    {
        /// <summary>
        /// 固定值：1
        /// </summary>
        public string Print { get => "1"; }
        /// <summary>
        /// fasle
        /// </summary>
        public string ShipWithoutTransaction { get; set; }
        /// <summary>
        /// ShipmentRequest Parameters
        /// </summary>
        public ShipmentRequest ShipmentRequest { get; set; }
    }
    /// <summary>
    /// 出货请求类
    /// </summary>
    public class ShipmentRequest
    {
        public PackageDefaults PackageDefaults { get; set; }
        public Packages[] Packages { get; set; }
    }
    //-------------------------------待取消-------------------------------------
    public class ImporterOfRecord
    {
        public string Account { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string Company { get; set; }
        public string Contact { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string StateProvince { get; set; }
        public string TaxId { get; set; }
    }
    public class ReturnAddress
    {
        public string Address1 { get; set; }
        public string City { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string StateProvince { get; set; }
    }
    public class ThirdPartyBillingAddress
    {
        public string Account { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Company { get; set; }
        public string Contact { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string StateProvince { get; set; }
    }
    //--------------------------------------------------------------------------
    /// <summary>
    /// 包裹默认信息
    /// </summary>
    public class PackageDefaults
    {
        public Service Service { get; set; }

        //public string CarrierInstructions { get; set; }

        public string CommercialInvoiceMethod { get; set; }//新增
        public Consignee Consignee { get; set; }

        //public string WorldEaseFlag { set; get; }

        //待取消-------------------------------------------------
        //comment by wenxing 2021-3-21
        //public ImporterOfRecord ImporterOfRecord { get; set; }
        //public ReturnAddress ReturnAddress { get; set; }
        //public ThirdPartyBillingAddress ThirdPartyBillingAddress { get; set; }
        //待取消-------------------------------------------------
        /// <summary>
        /// 项目
        /// </summary>
        public string Terms { get; set; }
        /// <summary>
        /// 第三方账单
        /// </summary>
        //public string ThirdPartyBilling { get; set; }
        /// <summary>
        /// 打包
        /// </summary>
        public string Packaging { get; set; }
        public Shipdate Shipdate { get; set; }
        /// <summary>
        /// 发货人
        /// </summary>
        public string Shipper { get; set; }
        /// <summary>
        /// 发货人参考
        /// </summary>
        public string ShipperReference { get; set; }
        /// <summary>
        /// 收货人参考
        /// </summary>
        public string ConsigneeReference { get; set; }
    }



    public class Service
    {
        /// <summary>
        /// 苹果服务等级
        /// </summary>
        public string Symbol { get; set; }
    }
    /// <summary>
    /// 收货人信息
    /// </summary>
    public class Consignee
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Address1 { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string StateProvince { get; set; }
    }
    /// <summary>
    /// 出货日期
    /// </summary>
    public class Shipdate
    {
        /// <summary>
        /// 年
        /// </summary>
        public string Year { get; set; }
        /// <summary>
        /// 月
        /// </summary>
        public string Month { get; set; }
        /// <summary>
        /// 日
        /// </summary>
        public string Day { get; set; }
    }

    /// <summary>
    /// 包裹详细信息
    /// </summary>
    public class Packages
    {
        /// <summary>
        /// HAWB
        /// </summary>
        public string WorldEaseCode { get; set; }
        /// <summary>
        /// 标识
        /// </summary>
        public string WorldEaseFlag { get; set; }
        public string CarrierInstructions { get; set; }

        /// <summary>
        /// 产品描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// tracking no
        /// </summary>
        public string TrackingNumber { get; set; }

        public CommodityContents[] CommodityContents { get; set; }
        //public Dimensions Dimensions { get; set; }
        /// <summary>
        /// 其他参考1:CPN
        /// </summary>
        public string MiscReference1 { get; set; }
        /// <summary>
        /// 其他参考2:SSCC18
        /// </summary>
        public string MiscReference2 { get; set; }
        /// <summary>
        /// 其他参考3:SAWB
        /// </summary>
        public string MiscReference3 { get; set; }
        public string MiscReference4 { get; set; }
        public string MiscReference5 { get; set; }
        public string MiscReference6 { get; set; }
        public string MiscReference7 { get; set; }
        public string MiscReference8 { get; set; }
        //public string MiscReference9 { get; set; }
        public string MiscReference10 { get; set; } //all gross weight of DN
        public string MiscReference11 { get; set; } //carton of DN
        public string MiscReference12 { get; set; } //all carton of DN
        public string MiscReference14 { get; set; } //sawb
        public string MiscReference15 { get; set; } //hawb

       
        /// <summary>
        /// 用户数据 Package Sequence Number
        /// </summary>
        //public string UserData1 { get; set; }
        ///// <summary>
        ///// 用户数据Package Total Number
        ///// </summary>
        //public string UserData2 { get; set; }
        ///// <summary>
        ///// 用户数据 Total Shipment Weight
        ///// </summary>
        //public string UserData3 { get; set; }

        public Weight Weight { get; set; }
    }
    /// <summary>
    /// 商品内容
    /// </summary>
    public class CommodityContents
    {
        /// <summary>
        /// 商品描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 产地
        /// </summary>
        public string OriginCountry { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string Quantity { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string QuantityUnitMeasure { get; set; }
        public UnitValue UnitValue { get; set; }
        public UnitWeight UnitWeight { get; set; }
    }
    /// <summary>
    /// 单片信息
    /// </summary>
    public class UnitValue
    {
        /// <summary>
        /// 单片数量
        /// </summary>
        public string Amount { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }
    }
    /// <summary>
    /// 单片重量
    /// </summary>
    public class UnitWeight
    {
        /// <summary>
        /// 数量
        /// </summary>
        public string Amount { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Units { get; set; }
    }
    /// <summary>
    /// 尺寸
    /// </summary>
    public class Dimensions
    {
        /// <summary>
        /// 高
        /// </summary>
        public string Height { get; set; }
        /// <summary>
        /// 长
        /// </summary>
        public string Length { get; set; }
        /// <summary>
        /// 宽
        /// </summary>
        public string Width { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
    }
    /// <summary>
    /// 重量
    /// </summary>
    public class Weight : UnitWeight { }
    #endregion

}

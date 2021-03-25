using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierWCF.Models
{
    public class VoidResponseModel
    {
        public Int64 ErrorCode { get; set; }
        public Packages[] Packages { get; set; }
    }

    public class Packages
    {
        public Int64 ErrorCode { get; set; }
        public string ArriveTime { get; set; }
        public Carrier Carrier { get; set; }
        public string CompanyId { get; set; }
        public Dimensions Dimensions { get; set; }
        public Int64 GlobalBundleId { get; set; }
        public Int64 GlobalMsn { get; set; }
        public string MachineName { get; set; }
        public Int64 ManifestId { get; set; }
        public RatedService RatedService { get; set; }
        public Service Service { get; set; }
        public Int64 ShipId { get; set; }
        public Int64 TimeInTransitDays { get; set; }
        public string TransactionId { get; set; }
        public bool Voided { get; set; }
        public string Description { get; set; }
        public string BarCode { get; set; }
        public BaseCharge BaseCharge { get; set; }
        public Int64 BundleId { get; set; }
        public Int64[] BundleIdList { get; set; }
        public CommodityContents[] CommodityContents { get; set; }
        public Consignee Consignee { get; set; }
        public string ConsigneeReference { get; set; }
        public ImporterOfRecord ImporterOfRecord { get; set; }
        public string[] Maxicode { get; set; }
        public string MiscReference1 { get; set; }
        public string MiscReference2 { get; set; }
        public Int64 Msn { get; set; }
        public Int64 NofnSequenceBundle { get; set; }
        public Int64 PackageListId { get; set; }
        public string Packaging { get; set; }
        public RatedWeight RatedWeight { get; set; }
        public ReturnAddress ReturnAddress { get; set; }
        public string RoutingCode { get; set; }
        public Shipdate Shipdate { get; set; }
        public string Shipper { get; set; }
        public string ShipperReference { get; set; }
        public string Terms { get; set; }
        public bool ThirdPartyBilling { get; set; }
        public ThirdPartyBillingAddress ThirdPartyBillingAddress { get; set; }
        public string TimeInTransit { get; set; }
        public Total Total { get; set; }
        public string TrackingNumber { get; set; }
        public string WaybillBolNumber { get; set; }
        public Weight Weight { get; set; }
        public bool WorldEaseFlag { get; set; }
        public Int64 WorldEaseId { get; set; }
        public string Zone { get; set; }
    }

    public class Carrier
    {
        public string Symbol { get; set; }
    }
    public class Dimensions
    {
        public Int64 Height { get; set; }
        public Int64 Length { get; set; }
        public Int64 Units { get; set; }
        public Int64 Width { get; set; }
    }
    public class RatedService : Service { }
    public class ShippedService : Service { }
    public class BaseCharge
    { public string Currency { get; set; } }
    public class CommodityContents
    {
        public string Description { get; set; }
        public string OriginCountry { get; set; }
        public string ProductCode { get; set; }
        public Int64 Quantity { get; set; }
        public string QuantityUnitMeasure { get; set; }
        public UnitValue UnitValue { get; set; }
        public UnitWeight UnitWeight { get; set; }
    }
    public class UnitValue
    {
        public Int64 Amount { get; set; }
        public string Currency { get; set; }
    }
    public class UnitWeight
    {
        public Int64 Amount { get; set; }
        public string Units { get; set; }
    }
    public class Consignee
    {
        public string Address1 { get; set; }
        public string City { get; set; }
        public string Company { get; set; }
        public string Contact { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public string StateProvince { get; set; }
    }

    public class RatedWeight
    {
        public Int64 Amount { get; set; }
        public string Units { get; set; }
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
    public class Weight : RatedWeight { }
}

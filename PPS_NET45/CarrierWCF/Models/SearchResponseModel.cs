using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierWCF.Models
{
    public class SearchOutputModel
    {
        public int ErrorCode { get; set; }
        public PackagesSearch[] Packages { get; set; }
        public Int64 TotalRecords { get; set; }
    }
    public class PackagesSearch
    {
        public Int64 ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ArriveTime { get; set; }
        public Carriers Carrier { get; set; }
        public string CompanyId { get; set; }
        public Int64 GlobalBundleId { get; set; }
        public Int64 GlobalMsn { get; set; }
        public string MachineName { get; set; }
        public Int64 ManifestId { get; set; }
        public string MiscReference13 { get; set; }
        public string MiscReference6 { get; set; }
        public string MiscReference7 { get; set; }
        public string MiscReference8 { get; set; }
        public RatedService RatedService { get; set; }
        public Service Service { get; set; }
        public Int64 ShipId { get; set; }
        public ShippedService ShippedService { get; set; }
        public Int64 TimeInTransitDays { get; set; }
        public string TransactionId { get; set; }
        public UserData1[] UserData1 { get; set; }
        public string Description { get; set; }
        public string BarCode { get; set; }
        public BaseCharge BaseCharge { get; set; }
        public Int64 BundleId { get; set; }
        public Int64[] BundleIdList { get; set; }
        public string CarrierInstructions { get; set; }
        public Int64 CommercialInvoiceMethod { get; set; }
        public CommodityContents[] CommodityContents { get; set; }
        public Consignee Consignee { get; set; }
        public string ConsigneeReference { get; set; }
        public ImporterOfRecord ImporterOfRecord { get; set; }
        public string[] Maxicode { get; set; }
        public string MiscReference1 { get; set; }
        public string MiscReference2 { get; set; }
        public string MiscReference3 { get; set; }
        public string MiscReference4 { get; set; }
        public string MiscReference5 { get; set; }
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
        public EventLogs[] EventLogs { get; set; }
    }
    public class Carriers
    {
        public string Symbol { get; set; }
    }
    //public class RatedService : Service { }
    //public class ShippedService : Service { }
    //public class Service
    //{
    //    public string Symbol { get; set; }
    //    public string Name { get; set; }
    //}
    public class UserData1
    {
        public string __type { get; set; }
        public string key { get; set; }
        public string value { get; set; }
    }
    //public class BaseCharge
    //{ public string Currency { get; set; } }
    public class EventLogs
    {
        public Int64 DatabaseEventType { get; set; }
        public string EventDate { get; set; }
        public Int64 GlobalMsn { get; set; }
        public string MachineName { get; set; }
        public Int64 PackageEventType { get; set; }
    }
}

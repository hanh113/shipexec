using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierWCF.Models
{

    public class ShipOutputModel
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public Shipmentresponse ShipmentResponse { get; set; }
    }

    public class Shipmentresponse
    {
        public Packagedefaults PackageDefaults { get; set; }
        public Package[] Packages { get; set; }
    }

    public class Packagedefaults
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ArriveTime { get; set; }
        public int ManifestId { get; set; }
        public Ratedservice RatedService { get; set; }
        public Service Service { get; set; }
        public Shippedservice ShippedService { get; set; }
        public int TimeInTransitDays { get; set; }
        public Basecharge BaseCharge { get; set; }
        public int[] BundleIdList { get; set; }
        public string TimeInTransit { get; set; }
        public Total Total { get; set; }
    }

    public class Ratedservice
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
    }

    public class Service
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
    }

    public class Shippedservice
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
    }

    public class Basecharge
    {
        public string Currency { get; set; }
    }

    public class Total
    {
        public string Currency { get; set; }
    }

    public class Package
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ArriveTime { get; set; }
        public Document[] Documents { get; set; }
        public int GlobalBundleId { get; set; }
        public int GlobalMsn { get; set; }
        public Ratedservice1 RatedService { get; set; }
        public Service1 Service { get; set; }
        public int ShipId { get; set; }
        public Shippedservice1 ShippedService { get; set; }
        public int TimeInTransitDays { get; set; }
        public string BarCode { get; set; }
        public int BundleId { get; set; }
        public bool DimensionalWeightRated { get; set; }
        public string[] Maxicode { get; set; }
        public int Msn { get; set; }
        public int NofnSequenceBundle { get; set; }
        public string RoutingCode { get; set; }
        public int PackageListId { get; set; }
        public string TrackingNumber { get; set; }
        public string WaybillBolNumber { get; set; }
        public int WorldEaseId { get; set; }
        public Ratedweight RatedWeight { get; set; }
        public string TimeInTransit { get; set; }
        public string Zone { get; set; }
    }

    public class Ratedservice1
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
    }

    public class Service1
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
    }

    public class Shippedservice1
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
    }

    public class Ratedweight
    {
        public float Amount { get; set; }
        public string Units { get; set; }
    }

    public class Document
    {
        public int ErrorCode { get; set; }
        public int Copies { get; set; }
        public Documentdimension DocumentDimension { get; set; }
        public string DocumentName { get; set; }
        public string DocumentSymbol { get; set; }
        public object[] ImageData { get; set; }
        public string LocalPort { get; set; }
        public object[] PdfData { get; set; }
        public string[] RawData { get; set; }
    }

    public class Documentdimension
    {
        public int Height { get; set; }
        public int Width { get; set; }
    }

}

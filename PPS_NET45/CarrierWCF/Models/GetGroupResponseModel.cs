using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierWCF.Models
{
    public class GetGroupResponseModel
    {
        public Int64 ErrorCode { get; set; }
        public Group Group { get; set; }
    }

    public class Group
    {
        public Int64 ErrorCode { get; set; }
        public DateOpened DateOpened { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public PackageRequest PackageRequest { get; set; }
        public Int64 Status { get; set; }
        public string Symbol { get; set; }
    }
    public class DateOpened
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
    }
    public class PackageRequest
    {
        public string ErrorCode { get; set; }
        public Service Service { get; set; }
        public string Comments { get; set; }
        public string ExportDeclarationStatement { get; set; }
        public string ExportReason { get; set; }
        public ImporterOfRecord ImporterOfRecord { get; set; }
        public string PortOfEntry { get; set; }
        public Shipdate Shipdate { get; set; }
        public string Shipper { get; set; }
        public string Terms { get; set; }
        public bool ThirdPartyBilling { get; set; }
        public string TrackingNumber { get; set; }
        public string WorldEaseCode { get; set; }
        public string WorldEaseMasterShipmentId { get; set; }
    }
    public class Exporter
    {
        public string Account { get; set; }
        public string TaxId { get; set; }
    }

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
    public class Shipdate
    {
        public Int64 Year { get; set; }
        public Int64 Month { get; set; }
        public Int64 Day { get; set; }
    }
}

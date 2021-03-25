using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierWCF.Models
{
    class RePrintResponseModel
    {
        public Int64 ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public DocumentResponses[] DocumentResponses { get; set; }
    }
    public class DocumentResponses
    {
        public Int64 ErrorCode { get; set; }
        public Int64 Copies { get; set; }
        public DocumentDimension DocumentDimension { get; set; }
        public string DocumentName { get; set; }
        public string DocumentSymbol { get; set; }
        public string LocalPort { get; set; }
        public string[] RawData { get; set; }
    }
    public class DocumentDimension
    {
        public float Height { get; set; }
        public float Width { get; set; }
    }
}

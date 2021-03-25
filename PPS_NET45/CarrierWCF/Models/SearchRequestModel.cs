using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CarrierWCF.Models
{
    public class SearchRequestModel : Credentials
    {
        public SearchCriteria SearchCriteria { get; set; }
    }
    public class SearchCriteria
    {
        public string Skip { get; set; }
        public string Take { get; set; }
        public WhereClauses[] WhereClauses { get; set; }
        public OrderByClauses[] OrderByClauses { get; set; }
    }
    public class WhereClauses
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string Operator { get; set; }
    }
    public class OrderByClauses
    {
        public string FieldName { get; set; }
        public string Direction { get; set; }
    }
}

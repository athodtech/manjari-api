using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class DataTablesRootObjecthh
    {
        public int draw { get; set; }
        public List<Columnhh> columns { get; set; }
        public List<Orderhh> order { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public Searchh search { get; set; }
        public int? stateId { get; set; }
        public int? districtId { get; set; }
        public int? blockId { get; set; }
        public int? villageId { get; set; }
        public int activityCategoryMappingId { get; set; }
        public int userId { get; set; }
        public int? currentStatus { get; set; }
        public string days { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }

    }

    public class Searchh
    {
        public string value { get; set; }
        public string name { get; set; }
        public string mobileNumber { get; set; }
        public string householdCode { get; set; }
        public string interventionStartedOn { get; set; }
        public string modifiedOn { get; set; }
        public string createdOn { get; set; }
        public string createdBy { get; set; }
        public string status { get; set; }
        public bool regex { get; set; }
    }
    public class Columnhh
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Searchh search { get; set; }

    }
    public class Orderhh
    {
        public int column { get; set; }
        public string dir { get; set; }
    }

    public class DataTablesResponsehh
    {
        public object data { get; set; }
        public int draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public string error { get; set; }
    }
}

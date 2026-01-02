using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace AthodBeTrackApi.Models
{
    public class DataTablesRootObjectIndicatorDue
    {
        public int draw { get; set; }
        public List<ColumnIndicatorDue> columns { get; set; }
        public List<OrderIndicatorDue> order { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public SearcIndicatorDue search { get; set; }
       
        public int userId { get; set; }
        public int? stateId { get; set; }
        public int? districtId { get; set; }
        public int? blockId { get; set; }
        public int? villageId { get; set; }
        public int activityCategoryMappingId { get; set; }  
        public int activityQuestionId { get; set; }
        public int? groupId { get; set; }      
        public string status { get; set; }      

    }

    public class SearcIndicatorDue
    {
        public string value { get; set; }
        public string name { get; set; }
        public string householdName { get; set; }
        public string mobileNo { get; set; }
        public string uniqueSetCode { get; set; }
        public string districtName { get; set; }
        public string blockName { get; set; }
        public string villageName { get; set; }
        public int StatusId { get; set; }
        public string modifyOn { get; set; }
        public bool regex { get; set; }
    }
    public class ColumnIndicatorDue
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public SearcIndicatorDue search { get; set; }

    }
    public class OrderIndicatorDue
    {
        public int column { get; set; }
        public string dir { get; set; }
    }

    public class DataTablesResponseIndicatorDue
    {
        public object data { get; set; }
        public int draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public string error { get; set; }
    }
}


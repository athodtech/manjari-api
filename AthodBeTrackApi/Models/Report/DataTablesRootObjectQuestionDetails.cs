using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class DataTablesRootObjectQuestionDetails
    {
        public int draw { get; set; }
        public List<ColumnQuestionDetails> columns { get; set; }
        public List<OrderQuestionDetails> order { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public SearcUpdatedHH search { get; set; }
        public int reportId { get; set; }
        public int activityQuestionId { get; set; }
        public string filter { get; set; }
        public bool isUniqueFilter { get; set; }
        public bool isPrimaryFilter { get; set; }
    }
    public class SearcQuestionDetails
    {
        public string value { get; set; }
        public string name { get; set; }
        public string householdName { get; set; }
        public string mobileNumber { get; set; }
        public string uniqueSetCode { get; set; }
        public string districtName { get; set; }
        public string blockName { get; set; }
        public string villageName { get; set; }
        public string questionValue { get; set; }
        public string updatedOn { get; set; }
        public bool regex { get; set; }
    }

    public class ColumnQuestionDetails
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public SearcQuestionDetails search { get; set; }

    }
    public class OrderQuestionDetails
    {
        public int column { get; set; }
        public string dir { get; set; }
    }


    public class DataTablesResponseQuestionDetails
    {
        public object data { get; set; }
        public int draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public string error { get; set; }
    }
}

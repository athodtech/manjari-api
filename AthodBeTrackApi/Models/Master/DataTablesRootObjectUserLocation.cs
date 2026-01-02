using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class DataTablesRootObjectUserLocation
    {
        public int draw { get; set; }
        public List<ColumnUserLocation> columns { get; set; }
        public List<OrderUserLocation> order { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public SearchUserLocation search { get; set; }
        public int userId { get; set; }
        public string stateId { get; set; }
        public string districtId { get; set; }
        public string blockId { get; set; }
        public string villageId { get; set; }
    }

    public class SearchUserLocation
    {
        public string value { get; set; }
        public string stateName { get; set; }
        public string districtName { get; set; }
        public string blockName { get; set; }
        public string villageName { get; set; }       
        public int totalHousehold { get; set; }
        public bool regex { get; set; }
    }
    public class ColumnUserLocation
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public SearchUserLocation search { get; set; }

    }
    public class OrderUserLocation
    {
        public int column { get; set; }
        public string dir { get; set; }
    }

    public class DataTablesResponseUserLocation
    {
        public object data { get; set; }
        public int draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public string error { get; set; }
    }
}

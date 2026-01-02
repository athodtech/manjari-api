using System;

namespace AthodBeTrackApi
{
    public class ExportHouseholdFilter
    {
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
}

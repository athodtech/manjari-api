using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class GetReports_Result
    {
        public int ReportId { get; set; }
        public int? ActivityCategoryMappingId { get; set; }
        public string ReportNo { get; set; }
        public string ReportName { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string ImageName { get; set; }
        public string StateId { get; set; }
        public bool AllState { get; set; }
        public string DistrictId { get; set; }
        public bool AllDistrict { get; set; }
        public string BlockId { get; set; }
        public bool AllBlock { get; set; }
        public string VillageId { get; set; }
        public bool AllVillage { get; set; }
        public string ReportingGroupIds { get; set; }
        public bool AllGroup { get; set; }
        public string ReportingTagIds { get; set; }
        public bool AllTag { get; set; }
        public string ReportQuestionIds { get; set; }
        public bool AllQuestion { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? ReportingFrequnecy { get; set; }
        public bool? ReportFilterEnable { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdated { get; set; }
        public int CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }
        public int ReportStatus { get; set; }
        public DateTime SharedOn { get; set; }
        public int SharedBy { get; set; }
        public string SharedByName { get; set; }
        public string SharedByImage { get; set; }
        public bool DefaultReport { get; set; }
        public bool IsUnique { get; set; }
        public bool IsPrimary { get; set; }
    }
}

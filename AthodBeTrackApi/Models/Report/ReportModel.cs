using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class ReportModel : BaseModel
    {
        public int ReportId { get; set; }
        public int? ActivityCategoryMappingId { get; set; }
        public string ReportNo { get; set; }
        [StringLength(50)]
        public string ReportName { get; set; }
        [StringLength(200)]       
        public string Description { get; set; }
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string StateId { get; set; }
        public bool? AllState { get; set; }
        [StringLength(1000)]
        public string DistrictId { get; set; }
        public bool? AllDistrict { get; set; }
        [StringLength(4000)]
        public string BlockId { get; set; }
        public bool? AllBlock { get; set; }      
        public string VillageId { get; set; }
        public bool? AllVillage { get; set; }
        [StringLength(100)]
        public string ReportingGroupIds { get; set; }
        public bool? AllGroup { get; set; }
        [StringLength(1000)]
        public string ReportingTagIds { get; set; }
        public bool? AllTag { get; set; }
        [StringLength(4000)]
        public string ReportQuestionIds { get; set; }
        public bool? AllQuestion { get; set; }      
        public DateTime? FromDate { get; set; }     
        public DateTime? ToDate { get; set; }
        public int? ReportingFrequnecy { get; set; }
        public bool? ReportFilterEnable { get; set; }
        public string Status { get; set; }
        public bool? IsPrimary { get; set; }
        public bool? IsUnique { get; set; }
    }
}

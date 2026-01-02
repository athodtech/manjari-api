using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Microsoft.EntityFrameworkCore.Index(nameof(ReportId), Name = "IX_Reports", IsUnique = true)]
    public partial class Report
    {
        public Report()
        {
            ReportsFavourites = new HashSet<ReportsFavourite>();
            ReportsShareLogs = new HashSet<ReportsShareLog>();
        }

        [Key]
        public int ReportId { get; set; }
        public int? ActivityCategoryMappingId { get; set; }
        [Required]
        [StringLength(20)]
        public string ReportNo { get; set; }
        [StringLength(50)]
        public string ReportName { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public int UserId { get; set; }
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
        [StringLength(1000)]
        public string ReportingGroupIds { get; set; }
        public bool? AllGroup { get; set; }
        [StringLength(1000)]
        public string ReportingTagIds { get; set; }
        public bool? AllTag { get; set; }
        [StringLength(4000)]
        public string ReportQuestionIds { get; set; }
        public bool? AllQuestion { get; set; }
        [Column(TypeName = "date")]
        public DateTime? FromDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ToDate { get; set; }
        public int? ReportingFrequnecy { get; set; }
        [StringLength(20)]
        public string Status { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public bool? ReportFilterEnable { get; set; }
        public bool? IsPrimary { get; set; }
        public bool? IsUnique { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public int? ReportStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SharedOn { get; set; }
        public int? SharedBy { get; set; }
        public bool? DefaultReportId { get; set; }

        [InverseProperty(nameof(ReportsFavourite.Report))]
        public virtual ICollection<ReportsFavourite> ReportsFavourites { get; set; }
        [InverseProperty(nameof(ReportsShareLog.Report))]
        public virtual ICollection<ReportsShareLog> ReportsShareLogs { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Keyless]
    [Table("DueReportDetail")]
    public partial class DueReportDetail
    {
        public int QuestionId { get; set; }
        public int? ActivityCategoryMappingId { get; set; }
        [Column("ActivityQuestionID")]
        public int ActivityQuestionId { get; set; }
        public int GroupId { get; set; }
        [Required]
        [StringLength(250)]
        public string GroupName { get; set; }
        [Required]
        [StringLength(1000)]
        public string Question { get; set; }
        [StringLength(100)]
        public string Help { get; set; }
        [Column("ReportingFrequecyTypeID")]
        public int ReportingFrequecyTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string ReportingFrequecyType { get; set; }
        public int? ActivityQuestionSetId { get; set; }
        [StringLength(50)]
        public string UniqueSetCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EndDate { get; set; }
        [Column(TypeName = "numeric(18, 0)")]
        public decimal? Due { get; set; }
        [Required]
        [StringLength(50)]
        public string Status { get; set; }
        public int? UserId { get; set; }
        [StringLength(10)]
        public string Stateid { get; set; }
        [StringLength(10)]
        public string BlockId { get; set; }
        [StringLength(10)]
        public string DistrictId { get; set; }
        [StringLength(10)]
        public string VillageId { get; set; }
        [Column("StatusID")]
        public int? StatusId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Keyless]
    [Table("DueReportSummary")]
    public partial class DueReportSummary
    {
        public int QuestionId { get; set; }
        public int ActivityCategoryMappingId { get; set; }
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
        [Column(TypeName = "numeric(18, 0)")]
        public decimal? ExpectedReading { get; set; }
        [Column(TypeName = "numeric(18, 0)")]
        public decimal? Dues { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}

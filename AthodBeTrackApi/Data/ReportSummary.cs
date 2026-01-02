using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Keyless]
    [Table("ReportSummary")]
    [Microsoft.EntityFrameworkCore.Index(nameof(SortingOrder), Name = "IX_ReportSummary")]
    public partial class ReportSummary
    {
        public int ReportId { get; set; }
        public int ActivityCategoryMappingId { get; set; }
        public int ActivityQuestionId { get; set; }
        [Required]
        [StringLength(500)]
        public string Question { get; set; }
        [StringLength(100)]
        public string QuestionItem { get; set; }
        [StringLength(500)]
        public string Help { get; set; }
        [Required]
        [StringLength(50)]
        public string Type { get; set; }
        [StringLength(50)]
        public string ReportingFrequencyType { get; set; }
        [Required]
        [StringLength(500)]
        public string Item { get; set; }
        [Required]
        public string QuestionValue { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? SortingOrder { get; set; }
        public int? ChartTypeId { get; set; }
        [StringLength(500)]
        public string ChartJson { get; set; }
        [StringLength(20)]
        public string ItemShortName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastUpdated { get; set; }
        public int? ItemShortOrder { get; set; }
        public int? AffectedHousehold { get; set; }
        [StringLength(50)]
        public string ChartSize { get; set; }
        [StringLength(500)]
        public string XaxisName { get; set; }
        [StringLength(500)]
        public string YaxisName { get; set; }
        public int? ColorPalette { get; set; }

        [ForeignKey(nameof(ChartTypeId))]
        public virtual Chart ChartType { get; set; }
        [ForeignKey(nameof(ReportId))]
        public virtual Report Report { get; set; }
    }
}

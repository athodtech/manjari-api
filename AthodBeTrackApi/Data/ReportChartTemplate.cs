using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("ReportChartTemplate")]
    public partial class ReportChartTemplate
    {
        [Key]
        public int ReportId { get; set; }
        [Key]
        public int QuestionId { get; set; }
        public int? ChartTypeId { get; set; }
        [StringLength(500)]
        public string XaxisName { get; set; }
        [StringLength(500)]
        public string YaxisName { get; set; }
        [StringLength(500)]
        public string Question { get; set; }
        [StringLength(500)]
        public string OriginalQuestion { get; set; }
        [StringLength(500)]
        public string Help { get; set; }
        [Key]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal SortingOrder { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? OriginalSortingOrder { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ChartWidth { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ChartHeight { get; set; }
        public int? ColorPalette { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        [StringLength(50)]
        public string ChartSize { get; set; }

        [ForeignKey(nameof(ChartTypeId))]
        [InverseProperty(nameof(Chart.ReportChartTemplates))]
        public virtual Chart ChartType { get; set; }
        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.ReportChartTemplateCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.ReportChartTemplateUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
    }
}

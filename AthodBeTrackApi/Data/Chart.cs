using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("Chart")]
    public partial class Chart
    {
        public Chart()
        {
            ReportChartTemplates = new HashSet<ReportChartTemplate>();
        }

        [Key]
        public int ChartTypeId { get; set; }
        [StringLength(50)]
        public string ChartType { get; set; }
        [StringLength(500)]
        public string ChartJson { get; set; }
        [StringLength(50)]
        public string Icon { get; set; }
        public int? Sort { get; set; }
        public bool? IsActive { get; set; }

        [InverseProperty(nameof(ReportChartTemplate.ChartType))]
        public virtual ICollection<ReportChartTemplate> ReportChartTemplates { get; set; }
    }
}

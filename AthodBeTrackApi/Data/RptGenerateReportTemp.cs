using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Keyless]
    [Table("RPT_GenerateReportTemp")]
    public partial class RptGenerateReportTemp
    {
        public int ReportId { get; set; }
        public int ActivityQuestionId { get; set; }
        public string Question { get; set; }
        [StringLength(100)]
        public string Help { get; set; }
        [StringLength(100)]
        public string HouseholdName { get; set; }
        [StringLength(12)]
        public string MobileNumber { get; set; }
        [StringLength(100)]
        public string UniqueSetCode { get; set; }
        [StringLength(100)]
        public string DistrictName { get; set; }
        [StringLength(100)]
        public string BlockName { get; set; }
        [StringLength(100)]
        public string VillageName { get; set; }
        public string QuestionValue { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? SortingOrder { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
    }
}

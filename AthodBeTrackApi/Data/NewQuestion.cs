using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Keyless]
    [Table("'New Question$'")]
    public partial class NewQuestion
    {
        public double? QuestionId { get; set; }
        [StringLength(255)]
        public string Question { get; set; }
        [Column("parent")]
        [StringLength(255)]
        public string Parent { get; set; }
        [Column("Parent Question")]
        public double? ParentQuestion { get; set; }
        public double? TypeId { get; set; }
        [StringLength(255)]
        public string Type { get; set; }
        public double? Mendatory { get; set; }
        [StringLength(255)]
        public string Min { get; set; }
        [StringLength(255)]
        public string Max { get; set; }
        [StringLength(255)]
        public string Decimal { get; set; }
        [Column("Skip Logic")]
        public double? SkipLogic { get; set; }
        [Column("Skip Question")]
        public double? SkipQuestion { get; set; }
        [Column("Skip Parent ID")]
        [StringLength(255)]
        public string SkipParentId { get; set; }
        [Column("Show/Hide Logic")]
        [StringLength(255)]
        public string ShowHideLogic { get; set; }
        [Column("Option Status")]
        [StringLength(255)]
        public string OptionStatus { get; set; }
        [Column("New Options")]
        [StringLength(255)]
        public string NewOptions { get; set; }
        public double? Frequency { get; set; }
        [Column("Activity Type")]
        [StringLength(255)]
        public string ActivityType { get; set; }
        [StringLength(255)]
        public string Calculation { get; set; }
        [Column("Chart Type")]
        [StringLength(255)]
        public string ChartType { get; set; }
        [StringLength(255)]
        public string Options { get; set; }
        [StringLength(255)]
        public string Skip2 { get; set; }
        [Column("Help Text")]
        [StringLength(255)]
        public string HelpText { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
        [Column("Tab / Pages")]
        [StringLength(255)]
        public string TabPages { get; set; }
        [StringLength(255)]
        public string After { get; set; }
        [StringLength(255)]
        public string Before { get; set; }
    }
}

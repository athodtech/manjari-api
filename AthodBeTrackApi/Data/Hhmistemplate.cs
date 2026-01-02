using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Keyless]
    [Table("HHMISTemplate")]
    public partial class Hhmistemplate
    {
        [Column("S#No#")]
        public double? SNo { get; set; }
        [Column("sno")]
        public int? Sno1 { get; set; }
        [StringLength(255)]
        public string Theme { get; set; }
        [Column("Sub Theme")]
        [StringLength(255)]
        public string SubTheme { get; set; }
        [Column("Sub - Sub Theme")]
        [StringLength(255)]
        public string SubSubTheme { get; set; }
        [Column("Activity / Information ")]
        [StringLength(255)]
        public string ActivityInformation { get; set; }
        [StringLength(255)]
        public string Type { get; set; }
        [Column("Sub Activity")]
        [StringLength(255)]
        public string SubActivity { get; set; }
        [Column("Help Text")]
        [StringLength(255)]
        public string HelpText { get; set; }
        [Column("Reporting Frequency")]
        [StringLength(255)]
        public string ReportingFrequency { get; set; }
        [Column("Input Type")]
        [StringLength(255)]
        public string InputType { get; set; }
        [Column("Required Additional Information")]
        [StringLength(255)]
        public string RequiredAdditionalInformation { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
    }
}

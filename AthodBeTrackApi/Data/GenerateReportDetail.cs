using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Keyless]
    public partial class GenerateReportDetail
    {
        public int ReportId { get; set; }
        public int DownloadedBy { get; set; }
        [Required]
        public string QuestionIds { get; set; }
        [Required]
        [StringLength(256)]
        public string FilePath { get; set; }
        [Required]
        [StringLength(50)]
        public string FileName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DownloadTime { get; set; }
    }
}

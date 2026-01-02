using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    public partial class ReportsShareLog
    {
        [Key]
        public int ReportsShareLogId { get; set; }
        public int UserId { get; set; }
        public int ReportId { get; set; }
        [Required]
        [StringLength(20)]
        public string ReportNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SharedOn { get; set; }
        public int SharedBy { get; set; }

        [ForeignKey(nameof(ReportId))]
        [InverseProperty("ReportsShareLogs")]
        public virtual Report Report { get; set; }
        [ForeignKey(nameof(SharedBy))]
        [InverseProperty("ReportsShareLogSharedByNavigations")]
        public virtual User SharedByNavigation { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("ReportsShareLogUsers")]
        public virtual User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("MailProcessContent")]
    public partial class MailProcessContent
    {
        [Key]
        public int Id { get; set; }
        public int ProcessId { get; set; }
        [Required]
        [StringLength(200)]
        public string Subject { get; set; }
        [StringLength(2000)]
        public string Content { get; set; }
        [Required]
        [StringLength(200)]
        public string Signature { get; set; }
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }

        [ForeignKey(nameof(ProcessId))]
        [InverseProperty("MailProcessContents")]
        public virtual Process Process { get; set; }
    }
}

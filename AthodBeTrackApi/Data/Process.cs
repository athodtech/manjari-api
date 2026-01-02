using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("Process")]
    public partial class Process
    {
        public Process()
        {
            MailProcessContents = new HashSet<MailProcessContent>();
        }

        [Key]
        public int ProcessId { get; set; }
        [Required]
        [StringLength(50)]
        public string ProcessName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }

        [InverseProperty(nameof(MailProcessContent.Process))]
        public virtual ICollection<MailProcessContent> MailProcessContents { get; set; }
    }
}

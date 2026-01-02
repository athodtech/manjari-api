using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("ApplicationEventType")]
    public partial class ApplicationEventType
    {
        public ApplicationEventType()
        {
            ApplicationLoggings = new HashSet<ApplicationLogging>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Value { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [InverseProperty(nameof(ApplicationLogging.Event))]
        public virtual ICollection<ApplicationLogging> ApplicationLoggings { get; set; }
    }
}

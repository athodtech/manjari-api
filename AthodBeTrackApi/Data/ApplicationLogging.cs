using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("ApplicationLogging")]
    public partial class ApplicationLogging
    {
        [Key]
        public long Id { get; set; }
        public int? ActivityCategoryMappingId { get; set; }
        public int EventId { get; set; }
        [Required]
        [StringLength(500)]
        public string Message { get; set; }
        public byte StatusId { get; set; }
        [StringLength(50)]
        public string IpAddress { get; set; }
        [StringLength(256)]
        public string Os { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsRead { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.ApplicationLoggingCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(EventId))]
        [InverseProperty(nameof(ApplicationEventType.ApplicationLoggings))]
        public virtual ApplicationEventType Event { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.ApplicationLoggingUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("Activity")]
    public partial class Activity
    {
        public Activity()
        {
            ActivityCategoryMappings = new HashSet<ActivityCategoryMapping>();
        }

        [Key]
        public int ActivityId { get; set; }
        [Required]
        [StringLength(1000)]
        public string ActivityName { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        public int Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.ActivityCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.ActivityUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
        [InverseProperty(nameof(ActivityCategoryMapping.Activity))]
        public virtual ICollection<ActivityCategoryMapping> ActivityCategoryMappings { get; set; }
    }
}

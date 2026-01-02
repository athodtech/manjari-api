using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("Category")]
    public partial class Category
    {
        public Category()
        {
            ActivityCategoryMappings = new HashSet<ActivityCategoryMapping>();
        }

        [Key]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(1000)]
        public string CategoryName { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        public int Status { get; set; }
        [StringLength(150)]
        public string IconClass { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.CategoryCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.CategoryUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
        [InverseProperty(nameof(ActivityCategoryMapping.Category))]
        public virtual ICollection<ActivityCategoryMapping> ActivityCategoryMappings { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("ActivityCategoryMapping")]
    public partial class ActivityCategoryMapping
    {
        public ActivityCategoryMapping()
        {
            ActivityQuestionSetUniqueIdentities = new HashSet<ActivityQuestionSetUniqueIdentity>();
            ActivityQuestions = new HashSet<ActivityQuestion>();
        }

        [Key]
        public int Id { get; set; }
        public int? ActivityId { get; set; }
        public int? CategoryId { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(ActivityId))]
        [InverseProperty("ActivityCategoryMappings")]
        public virtual Activity Activity { get; set; }
        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("ActivityCategoryMappings")]
        public virtual Category Category { get; set; }
        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.ActivityCategoryMappingCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.ActivityCategoryMappingUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
        [InverseProperty(nameof(ActivityQuestionSetUniqueIdentity.ActivityCategoryMapping))]
        public virtual ICollection<ActivityQuestionSetUniqueIdentity> ActivityQuestionSetUniqueIdentities { get; set; }
        [InverseProperty(nameof(ActivityQuestion.ActivityCategoryMapping))]
        public virtual ICollection<ActivityQuestion> ActivityQuestions { get; set; }
    }
}

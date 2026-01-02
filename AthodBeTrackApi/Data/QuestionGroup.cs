using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("QuestionGroup")]
    public partial class QuestionGroup
    {
        public QuestionGroup()
        {
            ActivityQuestionGroupMappings = new HashSet<ActivityQuestionGroupMapping>();
            ActivityQuestionSetGroupMappings = new HashSet<ActivityQuestionSetGroupMapping>();
        }

        [Key]
        public int GroupId { get; set; }
        [Required]
        [StringLength(250)]
        public string GroupName { get; set; }
        public int? ParentGroupId { get; set; }
        public int? SortingOrder { get; set; }
        public bool? Default { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.QuestionGroupCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.QuestionGroupUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
        [InverseProperty(nameof(ActivityQuestionGroupMapping.Group))]
        public virtual ICollection<ActivityQuestionGroupMapping> ActivityQuestionGroupMappings { get; set; }
        [InverseProperty(nameof(ActivityQuestionSetGroupMapping.Group))]
        public virtual ICollection<ActivityQuestionSetGroupMapping> ActivityQuestionSetGroupMappings { get; set; }
    }
}

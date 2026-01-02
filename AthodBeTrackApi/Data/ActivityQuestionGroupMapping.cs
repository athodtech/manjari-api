using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("ActivityQuestionGroupMapping")]
    public partial class ActivityQuestionGroupMapping
    {
        [Key]
        public int Id { get; set; }
        public int? ActivityQuestionId { get; set; }
        public int? GroupId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }

        [ForeignKey(nameof(ActivityQuestionId))]
        [InverseProperty("ActivityQuestionGroupMappings")]
        public virtual ActivityQuestion ActivityQuestion { get; set; }
        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.ActivityQuestionGroupMappingCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(GroupId))]
        [InverseProperty(nameof(QuestionGroup.ActivityQuestionGroupMappings))]
        public virtual QuestionGroup Group { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.ActivityQuestionGroupMappingUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("ActivityQuestionSetGroupMapping")]
    [Microsoft.EntityFrameworkCore.Index(nameof(ActivityQuestionSetId), nameof(GroupId), Name = "IX_ActivityQuestionSetGroupMapping", IsUnique = true)]
    public partial class ActivityQuestionSetGroupMapping
    {
        [Key]
        [Column("ActivityQuestionSetGMId")]
        public int ActivityQuestionSetGmid { get; set; }
        public int ActivityQuestionSetId { get; set; }
        [Column("GroupID")]
        public int GroupId { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(ActivityQuestionSetId))]
        [InverseProperty(nameof(ActivityQuestionSetUniqueIdentity.ActivityQuestionSetGroupMappings))]
        public virtual ActivityQuestionSetUniqueIdentity ActivityQuestionSet { get; set; }
        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.ActivityQuestionSetGroupMappingCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(GroupId))]
        [InverseProperty(nameof(QuestionGroup.ActivityQuestionSetGroupMappings))]
        public virtual QuestionGroup Group { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.ActivityQuestionSetGroupMappingUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
    }
}

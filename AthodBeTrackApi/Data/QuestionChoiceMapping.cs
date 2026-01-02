using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("QuestionChoiceMapping")]
    public partial class QuestionChoiceMapping
    {
        [Key]
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int? QuestionChoiceId { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.QuestionChoiceMappingCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(QuestionId))]
        [InverseProperty(nameof(QuestionBank.QuestionChoiceMappings))]
        public virtual QuestionBank Question { get; set; }
        [ForeignKey(nameof(QuestionChoiceId))]
        [InverseProperty("QuestionChoiceMappings")]
        public virtual QuestionChoice QuestionChoice { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.QuestionChoiceMappingUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
    }
}

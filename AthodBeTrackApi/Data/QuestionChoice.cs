using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("QuestionChoice")]
    public partial class QuestionChoice
    {
        public QuestionChoice()
        {
            QuestionChoiceItems = new HashSet<QuestionChoiceItem>();
            QuestionChoiceMappings = new HashSet<QuestionChoiceMapping>();
        }

        [Key]
        public int QuestionChoiceId { get; set; }
        [Required]
        [StringLength(256)]
        public string QuestionChoiceName { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.QuestionChoiceCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.QuestionChoiceUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
        [InverseProperty(nameof(QuestionChoiceItem.QuestionChoice))]
        public virtual ICollection<QuestionChoiceItem> QuestionChoiceItems { get; set; }
        [InverseProperty(nameof(QuestionChoiceMapping.QuestionChoice))]
        public virtual ICollection<QuestionChoiceMapping> QuestionChoiceMappings { get; set; }
    }
}

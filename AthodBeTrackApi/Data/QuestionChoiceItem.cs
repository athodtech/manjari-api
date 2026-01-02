using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("QuestionChoiceItem")]
    public partial class QuestionChoiceItem
    {
        [Key]
        public int Id { get; set; }
        public int QuestionChoiceId { get; set; }
        [Required]
        [StringLength(256)]
        public string Item { get; set; }
        public int Value { get; set; }
        public int? Sort { get; set; }
        [StringLength(12)]
        public string ItemShotName { get; set; }
        public bool IsActive { get; set; }
        public int? LanguageId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.QuestionChoiceItemCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(LanguageId))]
        [InverseProperty("QuestionChoiceItems")]
        public virtual Language Language { get; set; }
        [ForeignKey(nameof(QuestionChoiceId))]
        [InverseProperty("QuestionChoiceItems")]
        public virtual QuestionChoice QuestionChoice { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.QuestionChoiceItemUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
    }
}

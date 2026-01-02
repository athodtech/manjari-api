using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("Language")]
    public partial class Language
    {
        public Language()
        {
            QuestionBanks = new HashSet<QuestionBank>();
            QuestionChoiceItems = new HashSet<QuestionChoiceItem>();
        }

        [Key]
        public int LanguageId { get; set; }
        [Required]
        [StringLength(50)]
        public string LanguageName { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.LanguageCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.LanguageUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
        [InverseProperty(nameof(QuestionBank.Language))]
        public virtual ICollection<QuestionBank> QuestionBanks { get; set; }
        [InverseProperty(nameof(QuestionChoiceItem.Language))]
        public virtual ICollection<QuestionChoiceItem> QuestionChoiceItems { get; set; }
    }
}

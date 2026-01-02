using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("QuestionType")]
    public partial class QuestionType
    {
        public QuestionType()
        {
            QuestionBanks = new HashSet<QuestionBank>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(256)]
        public string Type { get; set; }
        public int Sort { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.QuestionTypeCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.QuestionTypeUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
        [InverseProperty(nameof(QuestionBank.QuestionType))]
        public virtual ICollection<QuestionBank> QuestionBanks { get; set; }
    }
}

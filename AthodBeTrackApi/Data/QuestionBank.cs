using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("QuestionBank")]
    public partial class QuestionBank
    {
        public QuestionBank()
        {
            ActivityQuestions = new HashSet<ActivityQuestion>();
            QuestionChoiceMappings = new HashSet<QuestionChoiceMapping>();
        }

        [Key]
        public int QuestionId { get; set; }
        [Required]
        [StringLength(1000)]
        public string Question { get; set; }
        public int QuestionTypeId { get; set; }
        public int? ParentQuestionId { get; set; }
        public bool Mandatory { get; set; }
        public int? Sort { get; set; }
        public int? LanguageId { get; set; }
        public int? MaxLength { get; set; }
        public int? MinLength { get; set; }
        [StringLength(200)]
        public string Constraint { get; set; }
        [StringLength(200)]
        public string Format { get; set; }
        public bool? IsFormula { get; set; }
        [StringLength(200)]
        public string Formula { get; set; }
        public bool? HavingChild { get; set; }
        public int? ReportingFrequencyTypeId { get; set; }
        public bool? TimeStampManual { get; set; }
        [StringLength(100)]
        public string Help { get; set; }
        public bool? SkipLogic { get; set; }
        public int? SkipQuestionId { get; set; }
        [StringLength(100)]
        public string SkipLogicDetail { get; set; }
        public bool? Primary { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        [StringLength(150)]
        public string PalceHolder { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.QuestionBankCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(LanguageId))]
        [InverseProperty("QuestionBanks")]
        public virtual Language Language { get; set; }
        [ForeignKey(nameof(QuestionTypeId))]
        [InverseProperty("QuestionBanks")]
        public virtual QuestionType QuestionType { get; set; }
        [ForeignKey(nameof(ReportingFrequencyTypeId))]
        [InverseProperty(nameof(QuestionReportingFrequency.QuestionBanks))]
        public virtual QuestionReportingFrequency ReportingFrequencyType { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.QuestionBankUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
        [InverseProperty(nameof(ActivityQuestion.Question))]
        public virtual ICollection<ActivityQuestion> ActivityQuestions { get; set; }
        [InverseProperty(nameof(QuestionChoiceMapping.Question))]
        public virtual ICollection<QuestionChoiceMapping> QuestionChoiceMappings { get; set; }
    }
}

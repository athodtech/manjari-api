using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("ActivityQuestion")]
    public partial class ActivityQuestion
    {
        public ActivityQuestion()
        {
            ActivityQuestionGroupMappings = new HashSet<ActivityQuestionGroupMapping>();
        }

        [Key]
        public int ActivityQuestionId { get; set; }
        public int ActivityCategoryMappingId { get; set; }
        public int QuestionId { get; set; }
        public int? ParentQuestionId { get; set; }
        public bool Mandatory { get; set; }
        public int? Sort { get; set; }
        public int? MaxLength { get; set; }
        public int? MinLength { get; set; }
        [StringLength(200)]
        public string Constraint { get; set; }
        public bool? TableDisplayColumn { get; set; }
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
        public int? ActivityQuestionStaticId { get; set; }
        [StringLength(150)]
        public string PalceHolder { get; set; }

        [ForeignKey(nameof(ActivityCategoryMappingId))]
        [InverseProperty("ActivityQuestions")]
        public virtual ActivityCategoryMapping ActivityCategoryMapping { get; set; }
        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.ActivityQuestionCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(QuestionId))]
        [InverseProperty(nameof(QuestionBank.ActivityQuestions))]
        public virtual QuestionBank Question { get; set; }
        [ForeignKey(nameof(ReportingFrequencyTypeId))]
        [InverseProperty(nameof(QuestionReportingFrequency.ActivityQuestions))]
        public virtual QuestionReportingFrequency ReportingFrequencyType { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.ActivityQuestionUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
        [InverseProperty(nameof(ActivityQuestionGroupMapping.ActivityQuestion))]
        public virtual ICollection<ActivityQuestionGroupMapping> ActivityQuestionGroupMappings { get; set; }
    }
}

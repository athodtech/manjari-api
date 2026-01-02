using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("QuestionReportingFrequency")]
    public partial class QuestionReportingFrequency
    {
        public QuestionReportingFrequency()
        {
            ActivityQuestions = new HashSet<ActivityQuestion>();
            QuestionBanks = new HashSet<QuestionBank>();
        }

        [Key]
        public int ReportingFrequencyTypeId { get; set; }
        [StringLength(50)]
        public string ReportingFrequencyType { get; set; }

        [InverseProperty(nameof(ActivityQuestion.ReportingFrequencyType))]
        public virtual ICollection<ActivityQuestion> ActivityQuestions { get; set; }
        [InverseProperty(nameof(QuestionBank.ReportingFrequencyType))]
        public virtual ICollection<QuestionBank> QuestionBanks { get; set; }
    }
}

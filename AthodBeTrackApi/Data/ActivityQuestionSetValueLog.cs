using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("ActivityQuestionSetValueLog")]
    public partial class ActivityQuestionSetValueLog
    {
        [Key]
        public int ActivityQuestionSetId { get; set; }
        [Key]
        public int ActivityQuestionId { get; set; }
        [Key]
        public int Sno { get; set; }
        [Key]
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Key]
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }
        public string QuestionValue { get; set; }
        public int? ReportingFrequencyTypeId { get; set; }
        public bool? TimeStampManual { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}

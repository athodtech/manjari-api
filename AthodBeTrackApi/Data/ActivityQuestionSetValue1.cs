using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("ActivityQuestionSetValue")]
    public partial class ActivityQuestionSetValue1
    {
        [Key]
        public int ActivityQuestionSetId { get; set; }
        [Key]
        public int ActivityQuestionId { get; set; }
        [Key]
        public int Sno { get; set; }
        public string QuestionValue { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Keyless]
    [Table("ActivityQuestionSetValue2")]
    public partial class ActivityQuestionSetValue2
    {
        [Column("activityQuestionId")]
        public int? ActivityQuestionId { get; set; }
        [Column("questionValue")]
        public string QuestionValue { get; set; }
        [Column("sno")]
        public int? Sno { get; set; }
    }
}

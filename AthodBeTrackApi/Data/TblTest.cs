using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Keyless]
    [Table("tblTest")]
    public partial class TblTest
    {
        [Column("activityQuestionId")]
        public int? ActivityQuestionId { get; set; }
        [Column("sno")]
        public int? Sno { get; set; }
        [Column("questionValue")]
        [StringLength(500)]
        public string QuestionValue { get; set; }
    }
}

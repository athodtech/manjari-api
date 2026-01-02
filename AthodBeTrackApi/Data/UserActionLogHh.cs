using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("UserActionLogHH")]
    public partial class UserActionLogHh
    {
        [Key]
        public int UserId { get; set; }
        [Key]
        [Column(TypeName = "datetime")]
        public DateTime ActionLogTime { get; set; }
        [Key]
        public int ActivityQuestionSetId { get; set; }
        [Key]
        public int Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}

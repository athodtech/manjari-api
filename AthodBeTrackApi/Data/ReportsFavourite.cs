using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("ReportsFavourite")]
    public partial class ReportsFavourite
    {
        [Key]
        public int Id { get; set; }
        public int ReportId { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(ReportId))]
        [InverseProperty("ReportsFavourites")]
        public virtual Report Report { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("ReportsFavourites")]
        public virtual User User { get; set; }
    }
}

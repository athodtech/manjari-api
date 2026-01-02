using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Keyless]
    [Table("ExceptionLog")]
    public partial class ExceptionLog
    {
        [Column("id")]
        public int Id { get; set; }
        public int? ErrorLine { get; set; }
        [StringLength(5000)]
        public string ErrorMessage { get; set; }
        public int? ErrorNumber { get; set; }
        [StringLength(128)]
        public string ErrorProcedure { get; set; }
        public int? ErrorSeverity { get; set; }
        public int? ErrorState { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateErrorRaised { get; set; }
    }
}

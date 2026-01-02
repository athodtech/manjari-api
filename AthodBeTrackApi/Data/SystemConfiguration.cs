using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("SystemConfiguration")]
    public partial class SystemConfiguration
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string ConfigurationType { get; set; }
        [Column(TypeName = "numeric(18, 0)")]
        public decimal? LowerRange { get; set; }
        [Column(TypeName = "numeric(18, 0)")]
        public decimal? UpperRange { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? MinLimit { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal MaxLimit { get; set; }
    }
}

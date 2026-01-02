using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("EmailConfiguration")]
    public partial class EmailConfiguration
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
        [StringLength(100)]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        public string Server { get; set; }
        public int Port { get; set; }
        [StringLength(50)]
        public string SenderEmail { get; set; }
        [Required]
        [StringLength(100)]
        public string FriendlyName { get; set; }
        [StringLength(200)]
        public string Signature { get; set; }
        [Column("SSLStatus")]
        public bool Sslstatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MaintenanceDateTime { get; set; }
        public bool IsMaintenance { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }
    }
}

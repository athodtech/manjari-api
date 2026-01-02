using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("UserLocation")]
    public partial class UserLocation
    {
        [Key]
        public int UserLocId { get; set; }
        public int UserId { get; set; }
        public int LocationLevel { get; set; }
        [Required]
        [StringLength(100)]
        public string StateId { get; set; }
        [StringLength(1000)]
        public string DistrictId { get; set; }
        [StringLength(2000)]
        public string BlockId { get; set; }
        [StringLength(3000)]
        public string VillageId { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty("UserLocationCreatedByNavigations")]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty("UserLocationUpdatedByNavigations")]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserLocationUsers")]
        public virtual User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("UserLocationLog")]
    public partial class UserLocationLog
    {
        [Key]
        public int Id { get; set; }
        public int UserLocId { get; set; }
        public int UserId { get; set; }
        public int LocationLevel { get; set; }
        public int StateId { get; set; }
        public int? DistrictId { get; set; }
        public int? BlockId { get; set; }
        public int? VillageId { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FromDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ToDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(BlockId))]
        [InverseProperty("UserLocationLogs")]
        public virtual Block Block { get; set; }
        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty("UserLocationLogCreatedByNavigations")]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(DistrictId))]
        [InverseProperty("UserLocationLogs")]
        public virtual District District { get; set; }
        [ForeignKey(nameof(StateId))]
        [InverseProperty("UserLocationLogs")]
        public virtual State State { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty("UserLocationLogUpdatedByNavigations")]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserLocationLogUsers")]
        public virtual User User { get; set; }
        [ForeignKey(nameof(VillageId))]
        [InverseProperty("UserLocationLogs")]
        public virtual Village Village { get; set; }
    }
}

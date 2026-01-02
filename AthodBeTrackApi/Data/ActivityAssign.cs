using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("ActivityAssign")]
    public partial class ActivityAssign
    {
        [Key]
        public int AssignId { get; set; }
        public int ActivityId { get; set; }
        public int? UserId { get; set; }
        public int Status { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty("ActivityAssignCreatedByNavigations")]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty("ActivityAssignUpdatedByNavigations")]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("ActivityAssignUsers")]
        public virtual User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("Village")]
    public partial class Village
    {
        public Village()
        {
            ActivityQuestionSetUniqueIdentities = new HashSet<ActivityQuestionSetUniqueIdentity>();
            UserLocationLogs = new HashSet<UserLocationLog>();
        }

        [Key]
        public int VillageId { get; set; }
        public int BlockId { get; set; }
        public int DistrictId { get; set; }
        public int StateId { get; set; }
        [Required]
        [StringLength(50)]
        public string VillageName { get; set; }
        [Required]
        [StringLength(6)]
        public string VillageCode { get; set; }
        public int LocationType { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(BlockId))]
        [InverseProperty("Villages")]
        public virtual Block Block { get; set; }
        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.VillageCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(DistrictId))]
        [InverseProperty("Villages")]
        public virtual District District { get; set; }
        [ForeignKey(nameof(StateId))]
        [InverseProperty("Villages")]
        public virtual State State { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.VillageUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
        [InverseProperty(nameof(ActivityQuestionSetUniqueIdentity.Village))]
        public virtual ICollection<ActivityQuestionSetUniqueIdentity> ActivityQuestionSetUniqueIdentities { get; set; }
        [InverseProperty(nameof(UserLocationLog.Village))]
        public virtual ICollection<UserLocationLog> UserLocationLogs { get; set; }
    }
}

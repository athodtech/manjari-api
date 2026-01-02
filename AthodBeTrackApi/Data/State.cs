using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("State")]
    public partial class State
    {
        public State()
        {
            ActivityQuestionSetUniqueIdentities = new HashSet<ActivityQuestionSetUniqueIdentity>();
            Blocks = new HashSet<Block>();
            Districts = new HashSet<District>();
            UserLocationLogs = new HashSet<UserLocationLog>();
            Villages = new HashSet<Village>();
        }

        [Key]
        public int StateId { get; set; }
        [Required]
        [StringLength(200)]
        public string StateName { get; set; }
        [Required]
        [StringLength(2)]
        public string StateCode { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public decimal? Longitude { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public decimal? Latitude { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.StateCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.StateUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
        [InverseProperty(nameof(ActivityQuestionSetUniqueIdentity.State))]
        public virtual ICollection<ActivityQuestionSetUniqueIdentity> ActivityQuestionSetUniqueIdentities { get; set; }
        [InverseProperty(nameof(Block.State))]
        public virtual ICollection<Block> Blocks { get; set; }
        [InverseProperty(nameof(District.State))]
        public virtual ICollection<District> Districts { get; set; }
        [InverseProperty(nameof(UserLocationLog.State))]
        public virtual ICollection<UserLocationLog> UserLocationLogs { get; set; }
        [InverseProperty(nameof(Village.State))]
        public virtual ICollection<Village> Villages { get; set; }
    }
}

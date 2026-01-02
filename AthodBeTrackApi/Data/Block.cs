using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("Block")]
    public partial class Block
    {
        public Block()
        {
            ActivityQuestionSetUniqueIdentities = new HashSet<ActivityQuestionSetUniqueIdentity>();
            UserLocationLogs = new HashSet<UserLocationLog>();
            Villages = new HashSet<Village>();
        }

        [Key]
        public int BlockId { get; set; }
        public int DistrictId { get; set; }
        public int StateId { get; set; }
        [Required]
        [StringLength(50)]
        public string BlockName { get; set; }
        [Required]
        [StringLength(5)]
        public string BlockCode { get; set; }
        public int BlockType { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Longitude { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Latitude { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.BlockCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(DistrictId))]
        [InverseProperty("Blocks")]
        public virtual District District { get; set; }
        [ForeignKey(nameof(StateId))]
        [InverseProperty("Blocks")]
        public virtual State State { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.BlockUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
        [InverseProperty(nameof(ActivityQuestionSetUniqueIdentity.Block))]
        public virtual ICollection<ActivityQuestionSetUniqueIdentity> ActivityQuestionSetUniqueIdentities { get; set; }
        [InverseProperty(nameof(UserLocationLog.Block))]
        public virtual ICollection<UserLocationLog> UserLocationLogs { get; set; }
        [InverseProperty(nameof(Village.Block))]
        public virtual ICollection<Village> Villages { get; set; }
    }
}

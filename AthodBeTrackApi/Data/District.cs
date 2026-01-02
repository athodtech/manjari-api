using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("District")]
    public partial class District
    {
        public District()
        {
            ActivityQuestionSetUniqueIdentities = new HashSet<ActivityQuestionSetUniqueIdentity>();
            Blocks = new HashSet<Block>();
            UserLocationLogs = new HashSet<UserLocationLog>();
            Villages = new HashSet<Village>();
        }

        [Key]
        public int DistrictId { get; set; }
        public int StateId { get; set; }
        [Required]
        [StringLength(200)]
        public string DistrictName { get; set; }
        [Required]
        [StringLength(3)]
        public string DistrictCode { get; set; }
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
        [InverseProperty(nameof(User.DistrictCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(StateId))]
        [InverseProperty("Districts")]
        public virtual State State { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.DistrictUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
        [InverseProperty(nameof(ActivityQuestionSetUniqueIdentity.District))]
        public virtual ICollection<ActivityQuestionSetUniqueIdentity> ActivityQuestionSetUniqueIdentities { get; set; }
        [InverseProperty(nameof(Block.District))]
        public virtual ICollection<Block> Blocks { get; set; }
        [InverseProperty(nameof(UserLocationLog.District))]
        public virtual ICollection<UserLocationLog> UserLocationLogs { get; set; }
        [InverseProperty(nameof(Village.District))]
        public virtual ICollection<Village> Villages { get; set; }
    }
}

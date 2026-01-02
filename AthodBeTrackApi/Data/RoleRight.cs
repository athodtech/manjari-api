using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    public partial class RoleRight
    {
        public RoleRight()
        {
            RoleRightsAttributes = new HashSet<RoleRightsAttribute>();
        }

        [Key]
        public int RoleRightId { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public bool ShowMenu { get; set; }
        public bool CreateRight { get; set; }
        public bool ViewRight { get; set; }
        public bool EditRight { get; set; }
        public bool DeleteRight { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.RoleRightCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(MenuId))]
        [InverseProperty("RoleRights")]
        public virtual Menu Menu { get; set; }
        [ForeignKey(nameof(RoleId))]
        [InverseProperty("RoleRights")]
        public virtual Role Role { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.RoleRightUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
        [InverseProperty(nameof(RoleRightsAttribute.RoleRight))]
        public virtual ICollection<RoleRightsAttribute> RoleRightsAttributes { get; set; }
    }
}

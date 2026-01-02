using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    public partial class UserRight
    {
        public UserRight()
        {
            UserRightsAttributes = new HashSet<UserRightsAttribute>();
        }

        [Key]
        public int UserRightId { get; set; }
        public int UserId { get; set; }
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
        [InverseProperty("UserRightCreatedByNavigations")]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(MenuId))]
        [InverseProperty("UserRights")]
        public virtual Menu Menu { get; set; }
        [ForeignKey(nameof(RoleId))]
        [InverseProperty("UserRights")]
        public virtual Role Role { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty("UserRightUpdatedByNavigations")]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserRightUsers")]
        public virtual User User { get; set; }
        [InverseProperty(nameof(UserRightsAttribute.UserRight))]
        public virtual ICollection<UserRightsAttribute> UserRightsAttributes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("Role")]
    public partial class Role
    {
        public Role()
        {
            RoleRights = new HashSet<RoleRight>();
            UserRights = new HashSet<UserRight>();
            Users = new HashSet<User>();
        }

        [Key]
        public int RoleId { get; set; }
        [Required]
        [StringLength(256)]
        public string RoleName { get; set; }
        [StringLength(500)]
        public string Details { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.RoleCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.RoleUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
        [InverseProperty(nameof(RoleRight.Role))]
        public virtual ICollection<RoleRight> RoleRights { get; set; }
        [InverseProperty(nameof(UserRight.Role))]
        public virtual ICollection<UserRight> UserRights { get; set; }
        [InverseProperty(nameof(User.Role))]
        public virtual ICollection<User> Users { get; set; }
    }
}

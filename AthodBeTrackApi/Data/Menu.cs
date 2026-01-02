using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("Menu")]
    public partial class Menu
    {
        public Menu()
        {
            RoleRights = new HashSet<RoleRight>();
            UserRights = new HashSet<UserRight>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("label")]
        [StringLength(256)]
        public string Label { get; set; }
        [Column("display")]
        [StringLength(150)]
        public string Display { get; set; }
        [Column("isTitle")]
        public bool IsTitle { get; set; }
        [Column("icon")]
        [StringLength(256)]
        public string Icon { get; set; }
        [Column("link")]
        [StringLength(256)]
        public string Link { get; set; }
        [Column("parentId")]
        public int? ParentId { get; set; }
        [Column("sort")]
        public int? Sort { get; set; }
        [Column("isView")]
        public bool? IsView { get; set; }
        [Column("isCreate")]
        public bool? IsCreate { get; set; }
        [Column("isEdit")]
        public bool? IsEdit { get; set; }
        [Column("isDelete")]
        public bool? IsDelete { get; set; }
        [Column("isActive")]
        public bool IsActive { get; set; }

        [InverseProperty(nameof(RoleRight.Menu))]
        public virtual ICollection<RoleRight> RoleRights { get; set; }
        [InverseProperty(nameof(UserRight.Menu))]
        public virtual ICollection<UserRight> UserRights { get; set; }
    }
}

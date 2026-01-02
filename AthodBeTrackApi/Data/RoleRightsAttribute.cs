using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("RoleRightsAttribute")]
    public partial class RoleRightsAttribute
    {
        [Key]
        public int AttributeId { get; set; }
        public int? RoleRightId { get; set; }
        public string Attribute { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.RoleRightsAttributeCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(RoleRightId))]
        [InverseProperty("RoleRightsAttributes")]
        public virtual RoleRight RoleRight { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.RoleRightsAttributeUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
    }
}

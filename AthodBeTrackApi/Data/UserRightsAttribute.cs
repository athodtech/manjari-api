using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("UserRightsAttribute")]
    public partial class UserRightsAttribute
    {
        [Key]
        public int AttributeId { get; set; }
        public int? UserRightId { get; set; }
        public string Attribute { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.UserRightsAttributeCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.UserRightsAttributeUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UserRightId))]
        [InverseProperty("UserRightsAttributes")]
        public virtual UserRight UserRight { get; set; }
    }
}

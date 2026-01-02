using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("SystemStartingNumber")]
    [Microsoft.EntityFrameworkCore.Index(nameof(Prefix), Name = "IX_SystemStartingNumber", IsUnique = true)]
    public partial class SystemStartingNumber
    {
        public int Counter { get; set; }
        [Key]
        [StringLength(50)]
        public string TableTransaction { get; set; }
        [StringLength(50)]
        public string TableName { get; set; }
        [StringLength(128)]
        public string ColumnName { get; set; }
        [Key]
        [StringLength(7)]
        public string Prefix { get; set; }
        public int? Number { get; set; }
        public int? NumberWidth { get; set; }
        public bool? IsEnabled { get; set; }
        public bool? IsMasterFile { get; set; }
        [StringLength(50)]
        public string TransactionDescription { get; set; }
        [StringLength(50)]
        public string ParentEntity { get; set; }
        public bool? IsPostingEntity { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateCreated { get; set; }
        public int? UserCreated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateModified { get; set; }
        public int? UserModified { get; set; }

        [ForeignKey(nameof(UserCreated))]
        [InverseProperty(nameof(User.SystemStartingNumberUserCreatedNavigations))]
        public virtual User UserCreatedNavigation { get; set; }
        [ForeignKey(nameof(UserModified))]
        [InverseProperty(nameof(User.SystemStartingNumberUserModifiedNavigations))]
        public virtual User UserModifiedNavigation { get; set; }
    }
}

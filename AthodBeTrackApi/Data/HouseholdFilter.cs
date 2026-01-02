using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("HouseholdFilter")]
    public partial class HouseholdFilter
    {
        [Key]
        public int HouseholdFilterId { get; set; }
        public int UserId { get; set; }
        public int ActivityCategoryMappingId { get; set; }
        [StringLength(10)]
        public string Days { get; set; }
        [Column(TypeName = "date")]
        public DateTime? FromDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ToDate { get; set; }
        public int? StateId { get; set; }
        public int? DistrictId { get; set; }
        public int? BlockId { get; set; }
        public int? VillageId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty("HouseholdFilterCreatedByNavigations")]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty("HouseholdFilterUpdatedByNavigations")]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("HouseholdFilterUsers")]
        public virtual User User { get; set; }
    }
}

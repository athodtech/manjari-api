using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("ActivityQuestionSetUniqueIdentity")]
    public partial class ActivityQuestionSetUniqueIdentity
    {
        public ActivityQuestionSetUniqueIdentity()
        {
            ActivityDocuments = new HashSet<ActivityDocument>();
            ActivityQuestionSetGroupMappings = new HashSet<ActivityQuestionSetGroupMapping>();
        }

        [Key]
        public int ActivityQuestionSetId { get; set; }
        public int ActivityCategoryMappingId { get; set; }
        [StringLength(50)]
        public string UniqueSetCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime InterventionStartDate { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
        public int? Stateid { get; set; }
        public int? BlockId { get; set; }
        public int? DistrictId { get; set; }
        public int? VillageId { get; set; }
        [StringLength(100)]
        public string ProfileImage { get; set; }
        [StringLength(200)]
        public string StateName { get; set; }
        [StringLength(200)]
        public string DistrictName { get; set; }
        [StringLength(50)]
        public string BlockName { get; set; }
        [StringLength(50)]
        public string VillageName { get; set; }
        [StringLength(100)]
        public string Column1 { get; set; }
        [StringLength(100)]
        public string Column2 { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(ActivityCategoryMappingId))]
        [InverseProperty("ActivityQuestionSetUniqueIdentities")]
        public virtual ActivityCategoryMapping ActivityCategoryMapping { get; set; }
        [ForeignKey(nameof(BlockId))]
        [InverseProperty("ActivityQuestionSetUniqueIdentities")]
        public virtual Block Block { get; set; }
        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty("ActivityQuestionSetUniqueIdentityCreatedByNavigations")]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(DistrictId))]
        [InverseProperty("ActivityQuestionSetUniqueIdentities")]
        public virtual District District { get; set; }
        [ForeignKey(nameof(Stateid))]
        [InverseProperty("ActivityQuestionSetUniqueIdentities")]
        public virtual State State { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty("ActivityQuestionSetUniqueIdentityUpdatedByNavigations")]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("ActivityQuestionSetUniqueIdentityUsers")]
        public virtual User User { get; set; }
        [ForeignKey(nameof(VillageId))]
        [InverseProperty("ActivityQuestionSetUniqueIdentities")]
        public virtual Village Village { get; set; }
        [InverseProperty(nameof(ActivityDocument.ActivityQuestionSet))]
        public virtual ICollection<ActivityDocument> ActivityDocuments { get; set; }
        [InverseProperty(nameof(ActivityQuestionSetGroupMapping.ActivityQuestionSet))]
        public virtual ICollection<ActivityQuestionSetGroupMapping> ActivityQuestionSetGroupMappings { get; set; }
    }
}

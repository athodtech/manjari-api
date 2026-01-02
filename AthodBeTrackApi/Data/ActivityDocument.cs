using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("ActivityDocument")]
    public partial class ActivityDocument
    {
        [Key]
        public int ActivityDocumentId { get; set; }
        public int ActivityQuestionSetId { get; set; }
        [Required]
        [StringLength(200)]
        public string OriginalDocumentName { get; set; }
        [Required]
        [StringLength(200)]
        public string InternalDocumentName { get; set; }
        [Required]
        [StringLength(200)]
        public string DocumentDescription { get; set; }
        public int Sort { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(ActivityQuestionSetId))]
        [InverseProperty(nameof(ActivityQuestionSetUniqueIdentity.ActivityDocuments))]
        public virtual ActivityQuestionSetUniqueIdentity ActivityQuestionSet { get; set; }
        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.ActivityDocumentCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.ActivityDocumentUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("Tag")]
    public partial class Tag
    {
        public Tag()
        {
            ActivityDocumentTagMappings = new HashSet<ActivityDocumentTagMapping>();
            QuestionTagMappings = new HashSet<QuestionTagMapping>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.TagCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.TagUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
        [InverseProperty(nameof(ActivityDocumentTagMapping.Tag))]
        public virtual ICollection<ActivityDocumentTagMapping> ActivityDocumentTagMappings { get; set; }
        [InverseProperty(nameof(QuestionTagMapping.Tag))]
        public virtual ICollection<QuestionTagMapping> QuestionTagMappings { get; set; }
    }
}

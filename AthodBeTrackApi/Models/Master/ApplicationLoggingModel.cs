using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class ApplicationLoggingModel
    {
        public long Id { get; set; }
        public int? ActivityCategoryMappingId { get; set; }
        public int EventId { get; set; }
        [Required]
        [StringLength(500)]
        public string Message { get; set; }
        [Required]
        public byte StatusId { get; set; }
        [StringLength(50)]
        public string IpAddress { get; set; }
        [StringLength(256)]
        public string Os { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsRead { get; set; }
    }
}

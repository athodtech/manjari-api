using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class ActivityModel : BaseModel
    {
        public int ActivityId { get; set; }
        [Required]
        [StringLength(1000)]
        public string ActivityName { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        public int Status { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

    }
}

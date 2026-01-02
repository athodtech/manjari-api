using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class ActivityQuestionSetModel
    {
        [Required]
        public int activityCategoryMappingId { get; set; }
        [Required]
        public int userId { get; set; }
    }
}

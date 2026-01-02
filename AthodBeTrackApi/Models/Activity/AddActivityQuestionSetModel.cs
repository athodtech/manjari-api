using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class AddActivityQuestionSetModel
    {
        [Required]
        public int activityQuestionSetId { get; set; }
        [Required]
        public int userId { get; set; }
        [Required]
        public int flag { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class QuestionChoiceModel : BaseModel
    {
        public int QuestionChoiceId { get; set; }
        [StringLength(256)]
        [Required]
        public string QuestionChoiceName { get; set; }
    }
}

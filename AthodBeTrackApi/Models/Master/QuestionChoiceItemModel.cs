using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class QuestionChoiceItemModel:BaseModel
    {
        public int Id { get; set; }
        public int QuestionChoiceId { get; set; }
        [Required]
        [StringLength(256)]
        public string Item { get; set; }
        [Required]
        public int Value { get; set; }
        public int? Sort { get; set; }
        [StringLength(12)]
        public string ItemShotName { get; set; }
    }
}

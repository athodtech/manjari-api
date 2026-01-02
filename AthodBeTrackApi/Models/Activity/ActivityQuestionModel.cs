using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class ActivityQuestionModel : BaseModel
    {
        public int ActivityQuestionId { get; set; }
        public int ActivityId { get; set; }
        public int QuestionId { get; set; }
        public string QuestionName { get; set; }
        public string QuestionType { get; set; }
        public bool Mandatory { get; set; }
        public int? Sort { get; set; }
        public string PalceHolder { get; set; }
       
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class ActivityQuestionSetValueViewModel
    {
       
        public int activityQuestionSetId { get; set; }
       
        public int userId { get; set; }
       
        public List<QuestionSetValue> activityQuestionSetValues { get; set; }
    }
    public class QuestionSetValue
    {
        public int activityQuestionId { get; set; }
        public string questionValue { get; set; }
        public int sno { get; set; }


    }
}

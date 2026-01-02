using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class ActivityQuestionSetValueModel
    {
        public int ActivityQuestionId { get; set; }
        public string QuestionValue { get; set; }
        public int Sno { get; set; }      
    }

    public class ActivityQuestionValueModel
    {
        public int ActivityQuestionId { get; set; }
        public string QuestionValue { get; set; }
        public int Sno { get; set; }
        public bool Primary { get; set; }
        public string ReportingFrequency { get; set; }
        public int? History { get; set; }
        public int? Overdue { get; set; }
        public string LastUpdate { get; set; }




    }
}

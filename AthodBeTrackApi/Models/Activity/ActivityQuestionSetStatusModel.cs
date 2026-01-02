using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class ActivityQuestionSetStatusModel
    {
        public int status { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime interventionStartDate { get; set; }
        public DateTime? submittedOn { get; set; } = null;
    }
}
 
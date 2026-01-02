using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class ActivityQuestionForMappingModel
    {
        public int questionId { get; set; }
        public string question { get; set; }
        public int isExist { get; set; }
        public int isUsed { get; set; }
    }
}

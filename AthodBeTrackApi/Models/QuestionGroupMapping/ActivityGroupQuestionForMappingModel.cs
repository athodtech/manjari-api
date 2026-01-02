using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class ActivityGroupQuestionForMappingModel
    {
        public int questionId { get; set; }
        public int activityQuestionId { get; set; }
        public string question { get; set; }
        public string parent { get; set; }
        public string type { get; set; }
        public bool mandatory { get; set; } = false;
        public string help { get; set; }
        public int isExist { get; set; }
        public string inExist { get; set; }

    }
}

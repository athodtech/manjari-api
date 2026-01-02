using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class ActivityQuestionGroupMappingModel : BaseModel
    {
        public List<activityQuestion> activityQuestionIds { get; set; }
        public int groupId { get; set; }
    }

    public class activityQuestion
    {
        public int activityQuestionId { get; set; }

    }

}

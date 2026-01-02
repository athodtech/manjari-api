using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class GetAssignedActivity_Result
    {
        public int activityId { get; set; }
        public string loginId { get; set; }
        public string userName { get; set; }
        public string imageName { get; set; }
        public DateTime assignUnassignOn { get; set; }
        public string status { get; set; }
      
    }
}

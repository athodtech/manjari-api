using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class GetActivityQuestionSetGroup_Result
    {
        public int groupId { get; set; }
        public string groupName { get; set; }
        public int inUsed { get; set; }
        public int isEnable { get; set; }
        public string type { get; set; }
    }
}

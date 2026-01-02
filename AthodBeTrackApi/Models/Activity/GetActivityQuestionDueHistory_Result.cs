using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class GetActivityQuestionDueHistory_Result
    {
        public string Tooltip { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PstartDate { get; set; }
        public DateTime PendDate { get; set; }
        public string Entry { get; set; }
        public string ImageName { get; set; }
        public string User { get; set; }
    }
}

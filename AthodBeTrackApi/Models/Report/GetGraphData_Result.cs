using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class GetGraphData_Result
    {
        public int QuestionID { get; set; }
        public string Question { get; set; }
        public string Help { get; set; }
        public string Type { get; set; }
        public string ReportingFrequencyType { get; set; }
        public string Item { get; set; }
        public string QuestionValue { get; set; }
    }
}

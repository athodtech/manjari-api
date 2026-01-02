using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class GetQuestionTagForMapping_Result
    {
        public int questionId { get; set; }
        public string question { get; set; }
        public string parent { get; set; }
        public int isExist { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class GetQuestionTagMapping_Result
    {
        public int tagId { get; set; }
        public string tagName { get; set; }
        public string groupName { get; set; }
        public int questionId { get; set; }
        public string question { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class UpdateQuestionTagMapping_Result:BaseModel
    {
        public List<TagQuestion> questionIds { get; set; }
        public int tagId { get; set; }
    }
    public class TagQuestion
    {
        public int questionId { get; set; }

    }
}

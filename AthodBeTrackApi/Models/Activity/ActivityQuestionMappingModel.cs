using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class ActivityQuestionMappingModel
    {
        public int activityQuestionId { get; set; }
        public string question { get; set; }
        public string parent { get; set; }
        public string type { get; set; }
        public bool mandatory { get; set; } = false;
        public string help { get; set; }
        public string tagName { get; set; }
        public int? minLength { get; set; }
        public int? maxLength { get; set; }
        public int? reportingFrequency { get; set; }

    }
    public class ActivityQuestionMappingInsertModel : BaseModel
    {
        public List<Question> questionIds { get; set; }
        public int ActivityCategoryMappingId { get; set; }
    }

    public class Question
    {
        public int questionId { get; set; }

    }






}

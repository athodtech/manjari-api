using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class ActivityCategoryMappingModel:BaseModel
    {
        public List<activityCategory> categoryIds { get; set; }
        public int activityId { get; set; }
    }
    public class activityCategory
    {
        public int categoryId { get; set; }

    }
}

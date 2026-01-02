using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class ActivityCategoryForMappingModel
    {
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public string description { get; set; }
        public string iconClass { get; set; }
        public int isExist { get; set; }
        public int isUsed { get; set; }
    }
}

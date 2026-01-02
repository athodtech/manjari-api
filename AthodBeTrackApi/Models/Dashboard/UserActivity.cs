using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class UserActivity
    {
        public int activityId { get; set; }        
        public string activityName { get; set; }
        public string description { get; set; }        
        public DateTime startDate { get; set; }        
        public DateTime endDate { get; set; }
        public List<Category> categories { get; set; }

    }

    public class Category
    {
        public int categoryId { get; set; }
        public int activityId { get; set; }
        public int activityCategoryMappingId { get; set; }
        public string categoryName { get; set; }
        public int value { get; set; }
    }
}

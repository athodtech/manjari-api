using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class SelectListModel
    {
        public int id { set; get; }
        public string value { set; get; }
    }

    public class SelectList3Model
    {
        public int id { set; get; }
        public string value { set; get; }
        public bool isActive { set; get; }
    }

    public class SelectListActivityCategory
    {
        public int activityCategoryMappingId { set; get; }
        public int categoryId { set; get; }
        public string value { set; get; }
    }

}

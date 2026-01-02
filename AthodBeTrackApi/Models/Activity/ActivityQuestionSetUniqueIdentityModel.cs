using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class ActivityQuestionSetUniqueIdentityModel : BaseModel
    {
        public int ActivityQuestionSetId { get; set; }
        public int ActivityCategoryMappingId { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
        public int? Stateid { get; set; }
        public int? BlockId { get; set; }
        public int? DistrictId { get; set; }
        public int? VillageId { get; set; }
        public DateTime InterventionStartDate { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
    }
}

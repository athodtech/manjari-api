using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class HouseholdFilterModel
    {
        public int HouseholdFilterId { get; set; }
        public int UserId { get; set; }
        public int ActivityCategoryMappingId { get; set; }
        public string Days { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int? StateId { get; set; }
        public int? DistrictId { get; set; }
        public int? BlockId { get; set; }
        public int? VillageId { get; set; }

    }
}

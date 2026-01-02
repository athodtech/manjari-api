using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class RPT_GetUpdatedHousehold_Result
    {
        public int ActivityQuestionSetId { get; set; }
        public string HouseholdName { get; set; }
        public string MobileNumber { get; set; }
        public string UniqueSetCode { get; set; }
        public int Status { get; set; }
        public string DistrictName { get; set; }
        public string BlockName { get; set; }
        public string VillageName { get; set; }
        public string UpdatedOn { get; set; }
    }
}

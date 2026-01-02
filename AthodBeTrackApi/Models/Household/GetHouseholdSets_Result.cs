using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class GetHouseholdSets_Result
    {
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string HouseholdCode { get; set; }
        public string InterventionStartedOn { get; set; }
        public string ModifiedOn { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ImageName { get; set; }
        public string ProfileImage { get; set; }
        public int Status { get; set; }
        public int ActivityQuestionSetId { get; set; }
        public bool IsActive { get; set; }

    }
}

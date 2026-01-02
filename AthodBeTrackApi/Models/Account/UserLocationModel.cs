using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class UserLocationModel:BaseModel
    {
        public int UserLocId { get; set; }
        public int UserId { get; set; }
        public int LocationLevel { get; set; }
        public string StateId { get; set; }
        public string StateName { get; set; }       
        public string DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string BlockId { get; set; }
        public string BlockName { get; set; }
        public string VillageId { get; set; }
        public string VillageName { get; set; }
    }
}

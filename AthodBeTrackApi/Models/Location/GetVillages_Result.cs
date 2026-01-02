using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class GetVillages_Result
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int BlockId { get; set; }
        public string BlockName { get; set; }
        public int VillageId { get; set; }
        public string VillageName { get; set; }
        public string VillageCode { get; set; }
        public int LocationType { get; set; }
        public bool IsActive { get; set; }
    }
}

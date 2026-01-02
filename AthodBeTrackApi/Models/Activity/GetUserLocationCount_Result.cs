using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class GetUserLocationCount_Result
    {
        public int TotalState { get; set; }
        public int TotalDistrict { get; set; }
        public int TotalBlock { get; set; }
        public int TotalVillage { get; set; }
    }
}
 
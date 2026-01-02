using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class ChartModel
    {
        public int ChartTypeId { get; set; }      
        public string ChartType { get; set; }       
        public string ChartJson { get; set; }       
        public string Icon { get; set; }
    }
}

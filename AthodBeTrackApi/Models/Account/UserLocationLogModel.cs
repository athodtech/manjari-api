using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class UserLocationLogModel:BaseModel
    {
        public int Id { get; set; }     
        public int UserLocId { get; set; }
        public int UserId { get; set; }
        public int LocationLevel { get; set; }
        public int StateId { get; set; }
        public int? DistrictId { get; set; }
        public int? BlockId { get; set; }
        public int? VillageId { get; set; }
        public DateTime? FromDate { get; set; }        
        public DateTime? ToDate { get; set; }
    }
}

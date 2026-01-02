using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class VillageModel : BaseModel
    {
        public int VillageId { get; set; }
        [Required]
        [StringLength(50)]
        public string VillageName { get; set; }
        [Required]
        [StringLength(6)]
        public string VillageCode { get; set; }
        [Required]
        public int BlockId { get; set; }
        public string BlockName { get; set; }
        [Required]
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        [Required]
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int LocationType { get; set; } = 1;
    }
}

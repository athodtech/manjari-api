using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class DistrictModel : BaseModel
    {
        public int DistrictId { get; set; }
        public int StateId { get; set; }
        [StringLength(200)]
        public string StateName { get; set; }
        [Required]
        [StringLength(200)]
        public string DistrictName { get; set; }
        [Required]
        [StringLength(3)]
        public string DistrictCode { get; set; }
    }
}

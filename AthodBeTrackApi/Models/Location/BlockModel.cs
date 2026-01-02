using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class BlockModel : BaseModel
    {
        public int BlockId { get; set; }
        public int DistrictId { get; set; }
        [StringLength(200)]
        public string DistrictName { get; set; }
        public int StateId { get; set; }
        [StringLength(200)]
        public string StateName { get; set; }
        [Required]
        [StringLength(50)]
        public string BlockName { get; set; }
        [Required]
        [StringLength(5)]
        public string BlockCode { get; set; }
        public int BlockType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class StateModel : BaseModel
    {
        public int StateId { get; set; }
        [Required]
        [StringLength(200)]
        public string StateName { get; set; }
        [Required]
        [StringLength(2)]
        public string StateCode { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class RoleModel : BaseModel
    {
        public int RoleId { get; set; }
        [StringLength(256)]
        public string RoleName { get; set; }
        [StringLength(500)]
        public string Details { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class ProfileModel:BaseModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        [Required]
        public string LastName { get; set; }
        [Required]
        [StringLength(10)]
        public string MobileNo { get; set; }
        [Required]
        [StringLength(50)]
        public string EmailId { get; set; }
        [StringLength(50)]
        public string ImageName { get; set; }
        [StringLength(500)]
        public string AboutUs { get; set; }
        public string Location { get; set; }
        [StringLength(256)]
        public string Organization { get; set; }
    }
}

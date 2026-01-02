using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class EmailConfigModel : BaseModel
    {
        public int Id { get; set; }
       
        public string UserName { get; set; }
        [StringLength(100)]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        public string Server { get; set; }
        public int Port { get; set; }
        public bool SSLStatus { get; set; }
        [StringLength(200)]
        public string Signature { get; set; }
        [StringLength(100)]
        public string FriendlyName { get; set; }

        [StringLength(50)]
        public string SenderEmail { get; set; }

        public DateTime? MaintenanceDateTime { get; set; }
        public bool IsMaintenance { get; set; }
    }
}

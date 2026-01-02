using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class UserLogModel
    {
        public long UserLogId { get; set; }
        public int? UserId { get; set; }
        [StringLength(256)]
        public string SessionId { get; set; }
        public DateTime? LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        [StringLength(50)]
        public string IpAddress { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(50)]
        public string Country { get; set; }
        [StringLength(50)]
        public string Zip { get; set; }
        [StringLength(50)]
        public string Lat { get; set; }
        [StringLength(50)]
        public string Lon { get; set; }
        [StringLength(500)]
        public string UserAgent { get; set; }
        [StringLength(256)]
        public string Os { get; set; }
        [StringLength(256)]
        public string Device { get; set; }
        [StringLength(100)]
        public string DeviceType { get; set; }
        [StringLength(256)]
        public string TimeZone { get; set; }
        [StringLength(256)]
        public string Remark { get; set; }
        public DateTime CreatedOn { get; set; } = new DateTime();
    }

}

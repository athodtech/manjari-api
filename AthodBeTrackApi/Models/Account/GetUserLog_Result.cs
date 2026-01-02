using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class GetUserLog_Result
    {
        public long UserLogId { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string SessionId { get; set; }
        public DateTime? LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }     
        public string IpAddress { get; set; }     
        public string City { get; set; }     
        public string Country { get; set; }      
        public string Zip { get; set; }       
        public string Lat { get; set; }       
        public string Lon { get; set; }      
        public string UserAgent { get; set; }      
        public string Os { get; set; }     
        public string Device { get; set; }     
        public string DeviceType { get; set; }       
        public string TimeZone { get; set; }       
        public string Remark { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}

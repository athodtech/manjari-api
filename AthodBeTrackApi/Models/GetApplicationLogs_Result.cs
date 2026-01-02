using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class GetApplicationLogs_Result
    {
        public long LogId { get; set; }
        public int EventId { get; set; }
        public string Event { get; set; }
        public string Message { get; set; }
        public byte StatusId { get; set; }      
        public string IpAddress { get; set; }       
        public string Os { get; set; }
        public string CreatedBy { get; set; }
        public string ImageName { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsRead { get; set; }
        public string Prject { get; set; }
    }
}

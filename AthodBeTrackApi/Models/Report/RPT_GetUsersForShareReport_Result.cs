using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class RPT_GetUsersForShareReport_Result
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string ImageName { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public string EmailID { get; set; }
        public string Location { get; set; }
        public bool Shared { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class AssigActivityForUser_Result
    {
        public int userId { get; set; }
        public string loginId { get; set; }
        public string userName { get; set; }
        public string imageName { get; set; }
        public string userRole { get; set; }
        public int isAssigned { get; set; }
    }
}

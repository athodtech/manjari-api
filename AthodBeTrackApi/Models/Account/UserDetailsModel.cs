using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class UserDetailsModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }        
        public string UserName { get; set; }      
        public string FirstName { get; set; }       
        public string LastName { get; set; }       
        public string MobileNo { get; set; }       
        public string EmailId { get; set; }      
        public string ImageName { get; set; }
        public string AboutUs { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SessionId { get; set; }
        public string Location { get; set; }     
        public string Organization { get; set; }


    }
}

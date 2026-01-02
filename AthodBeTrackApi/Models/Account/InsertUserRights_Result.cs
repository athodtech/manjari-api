using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class InsertUserRights_Result
    {
        public List<UserRights> userRights { get; set; }
        public int roleId { get; set; }
        public int userId { get; set; }
        public int CreatedBy { get; set; }
    }

    public class UserRights
    {
        public int MenuId { get; set; }
        public bool ShowMenu { get; set; }
        public bool CreateRight { get; set; }
        public bool EditRight { get; set; }
        public bool ViewRight { get; set; }
        public bool DeleteRight { get; set; }
        public int Attribute { get; set; }
    }
}

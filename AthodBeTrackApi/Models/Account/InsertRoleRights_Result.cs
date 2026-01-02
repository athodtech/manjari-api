using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class InsertRoleRights_Result
    {
        public List<RoleRights> roleRights { get; set; }
        public int roleId { get; set; }
        public int CreatedBy { get; set; }
    }
    public class RoleRights
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

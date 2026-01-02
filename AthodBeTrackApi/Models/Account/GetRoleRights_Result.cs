using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class GetRoleRights_Result
    {
        public int MenuId { get; set; }
        public string Menu { get; set; }
        public int? ParentId { get; set; }
        public string ParentMenu { get; set; }
        public bool ShowMenu { get; set; }
        public bool CreateRightShow { get; set; }
        public bool CreateRight { get; set; }
        public bool EditRightShow { get; set; }
        public bool EditRight { get; set; }
        public bool ViewRightShow { get; set; }
        public bool ViewRight { get; set; }
        public bool DeleteRightShow { get; set; }
        public bool DeleteRight { get; set; }
        public int Attribute { get; set; }

    }
}

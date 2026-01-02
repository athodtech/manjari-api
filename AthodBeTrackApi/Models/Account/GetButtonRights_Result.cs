using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class GetButtonRights_Result
    {
        public string routerLink { get; set; }
        public bool hasCreate { get; set; }
        public bool hasEdit { get; set; }
        public bool hasView { get; set; }
        public bool hasDelete { get; set; }
    }
}

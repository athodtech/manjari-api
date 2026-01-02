using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class MenuItem
    {
        public int id { get; set; }
        public string label { get; set; }
        public bool isTitle { get; set; }
        public string icon { get; set; }
        public string link { get; set; }
        public bool isView { get; set; }
        public List<SubItem> subItems { get; set; }
    }

    public class SubItem
    {
        public int id { get; set; }
        public string label { get; set; }
        public string link { get; set; }
        public int parentId { get; set; }
        public bool isView { get; set; }
        public List<SubSubItem> subItems { get; set; }
    }

    public class SubSubItem
    {
        public int id { get; set; }
        public string label { get; set; }
        public string link { get; set; }
        public int parentId { get; set; }
        public bool isView { get; set; }
    }
}

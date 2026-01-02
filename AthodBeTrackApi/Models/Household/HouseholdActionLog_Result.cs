using System;

namespace AthodBeTrackApi.Models
{
    public class HouseholdActionLog_Result
    {
        public DateTime Date { get; set; }
        public string UserName  { get; set; }
        public string ImageName { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public int Added { get; set; }
        public int Draft { get; set; }
        public int Submitted { get; set; }
        public int Archived { get; set; }
        public int Locked { get; set; }
     
    }
}

using System;

namespace AthodBeTrackApi.Models
{
    public class HouseholdMonthlyActionLog_Result
    {
        public string Period { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ImageName { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
        public int Added { get; set; }     
        public int Transaction { get; set; }
    }
}

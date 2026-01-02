using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class RPT_GetReportLocation_Result
    {
        public List<RPT_State> states { get; set; }
        public List<RPT_District> districts { get; set; }
        public List<RPT_Block> blocks { get; set; }
        public List<RPT_Village> villages { get; set; }
        public List<RPT_Group> groups { get; set; }
        public List<RPT_Tag> tags { get; set; }
        public List<RPT_Question> questions { get; set; }
    }

    public class RPT_State
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
    }

    public class RPT_District
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
    }

    public class RPT_Block
    {
        public int BlockId { get; set; }
        public string BlockName { get; set; }
    }
    public class RPT_Village
    {
        public int VillageId { get; set; }
        public string VillageName { get; set; }
    }

    public class RPT_Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }
    public class RPT_Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
    }
    public class RPT_Question
    {
        public int Id { get; set; }
        public string Question { get; set; }
    }
}

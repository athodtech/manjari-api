using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class AssignActivity_Result : BaseModel
    {
        public List<user> userIds { get; set; }
        public int activityId { get; set; }
    }
    public class user
    {
        public int userId { get; set; }

    }
}

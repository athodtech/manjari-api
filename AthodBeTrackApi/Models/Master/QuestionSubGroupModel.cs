using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class QuestionSubGroupModel:BaseModel
    {
        public int SubGroupId { get; set; }
        public int GroupId { get; set; }  
        public string GroupName { get; set; }        
        [Required]
        [StringLength(500)]
        public string SubGroupName { get; set; }
    }
}

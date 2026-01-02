using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class QuestionGroupModel:BaseModel
    {
        public int GroupId { get; set; }
        [Required]
        [StringLength(250)]
        public string GroupName { get; set; }
        public int? ParentGroupId { get; set; }
        public int? SortingOrder { get; set; }
    }
}

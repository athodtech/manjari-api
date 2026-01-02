using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class ShareReport
    {
        [Required]
        public int ReportId { get; set; }
        [Required]
        public string ReportNo { get; set; }

        [Required]
        public DateTime SharedOn { get; set; }
        [Required]
        public int SharedBy { get; set; }
        [Required]
        public string SharedByName { get; set; }
        public List<SharedUser> SharedUsers { get; set; }
    }

    public class SharedUser
    {
        public int userId { get; set; }
        public bool check { get; set; }
        public bool shared { get; set; }
    }
}

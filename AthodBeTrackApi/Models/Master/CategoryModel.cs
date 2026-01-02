using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class CategoryModel : BaseModel
    {
        public int CategoryId { get; set; }
        [Required]
        [StringLength(1000)]
        public string CategoryName { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        public int Status { get; set; } = 1;
        [StringLength(150)]
        public string IconClass { get; set; }
    }
}

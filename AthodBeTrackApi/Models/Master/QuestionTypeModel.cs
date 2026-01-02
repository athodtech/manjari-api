using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class QuestionTypeModel:BaseModel
    {
        public int Id { get; set; }       
        public string Type { get; set; }
        public int Sort { get; set; }
    }
}

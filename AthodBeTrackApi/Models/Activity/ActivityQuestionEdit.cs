using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class ActivityQuestionEdit:BaseModel
    {
        public int activityQuestionId { get; set; }
        public bool mandatory { get; set; }
        public int? sort { get; set; }
        public int? minLength { get; set; }
        public int? maxLength { get; set; }        
        public string constraint{ get; set; }
        public bool? tableDisplayColumn { get; set; }
        public int? reportingFrequencyTypeId { get; set; }
        public bool? timeStampManual { get; set; }
        public string help { get; set; }
    }
}

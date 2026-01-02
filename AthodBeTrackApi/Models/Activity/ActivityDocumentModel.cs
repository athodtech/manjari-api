using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class ActivityDocumentModel
    {
        public int activityQuestionSetId { get; set; }
        public string tagIds { get; set; }
        public string originalDocumentName { get; set; }
        public string internalDocumentName { get; set; }
        public string documentDescription { get; set; }
        public int sort { get; set; }
        public bool isActive { get; set; }
        public int createdBy { get; set; }
    }
}

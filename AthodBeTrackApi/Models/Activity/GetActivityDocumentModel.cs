using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class GetActivityDocumentModel
    {
        public int ActivityDocumentId { get; set; }
        public int ActivityQuestionSetId { get; set; }
        public string OriginalDocumentName { get; set; }
        public string InternalDocumentName { get; set; }
        public string DocumentDescription { get; set; }
        public string TagName { get; set; }
    }
}

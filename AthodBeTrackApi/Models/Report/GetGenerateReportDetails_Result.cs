using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class GetGenerateReportDetails_Result
    {
        public int ReportId { get; set; }
        public int DownloadedBy { get; set; }
        public string UserName { get; set; }
        public string ImageName { get; set; }
        public string QuestionIds { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public DateTime DownloadTime { get; set; }

    }
}

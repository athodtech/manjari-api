using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class ChartTemplate
    {
        public int ReportId { get; set; }
        public List<ReportChartTemplate> ChartTemplates { get; set; }
        public List<ReportItemTemplate> ItemTemplates { get; set; }
        public int UserId { get; set; }    
     
    }

    public class ReportChartTemplate
    {
        public int QuestionId { get; set; }
        public int? ChartTypeId { get; set; }
        public string XaxisName { get; set; }
        public string YaxisName { get; set; }
        public string Question { get; set; }
        public string OriginalQuestion { get; set; }
        public string Help { get; set; }
        public decimal? SortingOrder { get; set; }
        public decimal? OriginalSortingOrder { get; set; }
        public decimal? ChartWidth { get; set; }
        public decimal? ChartHeight { get; set; }
        public int? ColorPalette { get; set; }
        public string ChartSize { get; set; }
    }

    public class ReportItemTemplate
    {
        public int QuestionId { get; set; }
        public string Item { get; set; }
    }

}

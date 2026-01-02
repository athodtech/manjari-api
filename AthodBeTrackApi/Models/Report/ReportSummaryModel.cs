using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class ReportSummaryModel
    {
        public int ReportId { get; set; }
        public int ActivityCategoryMappingId { get; set; }
        public int ActivityQuestionId { get; set; }
        public string Question { get; set; }
        public string Help { get; set; }
        public string RevisedQuestion { get; set; }
        public string RevisedHelp { get; set; }
        public string Type { get; set; }
        public string ReportingFrequencyType { get; set; }
        public decimal? SortingOrder { get; set; }
        public decimal? OriginalSortingOrder { get; set; }
        public decimal? RevisedSortingOrder { get; set; }
        public int? ChartTypeId { get; set; }
        public int? RevisedChartTypeId { get; set; }
        public string ChartJson { get; set; }
        public string ItemShortName { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Item { get; set; }
        public string QuestionValue { get; set; }
        public int TotalHousehold { get; set; }       
        public int AffectedHousehold { get; set; }
        public int CreatedBy { get; set; }
        public string XaxisName { get; set; }
        public string YaxisName { get; set; }
        public string RevisedXaxisName { get; set; }
        public string RevisedYaxisName { get; set; }
        public int? ColorPalette { get; set; }
        public int? RevisedColorPalette { get; set; }
        public string ChartSize { get; set; }
        public string RevisedChartSize { get; set; }

    }

    public class ReportSummaryViewModel
    {
        public int ReportId { get; set; }
        public int CreatedBy { get; set; }
      
        public int TotalHousehold { get; set; }
        public int AffectedHousehold { get; set; }
        public List<Questions> questions { get; set; }
    }
    public class Questions
    {
        public int ActivityQuestionId { get; set; }
        public string Question { get; set; }
        public string Help { get; set; }
        public string XaxisName { get; set; }
        public string YaxisName { get; set; }
        public string RevisedXaxisName { get; set; }
        public string RevisedYaxisName { get; set; }
        public int? ColorPalette { get; set; }
        public int? RevisedColorPalette { get; set; }
        public string RevisedQuestion { get; set; }
        public string RevisedHelp { get; set; }
        public string Type { get; set; }
        public string ReportingFrequencyType { get; set; }
        public decimal? SortingOrder { get; set; }
        public decimal? OriginalSortingOrder { get; set; }
        public decimal? RevisedSortingOrder { get; set; }
        public int? ChartTypeId { get; set; }
        public int? RevisedChartTypeId { get; set; }
        public string ChartSize { get; set; }
        public string RevisedChartSize { get; set; }
        public string ChartJson { get; set; }
        public DateTime LastUpdated { get; set; }      
        public List<ChartValues> chartValues { get; set; }
    }
    public class ChartValues
    {
        public string Item { get; set; }
        public string ItemShortName { get; set; }
        public string QuestionValue { get; set; }

    }

}

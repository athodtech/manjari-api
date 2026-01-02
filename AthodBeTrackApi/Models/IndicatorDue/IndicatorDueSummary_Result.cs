namespace AthodBeTrackApi.Models
{
    public class IndicatorDueSummary_Result
    {
        public int QuestionId { get; set; }
        public int ActivityCategoryMappingId { get; set; }
        public int ActivityQuestionId { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string Question { get; set; }
        public string Help { get; set; }
        public int ReportingFrequecyTypeId { get; set; }
        public string ReportingFrequecyType { get; set; }
        public int Dues { get; set; }
    }
}

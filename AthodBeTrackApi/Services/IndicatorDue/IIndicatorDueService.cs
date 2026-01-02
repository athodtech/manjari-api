using AthodBeTrackApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public interface IIndicatorDueService
    {
        Task<List<DueReportSummaryModel>> GetDueReportSummary(int activityCategoryMappingId, int groupId, int reportingFrequecyTypeId);
        Task<List<IndicatorDueSummary_Result>> GetIndicatorDueSummary(int userId, int activityCategoryMappingId, int? stateId, int? districtId, int? blockId, int? villageId, int? groupId, int? reportingFrequencyId, string status);
        Task<int> GetIndicatorDueDetailsCountDynamic(int userId, int? stateId, int? districtId, int? blockId, int? villageId, int activityCategoryMappingId, int ActivityQuestionId, int? groupId, string status, string searchText);
        Task<List<DueReportDetailDynamic_Result>> GetIndicatorDueDetailsDynamic(int userId, int? stateId, int? districtId, int? blockId, int? villageId, int activityCategoryMappingId, int ActivityQuestionId, int? groupId, string status, int start, int length, string orderBy, string searchText);
        Task<List<DueReportDetailDynamic_Result>> GetIndicatorDueDetailsExport(int userId, int? stateId, int? districtId, int? blockId, int? villageId, int activityCategoryMappingId, int ActivityQuestionId, int? groupId, string status);
        Task RefreshDueIndicator();
        Task<DateTime?> GetLastUpdateDueIndicator();
        Task<bool> CheckDueIndicatorReportGenerated();
    }
}
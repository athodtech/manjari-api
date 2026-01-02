using System.Data;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Repositories
{
    public interface IIndicatorDueRepository
    {
        Task<bool> CheckDueIndicatorReportGeneratedAsync();
        Task<DataTable> GetIndicatorDueDetailsCountDynamic(int userId, int? stateId, int? districtId, int? blockId, int? villageId, int activityCategoryMappingId, int ActivityQuestionId, int? groupId, string status, string where);
        Task<DataTable> GetIndicatorDueDetailsDynamic(int userId, int? stateId, int? districtId, int? blockId, int? villageId, int activityCategoryMappingId, int ActivityQuestionId, int? groupId, string status, string where, string limitQuery);
        Task<DataTable> GetIndicatorDueSummary(int userId, int activityCategoryMappingId, int? stateId, int? districtId, int? blockId, int? villageId, int? groupId, int? reportingFrequencyId, string status);
        Task RefreshDueIndicator();
    }
}
using System.Data;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Repositories
{
    public interface IMasterRepository
    {
        Task DeleteQuestionTagMappingAsync(int questionId);
        Task<DataTable> GetApplicationLogsAsync(string activityCategoryIds, string userIds, string strwhere);      
        Task<DataTable> GetFilterVillageCountAsync(string where);
        DataTable GetQuestionBank(int? questionId);
        Task<DataTable> GetUserLoationDetailsAsync(int userId, string stateIds, string districtIds, string blockIds, string villageIds, string whereClause, string limitQuery);
        Task<DataTable> GetUserLoationDetailsCountAsync(int userId, string stateIds, string districtIds, string blockIds, string villageIds, string whereClause);
        Task<DataTable> GetVillagesAsync(string where);
    }
}
using AthodBeTrackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public interface IMasterService
    {
        Task<bool> AddLogsAsync(ApplicationLoggingModel model);
        Task<List<GetApplicationLogs_Result>> GetApplicationLogsAsync(string activityCategoryIds, string userIds, string strwhere);
        Task<List<QuestionTypeModel>> GetQuestionTypeAsync();
        Task<bool> ReadLog(int userId, int logId, bool isRead);
    }
}
using AthodBeTrackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public interface IActivityQuestionService
    {
        Task<bool> AddAsync(List<ActivityQuestionModel> model);
        Task DeleteAsync(int id);
        Task<List<ActivityQuestionModel>> GetAsync(int? activityId);
        Task<List<ActivityQuestionModel>> GetAssignActivityQuestion(int activityId);
        Task<ActivityQuestionModel> GetByIdAsync(int id);
        Task<bool> UpdateAsync(ActivityQuestionModel request);
        Task<List<ActivityGroupQuestionModel>> GetActivityGroupQuestion(int groupId, int activityCategoryMappingId);
        Task<List<ActivityGroupQuestionForMappingModel>> GetActivityGroupQuestionForMapping(int groupId, int activityCategoryMappingId);
        Task<bool> AddActivityQuestionGroupMapping(ActivityQuestionGroupMappingModel request);
        Task<List<ActivityQuestionMappingModel>> GetActivityQuestion(int activityCategoryMappingId);
        Task<List<ActivityQuestionForMappingModel>> GetActivityQuestionForMapping(int activityCategoryMappingId);
        Task<bool> AddActivityQuestion(ActivityQuestionMappingInsertModel request);
        Task<ActivityQuestionEdit> GetActivityQuestionForEdit(int activityQuestionId);
        Task<bool> UpdateActivityQuestion(ActivityQuestionEdit model);
    }
}
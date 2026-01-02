using AthodBeTrackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public interface IActivityService
    {
        Task<bool> AddActivityCategoryMapping(ActivityCategoryMappingModel request);
        Task<bool> AddAsync(ActivityModel model);
        Task<bool> AssignActivity(AssignActivity_Result request);
        Task DeleteAsync(int id);
        Task<List<ActivityCategoryForMappingModel>> GetActivityCategoryForMapping(int groupId);
        List<dynamic> GetActivityCategoryMapping(int activityId);
        Task<List<AssigActivityForUser_Result>> GetAssignActivityForUser(int activityId);
        Task<List<GetAssignedActivity_Result>> GetAssignedActivity(int activityId);
        Task<List<ActivityModel>> GetAsync();
        Task<ActivityModel> GetByIdAsync(int id);
        Task<bool> UpdateAsync(ActivityModel request);
        Task<bool> UpdateHouseholdProfileImage(int activityQuestionSetId, string profileImage);
    }
}
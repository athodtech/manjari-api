using System.Collections.Generic;
using System.Data;

namespace AthodBeTrackApi.Repositories
{
    public interface IActivityRepository
    {
        DataTable GetActivityGroupQuestionForMapping(int groupId, int activityCategoryMappingId);
        DataTable GetAssignActivityQuestion(int? activityId);
        DataTable GetActivityGroupQuestion(int groupId, int activityCategoryMappingId);
        bool UpdateActivityQuestionGroupMapping(int groupId, int userId, DataTable ActivityQuestionGroupMapping);
        List<dynamic> GetActivityCategoryMapping(int activityId);
        DataTable GetActivityCategoryForMapping(int activityId);
        bool UpdateActivityCategoryMapping(int activityId, int userId, DataTable ActivityQuestionGroupMapping);
        DataTable GetActivityQuestion(int activityCategoryMappingId);
        DataTable GetActivityQuestionForMapping(int activityCategoryMappingId);
        bool UpdateActivityQuestion(int activityCategoryMappingId, int userId, DataTable ActivityQuestionGroupMapping);
        DataTable GetAssignedActivity(int activityId);
        bool AssignActivity(int activityId, int userId, DataTable userIds);
        DataTable GetAssignActivityForUser(int activityId);
    }
}
using System.Data;

namespace AthodBeTrackApi.Repositories
{
    public interface IDashboardRepository
    {
        DataTable GetUserAssignedActivity(int userId);
        DataTable GetUserAssignedActivityCategory(int userId, int activityId);
    }
}
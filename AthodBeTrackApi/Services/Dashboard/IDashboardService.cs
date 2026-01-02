using AthodBeTrackApi.Models;
using System.Collections.Generic;

namespace AthodBeTrackApi.Services
{
    public interface IDashboardService
    {
        List<UserActivity> GetUserAssignedActivity(int userId);
    }
}
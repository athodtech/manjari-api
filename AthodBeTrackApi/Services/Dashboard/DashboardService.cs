using AthodBeTrackApi.Models;
using AthodBeTrackApi.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IMapper _mapper;
        private readonly IDashboardRepository _dashboardRepository;
        public DashboardService(IMapper mapper, IDashboardRepository dashboardRepository)
        {
            _mapper = mapper;
            _dashboardRepository = dashboardRepository;
        }

        public List<UserActivity> GetUserAssignedActivity(int userId)
        {
            try
            {
                var userActivities = new List<UserActivity>();
                var activityDt = _dashboardRepository.GetUserAssignedActivity(userId);
                if (activityDt != null && activityDt.Rows.Count > 0)
                {
                    for (int i = 0; i < activityDt.Rows.Count; i++)
                    {
                        var activity = new UserActivity();
                        activity.categories = new List<Category>();
                        activity.activityId = Convert.ToInt32(activityDt.Rows[i]["ActivityId"]);
                        activity.activityName = Convert.ToString(activityDt.Rows[i]["ActivityName"]);
                        activity.description = Convert.ToString(activityDt.Rows[i]["Description"]);
                        activity.startDate = Convert.ToDateTime(activityDt.Rows[i]["StartDate"]);
                        activity.endDate = Convert.ToDateTime(activityDt.Rows[i]["EndDate"]);

                        var categoryDt = _dashboardRepository.GetUserAssignedActivityCategory(userId, activity.activityId);
                        activity.categories = ExtensionMethods.ConvertToList<Category>(categoryDt);

                        userActivities.Add(activity);
                    }
                }
                return userActivities;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

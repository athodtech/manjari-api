using AthodBeTrackApi.Data;
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
    public class ActivityService : IActivityService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _genericRepository;
        private readonly IActivityRepository _activityRepository;
        public ActivityService(IMapper mapper, IGenericRepository genericRepository, IActivityRepository activityRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
            _activityRepository = activityRepository;
        }

        public async Task<List<ActivityModel>> GetAsync()
        {
            try
            {
                var surveys = await _genericRepository.GetAsync<Activity>();
                return _mapper.Map<List<ActivityModel>>(surveys);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ActivityModel> GetByIdAsync(int id)
        {
            try
            {
                var survey = await _genericRepository.GetByIDAsync<Activity>(id);
                return _mapper.Map<ActivityModel>(survey);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddAsync(ActivityModel model)
        {
            try
            {
                var survey = _mapper.Map<Activity>(model);
                var res = await _genericRepository.InsertAsync(survey);
                if (res > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<bool> UpdateAsync(ActivityModel request)
        {
            try
            {
                var survey = await _genericRepository.GetByIDAsync<Activity>(request.ActivityId);
                survey.ActivityName = request.ActivityName;
                survey.Description = request.Description;
                survey.StartDate = request.StartDate;
                survey.EndDate = request.EndDate;
                survey.Status = request.Status;
                survey.IsActive = request.IsActive;
                survey.UpdatedOn = request.UpdatedOn;
                survey.UpdatedBy = request.UpdatedBy;
                await _genericRepository.UpdateAsync(survey);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _genericRepository.DeleteAsync<Activity>(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<dynamic> GetActivityCategoryMapping(int activityId)
        {
            try
            {
                return _activityRepository.GetActivityCategoryMapping(activityId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<ActivityCategoryForMappingModel>> GetActivityCategoryForMapping(int activityId)
        {
            try
            {
                var dataTable = await Task.FromResult(_activityRepository.GetActivityCategoryForMapping(activityId));
                return ExtensionMethods.ConvertToList<ActivityCategoryForMappingModel>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddActivityCategoryMapping(ActivityCategoryMappingModel request)
        {
            try
            {
                var activityCategoryMapping = ExtensionMethods.ToDataTable(request.categoryIds);
                activityCategoryMapping.TableName = "ActivityCategoryMapping";
                await Task.FromResult(_activityRepository.UpdateActivityCategoryMapping
                    (request.activityId, request.CreatedBy, activityCategoryMapping));
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<GetAssignedActivity_Result>> GetAssignedActivity(int activityId)
        {
            try
            {
                var activites = await Task.FromResult(_activityRepository.GetAssignedActivity(activityId));
                return ExtensionMethods.ConvertToList<GetAssignedActivity_Result>(activites);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<AssigActivityForUser_Result>> GetAssignActivityForUser(int activityId)
        {
            try
            {
                var activites = await Task.FromResult(_activityRepository.GetAssignActivityForUser(activityId));
                return ExtensionMethods.ConvertToList<AssigActivityForUser_Result>(activites);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AssignActivity(AssignActivity_Result request)
        {
            try
            {
                var assignActivity = ExtensionMethods.ToDataTable(request.userIds);
                assignActivity.TableName = "AssignActivity";
                await Task.FromResult(_activityRepository.AssignActivity
                    (request.activityId, request.CreatedBy, assignActivity));
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateHouseholdProfileImage(int activityQuestionSetId, string profileImage)
        {
            try
            {
                var activityQuestionSetUnique = await _genericRepository.GetByIDAsync<ActivityQuestionSetUniqueIdentity>(activityQuestionSetId);
                if (activityQuestionSetUnique != null)
                {
                    activityQuestionSetUnique.ProfileImage = profileImage;

                    await _genericRepository.UpdateAsync<ActivityQuestionSetUniqueIdentity>(activityQuestionSetUnique);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

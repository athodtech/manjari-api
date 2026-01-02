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
    public class ActivityQuestionService : IActivityQuestionService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _genericRepository;
        private readonly IActivityRepository _activityRepository;
        public ActivityQuestionService(IMapper mapper, IGenericRepository genericRepository, IActivityRepository activityRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
            _activityRepository = activityRepository;
        }

        public async Task<List<ActivityQuestionModel>> GetAsync(int? activityCategoryMappingId)
        {
            try
            {
                var surveyQuestions = await Task.FromResult(_genericRepository.GetIQueryable<ActivityQuestion>(x => activityCategoryMappingId.HasValue ? x.ActivityCategoryMappingId == activityCategoryMappingId.Value : (1 > 0)).Include(q => q.Question).Include(t => t.Question.QuestionType));
                return _mapper.Map<List<ActivityQuestionModel>>(surveyQuestions);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ActivityQuestionModel>> GetAssignActivityQuestion(int surveyId)
        {
            try
            {
                var dataTable = await Task.FromResult(_activityRepository.GetAssignActivityQuestion(surveyId));
                return ExtensionMethods.ConvertToList<ActivityQuestionModel>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<ActivityQuestionModel> GetByIdAsync(int id)
        {
            try
            {
                var surveyQuestion = await _genericRepository.GetByIDAsync<ActivityQuestion>(id);
                return _mapper.Map<ActivityQuestionModel>(surveyQuestion);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddAsync(List<ActivityQuestionModel> model)
        {
            try
            {
                var surveyQuestions = _mapper.Map<List<ActivityQuestion>>(model);
                surveyQuestions.ForEach(c => c.Question = null);
                var result = await _genericRepository.AddMultipleEntityAsync(surveyQuestions);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<bool> UpdateAsync(ActivityQuestionModel request)
        {
            try
            {
                var question = await _genericRepository.GetByIDAsync<ActivityQuestion>(request.ActivityQuestionId);
                question.Sort = request.Sort;
                question.Mandatory = request.Mandatory;
                question.IsActive = request.IsActive;
                question.UpdatedOn = request.UpdatedOn;
                question.UpdatedBy = request.UpdatedBy;
                await _genericRepository.UpdateAsync(question);
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
                await _genericRepository.DeleteAsync<ActivityQuestion>(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ActivityGroupQuestionModel>> GetActivityGroupQuestion(int groupId, int activityCategoryMappingId)
        {
            try
            {
                var dataTable = await Task.FromResult(_activityRepository.GetActivityGroupQuestion(groupId, activityCategoryMappingId));
                return ExtensionMethods.ConvertToList<ActivityGroupQuestionModel>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<ActivityGroupQuestionForMappingModel>> GetActivityGroupQuestionForMapping(int groupId, int activityCategoryMappingId)
        {
            try
            {
                var dataTable = await Task.FromResult(_activityRepository.GetActivityGroupQuestionForMapping(groupId, activityCategoryMappingId));
                return ExtensionMethods.ConvertToList<ActivityGroupQuestionForMappingModel>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddActivityQuestionGroupMapping(ActivityQuestionGroupMappingModel request)
        {
            try
            {
                var ActivityQuestionGroupMapping = ExtensionMethods.ToDataTable(request.activityQuestionIds);
                ActivityQuestionGroupMapping.TableName = "ActivityQuestionGroupMapping";
                await Task.FromResult(_activityRepository.UpdateActivityQuestionGroupMapping
                    (request.groupId, request.CreatedBy, ActivityQuestionGroupMapping));
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ActivityQuestionMappingModel>> GetActivityQuestion(int activityCategoryMappingId)
        {
            try
            {
                var dataTable = await Task.FromResult(_activityRepository.GetActivityQuestion(activityCategoryMappingId));
                return ExtensionMethods.ConvertToList<ActivityQuestionMappingModel>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<ActivityQuestionForMappingModel>> GetActivityQuestionForMapping(int activityCategoryMappingId)
        {
            try
            {
                var dataTable = await Task.FromResult(_activityRepository.GetActivityQuestionForMapping(activityCategoryMappingId));
                return ExtensionMethods.ConvertToList<ActivityQuestionForMappingModel>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddActivityQuestion(ActivityQuestionMappingInsertModel request)
        {
            try
            {
                var activityQuestion = ExtensionMethods.ToDataTable(request.questionIds);
                activityQuestion.TableName = "ActivityQuestion";
                await Task.FromResult(_activityRepository.UpdateActivityQuestion
                    (request.ActivityCategoryMappingId, request.CreatedBy, activityQuestion));
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ActivityQuestionEdit> GetActivityQuestionForEdit(int activityQuestionId)
        {
            try
            {
                var res = new ActivityQuestionEdit();
                var activityQuestion = await _genericRepository.GetByIDAsync<ActivityQuestion>(activityQuestionId);
                if (activityQuestion != null)
                {
                    res.activityQuestionId = activityQuestion.ActivityQuestionId;
                    res.mandatory = activityQuestion.Mandatory;
                    res.sort = activityQuestion.Sort;
                    res.minLength = activityQuestion.MinLength;
                    res.maxLength = activityQuestion.MaxLength;
                    res.constraint = activityQuestion.Constraint;
                    res.tableDisplayColumn = activityQuestion.TableDisplayColumn;
                    res.reportingFrequencyTypeId = activityQuestion.ReportingFrequencyTypeId;
                    res.timeStampManual = activityQuestion.TimeStampManual;
                    res.help = activityQuestion.Help;
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateActivityQuestion(ActivityQuestionEdit model)
        {
            try
            {
                var activityQuestion = await _genericRepository.GetByIDAsync<ActivityQuestion>(model.activityQuestionId);
                if (activityQuestion != null)
                {
                    activityQuestion.Mandatory = model.mandatory;
                    activityQuestion.Sort = model.sort;
                    activityQuestion.MinLength = model.minLength;
                    activityQuestion.MaxLength = model.maxLength;
                    activityQuestion.Constraint = model.constraint;
                    activityQuestion.TableDisplayColumn = model.tableDisplayColumn;
                    activityQuestion.ReportingFrequencyTypeId = model.reportingFrequencyTypeId;
                    activityQuestion.TimeStampManual = model.timeStampManual;
                    activityQuestion.Help = model.help;
                    activityQuestion.UpdatedBy = model.UpdatedBy;
                    activityQuestion.UpdatedOn = model.UpdatedOn;

                    await _genericRepository.UpdateAsync<ActivityQuestion>(activityQuestion);
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }

    }
}

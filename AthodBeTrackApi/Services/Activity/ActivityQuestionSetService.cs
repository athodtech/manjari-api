using AthodBeTrackApi.Data;
using AthodBeTrackApi.Models;
using AthodBeTrackApi.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;


namespace AthodBeTrackApi.Services
{
    public class ActivityQuestionSetService : IActivityQuestionSetService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _genericRepository;
        private readonly IActivityQuestionSetRepository _questionSetRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ActivityQuestionSetService(IMapper mapper, IGenericRepository genericRepository, IActivityQuestionSetRepository questionSetRepository, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
            _questionSetRepository = questionSetRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public string GetActivityQuestionSet(int activityCategoryMappingId, int userId, string days, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                string JSONString = string.Empty;
                var dataTable = _questionSetRepository.GetActivityQuestionSet(activityCategoryMappingId, userId, days, fromDate, toDate);
                JSONString = JsonConvert.SerializeObject(dataTable);
                return JSONString;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetActivityQuestionSetWithLocationFilter(int activityCategoryMappingId, int userId, string days, DateTime? fromDate, DateTime? toDate, string stateId, string districtId, string blockId, string villageId)
        {
            try
            {
                string JSONString = string.Empty;
                var dataTable = _questionSetRepository.GetActivityQuestionSet(activityCategoryMappingId, userId, days, fromDate, toDate, stateId, districtId, blockId, villageId);
                JSONString = JsonConvert.SerializeObject(dataTable);
                return JSONString;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<GetHouseholdSets_Result>> GetHouseholdSets(int activityCategoryMappingId, string days, DateTime? fromDate, DateTime? toDate, int start, int length, string orderBy, string searchText, string lFilter, int userId)
        {
            try
            {
                string whereClause = "";
                string completeQuery = "";
                string limitQuery = "" + orderBy + " OFFSET " + start + " ROWS FETCH FIRST " + length + " ROWS ONLY";
                if (!string.IsNullOrEmpty(searchText))
                {
                    whereClause = " WHERE [name] like '%" + searchText + "%' or MobileNumber like '%" + searchText + "%' or HouseholdCode like '%" + searchText + "%' or InterventionStartedOn like '%" + searchText + "%' or ModifiedOn like '%" + searchText + "%' or [Status] like '%" + searchText + "%' or [CreatedOn] like '%" + searchText + "%' or [CreatedBy] like '%" + searchText + "%'";
                    completeQuery = string.Format("{0}", whereClause);
                }

                var dataTable = await _questionSetRepository.GetHouseholdSetsAsync(activityCategoryMappingId, days, fromDate, toDate, lFilter, whereClause, limitQuery, userId);

                return ExtensionMethods.ConvertToList<GetHouseholdSets_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> GetHouseholdSetsCount(int activityCategoryMappingId, string days, DateTime? fromDate, DateTime? toDate, string searchText, string lFilter, int userId)
        {
            int recordsFiltered = 0;
            try
            {
                string whereClause = "";

                if (!string.IsNullOrEmpty(searchText))
                {
                    whereClause = " WHERE [name] like '%" + searchText + "%' or MobileNumber like '%" + searchText + "%' or HouseholdCode like '%" + searchText + "%' or InterventionStartedOn like '%" + searchText + "%' or ModifiedOn like '%" + searchText + "%' or [Status] like '%" + searchText + "%' or [CreatedOn] like '%" + searchText + "%' or [CreatedBy] like '%" + searchText + "%'";
                }

                var dataTable = await _questionSetRepository.GetHouseholdSetsCountAsync(activityCategoryMappingId, days, fromDate, toDate, lFilter, whereClause, userId);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    recordsFiltered = Convert.ToString(dataTable.Rows[0][0]) == "" ? 0 : Convert.ToInt32(dataTable.Rows[0][0]);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return recordsFiltered;
        }


        public async Task<List<GetHouseholdSets_Result>> GetHouseholdDeletedSets(int activityCategoryMappingId, string days, DateTime? fromDate, DateTime? toDate, int start, int length, string orderBy, string searchText, string lFilter, int userId)
        {
            try
            {
                string whereClause = "";
                string completeQuery = "";
                string limitQuery = "" + orderBy + " OFFSET " + start + " ROWS FETCH FIRST " + length + " ROWS ONLY";
                if (!string.IsNullOrEmpty(searchText))
                {
                    whereClause = " WHERE [name] like '%" + searchText + "%' or MobileNumber like '%" + searchText + "%' or HouseholdCode like '%" + searchText + "%' or InterventionStartedOn like '%" + searchText + "%' or ModifiedOn like '%" + searchText + "%' or [Status] like '%" + searchText + "%' or [CreatedOn] like '%" + searchText + "%' or [CreatedBy] like '%" + searchText + "%'";
                    completeQuery = string.Format("{0}", whereClause);
                }

                var dataTable = await _questionSetRepository.GetHouseholdDeletedSetsAsync(activityCategoryMappingId, days, fromDate, toDate, lFilter, whereClause, limitQuery, userId);

                return ExtensionMethods.ConvertToList<GetHouseholdSets_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> GetHouseholdDeletedSetsCount(int activityCategoryMappingId, string days, DateTime? fromDate, DateTime? toDate, string searchText, string lFilter, int userId)
        {
            int recordsFiltered = 0;
            try
            {
                string whereClause = "";

                if (!string.IsNullOrEmpty(searchText))
                {
                    whereClause = " WHERE [name] like '%" + searchText + "%' or MobileNumber like '%" + searchText + "%' or HouseholdCode like '%" + searchText + "%' or InterventionStartedOn like '%" + searchText + "%' or ModifiedOn like '%" + searchText + "%' or [Status] like '%" + searchText + "%' or [CreatedOn] like '%" + searchText + "%' or [CreatedBy] like '%" + searchText + "%'";
                }

                var dataTable = await _questionSetRepository.GetHouseholdDeletedSetsCountAsync(activityCategoryMappingId, days, fromDate, toDate, lFilter, whereClause, userId);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    recordsFiltered = Convert.ToString(dataTable.Rows[0][0]) == "" ? 0 : Convert.ToInt32(dataTable.Rows[0][0]);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return recordsFiltered;
        }

        public async Task<List<GetHouseholdSets_Result>> ExportHouseholdSetsAsync(int activityCategoryMappingId, string days, DateTime? fromDate, DateTime? toDate, string lFilter, int userId)
        {
            try
            {
                try
                {
                    var dataTable = await _questionSetRepository.GetHouseholdSetsAsync(activityCategoryMappingId, days, fromDate, toDate, lFilter, "", "", userId);
                    return ExtensionMethods.ConvertToList<GetHouseholdSets_Result>(dataTable);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateActivityQuestionSetValue(ActivityQuestionSetValueViewModel setValueModel)
        {
            try
            {
                var dataTable = ExtensionMethods.ToDataTable(setValueModel.activityQuestionSetValues);
                dataTable.TableName = "ActivityQuestionSetValue";
                _questionSetRepository.UpdateActivityQuestionSetValue(setValueModel.activityQuestionSetId, setValueModel.userId, dataTable);
                return true;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ActivityQuestionSetValueModel> GetActivityQuestionSets(int activityQuestionSetId, int flag)
        {
            try
            {
                var dataTable = _questionSetRepository.GetActivityQuestionSets(activityQuestionSetId, flag);
                return ExtensionMethods.ConvertToList<ActivityQuestionSetValueModel>(dataTable);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ActivityQuestionValueModel> GetActivityQuestionValueSet(int activityQuestionSetId, int flag)
        {
            try
            {
                var dataTable = _questionSetRepository.GetActivityQuestionValueSet(activityQuestionSetId, flag);
                return ExtensionMethods.ConvertToList<ActivityQuestionValueModel>(dataTable);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool AddActivityQuestionSet(int activityQuestionSetId, int userId, int flag)
        {
            try
            {
                return _questionSetRepository.AddActivityQuestionSet(activityQuestionSetId, userId, flag);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool MarkActivityQuestionSetPrimary(int activityQuestionSetId, int activityQuestionId, int sno, bool primary)
        {
            try
            {
                return _questionSetRepository.MarkActivityQuestionSetPrimary(activityQuestionSetId, activityQuestionId, sno, primary);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> AddActivityQuestionSetAsync(ActivityQuestionSetUniqueIdentityModel model)
        {
            try
            {
                return await Task.FromResult(_questionSetRepository.CreateActivityQuestionSet(model));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteActivityQuestionSet(int activityQuestionSetId, int userId, int sno, int flag)
        {
            try
            {
                return _questionSetRepository.DeleteActivityQuestionSet(activityQuestionSetId, userId, sno, flag);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ActivityQuestionValueModel> GetActivityQuestionValue(int activityQuestionSetId)
        {
            try
            {
                var dataTable = _questionSetRepository.GetActivityQuestionValue(activityQuestionSetId);
                return ExtensionMethods.ConvertToList<ActivityQuestionValueModel>(dataTable);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ActivityQuestionSetValueModel>> GetActivityQuestionSetValue(int activityQuestionSetId)
        {
            try
            {
                var activityQuestionSetValues = await _genericRepository.GetAsync<ActivityQuestionSetValue>(x => x.ActivityQuestionSetId == activityQuestionSetId && x.Sno == 0);
                return _mapper.Map<List<ActivityQuestionSetValueModel>>(activityQuestionSetValues);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            try
            {
                var activityQuestionSet = await _genericRepository.GetByIDAsync<ActivityQuestionSetUniqueIdentity>(id);
                activityQuestionSet.IsActive = false;
                activityQuestionSet.UpdatedOn = DateTime.Now;
                activityQuestionSet.UpdatedBy = userId;
                await _genericRepository.UpdateAsync(activityQuestionSet);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<bool> ActiveAsync(int id, int userId)
        {
            try
            {
                var activityQuestionSet = await _genericRepository.GetByIDAsync<ActivityQuestionSetUniqueIdentity>(id);
                activityQuestionSet.IsActive = true;
                activityQuestionSet.UpdatedOn = DateTime.Now;
                activityQuestionSet.UpdatedBy = userId;
                await _genericRepository.UpdateAsync(activityQuestionSet);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteActivityQuestionSet(int activityQuestionSetId, int userId)
        {
            try
            {
                return await _questionSetRepository.DeleteActivityQuestionSet(activityQuestionSetId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<ActivityUserLocationModel> GetActivityUserLocationAsync(int activityQuestionSetId)
        {
            try
            {
                var model = new ActivityUserLocationModel();
                var location = await Task.FromResult(_questionSetRepository.GetActivityUserLocation(activityQuestionSetId));
                if (location != null && location.Rows.Count > 0)
                {
                    model.StateName = Convert.ToString(location.Rows[0]["StateName"]);
                    model.DistrictName = Convert.ToString(location.Rows[0]["DistrictName"]);
                    model.BlockName = Convert.ToString(location.Rows[0]["BlockName"]);
                    model.VillageName = Convert.ToString(location.Rows[0]["VillageName"]);
                    model.Code = Convert.ToString(location.Rows[0]["Code"]);
                    model.ProfileImage = Convert.ToString(location.Rows[0]["ProfileImage"]);
                }
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsertActivityDocument(ActivityDocumentModel model)
        {
            try
            {
                _questionSetRepository.InsertActivityDocument(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetActivityDocumentModel>> GetActivityDocumentAsync(int activityQuestionSetId)
        {
            try
            {
                var dataTable = await Task.FromResult(_questionSetRepository.GetActivityDocument(activityQuestionSetId));
                return ExtensionMethods.ConvertToList<GetActivityDocumentModel>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteActivityDocument(int activityDocumentId)
        {
            try
            {
                var activityDocument = _genericRepository.GetByID<ActivityDocument>(activityDocumentId);
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, @"documents\images\" + activityDocument.InternalDocumentName);
                if (System.IO.File.Exists(filePath))
                {
                    ExtensionMethods.DeleteFile(Path.Combine(_webHostEnvironment.WebRootPath, filePath));
                }
                _questionSetRepository.DeleteActivityDocument(activityDocumentId);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> SubmitActivityQuestionSetAsync(int activityQuestionSetId, int status, int userId)
        {
            try
            {
                //var activityQuestionSet = await _genericRepository.GetByIDAsync<ActivityQuestionSetUniqueIdentity>(activityQuestionSetId);
                //activityQuestionSet.Status = status;
                //activityQuestionSet.UpdatedOn = DateTime.Now;
                //activityQuestionSet.UpdatedBy = userId;
                //await _genericRepository.UpdateAsync(activityQuestionSet);
                await Task.FromResult(_questionSetRepository.SubmitActivityQuestionSet(activityQuestionSetId, status, userId));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ArchivalActivityQuestionSetAsync(int activityQuestionSetId, int status, int userId)
        {
            try
            {
                var activityQuestionSet = await _genericRepository.GetByIDAsync<ActivityQuestionSetUniqueIdentity>(activityQuestionSetId);
                activityQuestionSet.Status = status;
                activityQuestionSet.UpdatedOn = DateTime.Now;
                activityQuestionSet.UpdatedBy = userId;
                await _genericRepository.UpdateAsync(activityQuestionSet);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> LockActivityQuestionSetAsync(int activityQuestionSetId, int status, int userId)
        {
            try
            {
                var activityQuestionSet = await _genericRepository.GetByIDAsync<ActivityQuestionSetUniqueIdentity>(activityQuestionSetId);
                activityQuestionSet.Status = status;
                activityQuestionSet.UpdatedOn = DateTime.Now;
                activityQuestionSet.UpdatedBy = userId;
                await _genericRepository.UpdateAsync(activityQuestionSet);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<ValidateActivityQuestionSet>> ValidateActivityQuestionSet(int activityQuestionSetId)
        {
            try
            {
                var dataTable = await Task.FromResult(_questionSetRepository.ValidateActivityQuestionSet(activityQuestionSetId));
                return ExtensionMethods.ConvertToList<ValidateActivityQuestionSet>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CheckUniqueHouseHold(string name, string mobile, int activityQuestionSetId)
        {
            try
            {
                return await Task.FromResult(_questionSetRepository.CheckUniqueHouseHold(name, mobile, activityQuestionSetId));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CheckUniqueHouseHoldBeforeCreation(int stateId, int districtId, int? blockId, int? villageId, string name, string mobile)
        {
            try
            {
                return await Task.FromResult(_questionSetRepository.CheckUniqueHouseHoldBeforeCreation(stateId, districtId, blockId, villageId, name, mobile));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ActivityQuestionSetStatusModel> GetActivityQuestionSetStatusDetails(int activityQuestionSetId)
        {
            try
            {
                var statusModel = new ActivityQuestionSetStatusModel();
                var activityQuestionSetUniqueIdentity = await _genericRepository.GetByIDAsync<ActivityQuestionSetUniqueIdentity>(activityQuestionSetId);
                if (activityQuestionSetUniqueIdentity != null)
                {
                    statusModel.status = activityQuestionSetUniqueIdentity.Status;
                    statusModel.createdOn = activityQuestionSetUniqueIdentity.CreatedOn;
                    statusModel.interventionStartDate = activityQuestionSetUniqueIdentity.InterventionStartDate;
                    statusModel.submittedOn = activityQuestionSetUniqueIdentity.UpdatedOn;
                }
                return statusModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> GetActivityQuestionSetStatus(int activityQuestionSetId)
        {
            try
            {

                var activityQuestionSetUniqueIdentity = await _genericRepository.GetByIDAsync<ActivityQuestionSetUniqueIdentity>(activityQuestionSetId);

                return activityQuestionSetUniqueIdentity.Status;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetActivityQuestionDueHistory_Result>> GetActivityQuestionDueHistory(int activityQuestionSetId, int activityQuestionId, int sno)
        {
            try
            {
                var dataTable = await Task.FromResult(_questionSetRepository.GetActivityQuestionDueHistory(activityQuestionSetId, activityQuestionId, sno));
                return ExtensionMethods.ConvertToList<GetActivityQuestionDueHistory_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<GetActivityQuestionSetGroup_Result>> GetActivityQuestionSetGroup(int activityQuestionSetId)
        {
            try
            {
                var dataTable = await Task.FromResult(_questionSetRepository.GetActivityQuestionSetGroup(activityQuestionSetId));
                return ExtensionMethods.ConvertToList<GetActivityQuestionSetGroup_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddActivityQuestionSetGroup(int activityQuestionSetId, int userId, string groupIds)
        {
            try
            {
                return await Task.FromResult(_questionSetRepository.AddActivityQuestionSetGroup(activityQuestionSetId, userId, groupIds));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetUserLocationCount_Result> GetUserLocationCount(int userId)
        {
            try
            {
                var model = new GetUserLocationCount_Result();
                var location = await Task.FromResult(_questionSetRepository.GetUserLocationCount(userId));
                if (location != null && location.Rows.Count > 0)
                {
                    model.TotalState = Convert.ToInt32(location.Rows[0]["TotalState"]);
                    model.TotalDistrict = Convert.ToInt32(location.Rows[0]["TotalDistrict"]);
                    model.TotalBlock = Convert.ToInt32(location.Rows[0]["TotalBlock"]);
                    model.TotalVillage = Convert.ToInt32(location.Rows[0]["TotalVillage"]);
                }
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetActivityDates_Result> GetActivityDates(int activityCategoryMappingId)
        {
            try
            {
                var model = new GetActivityDates_Result();
                var activity = await Task.FromResult(_questionSetRepository.GetActivityDates(activityCategoryMappingId));
                if (activity != null && activity.Rows.Count > 0)
                {
                    model.StartDate = Convert.ToDateTime(activity.Rows[0]["StartDate"]);
                    model.EndDate = Convert.ToDateTime(activity.Rows[0]["EndDate"]);
                }
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> GetActivityTotalDues(int activityQuestionSetId)
        {
            try
            {
                return await Task.FromResult(_questionSetRepository.GetActivityTotalDues(activityQuestionSetId));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> SaveHHFilter(HouseholdFilterModel model)
        {
            try
            {
                var householdFilter = _mapper.Map<HouseholdFilter>(model);
                var household = await _genericRepository.GetFirstOrDefaultAsync<HouseholdFilter>(x => x.UserId == model.UserId && x.ActivityCategoryMappingId == model.ActivityCategoryMappingId);
                if (household == null)
                {
                    householdFilter.HouseholdFilterId = 0;
                    await _genericRepository.InsertAsync(householdFilter);
                }
                else
                {
                    household.FromDate = model.FromDate;
                    household.ToDate = model.ToDate;
                    household.Days = model.Days;
                    household.StateId = model.StateId;
                    household.DistrictId = model.DistrictId;
                    household.BlockId = model.BlockId;
                    household.VillageId = model.VillageId;
                    household.UpdatedBy = model.CreatedBy;
                    household.UpdatedOn = model.CreatedOn;
                    await _genericRepository.UpdateAsync(household);
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HouseholdFilterModel> GetHHFilter(int userId, int activityCategoryMappingId)
        {
            try
            {
                var householdFilter = await _genericRepository.GetFirstOrDefaultAsync<HouseholdFilter>(x => x.UserId == userId && x.ActivityCategoryMappingId == activityCategoryMappingId);
                if (householdFilter == null)
                {
                    householdFilter = new HouseholdFilter()
                    {
                        Days = "30"
                    };
                }
                if (string.IsNullOrEmpty(householdFilter.Days) && householdFilter.FromDate == null && householdFilter.ToDate == null)
                    householdFilter.Days = "30";

                return _mapper.Map<HouseholdFilterModel>(householdFilter);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<HouseholdActionLog_Result>> GetHouseholdActionLog(string activityCategoryIds, string userIds, DateTime fromdate, DateTime todate)
        {
            try
            {
                var dataTable = await Task.FromResult(_questionSetRepository.GetHouseholdActionLog(activityCategoryIds, userIds, fromdate, todate));

                return ExtensionMethods.ConvertToList<HouseholdActionLog_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<HouseholdMonthlyActionLog_Result>> GetHouseholdMonthlyActionLog(string activityCategoryIds, string userIds, DateTime fromdate, DateTime todate)
        {
            try
            {
                var dataTable = await Task.FromResult(_questionSetRepository.GetHouseholdMonthlyActionLog(activityCategoryIds, userIds, fromdate, todate));
                return ExtensionMethods.ConvertToList<HouseholdMonthlyActionLog_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CreateUserActionLogHH(int userId, DateTime actionLogTime, int activityQuestionSetId, int status)
        {
            try
            {
                return await _questionSetRepository.CreateUserActionLogHH(userId, actionLogTime, activityQuestionSetId, status);

            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}

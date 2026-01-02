using AthodBeTrackApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public interface IActivityQuestionSetService
    {
        bool AddActivityQuestionSet(int activityQuestionSetId, int userId, int flag);
        Task<int> AddActivityQuestionSetAsync(ActivityQuestionSetUniqueIdentityModel model);      
        bool DeleteActivityQuestionSet(int activityQuestionSetId, int userId, int sno, int flag);
        Task<bool> DeleteAsync(int id, int userId);
        string GetActivityQuestionSet(int activityCategoryMappingId, int userId, string days, DateTime? fromDate, DateTime? toDate);
        List<ActivityQuestionSetValueModel> GetActivityQuestionSets(int activityQuestionSetId, int flag);
        Task<ActivityUserLocationModel> GetActivityUserLocationAsync(int activityQuestionSetId);
        Task<List<ActivityQuestionSetValueModel>> GetActivityQuestionSetValue(int activityQuestionSetId);      
        bool UpdateActivityQuestionSetValue(ActivityQuestionSetValueViewModel setValueModel);
        void InsertActivityDocument(ActivityDocumentModel model);
        Task<List<GetActivityDocumentModel>> GetActivityDocumentAsync(int activityQuestionSetId);
        bool DeleteActivityDocument(int activityDocumentId);
        Task<bool> SubmitActivityQuestionSetAsync(int activityQuestionSetId, int status, int userId);
        Task<List<ValidateActivityQuestionSet>> ValidateActivityQuestionSet(int activityQuestionSetId);
        Task<bool> CheckUniqueHouseHold(string name, string mobile, int activityQuestionSetId);
        Task<int> GetActivityQuestionSetStatus(int activityQuestionSetId);
        Task<ActivityQuestionSetStatusModel> GetActivityQuestionSetStatusDetails(int activityQuestionSetId);
        List<ActivityQuestionValueModel> GetActivityQuestionValue(int activityQuestionSetId);
        List<ActivityQuestionValueModel> GetActivityQuestionValueSet(int activityQuestionSetId, int flag);
        Task<List<GetActivityQuestionDueHistory_Result>> GetActivityQuestionDueHistory(int activityQuestionSetId, int activityQuestionId, int sno);
        Task<bool> LockActivityQuestionSetAsync(int activityQuestionSetId, int status, int userId);
        Task<bool> ArchivalActivityQuestionSetAsync(int activityQuestionSetId, int status, int userId);
        Task<bool> CheckUniqueHouseHoldBeforeCreation(int stateId, int districtId, int? blockId, int? villageId, string name, string mobile);
        Task<List<GetActivityQuestionSetGroup_Result>> GetActivityQuestionSetGroup(int activityQuestionSetId);
        Task<bool> AddActivityQuestionSetGroup(int activityQuestionSetId, int userId, string groupIds);
        Task<GetUserLocationCount_Result> GetUserLocationCount(int userId);
        Task<GetActivityDates_Result> GetActivityDates(int activityCategoryMappingId);
        Task<int> GetActivityTotalDues(int activityQuestionSetId);
        Task<bool> SaveHHFilter(HouseholdFilterModel model);
        Task<HouseholdFilterModel> GetHHFilter(int userId, int activityCategoryMappingId);
        bool MarkActivityQuestionSetPrimary(int activityQuestionSetId, int activityQuestionId, int sno, bool primary);
        string GetActivityQuestionSetWithLocationFilter(int activityCategoryMappingId, int userId, string days, DateTime? fromDate, DateTime? toDate, string stateId, string districtId, string blockId, string villageId);
        Task<List<GetHouseholdSets_Result>> GetHouseholdSets(int activityCategoryMappingId, string days, DateTime? fromDate, DateTime? toDate, int start, int length, string orderBy, string searchText, string lFilter,int userId);
        Task<int> GetHouseholdSetsCount(int activityCategoryMappingId, string days, DateTime? fromDate, DateTime? toDate, string searchText, string lFilter, int userId);
        Task<List<HouseholdActionLog_Result>> GetHouseholdActionLog(string activityCategoryIds, string userIds, DateTime fromdate, DateTime todate);
        Task<bool> ActiveAsync(int id, int userId);
        Task<List<HouseholdMonthlyActionLog_Result>> GetHouseholdMonthlyActionLog(string activityCategoryIds, string userIds, DateTime fromdate, DateTime todate);
        Task<bool> CreateUserActionLogHH(int userId, DateTime actionLogTime, int activityQuestionSetId, int status);
        Task<List<GetHouseholdSets_Result>> ExportHouseholdSetsAsync(int activityCategoryMappingId, string days, DateTime? fromDate, DateTime? toDate, string lFilter, int userId);
        Task<bool> DeleteActivityQuestionSet(int activityQuestionSetId, int userId);
        Task<int> GetHouseholdDeletedSetsCount(int activityCategoryMappingId, string days, DateTime? fromDate, DateTime? toDate, string searchText, string lFilter, int userId);
        Task<List<GetHouseholdSets_Result>> GetHouseholdDeletedSets(int activityCategoryMappingId, string days, DateTime? fromDate, DateTime? toDate, int start, int length, string orderBy, string searchText, string lFilter, int userId);
    }
}
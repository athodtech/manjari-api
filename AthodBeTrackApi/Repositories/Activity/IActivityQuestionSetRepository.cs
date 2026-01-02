using AthodBeTrackApi.Models;
using System;
using System.Data;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Repositories
{
    public interface IActivityQuestionSetRepository
    {
        bool AddActivityQuestionSet(int activityQuestionSetId, int userId, int flag);
        bool AddActivityQuestionSetGroup(int activityQuestionSetId, int userId, string groupIds);
        bool CheckUniqueHouseHold(string name, string mobile, int activityQuestionSetId);
        bool CheckUniqueHouseHoldBeforeCreation(int stateId, int districtId, int? blockId, int? villageId, string name, string mobile);
        int CreateActivityQuestionSet(ActivityQuestionSetUniqueIdentityModel request);
        Task<bool> CreateUserActionLogHH(int userId, DateTime actionLogTime, int activityQuestionSetId, int status);
        void DeleteActivityDocument(int activityDocumentId);
        bool DeleteActivityQuestionSet(int activityQuestionSetId, int userId, int sno, int flag);
        Task<bool> DeleteActivityQuestionSet(int activityQuestionSetId, int userId);
        Task<DataTable> ExportHouseholdSetsAsync(int activityCategoryMappingId, int userId, int status);
        DataTable GetActivityDates(int activityCategoryMappingId);
        DataTable GetActivityDocument(int activityQuestionSetId);
        DataTable GetActivityQuestionDueHistory(int activityQuestionSetId, int activityQuestionId, int sno);
        DataTable GetActivityQuestionSet(int activityCategoryMappingId, int userId, string days, DateTime? fromDate, DateTime? toDate);
        DataTable GetActivityQuestionSet(int activityCategoryMappingId, int userId, string days, DateTime? fromDate, DateTime? toDate, string stateId, string districtId, string blockId, string villageId);
        DataTable GetActivityQuestionSetGroup(int activityQuestionSetId);
        DataTable GetActivityQuestionSets(int activityQuestionSetId, int flag);
        DataTable GetActivityQuestionValue(int activityQuestionSetId);
        DataTable GetActivityQuestionValueSet(int activityQuestionSetId, int flag);
        int GetActivityTotalDues(int activityQuestionSetId);
        DataTable GetActivityUserLocation(int activityQuestionSetId);
        DataTable GetHouseholdActionLog(string activityCategoryIds, string userIds, DateTime fromdate, DateTime todate);
        Task<DataTable> GetHouseholdDeletedSetsAsync(int activityCategoryMappingId, string days, DateTime? fromDate, DateTime? toDate, string lFilter, string whereClause, string limitQuery, int userId);
        Task<DataTable> GetHouseholdDeletedSetsCountAsync(int activityCategoryMappingId, string days, DateTime? fromDate, DateTime? toDate, string lFilter, string whereClause, int userId);
        DataTable GetHouseholdMonthlyActionLog(string activityCategoryIds, string userIds, DateTime fromdate, DateTime todate);
        Task<DataTable> GetHouseholdSetsAsync(int activityCategoryMappingId, string days, DateTime? fromDate, DateTime? toDate, string lFilter, string whereClause, string limitQuery, int userId);
        Task<DataTable> GetHouseholdSetsCountAsync(int activityCategoryMappingId, string days, DateTime? fromDate, DateTime? toDate, string lFilter, string whereClause, int userId);
        DataTable GetUserLocationCount(int userId);
        void InsertActivityDocument(ActivityDocumentModel model);
        bool MarkActivityQuestionSetPrimary(int activityQuestionSetId, int activityQuestionId, int sno, bool primary);
        bool SubmitActivityQuestionSet(int activityQuestionSetId, int status, int userId);
        int UpdateActivityQuestionSetValue(int activityQuestionSetId, int userId, DataTable activityQuestionSetValue);
        DataTable ValidateActivityQuestionSet(int activityQuestionSetId);
    }
}
using AthodBeTrackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public interface IDropdownService
    {
        Task<List<SelectListModel>> GetRolesAsync(int? roleId);
        Task<List<SelectListModel>> GetStateAsync(int? stateId);
        Task<List<SelectListModel>> GetDistrictAsync(int? stateId, int? districtId);
        Task<List<SelectListModel>> GetBlockAsync(int? stateId, int? districtId, int? blockId);
        Task<List<SelectListModel>> GetVillageAsync(int? stateId, int? districtId, int? blockId, int? villageId);
        Task<List<SelectListModel>> GetQuestionTypeAsync(int? id);
        Task<List<SelectListModel>> GetQuestionAsync(int? questionId);
        Task<List<SelectListModel>> GetQuestionChoiceAsync(int? id);
        Task<List<SelectListModel>> GetActivityAsync(int? activityId);
        Task<List<SelectListModel>> GetUserByRoleIdAsync(int? roleId);
        Task<List<SelectListModel>> GetQuestionGroupAsync(int? groupId, bool? isDefault);
        Task<List<SelectListModel>> GetUserLocationLevelDll(int userId);
        Task<List<SelectListModel>> GetUserLocationStateDll(int userId);
        Task<List<SelectListModel>> GetUserLocationDistrictDll(int userId, int stateId);
        Task<List<SelectListModel>> GetUserLocationBlockDll(int userId, int stateId, int districtId);
        Task<List<SelectListModel>> GetUserLocationVillageDll(int userId, int stateId, int districtId, int blockId);
        Task<List<SelectListModel>> GetQuestionReportingFrequencyAsync(int? reportingFrequencyTypeId);
        Task<List<SelectListActivityCategory>> GetActivityQuestionDll();
        Task<List<SelectListModel>> GetActivityQuestionByGroupId(string groupIds);
        Task<List<SelectList2Model>> RPT_GetLocation(int? userId);
        Task<List<SelectListModel>> GetTagAsync(int? tagId);
        Task<List<SelectListModel>> GetAssignUserLocationDropdown(string flag, string ids);
        Task<List<SelectListModel>> RPT_GetTagsdll(string groupIds);
        Task<List<SelectListModel>> RPT_GetQuestiondll(string tagIds, int activityCategoryMapping);
        Task<List<SelectListModel>> RPT_GetAssignUserLocationDropdown(int userId, string flag, string ids);
        Task<List<ChartModel>> GetChartType();
        Task<List<SelectListModel>> GetRolesExcludingRoleIdAsync(int? roleId);
        Task<List<SelectListModel>> GetUsersExcludingRoleIdAsync(int? roleId);
        Task<List<SelectListModel>> RPT_GetUserLocationStates(int userId);
        Task<List<SelectListModel>> GetQuestionGroupExceptIdAsync(int? groupId);
        Task<List<SelectListModel>> RPT_GetQuestionGroupAsync(int userId);
        Task<List<SelectListModel>> RPT_GetTagsAsync(string groupIds, int userId);
        Task<List<SelectListModel>> GetApplicationEventTypeAsync(int? id);
        Task<List<SelectListModel>> RPT_GetQuestiondll(string tagIds, int activityCategoryMapping, string strWhere);
        Task<List<SelectList3Model>> GetAllUsersExcludingRoleIdAsync(int? roleId);
        Task<List<SelectListModel>> GetActivityWithMappingidDll(int? userId);
        Task<List<SelectListModel>> GetQuestionChoiceItemAsync(int id);
        Task<List<SelectListModel>> RPT_GetAssignUserLocationDropdown2(int userId, string flag, string sIds, string dIds, string bIds);
        Task<List<SelectListModel>> RPT_GetQuestiondll2(string groupIds, string tagIds, int activityCategoryMapping);
    }
}
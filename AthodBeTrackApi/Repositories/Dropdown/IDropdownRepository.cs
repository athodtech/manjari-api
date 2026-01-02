using System.Data;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Repositories
{
    public interface IDropdownRepository
    {
        DataTable GetActivityQuestionByGroupId(string groupIds);
        DataTable GetActivityQuestionDll();
        DataTable GetActivityWithMappingidDll(int? userId);
        DataTable GetAssignUserLocationDropdown(string flag, string ids);
        DataTable GetUserLocationBlocks(int userId, int stateId, int districtId);
        DataTable GetUserLocationDistricts(int userId, int stateId);
        DataTable GetUserLocationLevel(int userId);
        DataTable GetUserLocationStates(int userId);
        DataTable GetUserLocationVillages(int userId, int stateId, int districtId, int blockId);
        DataTable RPT_GetAssignUserLocationDropdown(int userId, string flag, string ids);
        DataTable RPT_GetAssignUserLocationDropdown2(int userId, string flag, string sIds, string dIds, string bIds);
        DataTable RPT_GetLocation(int? userId);
        DataTable RPT_GetQuestiondll(string tagIds, int activityCategoryMapping);
        DataTable RPT_GetQuestiondll(string tagIds, int activityCategoryMapping, string strWhere);
      
        Task<DataTable> RPT_GetQuestionGroupAsync(int userId);
        Task<DataTable> RPT_GetTagsAsync(string groupIds, int userId);
        DataTable RPT_GetTagsdll(string groupIds);
        DataTable RPT_GetUserLocationStates(int userId);
    }
}
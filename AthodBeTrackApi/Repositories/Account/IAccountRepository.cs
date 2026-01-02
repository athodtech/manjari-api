using AthodBeTrackApi.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Repositories
{
    public interface IAccountRepository
    {
        bool VerifyUser(string sUserName, out string dbPassword);
        List<MenuItem> GetMenu(int roleId, int userId);
        DataTable GetUserLocation(int userId);
        DataTable GetUserLogs(int? userId, int? lastDays);
        bool LogOutOtherDeviceUser(string userName);
        DataTable GetRoleRights(int roleId);
        bool AssignRoleRights(int roleId, int userId, DataTable roleRights);
        DataTable GetUserRights(int roleId, int userId);
        bool AssignUserRights(int roleId, int userId, int cretedBy, DataTable userRights);
        DataTable GetButtonRights(int roleId, int userId);
        bool DeleteUser(int userId);
        DataTable GetUserLogsWithPagination(int? userId, int pageNumber, int rowsOfPage);
        Task<bool> LogOutOtherDeviceUserAsync(string userName);
        Task<DataTable> GetUserLogDetails(int? userId);
        Task<bool> ResetUserRightsAsync(int roleId, int userId);
    }
}
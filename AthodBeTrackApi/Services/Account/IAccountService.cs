using AthodBeTrackApi.Data;
using AthodBeTrackApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public interface IAccountService
    {
        bool AuthenticateUser(SingIn singIn, out UserDetailsModel userDetails);
        Task<List<UserModel>> GetUsersAsync();
        Task<UserModel> GetUserByIdAsync(int id);
        Task<bool> AddUserAsync(UserModel model);
        Task<bool> UpdateAsync(UserModel userModel);
        Task<bool> UpdateProfileAsync(ProfileModel userModel);
        Task<string> ChangePasswordAsync(ChangePassword request);
        Task DeleteUserAsync(int id);
        List<MenuItem> GetMenu(int roleId, int userId);
        Task<bool> AddUserLogAsync(UserLogModel model);
        Task<List<UserLogModel>> GetUserLogsAsync(int? userId, int? lastDays);
        bool CheckUserAlreadyLogin(string userName);
        Task<bool> CheckUserLogoutByOtherDevice(string sessionId);
        bool LogOutOtherDeviceUser(string userName);
        Task<List<GetRoleRights_Result>> GetRoleRights(int roleId);
        Task<bool> AssignRoleRights(InsertRoleRights_Result request);
        Task<List<GetRoleRights_Result>> GetUserRights(int roleId, int userId);
        Task<bool> AssignUserRights(InsertUserRights_Result request);
        Task<List<GetButtonRights_Result>> GetButtonRights(int roleId, int userId);
        Task<bool> ResetPassword(int userId);
        Task<List<UserLogModel>> GetUserLogsWithPagination(int? userId, int pageNumber, int rowsOfPage);
        Task<bool> CheckUserAlreadyLoginAsync(string userName);
        Task<bool> LogOutOtherDeviceUserAsync(string userName);
        Task<List<GetUserLog_Result>> GetUserLogDetails(int? userId);
        Task<bool> ResetUserRightsAsync(int roleId, int userId);
        Task<User> ValidateEmailUserName(string key);
        Task<bool> ForgotPasswordSendEmail(User user);
        Task<bool> ForgotPasswordLinkEmail(User user, bool isProduction);
        Task<bool> ValidateForgotPasswordLink(string key);
    }
}
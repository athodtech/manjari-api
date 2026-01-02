using AthodBeTrackApi.Data;
using AthodBeTrackApi.Helpers;
using AthodBeTrackApi.Models;
using AthodBeTrackApi.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _genericRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _iconfiguration;
        public AccountService(IMapper mapper, IGenericRepository genericRepository, IAccountRepository accountRepository,IConfiguration iconfiguration)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
            _accountRepository = accountRepository;
            _iconfiguration = iconfiguration;
        }

        public bool AuthenticateUser(SingIn singIn, out UserDetailsModel userDetails)
        {
            string dbPassword = string.Empty;
            bool userExits = false;
            userDetails = new UserDetailsModel();
            userExits = _accountRepository.VerifyUser(singIn.UserName, out dbPassword);
            if (!userExits)
            {
                return false;
            }
            bool isMatch = Password.VerifyPassword(singIn.Password, dbPassword, Password.Password_Salt);

            if (isMatch)
            {
                var uData = _genericRepository.GetIQueryable<User>(u => u.IsActive == true && u.UserName == singIn.UserName).Include(r => r.Role).FirstOrDefault();
                if (uData != null)
                {
                    userDetails.UserId = uData.UserId;
                    userDetails.UserName = uData.UserName;
                    userDetails.FirstName = uData.FirstName;
                    userDetails.LastName = uData.LastName;
                    userDetails.AboutUs = uData.AboutUs;
                    userDetails.MobileNo = uData.MobileNo;
                    userDetails.EmailId = uData.EmailId;
                    userDetails.ImageName = uData.ImageName;
                    userDetails.RoleId = uData.Role.RoleId;
                    userDetails.RoleName = uData.Role?.RoleName;
                    userDetails.Location = uData.Location;
                    userDetails.Organization = uData.Organization;
                }
            }
            else
            {
                return false;
            }

            if (userDetails.UserId > 0) { return true; }
            else { return false; }

        }
        public async Task<List<UserModel>> GetUsersAsync()
        {
            try
            {
                var users = await Task.FromResult(_genericRepository.GetIQueryable<User>().Include(r => r.Role));
                return _mapper.Map<List<UserModel>>(users);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _genericRepository.GetByIDAsync<User>(id);
                return _mapper.Map<UserModel>(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddUserAsync(UserModel model)
        {
            try
            {
                var map = _mapper.Map<User>(model);
                map.Role = null;
                var res = await _genericRepository.InsertAsync(map);
                if (res > 0)
                {
                    var isExist = await _genericRepository.ExistsAsync<RoleRight>(x => x.RoleId == model.RoleId && x.MenuId == 2);
                    if (!isExist)
                    {
                        var roleModel = new RoleRight
                        {
                            RoleId = model.RoleId,
                            MenuId = 2,
                            CreatedBy = model.CreatedBy,
                            CreatedOn = model.CreatedOn
                        };

                        await _genericRepository.InsertAsync<RoleRight>(roleModel);

                        var roleModel2 = new RoleRight
                        {
                            RoleId = model.RoleId,
                            MenuId = 1,
                            CreatedBy = model.CreatedBy,
                            CreatedOn = model.CreatedOn
                        };
                        await _genericRepository.InsertAsync<RoleRight>(roleModel2);
                    }

                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(UserModel userModel)
        {
            try
            {
                var UserId = userModel.UserId;
                var user = await _genericRepository.GetByIDAsync<User>(UserId);
                user.RoleId = userModel.RoleId;
                user.FirstName = userModel.FirstName;
                user.LastName = userModel.LastName;
                user.EmailId = userModel.EmailId;
                user.MobileNo = userModel.MobileNo;
                user.ImageName = userModel.ImageName;
                user.AboutUs = userModel.AboutUs;
                user.Location = userModel.Location;
                user.Organization = userModel.Organization;
                user.IsActive = userModel.IsActive;

                var exists = await _genericRepository.ExistsAsync<User>(x => (x.UserName.ToLower().Trim() == userModel.UserName.ToLower().Trim())
                && (x.RoleId.Equals(userModel.RoleId)) && (x.UserId != userModel.UserId));

                if (!exists)
                {
                    await _genericRepository.UpdateAsync(user);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateProfileAsync(ProfileModel userModel)
        {
            try
            {
                var user = await _genericRepository.GetByIDAsync<User>(userModel.UserId);
                user.FirstName = userModel.FirstName;
                user.LastName = userModel.LastName;
                user.EmailId = userModel.EmailId;
                user.MobileNo = userModel.MobileNo;
                user.ImageName = userModel.ImageName;
                user.AboutUs = userModel.AboutUs;
                user.Location = userModel.Location;
                user.Organization = userModel.Organization;
                await _genericRepository.UpdateAsync(user);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> ChangePasswordAsync(ChangePassword request)
        {
            try
            {
                var user = await _genericRepository.GetByIDAsync<User>(request.userId);
                string dbPassword = user.Password;

                bool isMatch = Password.VerifyPassword(request.currentPassword, dbPassword, Password.Password_Salt);

                if (isMatch)
                {
                    var passwordHash = Password.CreatePasswordHash(request.newPassword.Trim(), Password.CreateSalt(Password.Password_Salt));

                    if (!string.IsNullOrEmpty(passwordHash))
                    {
                        user.Password = passwordHash;
                        _genericRepository.Update(user);
                        return "success";
                    }
                    else
                        return "failed";
                }
                else
                {
                    return "notmatch";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<bool> ResetPassword(int userId)
        {
            try
            {
                var user = await _genericRepository.GetByIDAsync<User>(userId);
                if (user != null)
                {
                    var defaultPssword = user.UserName + "@12345";
                    var passwordHash = Password.CreatePasswordHash(defaultPssword.Trim(), Password.CreateSalt(Password.Password_Salt));

                    if (!string.IsNullOrEmpty(passwordHash))
                    {
                        user.Password = passwordHash;
                        _genericRepository.Update(user);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            try
            {
                var isDeleted = _accountRepository.DeleteUser(id);
                if (!isDeleted)
                {
                    var user = await _genericRepository.GetByIDAsync<User>(id);
                    if (user != null)
                    {
                        user.IsActive = false;
                        user.UpdatedOn = DateTime.Now;
                        await _genericRepository.UpdateAsync(user);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<MenuItem> GetMenu(int roleId, int userId)
        {
            try
            {
                return _accountRepository.GetMenu(roleId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddUserLogAsync(UserLogModel model)
        {
            try
            {
                if (model.Remark.ToLower() == "signed in")
                {
                    model.LoginTime = DateTime.Now;
                    model.LogoutTime = null;
                    model.CreatedOn = DateTime.Now;
                }
                if (model.Remark.ToLower() == "signed out" || model.Remark.ToLower() == "auto sign out")
                {
                    model.LoginTime = null;
                    model.LogoutTime = DateTime.Now;
                    model.CreatedOn = DateTime.Now;
                }

                var userLog = _mapper.Map<UserLog>(model);
                var res = await _genericRepository.InsertLongAsync(userLog);
                if (res > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<UserLogModel>> GetUserLogsAsync(int? userId, int? lastDays)
        {
            try
            {

                var dataTable = await Task.FromResult(_accountRepository.GetUserLogs(userId, lastDays));
                return ExtensionMethods.ConvertToList<UserLogModel>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<UserLogModel>> GetUserLogsWithPagination(int? userId, int pageNumber, int rowsOfPage)
        {
            try
            {
                var dataTable = await Task.FromResult(_accountRepository.GetUserLogsWithPagination(userId, pageNumber, rowsOfPage));
                return ExtensionMethods.ConvertToList<UserLogModel>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetUserLog_Result>> GetUserLogDetails(int? userId)
        {
            try
            {
                var dataTable = await _accountRepository.GetUserLogDetails(userId);
                return ExtensionMethods.ConvertToList<GetUserLog_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckUserAlreadyLogin(string userName)
        {
            try
            {
                int userId = 0;
                var user = _genericRepository.GetFirstOrDefault<User>(x => x.UserName == userName);
                if (user != null)
                    userId = user.UserId;
                if (userId > 0)
                {
                    var userLog = _genericRepository.Get<UserLog>(x => x.UserId == userId).OrderByDescending(x => x.UserLogId).FirstOrDefault();
                    if (userLog != null)
                    {
                        if (userLog.LogoutTime == null)
                            return true;
                        else
                            return false;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CheckUserAlreadyLoginAsync(string userName)
        {
            try
            {
                int userId = 0;
                var user = await _genericRepository.GetFirstOrDefaultAsync<User>(x => x.UserName == userName);
                if (user != null)
                    userId = user.UserId;
                if (userId > 0)
                {
                    var userLog = _genericRepository.GetAsync<UserLog>(x => x.UserId == userId).GetAwaiter().GetResult().OrderByDescending(x => x.UserLogId).FirstOrDefault();
                    var expirHours = _iconfiguration.GetValue<int>("JwtSettings:ExpirHours");// 48 Hours
                    if (userLog != null)
                    {
                        if (userLog.LogoutTime == null)
                        {
                            var cdate = DateTime.Now;
                          
                            var tokenLog = _genericRepository.GetAsync<ApiUserRefreshToken>(x => x.UserId == userId && x.SessionId == userLog.SessionId).GetAwaiter().GetResult().OrderByDescending(x => x.Id).FirstOrDefault();

                            if (tokenLog != null && tokenLog.CreatedDate.AddHours(expirHours) < cdate)
                            {
                                var userLogModel = new UserLog();
                                userLogModel = userLog;
                                userLogModel.UserLogId = 0;                              
                                userLogModel.LogoutTime = tokenLog.CreatedDate.ToLocalTime().AddHours(expirHours);
                                userLogModel.LoginTime = null;
                                userLogModel.CreatedOn = DateTime.Now;
                                userLogModel.Remark = "auto sign out";
                                if (await _genericRepository.ExistsAsync<UserLog>(x => x.UserId == userId
                                    && x.SessionId == userLog.SessionId && x.LogoutTime == null))
                                {
                                    await _genericRepository.InsertLongAsync(userLogModel);
                                }
                                return false;
                            }

                            //else if (tokenLog == null)
                            //{
                            //    var userLogModel = new UserLog();
                            //    userLogModel = userLog;
                            //    userLogModel.UserLogId = 0;
                            //    userLogModel.LogoutTime = userLogModel.LoginTime.Value.AddHours(expirHours);
                            //    userLogModel.LoginTime = null;
                            //    userLogModel.CreatedOn = DateTime.Now;
                            //    userLogModel.Remark = "signed out";
                            //    await _genericRepository.InsertLongAsync(userLogModel);

                            //    return false;
                            //}
                            else
                                return true;
                        }
                        else
                            return false;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CheckUserLogoutByOtherDevice(string sessionId)
        {
            try
            {
                return await _genericRepository.ExistsAsync<UserLog>(x => x.SessionId == sessionId && x.LogoutTime != null);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool LogOutOtherDeviceUser(string userName)
        {
            try
            {
                return _accountRepository.LogOutOtherDeviceUser(userName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> LogOutOtherDeviceUserAsync(string userName)
        {
            try
            {
                return await _accountRepository.LogOutOtherDeviceUserAsync(userName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetRoleRights_Result>> GetRoleRights(int roleId)
        {
            try
            {
                var dataTable = await Task.FromResult(_accountRepository.GetRoleRights(roleId));
                return ExtensionMethods.ConvertToList<GetRoleRights_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AssignRoleRights(InsertRoleRights_Result request)
        {
            try
            {
                var dataTable = ExtensionMethods.ToDataTable(request.roleRights);
                dataTable.TableName = "RoleRights";
                await Task.FromResult(_accountRepository.AssignRoleRights
                    (request.roleId, request.CreatedBy, dataTable));
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetRoleRights_Result>> GetUserRights(int roleId, int userId)
        {
            try
            {
                var dataTable = await Task.FromResult(_accountRepository.GetUserRights(roleId, userId));
                return ExtensionMethods.ConvertToList<GetRoleRights_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ResetUserRightsAsync(int roleId, int userId)
        {
            try
            {
                return await _accountRepository.ResetUserRightsAsync(roleId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AssignUserRights(InsertUserRights_Result request)
        {
            try
            {
                var dataTable = ExtensionMethods.ToDataTable(request.userRights);
                dataTable.TableName = "UserRights";
                await Task.FromResult(_accountRepository.AssignUserRights
                    (request.roleId, request.userId, request.CreatedBy, dataTable));
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetButtonRights_Result>> GetButtonRights(int roleId, int userId)
        {
            try
            {
                var dataTable = await Task.FromResult(_accountRepository.GetButtonRights(roleId, userId));
                return ExtensionMethods.ConvertToList<GetButtonRights_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> ValidateEmailUserName(string key)
        {
            try
            {
                return await _genericRepository.GetFirstOrDefaultAsync<User>(x => x.IsActive == true && (x.UserName == key || x.EmailId == key || x.UserId.ToString() == key));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> ForgotPasswordSendEmail(User user)
        {
            try
            {
                var emailsetting = await _genericRepository.GetAsync<EmailConfiguration>(x => x.IsActive == true);
                var emailMessage = _mapper.Map<EmailMessage>(emailsetting.FirstOrDefault());
                if (emailMessage != null)
                {
                    string decryptPassword = string.Empty;
                    string encriptPassword = string.Empty;
                    decryptPassword = Password.CreateRandomPassword(8);

                    encriptPassword = Password.CreatePasswordHash(decryptPassword, Password.CreateSalt(Password.Password_Salt));

                    string body = "Hi " + user.FirstName + ",<br><br> Your password has been reset. <br><br><b> Here’s your new password: " + decryptPassword + " </b><br><br>We recommend that you change your password after login. <br><br>Go to Account → Profile → Edit Profile → Change Password <br><br>Thank you for taking prompt action to secure your account.<br><br>Thank You<br>The MAAP Team";

                    emailMessage.To = user.EmailId;
                    emailMessage.Subject = "Your new MAAP password";
                    emailMessage.Body = body;
                    var flag = EmailHelper.SendEmail(emailMessage);
                    if (flag)
                    {
                        user.Password = encriptPassword;
                        user.ResetPasswrodTime = null;
                        await _genericRepository.UpdateAsync(user);
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<bool> ValidateForgotPasswordLink(string key)
        {
            var user = await _genericRepository.GetFirstOrDefaultAsync<User>(
                x => x.IsActive && (x.UserId.ToString() == key || x.EmailId == key));

            if (user == null)
                return false;

            if (user.ResetPasswrodTime.HasValue)
            {
                // Link valid for 4 hours
                if (user.ResetPasswrodTime.Value.AddHours(4) > DateTime.Now)
                    return true;
            }

            return false;
        }


        public async Task<bool> ForgotPasswordLinkEmail(User user, bool isProduction)
        {
            try
            {
                var emailsetting = await _genericRepository.GetAsync<EmailConfiguration>(x => x.IsActive == true);
                var emailMessage = _mapper.Map<EmailMessage>(emailsetting.FirstOrDefault());
                if (emailMessage != null)
                {
                    string link = string.Empty;
                    if (isProduction)
                    {
                        link = "https://manjari.maap.plus/auth/confirm-reset-password?key=" + user.UserId;
                    }
                    else
                    {
                        link = "https://app.maap.plus/auth/confirm-reset-password?key=" + user.UserId;
                        //link = "http://localhost:4200/auth/confirm-reset-password?key=" + user.UserId;
                    }

                    string body = "Hi " + user.FirstName +
                    ",<br><br>We received a request to reset the password for your MAAP account.<br><br>" +
                    "<b>Click the link below to confirm the reset.</b><br><br>" +
                    "<a href='" + link + "' style='color:#1a73e8; text-decoration:none; font-weight:bold;'>" + link + "</a><br><br>" +
                    "<b>This link will expire in 4 hours.</b><br><br>" +
                    "If you didn’t request this, you can safely ignore this email—your password won’t change.<br><br>" +
                    "Thank You<br>The MAAP Team";


                    emailMessage.To = user.EmailId;
                    emailMessage.Subject = "Reset your password";
                    emailMessage.Body = body;
                    var flag = EmailHelper.SendEmail(emailMessage);
                    if (flag)
                    {
                        user.ResetPasswrodTime = DateTime.Now;
                        await _genericRepository.UpdateAsync(user);
                    }
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

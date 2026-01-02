using AthodBeTrackApi.Helpers;
using AthodBeTrackApi.Models;
using AthodBeTrackApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IJWTManagerService _jWTManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _iconfiguration;
        public AccountController(ILogger<AccountController> logger, IAccountService accountService, IJWTManagerService jWTManagerService, IConfiguration iconfiguration)
        {
            _accountService = accountService;
            _jWTManager = jWTManagerService;
            _logger = logger;
            _iconfiguration = iconfiguration;
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(SingIn singIn)
        {
            ResponseModel<UserDetailsModel> res = new();
            MessageModel message = new();
            var expirHours = _iconfiguration.GetValue<int>("JwtSettings:ExpirHours");// 48 Hours
            var decryptPassword = ExtensionMethods.DecryptStringAES(singIn.Password);
            singIn.Password = decryptPassword;
            try
            {
                if (!ModelState.IsValid)
                {
                    message.status = Message.error;
                    message.message = "Invalid request";
                    return Ok(message);
                }

                bool isAuthenticated = _accountService.AuthenticateUser(singIn, out UserDetailsModel uDetail);
                if (isAuthenticated)
                {
                    var alreadyLogin = await _accountService.CheckUserAlreadyLoginAsync(singIn.UserName);

                    if (alreadyLogin)
                    {
                        message.status = "logged";
                        message.message = "This user is currently active on another device. Please try again later.";
                        return Ok(message);
                    }

                    var token = await _jWTManager.GetTokenAsync(uDetail.UserName, uDetail.UserId, HttpContext.Connection.RemoteIpAddress.ToString());
                    if (token?.Token != null)
                    {
                        uDetail.Token = token.Token;
                        uDetail.ExpirationDate = DateTime.Now.AddHours(expirHours);
                        uDetail.RefreshToken = token.RefreshToken;
                        uDetail.SessionId = token.SessionId;
                        res.status = Message.success;
                        res.data = uDetail;
                        return Ok(res);
                    }
                    else
                    {
                        message.status = "notoken";
                        message.message = "This user is currently active on another device. Please try again later.";
                        return Ok(message);
                    }
                }
                else
                {
                    message.status = Message.error;
                    message.message = "Invalid credentials.";
                    return Ok(message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                message.status = Message.error;
                message.message = ex.Message;
                return Ok(message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAsync()
        {
            ResponseModel<List<UserModel>> res = new();
            MessageModel message = new();

            #region Token Validation
            if (Request.Headers.TryGetValue("Authorization", out StringValues authorization))
            {
                string token = Convert.ToString(authorization).Replace("Bearer", "").Trim();

                if (!await _jWTManager.ValidateToken(token))
                {
                    message.status = Message.error;
                    message.message = "Unauthorized";
                    return Unauthorized(message);
                }
            }
            else
            {
                message.status = Message.error;
                message.message = "Unauthorized";
                return Unauthorized(message);
            }
            #endregion

            try
            {
                var userlist = await _accountService.GetUsersAsync();
                res.status = "success";
                res.data = userlist;
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                message.status = Message.error;
                message.message = ex.Message;
                return Ok(message);
            }
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAsync(int id)
        {
            ResponseModel<UserModel> res = new();
            MessageModel message = new();
            #region Token Validation
            if (Request.Headers.TryGetValue("Authorization", out StringValues authorization))
            {
                string token = Convert.ToString(authorization).Replace("Bearer", "").Trim();

                if (!await _jWTManager.ValidateToken(token))
                {
                    message.status = Message.error;
                    message.message = "Unauthorized";
                    return Unauthorized(message);
                }
            }
            else
            {
                message.status = Message.error;
                message.message = "Unauthorized";
                return Unauthorized(message);
            }
            #endregion
            var user = await _accountService.GetUserByIdAsync(id);
            try
            {
                res.status = Message.success;
                res.data = user;
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                message.status = Message.error;
                message.message = ex.Message;
                return Ok(message);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] UserModel request)
        {
            MessageModel message = new();

            try
            {
                if (!ModelState.IsValid)
                {
                    message.status = Message.error;
                    message.message = "Invalid request.";
                    return Ok(message);
                }

                #region Token Validation
                if (Request.Headers.TryGetValue("Authorization", out StringValues authorization))
                {
                    string token = Convert.ToString(authorization).Replace("Bearer", "").Trim();

                    if (!await _jWTManager.ValidateToken(token))
                    {
                        message.status = Message.error;
                        message.message = "Unauthorized";
                        return Unauthorized(message);
                    }
                }
                else
                {
                    message.status = Message.error;
                    message.message = "Unauthorized";
                    return Unauthorized(message);
                }
                #endregion
                string ErrorMessage = string.Empty;
                if (!Password.ValidatePassword(request.Password, out ErrorMessage))
                {
                    message.status = Message.error;
                    message.message = ErrorMessage;
                    return Ok(message);
                }
                string decryptPassword = string.Empty;
                decryptPassword = request.Password.Trim();

                request.Password = Password.CreatePasswordHash(request.Password.Trim(), Password.CreateSalt(Password.Password_Salt));

                request.UserName = request.UserName.ToLower();
                if (await _accountService.AddUserAsync(request))
                {
                    message.status = Message.success;
                    message.message = "User created successfully.";
                    return Ok(message);

                }
                else
                {
                    message.status = Message.error;
                    message.message = "User creation failed.";
                    return Ok(message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                message.status = Message.error;
                message.message = ex.Message;
                return Ok(message);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] UserModel request)
        {
            MessageModel message = new();
            try
            {
                if (!ModelState.IsValid)
                {
                    message.status = Message.error;
                    message.message = "Invalid request.";
                    return Ok(message);
                }

                #region Token Validation
                if (Request.Headers.TryGetValue("Authorization", out StringValues authorization))
                {
                    string token = Convert.ToString(authorization).Replace("Bearer", "").Trim();

                    if (!await _jWTManager.ValidateToken(token))
                    {
                        message.status = Message.error;
                        message.message = "Unauthorized";
                        return Unauthorized(message);
                    }
                }
                else
                {
                    message.status = Message.error;
                    message.message = "Unauthorized";
                    return Unauthorized(message);
                }
                #endregion
                var result = await _accountService.UpdateAsync(request);
                if (result)
                {
                    message.status = Message.success;
                    message.message = "User updated successfully.";
                }

                else
                {
                    message.status = Message.error;
                    message.message = "User updation failed.";
                }

                return Ok(message);

            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                message.status = Message.error;
                message.message = ex.Message;
                return Ok(message);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileModel request)
        {
            MessageModel message = new();
            try
            {
                if (!ModelState.IsValid)
                {
                    message.status = Message.error;
                    message.message = "Invalid request.";
                    return Ok(message);
                }
                #region Token Validation
                if (Request.Headers.TryGetValue("Authorization", out StringValues authorization))
                {
                    string token = Convert.ToString(authorization).Replace("Bearer", "").Trim();

                    if (!await _jWTManager.ValidateToken(token))
                    {
                        message.status = Message.error;
                        message.message = "Unauthorized";
                        return Unauthorized(message);
                    }
                }
                else
                {
                    message.status = Message.error;
                    message.message = "Unauthorized";
                    return Unauthorized(message);
                }
                #endregion
                var result = await _accountService.UpdateProfileAsync(request);
                if (result)
                {
                    message.status = Message.success;
                    message.message = "Profile updated successfully.";
                }
                else
                {
                    message.status = Message.error;
                    message.message = "Profile updation failed.";
                }

                return Ok(message);

            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                message.status = Message.error;
                message.message = ex.Message;
                return Ok(message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword request)
        {
            MessageModel message = new();
            try
            {

                var result = await _accountService.ChangePasswordAsync(request);
                if (result == "success")
                {
                    message.status = Message.success;
                    message.message = "Password changed successfully.";
                }
                else if (result == "notmatch")
                {

                    message.status = Message.error;
                    message.message = "Current password not match.";
                }

                else
                {
                    message.status = Message.error;
                    message.message = "Password updation failed.";
                }
                return Ok(message);
            }


            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                message.status = Message.error;
                message.message = ex.Message;
                return Ok(message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ResetPassword([BindRequired, FromQuery] int userId)
        {
            MessageModel message = new();
            try
            {
                #region Token Validation
                if (Request.Headers.TryGetValue("Authorization", out StringValues authorization))
                {
                    string token = Convert.ToString(authorization).Replace("Bearer", "").Trim();

                    if (!await _jWTManager.ValidateToken(token))
                    {
                        message.status = Message.error;
                        message.message = "Unauthorized";
                        return Unauthorized(message);
                    }
                }
                else
                {
                    message.status = Message.error;
                    message.message = "Unauthorized";
                    return Unauthorized(message);
                }
                #endregion
                var result = await _accountService.ResetPassword(userId);
                if (result)
                {
                    message.status = Message.success;
                    message.message = "Password reset successfully.";
                }
                else
                {

                    message.status = Message.error;
                    message.message = "Password reset failed.";
                }
                return Ok(message);
            }


            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                message.status = Message.error;
                message.message = ex.Message;
                return Ok(message);
            }
        }


        [HttpDelete("{userId:int:min(1)}")]
        [Authorize]
        public async Task<IActionResult> Delete(int userId)
        {
            MessageModel message = new();
            try
            {
                #region Token Validation
                if (Request.Headers.TryGetValue("Authorization", out StringValues authorization))
                {
                    string token = Convert.ToString(authorization).Replace("Bearer", "").Trim();

                    if (!await _jWTManager.ValidateToken(token))
                    {
                        message.status = Message.error;
                        message.message = "Unauthorized";
                        return Unauthorized(message);
                    }
                }
                else
                {
                    message.status = Message.error;
                    message.message = "Unauthorized";
                    return Unauthorized(message);
                }
                #endregion
                await _accountService.DeleteUserAsync(userId);
                message.status = Message.success;
                message.message = $"User with id = {userId} deleted.";
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                var innerMsg = ex.InnerException?.Message;
                if (innerMsg != null && innerMsg.Contains("REFERENCE"))
                    message.message = "You can't delete because this is used by another process.";
                else
                    message.message = ex.Message;
                message.status = Message.error;
                return Ok(message);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetMenu([BindRequired, FromQuery] int roleId, [BindRequired, FromQuery] int userId)
        {
            try
            {

                var menus = _accountService.GetMenu(roleId, userId);
                return Ok(menus);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUserLog(UserLogModel userLog)
        {
            MessageModel message = new();
            try
            {
                if (!ModelState.IsValid)
                {
                    message.status = Message.error;
                    message.message = "Invalid request";
                    return Ok(message);
                }

                var flag = await _accountService.AddUserLogAsync(userLog);
                if (flag)
                {
                    message.status = Message.success;
                    message.message = "User logs saved successfully.";
                    return Ok(message);
                }
                else
                {
                    message.status = Message.error;
                    message.message = "User logs saving failed";
                    return Ok(message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                message.status = Message.error;
                message.message = ex.Message;
                return Ok(message);
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserLogs(int? userId, int? lastDays)
        {
            ResponseModel<List<UserLogModel>> res = new();
            MessageModel message = new();
            #region Token Validation
            if (Request.Headers.TryGetValue("Authorization", out StringValues authorization))
            {
                string token = Convert.ToString(authorization).Replace("Bearer", "").Trim();

                if (!await _jWTManager.ValidateToken(token))
                {
                    message.status = Message.error;
                    message.message = "Unauthorized";
                    return Unauthorized(message);
                }
            }
            else
            {
                message.status = Message.error;
                message.message = "Unauthorized";
                return Unauthorized(message);
            }
            #endregion
            try
            {
                var userLogs = await _accountService.GetUserLogsAsync(userId, lastDays);
                res.status = "success";
                res.data = userLogs;
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                message.status = Message.error;
                message.message = ex.Message;
                return Ok(message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserLogsWithPagination([BindRequired, FromQuery] int pageNumber, [BindRequired, FromQuery] int rowsOfPage, int? userId)
        {
            ResponseModel<List<UserLogModel>> res = new();
            MessageModel message = new();
            #region Token Validation
            if (Request.Headers.TryGetValue("Authorization", out StringValues authorization))
            {
                string token = Convert.ToString(authorization).Replace("Bearer", "").Trim();

                if (!await _jWTManager.ValidateToken(token))
                {
                    message.status = Message.error;
                    message.message = "Unauthorized";
                    return Unauthorized(message);
                }
            }
            else
            {
                message.status = Message.error;
                message.message = "Unauthorized";
                return Unauthorized(message);
            }
            #endregion
            try
            {
                var userLogs = await _accountService.GetUserLogsWithPagination(userId, pageNumber, rowsOfPage);
                res.status = "success";
                res.data = userLogs;
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                message.status = Message.error;
                message.message = ex.Message;
                return Ok(message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserLogDetails(int? userId)
        {
            ResponseModel<List<GetUserLog_Result>> res = new();
            MessageModel message = new();
            #region Token Validation
            if (Request.Headers.TryGetValue("Authorization", out StringValues authorization))
            {
                string token = Convert.ToString(authorization).Replace("Bearer", "").Trim();

                if (!await _jWTManager.ValidateToken(token))
                {
                    message.status = Message.error;
                    message.message = "Unauthorized";
                    return Unauthorized(message);
                }
            }
            else
            {
                message.status = Message.error;
                message.message = "Unauthorized";
                return Unauthorized(message);
            }
            #endregion
            try
            {
                var userLogs = await _accountService.GetUserLogDetails(userId);
                res.status = "success";
                res.data = userLogs;
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                message.status = Message.error;
                message.message = ex.Message;
                return Ok(message);
            }
        }


        [HttpGet]
        public IActionResult CheckUserAlreadyLogin([BindRequired, FromQuery] string userName)
        {
            ResponseModel<bool> res = new();
            MessageModel message = new();
            try
            {
                var isLogin = _accountService.CheckUserAlreadyLogin(userName);
                res.status = "success";
                res.data = isLogin;
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                message.status = Message.error;
                message.message = ex.Message;
                return Ok(message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> CheckUserLogoutByOtherDevice([BindRequired, FromQuery] string sessionId)
        {
            ResponseModel<bool> res = new();
            MessageModel message = new();
            try
            {
                var isLogOut = await _accountService.CheckUserLogoutByOtherDevice(sessionId);
                res.status = "success";
                res.data = isLogOut;
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                message.status = Message.error;
                message.message = ex.Message;
                return Ok(message);
            }
        }

        [HttpGet]
        public IActionResult LogOutOtherDeviceUser([BindRequired, FromQuery] string userName)
        {
            ResponseModel<bool> res = new();
            MessageModel message = new();
            try
            {
                var isLogOut = _accountService.LogOutOtherDeviceUser(userName);
                res.status = "success";
                res.data = isLogOut;
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                message.status = Message.error;
                message.message = ex.Message;
                return Ok(message);
            }

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetRoleRights([BindRequired, FromQuery] int roleId)
        {
            ResponseModel<List<GetRoleRights_Result>> res = new();
            MessageModel message = new();
            #region Token Validation
            if (Request.Headers.TryGetValue("Authorization", out StringValues authorization))
            {
                string token = Convert.ToString(authorization).Replace("Bearer", "").Trim();

                if (!await _jWTManager.ValidateToken(token))
                {
                    message.status = Message.error;
                    message.message = "Unauthorized";
                    return Unauthorized(message);
                }
            }
            else
            {
                message.status = Message.error;
                message.message = "Unauthorized";
                return Unauthorized(message);
            }
            #endregion
            try
            {
                var roleRights_Results = await _accountService.GetRoleRights(roleId);
                res.status = "success";
                res.data = roleRights_Results;
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                message.status = Message.error;
                message.message = ex.Message;
                return Ok(message);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AssignRoleRights(InsertRoleRights_Result request)
        {
            MessageModel message = new();
            try
            {
                #region Token Validation
                if (Request.Headers.TryGetValue("Authorization", out StringValues authorization))
                {
                    string token = Convert.ToString(authorization).Replace("Bearer", "").Trim();

                    if (!await _jWTManager.ValidateToken(token))
                    {
                        message.status = Message.error;
                        message.message = "Unauthorized";
                        return Unauthorized(message);
                    }
                }
                else
                {
                    message.status = Message.error;
                    message.message = "Unauthorized";
                    return Unauthorized(message);
                }
                #endregion
                var flag = await _accountService.AssignRoleRights(request);
                if (flag)
                {
                    message.status = Message.success;
                    message.message = "Role rights assigned successfully.";
                }

                else
                {
                    message.status = Message.error;
                    message.message = "Role rights assigning failed.";
                }
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                message.status = Message.error;
                message.message = ex.Message;
                return Ok(message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserRights([BindRequired, FromQuery] int roleId, [BindRequired, FromQuery] int userId)
        {
            ResponseModel<List<GetRoleRights_Result>> res = new();
            MessageModel message = new();
            try
            {
                #region Token Validation
                if (Request.Headers.TryGetValue("Authorization", out StringValues authorization))
                {
                    string token = Convert.ToString(authorization).Replace("Bearer", "").Trim();

                    if (!await _jWTManager.ValidateToken(token))
                    {
                        message.status = Message.error;
                        message.message = "Unauthorized";
                        return Unauthorized(message);
                    }
                }
                else
                {
                    message.status = Message.error;
                    message.message = "Unauthorized";
                    return Unauthorized(message);
                }
                #endregion
                var userRights_Results = await _accountService.GetUserRights(roleId, userId);
                res.status = "success";
                res.data = userRights_Results;
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                message.status = Message.error;
                message.message = ex.Message;
                return Ok(message);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AssignUserRights(InsertUserRights_Result request)
        {
            MessageModel msg = new();
            try
            {
                #region Token Validation
                if (Request.Headers.TryGetValue("Authorization", out StringValues authorization))
                {
                    string token = Convert.ToString(authorization).Replace("Bearer", "").Trim();

                    if (!await _jWTManager.ValidateToken(token))
                    {
                        msg.status = Message.error;
                        msg.message = "Unauthorized";
                        return Unauthorized(msg);
                    }
                }
                else
                {
                    msg.status = Message.error;
                    msg.message = "Unauthorized";
                    return Unauthorized(msg);
                }
                #endregion
                var flag = await _accountService.AssignUserRights(request);
                if (flag)
                {
                    msg.status = Message.success;
                    msg.message = "User rights assigned successfully.";
                }

                else
                {
                    msg.status = Message.error;
                    msg.message = "User rights assigning failed.";
                }
                return Ok(msg);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                msg.status = Message.error;
                msg.message = ex.Message;
                return Ok(msg);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetButtonRights([BindRequired, FromQuery] int roleId, [BindRequired, FromQuery] int userId)
        {
            ResponseModel<List<GetButtonRights_Result>> res = new();
            MessageModel message = new();
            try
            {
                #region Token Validation
                if (Request.Headers.TryGetValue("Authorization", out StringValues authorization))
                {
                    string token = Convert.ToString(authorization).Replace("Bearer", "").Trim();

                    if (!await _jWTManager.ValidateToken(token))
                    {
                        message.status = Message.error;
                        message.message = "Unauthorized";
                        return Unauthorized(message);
                    }
                }
                else
                {
                    message.status = Message.error;
                    message.message = "Unauthorized";
                    return Unauthorized(message);
                }
                #endregion
                var buttonRights_Results = await _accountService.GetButtonRights(roleId, userId);
                res.status = "success";
                res.data = buttonRights_Results;
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                message.status = Message.error;
                message.message = ex.Message;
                return Ok(message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ResetUserRights([BindRequired, FromQuery] int roleId, [BindRequired, FromQuery] int userId)
        {
            MessageModel message = new();
            try
            {
                #region Token Validation
                if (Request.Headers.TryGetValue("Authorization", out StringValues authorization))
                {
                    string token = Convert.ToString(authorization).Replace("Bearer", "").Trim();

                    if (!await _jWTManager.ValidateToken(token))
                    {
                        message.status = Message.error;
                        message.message = "Unauthorized";
                        return Unauthorized(message);
                    }
                }
                else
                {
                    message.status = Message.error;
                    message.message = "Unauthorized";
                    return Unauthorized(message);
                }
                #endregion
                var flag = await _accountService.ResetUserRightsAsync(roleId, userId);
                if (flag)
                {
                    message.status = Message.success;
                    message.message = "User right settings successfully reset.";

                }
                else
                {
                    message.status = Message.error;
                    message.message = "User right settings reset failed.";
                }
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                message.status = Message.error;
                message.message = ex.Message;
                return Ok(message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([BindRequired, FromQuery] string key)
        {
            MessageModel message = new();
            if (string.IsNullOrEmpty(key))
            {
                message.status = Message.error;
                message.message = "Invalid request";
                return Ok(message);
            }
            else
            {
                var user = await _accountService.ValidateEmailUserName(key);
                if (user != null)
                {
                    await _accountService.ForgotPasswordSendEmail(user);
                }

                message.status = Message.success;
                message.message = "We’ve emailed you a new password to your registered email. Use it to sign in, then create a new password.";
                return Ok(message);
            }

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ValidateForgotPasswordLink([BindRequired, FromQuery] string key)
        {
            MessageModel message = new();
            if (string.IsNullOrEmpty(key))
            {
                message.status = Message.error;
                message.message = "Invalid request";
                return Ok(message);
            }
            else
            {
                var IsValid = await _accountService.ValidateForgotPasswordLink(key);
                if (IsValid)
                {
                    message.status = Message.success;
                    message.message = "success";
                    return Ok(message);
                }
                else
                {
                    message.status = Message.error;
                    message.message = "This reset link has expired. Request a new reset link to get a new password.";
                    return Ok(message);
                }
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPasswordLinkEmail([BindRequired, FromQuery] string key, [BindRequired, FromQuery] bool isProduction)
        {
            MessageModel message = new();
            if (string.IsNullOrEmpty(key))
            {
                message.status = Message.error;
                message.message = "Invalid request";
                return Ok(message);
            }
            else
            {
                var user = await _accountService.ValidateEmailUserName(key);
                if (user != null)
                {
                    await _accountService.ForgotPasswordLinkEmail(user,isProduction);
                }

                message.status = Message.success;
                message.message = "If your account is active, instructions for resetting your password will be sent to your email address.";
                return Ok(message);
            }

        }
    }
}

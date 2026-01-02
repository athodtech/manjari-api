using AthodBeTrackApi.Helpers;
using AthodBeTrackApi.Models;
using AthodBeTrackApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class DropdownController : ControllerBase
    {
        private readonly IDropdownService _dropdownService;
        private readonly ILogger<DropdownController> _logger;
        private readonly IJWTManagerService _jWTManager;
        readonly MessageModel message = new();
        public DropdownController(ILogger<DropdownController> logger, IDropdownService dropdownService, IJWTManagerService jWTManager)
        {
            _dropdownService = dropdownService;
            _logger = logger;
            _jWTManager = jWTManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetRole(int? roleId)
        {
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
                var list = await _dropdownService.GetRolesAsync(roleId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetState(int? stateId)
        {
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
                var list = await _dropdownService.GetStateAsync(stateId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDistrict(int? stateId, int? districtId)
        {
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
                var list = await _dropdownService.GetDistrictAsync(stateId, districtId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBlock(int? stateId, int? districtId, int? blockId)
        {
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
                var list = await _dropdownService.GetBlockAsync(stateId, districtId, blockId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetVillage(int? stateId, int? districtId, int? blockId, int? villageId)
        {
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

                var list = await _dropdownService.GetVillageAsync(stateId, districtId, blockId, villageId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestionType(int? id)
        {
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

                var list = await _dropdownService.GetQuestionTypeAsync(id);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestionChoice(int? id)
        {
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

                var list = await _dropdownService.GetQuestionChoiceAsync(id);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestionChoiceItem(int id)
        {
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

                var list = await _dropdownService.GetQuestionChoiceItemAsync(id);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetActivity(int? id)
        {
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

                var list = await _dropdownService.GetActivityAsync(id);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserByRoleId(int? roleId)
        {
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

                var list = await _dropdownService.GetUserByRoleIdAsync(roleId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestionGroup(int? id, bool? isDefault)
        {
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

                var list = await _dropdownService.GetQuestionGroupAsync(id, isDefault);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestionGroupExceptId(int? exceptGroupId)
        {
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

                var list = await _dropdownService.GetQuestionGroupExceptIdAsync(exceptGroupId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserLocationLevel(int userId)
        {
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

                var list = await _dropdownService.GetUserLocationLevelDll(userId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetUserLocationState(int userId)
        {
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

                var list = await _dropdownService.GetUserLocationStateDll(userId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetUserLocationDistrict(int userId, int stateId)
        {
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

                var list = await _dropdownService.GetUserLocationDistrictDll(userId, stateId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetUserLocationBlock(int userId, int stateId, int districtId)
        {
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

                var list = await _dropdownService.GetUserLocationBlockDll(userId, stateId, districtId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserLocationVillage(int userId, int stateId, int districtId, int blockId)
        {
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
                var list = await _dropdownService.GetUserLocationVillageDll(userId, stateId, districtId, blockId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestion(int? id)
        {
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

                var list = await _dropdownService.GetQuestionAsync(id);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestionReportingFrequency(int? id)
        {
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

                var list = await _dropdownService.GetQuestionReportingFrequencyAsync(id);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetActivityQuestionDll()
        {
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

                var list = await _dropdownService.GetActivityQuestionDll();
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> RPT_GetActivityQuestionByGroupIds([BindRequired, FromQuery] string groupIds)
        {
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

                var list = await _dropdownService.GetActivityQuestionByGroupId(groupIds);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> RPT_GetLocation(int? userId)
        {
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

                var list = await _dropdownService.RPT_GetLocation(userId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetTag(int? tagId)
        {
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

                var list = await _dropdownService.GetTagAsync(tagId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> RPT_GetUserLocationStates(int userId)
        {
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

                var list = await _dropdownService.RPT_GetUserLocationStates(userId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAssignUserLocationDropdown([BindRequired, FromQuery] string flag, [BindRequired, FromQuery] string ids)
        {
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

                var list = await _dropdownService.GetAssignUserLocationDropdown(flag, ids);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> RPT_GetAssignUserLocationDropdown([BindRequired, FromQuery] int userId, [BindRequired, FromQuery] string flag, [BindRequired, FromQuery] string ids)
        {
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

                var list = await _dropdownService.RPT_GetAssignUserLocationDropdown(userId, flag, ids);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> RPT_GetAssignUserLocationDropdown2([BindRequired, FromQuery] int userId, [BindRequired, FromQuery] string flag, string sIds, string dIds, string bIds)
        {
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

                var list = await _dropdownService.RPT_GetAssignUserLocationDropdown2(userId, flag, sIds, dIds, bIds);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> RPT_GetTagsdll(string groupIds)
        {
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

                var list = await _dropdownService.RPT_GetTagsdll(groupIds);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> RPT_GetQuestiondll(string tagIds, int activityCategoryMapping)
        {
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

                var list = await _dropdownService.RPT_GetQuestiondll(tagIds, activityCategoryMapping);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> RPT_GetQuestiondll2(string groupIds, string tagIds, int activityCategoryMapping)
        {
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

                var list = await _dropdownService.RPT_GetQuestiondll2(groupIds, tagIds, activityCategoryMapping);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> RPT_GetQuestiondllWithPeriod(string tagIds, int activityCategoryMapping, int? period, DateTime? fromdate, DateTime? todate)
        {
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
                string strwhere = string.Empty;
                if (period.HasValue)
                {
                    if (period.Value != (int)ReportPeriods.AllTime)
                    {
                        var str = MakePeriodLogic(period.Value);
                        if (!string.IsNullOrEmpty(str))
                        {
                            strwhere += " AND " + str + "";
                        }
                    }
                }
                else
                {
                    if (fromdate.HasValue && todate.HasValue)
                    {
                        strwhere += " AND CONVERT(DATE,v.UpdatedOn) between CONVERT(DATE,'" + fromdate.Value.Date.ToString("yyyy-MM-dd") + "') and CONVERT(DATE,'" + todate.Value.Date.ToString("yyyy-MM-dd") + "')";
                    }
                }

                var list = await _dropdownService.RPT_GetQuestiondll(tagIds, activityCategoryMapping, strwhere);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        private string MakePeriodLogic(int period)
        {
            string str = string.Empty;

            if (period == (int)ReportPeriods.Last7Days)
            {
                str = "  CONVERT(DATE,v.UpdatedOn) between CONVERT(DATE,'" + DateTime.Now.AddDays(-6).Date.ToString("yyyy-MM-dd") + "') and CONVERT(DATE,'" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "')";
            }
            else if (period == (int)ReportPeriods.Last30Days)
            {
                str = "  CONVERT(DATE,v.UpdatedOn) between CONVERT(DATE,'" + DateTime.Now.AddDays(-29).Date.ToString("yyyy-MM-dd") + "') and CONVERT(DATE,'" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "')";
            }
            else if (period == (int)ReportPeriods.Last90Days)
            {
                str = "  CONVERT(DATE,v.UpdatedOn) between CONVERT(DATE,'" + DateTime.Now.AddDays(-89).Date.ToString("yyyy-MM-dd") + "') and CONVERT(DATE,'" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "')";
            }
            else if (period == (int)ReportPeriods.Last1Year)
            {
                str = "  CONVERT(DATE,v.UpdatedOn) between CONVERT(DATE,'" + DateTime.Now.AddYears(-1).Date.ToString("yyyy-MM-dd") + "') and CONVERT(DATE,'" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "')";
            }
            return str;
        }

        [HttpGet]
        public async Task<IActionResult> GetChartType()
        {
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

                var list = await _dropdownService.GetChartType();
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRolesExcludingRoleIdAsync(int? roleId)
        {
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

                var list = await _dropdownService.GetRolesExcludingRoleIdAsync(roleId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersExcludingRoleIdAsync(int? roleId)
        {
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

                var list = await _dropdownService.GetUsersExcludingRoleIdAsync(roleId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> RPT_GetQuestionGroup([BindRequired, FromQuery] int userId)
        {
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

                var list = await _dropdownService.RPT_GetQuestionGroupAsync(userId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> RPT_GetTags(string groupIds, [BindRequired, FromQuery] int userId)
        {
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

                var list = await _dropdownService.RPT_GetTagsAsync(groupIds, userId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetApplicationEventTypeAsync(int? id)
        {
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

                var list = await _dropdownService.GetApplicationEventTypeAsync(id);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUsersExcludingRoleId(int? roleId)
        {
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

                var list = await _dropdownService.GetAllUsersExcludingRoleIdAsync(roleId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetActivityWithMappingidDll(int? userId)
        {
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

                var list = await _dropdownService.GetActivityWithMappingidDll(userId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }
    }
}

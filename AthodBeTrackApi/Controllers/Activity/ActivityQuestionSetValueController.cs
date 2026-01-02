using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AthodBeTrackApi.Helpers;
using AthodBeTrackApi.Models;
using AthodBeTrackApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;

namespace AthodBeTrackApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ActivityQuestionSetValueController : ControllerBase
    {
        private readonly IActivityQuestionSetService _service;
        private readonly ILogger<ActivityQuestionSetValueController> _logger;
        private readonly IJWTManagerService _jWTManager;
        readonly MessageModel msg = new();
        public ActivityQuestionSetValueController(IActivityQuestionSetService service, ILogger<ActivityQuestionSetValueController> logger, IJWTManagerService jWTManager)
        {
            _service = service;
            _logger = logger;
            _jWTManager = jWTManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int activityQuestionSetId)
        {
            var res = new ResponseModel<List<ActivityQuestionSetValueModel>>();
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
                var activityQuestionSetValues = await _service.GetActivityQuestionSetValue(activityQuestionSetId);
                res.status = Message.success;
                res.data = activityQuestionSetValues;
                return Ok(res);
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
        [Route("GetActivityQuestionValue")]
        public async Task<IActionResult> GetActivityQuestionValue(int activityQuestionSetId)
        {
            var res = new ResponseModel<List<ActivityQuestionValueModel>>();
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
                var activityQuestionValues = _service.GetActivityQuestionValue(activityQuestionSetId);
                res.status = Message.success;
                res.data = activityQuestionValues;
                return Ok(res);
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
        [Route("GetActivityQuestionValueSet")]
        public async Task<IActionResult> GetActivityQuestionValueSet(int activityQuestionSetId, int flag)
        {
            var res = new ResponseModel<List<ActivityQuestionValueModel>>();
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
                var activityQuestionValueSet = _service.GetActivityQuestionValueSet(activityQuestionSetId, flag);
                res.status = Message.success;
                res.data = activityQuestionValueSet;
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                msg.status = Message.error;
                msg.message = ex.Message;
                return Ok(msg);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ActivityQuestionSetValueViewModel request)
        {
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
                if (!ModelState.IsValid)
                {
                    msg.status = Message.error;
                    msg.message = "Invalid request.";
                    return Ok(msg);
                }


                var result = _service.UpdateActivityQuestionSetValue(request);
                if (result)
                {
                    msg.status = Message.success;
                    msg.message = "Data saved successfully.";
                }

                else
                {
                    msg.status = Message.error;
                    msg.message = "Data saving failed.";
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
        [Route("GetActivityQuestionSet")]
        public async Task<IActionResult> GetActivityQuestionSet(int activityQuestionSetId, int flag)
        {
            var res = new ResponseModel<List<ActivityQuestionSetValueModel>>();
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
                var activityQuestionSetValues = _service.GetActivityQuestionSets(activityQuestionSetId, flag);
                res.status = Message.success;
                res.data = activityQuestionSetValues;
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                msg.status = Message.error;
                msg.message = ex.Message;
                return Ok(msg);
            }
        }

        [HttpPost]
        [Route("MarkActivityQuestionSetPrimary")]
        public async Task<IActionResult> MarkActivityQuestionSetPrimary([BindRequired, FromQuery] int activityQuestionSetId, [BindRequired, FromQuery] int activityQuestionId, [BindRequired, FromQuery] int sno, [BindRequired, FromQuery] bool primary)
        {
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

                var result = _service.MarkActivityQuestionSetPrimary(activityQuestionSetId, activityQuestionId, sno, primary);
                if (result)
                {
                    msg.status = Message.success;
                    msg.message = "Set updated successfully.";
                }

                else
                {
                    msg.status = Message.error;
                    msg.message = "Set updation failed.";
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

        [HttpPost]
        [Route("AddActivityQuestionSet")]
        public async Task<IActionResult> AddActivityQuestionSet(AddActivityQuestionSetModel request)
        {
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

                var result = _service.AddActivityQuestionSet(request.activityQuestionSetId, request.userId, request.flag);
                if (result)
                {
                    msg.status = Message.success;
                    msg.message = "Set added successfully.";
                }

                else
                {
                    msg.status = Message.error;
                    msg.message = "Set adding failed.";
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
        [Route("GetActivityUserLocation")]
        public async Task<IActionResult> GetActivityUserLocation(int activityQuestionSetId)
        {
            var res = new ResponseModel<ActivityUserLocationModel>();
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
                var activityUserLocation = await _service.GetActivityUserLocationAsync(activityQuestionSetId);
                res.status = Message.success;
                res.data = activityUserLocation;
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                msg.status = Message.error;
                msg.message = ex.Message;
                return Ok(msg);
            }
        }

        [HttpDelete]
        [Route("DeleteActivityQuestionSet")]
        public async Task<IActionResult> DeleteActivityQuestionSet(int activityQuestionSetId, int userId, int sno, int flag)
        {
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

                var result = _service.DeleteActivityQuestionSet(activityQuestionSetId, userId, sno, flag);
                if (result)
                {
                    msg.status = Message.success;
                    msg.message = "Set deleted successfully.";
                }

                else
                {
                    msg.status = Message.error;
                    msg.message = "Set deletion failed.";
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

        [HttpPost]
        [Route("InsertActivityDocument")]
        public async Task<IActionResult> InsertActivityDocument(ActivityDocumentModel model)
        {
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
                _service.InsertActivityDocument(model);
                msg.status = Message.success;
                msg.message = "Document saved successfully.";
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
        [Route("GetActivityDocument")]
        public async Task<IActionResult> GetActivityDocument([BindRequired, FromQuery] int activityQuestionSetId)
        {
            var res = new ResponseModel<List<GetActivityDocumentModel>>();
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

                var activityDocuments = await _service.GetActivityDocumentAsync(activityQuestionSetId);
                res.status = Message.success;
                res.data = activityDocuments;
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                msg.status = Message.error;
                msg.message = ex.Message;
                return Ok(msg);

            }
        }

        [HttpDelete]
        [Route("DeleteActivityDocument")]
        public async Task<IActionResult> DeleteActivityDocument([BindRequired, FromQuery] int activityDocumentId)
        {
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

                var result = _service.DeleteActivityDocument(activityDocumentId);
                if (result)
                {
                    msg.status = Message.success;
                    msg.message = "Document deleted successfully.";
                }

                else
                {
                    msg.status = Message.error;
                    msg.message = "Document deletion failed.";
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

        [HttpPost]
        [Route("SubmitActivityQuestionSet")]
        public async Task<IActionResult> SubmitActivityQuestionSet([BindRequired, FromQuery] int activityQuestionSetId, [BindRequired, FromQuery] int status, [BindRequired, FromQuery] int userId)
        {
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

                var result = await _service.SubmitActivityQuestionSetAsync(activityQuestionSetId, status, userId);
                if (result)
                {
                    await _service.CreateUserActionLogHH(userId, DateTime.Now, activityQuestionSetId, status);
                    msg.status = Message.success;
                    msg.message = "Data submitted successfully.";
                }

                else
                {
                    msg.status = Message.error;
                    msg.message = "Data submission failed.";
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

        [HttpPost]
        [Route("ArchivalActivityQuestionSet")]
        public async Task<IActionResult> ArchivalActivityQuestionSet([BindRequired, FromQuery] int activityQuestionSetId, [BindRequired, FromQuery] int status, [BindRequired, FromQuery] int userId)
        {
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

                var result = await _service.ArchivalActivityQuestionSetAsync(activityQuestionSetId, status, userId);
                if (result)
                {
                    await _service.CreateUserActionLogHH(userId, DateTime.Now, activityQuestionSetId, status);
                    msg.status = Message.success;
                    msg.message = "Data archived successfully.";
                }

                else
                {
                    msg.status = Message.error;
                    msg.message = "Data archival failed.";
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

        [HttpPost]
        [Route("LockActivityQuestionSet")]
        public async Task<IActionResult> LockActivityQuestionSet([BindRequired, FromQuery] int activityQuestionSetId, [BindRequired, FromQuery] int status, [BindRequired, FromQuery] int userId)
        {
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

                int activityStatus = await _service.GetActivityQuestionSetStatus(activityQuestionSetId);
                if (activityStatus != 1)
                {
                    msg.status = Message.error;
                    msg.message = "Submit the form before locking.";
                }
                else
                {
                    var result = await _service.LockActivityQuestionSetAsync(activityQuestionSetId, status, userId);
                    if (result)
                    {
                        await _service.CreateUserActionLogHH(userId, DateTime.Now, activityQuestionSetId, status);
                        msg.status = Message.success;
                        msg.message = "Data locked successfully.";
                    }

                    else
                    {
                        msg.status = Message.error;
                        msg.message = "Data locking failed.";
                    }
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

        [HttpPost]
        [Route("ValidateActivityQuestionSet")]
        public async Task<IActionResult> ValidateActivityQuestionSet([BindRequired, FromQuery] int activityQuestionSetId)
        {
            var res = new ResponseModel<List<ValidateActivityQuestionSet>>();
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

                var questionSets = await _service.ValidateActivityQuestionSet(activityQuestionSetId);
                res.status = Message.success;
                res.data = questionSets;
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                msg.status = Message.error;
                msg.message = ex.Message;
                return Ok(msg);

            }
        }
        [HttpPost]
        [Route("CheckUniqueHouseHold")]
        public async Task<IActionResult> CheckUniqueHouseHold([BindRequired, FromQuery] string name, [BindRequired, FromQuery] string mobile, [BindRequired, FromQuery] int activityQuestionSetId)
        {
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

                var result = await _service.CheckUniqueHouseHold(name, mobile, activityQuestionSetId);
                if (result)
                {
                    msg.status = Message.error;
                    msg.message = $"Household member name: {name} with mobile number: {mobile} is already exists.";
                }

                else
                {
                    msg.status = Message.success;
                    msg.message = Message.success;
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

        [HttpPost]
        [Route("CheckUniqueHouseHoldBeforeCreation")]
        public async Task<IActionResult> CheckUniqueHouseHoldBeforeCreation([BindRequired, FromQuery] string name, [BindRequired, FromQuery] string mobile, [BindRequired, FromQuery] int stateId, [BindRequired, FromQuery] int districtId, [FromQuery] int? blockId, [FromQuery] int? villageId)
        {
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

                var result = await _service.CheckUniqueHouseHoldBeforeCreation(stateId, districtId, blockId, villageId, name, mobile);
                if (result)
                {
                    msg.status = Message.error;
                    msg.message = $"Household member name: {name} with mobile number: {mobile} is already exists.";
                }

                else
                {
                    msg.status = Message.success;
                    msg.message = Message.success;
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
        [Route("GetActivityQuestionSetStatus")]
        public async Task<IActionResult> GetActivityQuestionSetStatus([BindRequired, FromQuery] int activityQuestionSetId)
        {
            var res = new ResponseModel<ActivityQuestionSetStatusModel>();
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

                var result = await _service.GetActivityQuestionSetStatusDetails(activityQuestionSetId);
                res.status = Message.success;
                res.data = result;
                return Ok(res);
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
        [Route("GetActivityQuestionDueHistory")]
        public async Task<IActionResult> GetActivityQuestionDueHistory([BindRequired, FromQuery] int activityQuestionSetId, [BindRequired, FromQuery] int activityQuestionId, [BindRequired, FromQuery] int sno)
        {
            var res = new ResponseModel<List<GetActivityQuestionDueHistory_Result>>();
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

                var activityQuestionDueHistory_Results = await _service.GetActivityQuestionDueHistory(activityQuestionSetId, activityQuestionId, sno);
                res.status = Message.success;
                res.data = activityQuestionDueHistory_Results;
                return Ok(res);
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
        [Route("GetActivityQuestionSetGroup")]
        public async Task<IActionResult> GetActivityQuestionSetGroup([BindRequired, FromQuery] int activityQuestionSetId)
        {
            var res = new ResponseModel<List<GetActivityQuestionSetGroup_Result>>();
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

                var activityDocuments = await _service.GetActivityQuestionSetGroup(activityQuestionSetId);
                res.status = Message.success;
                res.data = activityDocuments;
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                msg.status = Message.error;
                msg.message = ex.Message;
                return Ok(msg);

            }
        }

        [HttpPost]
        [Route("AddActivityQuestionSetGroup")]
        public async Task<IActionResult> AddActivityQuestionSetGroup([BindRequired, FromQuery] int activityQuestionSetId, [BindRequired, FromQuery] int userId, [BindRequired, FromQuery] string groupIds)
        {
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
                var result = await _service.AddActivityQuestionSetGroup(activityQuestionSetId, userId, groupIds);
                if (result)
                {
                    msg.status = Message.success;
                    msg.message = "Themes updated.";
                }

                else
                {
                    msg.status = Message.error;
                    msg.message = "Themes updation failed.";
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
        [Route("GetUserLocationCount")]
        public async Task<IActionResult> GetUserLocationCount([BindRequired, FromQuery] int userId)
        {
            var res = new ResponseModel<GetUserLocationCount_Result>();
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

                var activityUserLocation = await _service.GetUserLocationCount(userId);
                res.status = Message.success;
                res.data = activityUserLocation;
                return Ok(res);
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
        [Route("GetActivityDates")]
        public async Task<IActionResult> GetActivityDates([BindRequired, FromQuery] int activityCategoryMappingId)
        {
            var res = new ResponseModel<GetActivityDates_Result>();
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

                var activityDates = await _service.GetActivityDates(activityCategoryMappingId);
                res.status = Message.success;
                res.data = activityDates;
                return Ok(res);
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
        [Route("GetActivityTotalDues")]
        public async Task<IActionResult> GetActivityTotalDues([BindRequired, FromQuery] int activityQuestionSetId)
        {
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

                var res = new ResponseModel<int>();
                var count = await _service.GetActivityTotalDues(activityQuestionSetId);
                res.status = Message.success;
                res.data = count;
                return Ok(res);

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
        [Route("GetHouseholdActionLog")]
        public async Task<IActionResult> GetHouseholdActionLog(string activityCategoryIds, string userIds, int? period, DateTime? fromdate, DateTime? todate)
        {
            var res = new ResponseModel<List<HouseholdActionLog_Result>>();
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
                DateTime fdate;
                DateTime tdate;
                if (period.HasValue)
                {
                    MakePeriodLogic(period.Value, out fdate, out tdate);
                    fromdate = fdate;
                    todate = tdate;
                }
                var householdActionLog_Result = await _service.GetHouseholdActionLog(activityCategoryIds, userIds, fromdate ?? DateTime.Now.Date, todate ?? DateTime.Now.Date);
                res.status = Message.success;
                res.data = householdActionLog_Result;
                return Ok(res);
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
        [Route("GetHouseholdMonthlyActionLog")]
        public async Task<IActionResult> GetHouseholdMonthlyActionLog(string activityCategoryIds, string userIds, int? period, DateTime? fromdate, DateTime? todate)
        {
            var res = new ResponseModel<List<HouseholdMonthlyActionLog_Result>>();
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
                DateTime fdate;
                DateTime tdate;
                if (period.HasValue)
                {
                    MakePeriodLogic(period.Value, out fdate, out tdate);
                    fromdate = fdate;
                    todate = tdate;
                }
                var householdMonthlyActionLog = await _service.GetHouseholdMonthlyActionLog(activityCategoryIds,userIds, fromdate ?? DateTime.Now.Date, todate ?? DateTime.Now.Date);
                res.status = Message.success;
                res.data = householdMonthlyActionLog;
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                msg.status = Message.error;
                msg.message = ex.Message;
                return Ok(msg);
            }
        }


        [HttpPost]
        [Route("CreateUserActionLogHH")]
        public async Task<IActionResult> CreateUserActionLogHH([BindRequired, FromQuery] int userId, [BindRequired, FromQuery] int activityQuestionSetId, [BindRequired, FromQuery] int status)
        {
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

                var result = await _service.CreateUserActionLogHH(userId, actionLogTime: DateTime.Now, activityQuestionSetId, status);
                if (result)
                {
                    msg.status = Message.success;
                    msg.message = "Household user action log saved.";
                }

                else
                {
                    msg.status = Message.error;
                    msg.message = "Household user action log saving failed.";
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

        private void MakePeriodLogic(int period, out DateTime fromDate, out DateTime todate)
        {
            if (period == (int)LogPeriods.Today)
            {
                fromDate = DateTime.Now.Date;
                todate = DateTime.Now.Date;

            }
            else if (period == (int)LogPeriods.Yesterday)
            {
                fromDate = DateTime.Now.AddDays(-1).Date;
                todate = DateTime.Now.AddDays(-1).Date;
            }
            else if (period == (int)LogPeriods.Last7Days)
            {
                fromDate = DateTime.Now.AddDays(-6).Date;
                todate = DateTime.Now.Date;
            }
            else if (period == (int)LogPeriods.Last30Days)
            {
                fromDate = DateTime.Now.AddDays(-29).Date;
                todate = DateTime.Now.Date;
            }
            else if (period == (int)LogPeriods.ThisMonth)
            {
                string yyyy = DateTime.Now.Year.ToString();
                string mm = DateTime.Now.Month > 9 ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month.ToString();
                var fDate = $"{yyyy}-{mm}-01";

                fromDate = Convert.ToDateTime(fDate);
                todate = DateTime.Now.Date;
            }
            else if (period == (int)LogPeriods.LastMonth)
            {

                var lastMonth = DateTime.Today.AddMonths(-1);
                DateTime fDate = new(lastMonth.Year, lastMonth.Month, 1);
                DateTime tdate = new(lastMonth.Year, lastMonth.Month, DateTime.DaysInMonth(lastMonth.Year, lastMonth.Month));

                fromDate = fDate.Date;
                todate = tdate.Date;
            }
            else
            {
                fromDate = DateTime.Now.Date;
                todate = DateTime.Now.Date;
            }
        }
    }
}

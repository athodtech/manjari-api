using AthodBeTrackApi.Helpers;
using AthodBeTrackApi.Models;
using AthodBeTrackApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Controllers.Survey
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ActivityQuestionController : ControllerBase
    {
        private readonly IActivityQuestionService _service;
        private readonly ILogger<ActivityQuestionController> _logger;
        private readonly IJWTManagerService _jWTManager;
        readonly MessageModel msg = new();
        public ActivityQuestionController(IActivityQuestionService service, ILogger<ActivityQuestionController> logger, IJWTManagerService jWTManager)
        {
            _service = service;
            _logger = logger;
            _jWTManager = jWTManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(int? activityId)
        {
            var res = new ResponseModel<List<ActivityQuestionModel>>();
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
                var questions = await _service.GetAsync(activityId);
                res.status = Message.success;
                res.data = questions;
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
        [Route("GetAssignQuestion")]
        public async Task<IActionResult> GetAssignActivityQuestion(int activityId)
        {
            var res = new ResponseModel<List<ActivityQuestionModel>>();
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
                var questions = await _service.GetAssignActivityQuestion(activityId);
                res.status = Message.success;
                res.data = questions;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var res = new ResponseModel<ActivityQuestionModel>();
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
            var question = await _service.GetByIdAsync(id);
            if (question == null)
            {
                msg.status = Message.error;
                msg.message = $"Question with Id: { id} not found.";
                return Ok(msg);
            }
            res.status = Message.success;
            res.data = question;
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<ActivityQuestionModel> request)
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
                    msg.message = "Invalid request";
                    return Ok(msg);
                }
                var result = await _service.AddAsync(request);
                if (result)
                {
                    msg.status = Message.success;
                    msg.message = "Question added successfully.";
                }

                else
                {
                    msg.status = Message.error;
                    msg.message = "Question adding failed.";
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


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ActivityQuestionModel request)
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

                var result = await _service.UpdateAsync(request);
                if (result)
                {
                    msg.status = Message.success;
                    msg.message = "Question updated successfully.";
                }

                else
                {
                    msg.status = Message.success;
                    msg.message = "Question updation failed.";
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

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Delete(int id)
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
                await _service.DeleteAsync(id);
                msg.status = Message.success;
                msg.message = $"Question with Id: { id} deleted.";
                return Ok(msg);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                var innerMsg = ex.InnerException?.Message;
                if (innerMsg != null && innerMsg.Contains("REFERENCE"))
                    msg.message = "You can't delete because this is used by another process.";
                else
                    msg.message = ex.Message;
                msg.status = Message.error;
                return Ok(msg);
            }
        }
        

    }
}

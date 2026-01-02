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

namespace AthodBeTrackApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionChoiceItemController : ControllerBase
    {
        private readonly IQuestionChoiceItemService _service;
        private readonly ILogger<QuestionChoiceItemController> _logger;
        private readonly IJWTManagerService _jWTManager;
        readonly MessageModel msg = new();

        public QuestionChoiceItemController(IQuestionChoiceItemService service, ILogger<QuestionChoiceItemController> logger, IJWTManagerService jWTManager)
        {
            _service = service;
            _logger = logger;
            _jWTManager = jWTManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(int? questionChoiceId)
        {
            var res = new ResponseModel<List<QuestionChoiceItemModel>>();
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

                var questionChoiceItems = await _service.GetAsync(questionChoiceId);
                res.status = Message.success;
                res.data = questionChoiceItems;
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
            var res = new ResponseModel<QuestionChoiceItemModel>();
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

            var questionChoiceItem = await _service.GetByIdAsync(id);
            if (questionChoiceItem == null)
            {
                msg.status = Message.error;
                msg.message = $"Question Choice Item with Id: { id} not found.";
                return Ok(msg);
            }
            res.status = Message.success;
            res.data = questionChoiceItem;
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] QuestionChoiceItemModel request)
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
                    msg.message = "Question Choice Item added successfully.";
                }

                else
                {
                    msg.status = Message.error;
                    msg.message = "Question Choice Item adding failed.";
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
        public async Task<IActionResult> Put([FromBody] QuestionChoiceItemModel request)
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
                    msg.message = "Question Choice Item updated successfully.";
                }

                else
                {
                    msg.status = Message.success;
                    msg.message = "Question Choice Item updation failed.";
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
                msg.message = $"Question Choice Item with Id: { id} deleted.";
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
    }
}

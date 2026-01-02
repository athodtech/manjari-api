using AthodBeTrackApi.Helpers;
using AthodBeTrackApi.Models;
using AthodBeTrackApi.Services;
using Microsoft.AspNetCore.Authorization;
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
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly ILogger<TagController> _logger;
        private readonly IJWTManagerService _jWTManager;
        readonly MessageModel msg = new();
        public TagController(ITagService tagService, ILogger<TagController> logger, IJWTManagerService jWTManager)
        {
            _tagService = tagService;
            _logger = logger;
            _jWTManager = jWTManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            ResponseModel<List<TagModel>> res = new ResponseModel<List<TagModel>>();
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

                var tags = await _tagService.GetAsync();
                res.status = Message.success;
                res.data = tags;
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

            var res = new ResponseModel<TagModel>();
            var tag = await _tagService.GetByIdAsync(id);
            if (tag == null)
            {
                msg.status = Message.error;
                msg.message = $"Tag with id: { id} not found.";
                return Ok(msg);
            }
            res.status = Message.success;
            res.data = tag;
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TagModel request)
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
                var result = await _tagService.AddAsync(request);
                if (result)
                {
                    msg.status = Message.success;
                    msg.message = "Tag added successfully.";
                }
                else
                {
                    msg.status = Message.error;
                    msg.message = "Tag adding failed.";
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
        public async Task<IActionResult> Put([FromBody] TagModel request)
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

                var result = await _tagService.UpdateAsync(request);
                if (result)
                {
                    msg.status = Message.success;
                    msg.message = "Tag updated successfully.";
                }

                else
                {
                    msg.status = Message.error;
                    msg.message = "Tag updation failed.";
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

                await _tagService.DeleteAsync(id);
                msg.status = Message.success;
                msg.message = $"Tag with id: { id} deleted.";
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

        [HttpGet]
        [Route("GetQuestionTagMapping")]
        public async Task<IActionResult> GetQuestionTagMapping(int tagId)
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

            var res = new ResponseModel<List<GetQuestionTagMapping_Result>>();
            try
            {
                var results = await _tagService.GetQuestionTagMapping(tagId);
                res.status = Message.success;
                res.data = results;
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
        [Route("GetQuestionTagForMapping")]
        public async Task<IActionResult> GetQuestionTagForMapping(int tagId)
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

            var res = new ResponseModel<List<GetQuestionTagForMapping_Result>>();
            try
            {
                var results = await _tagService.GetQuestionTagForMapping(tagId);
                res.status = Message.success;
                res.data = results;
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
        [Route("UpdateQuestionTagMapping")]
        public async Task<IActionResult> UpdateQuestionTagMapping(UpdateQuestionTagMapping_Result request)
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
          
            try
            {
                var flag = await _tagService.UpdateQuestionTagMapping(request);
                if (flag)
                {
                    msg.status = Message.success;
                    msg.message = "Questions mapped with tag successfully.";
                }

                else
                {
                    msg.status = Message.error;
                    msg.message = "Questions mapping with tag failed.";
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
    }
}

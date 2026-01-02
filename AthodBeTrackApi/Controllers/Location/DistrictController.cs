using AthodBeTrackApi.Helpers;
using AthodBeTrackApi.Models;
using AthodBeTrackApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class DistrictController : ControllerBase
    {
        private readonly ILocationService _service;
        private readonly ILogger<DistrictController> _logger;
        private readonly IJWTManagerService _jWTManager;
        readonly MessageModel msg = new();
        public DistrictController(ILocationService service, ILogger<DistrictController> logger, IJWTManagerService jWTManager)
        {
            _service = service;
            _logger = logger;
            _jWTManager = jWTManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var res = new ResponseModel<List<DistrictModel>>();
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

                var districts = await _service.GetDistrictAsync();
                res.status = Message.success;
                res.data = districts;
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
            var res = new ResponseModel<DistrictModel>();
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

            var district = await _service.GetDistrictByIdAsync(id);
            if (district == null)
            {
                msg.status = Message.error;
                msg.message = $"District with Id: { id} not found.";
                return Ok(msg);
            }
            res.status = Message.success;
            res.data = district;
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DistrictModel request)
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
                var result = await _service.AddDistrictAsync(request);
                if (result)
                {
                    msg.status = Message.success;
                    msg.message = "District added successfully.";
                }

                else
                {
                    msg.status = Message.error;
                    msg.message = "District adding failed.";
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
        public async Task<IActionResult> Put([FromBody] DistrictModel request)
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

                var identity = await _service.CheckDistictIsUsed(request.DistrictId);
                if (identity != null)
                {
                    if (identity.Stateid != request.StateId)
                    {
                        msg.status = Message.error;
                        msg.message = "District has been used in report, you do not change parent location.";
                    }
                    else
                    {
                        var result = await _service.UpdateDistrictAsync(request);
                        if (result)
                        {
                            msg.status = Message.success;
                            msg.message = "District updated successfully.";
                        }

                        else
                        {
                            msg.status = Message.success;
                            msg.message = "District updation failed.";
                        }
                    }
                }
                else
                {
                    var result = await _service.UpdateDistrictAsync(request);
                    if (result)
                    {
                        msg.status = Message.success;
                        msg.message = "District updated successfully.";
                    }

                    else
                    {
                        msg.status = Message.success;
                        msg.message = "District updation failed.";
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

                await _service.DeleteDistrictAsync(id);
                msg.status = Message.success;
                msg.message = $"District with Id: { id} deleted.";
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

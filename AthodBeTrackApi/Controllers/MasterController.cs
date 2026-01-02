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
using System.Collections.Generic;
using System.Threading.Tasks;
namespace AthodBeTrackApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class MasterController : ControllerBase
    {
        private readonly IMasterService _masterService;
        private readonly ILogger<MasterController> _logger;
        private readonly IJWTManagerService _jWTManager;
        readonly MessageModel msg = new();
        public MasterController(ILogger<MasterController> logger, IMasterService masterService, IJWTManagerService jWTManager)
        {
            _masterService = masterService;
            _logger = logger;
            _jWTManager = jWTManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestionType()
        {
            ResponseModel<List<QuestionTypeModel>> res = new();
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

                var questions = await _masterService.GetQuestionTypeAsync();
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

        [HttpPost]
        public async Task<IActionResult> AddLogs([FromBody] ApplicationLoggingModel request)
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
                var result = await _masterService.AddLogsAsync(request);
                if (result)
                {
                    msg.status = Message.success;
                    msg.message = "Log created successfully.";
                }

                else
                {
                    msg.status = Message.error;
                    msg.message = "Log creating failed.";
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
        public async Task<IActionResult> GetApplicationLogs(string activityCategoryIds, string userIds, int? statusId, int? eventId, int? period, DateTime? fromdate, DateTime? todate)
        {
            var res = new ResponseModel<List<GetApplicationLogs_Result>>();
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

                string strwhere = string.Empty;

                if (statusId.HasValue)
                    strwhere = " where l.StatusId = " + statusId + " ";
                if (eventId.HasValue)
                {
                    if (!string.IsNullOrEmpty(strwhere))
                    {
                        strwhere += " AND l.EventId = " + eventId + " ";
                    }
                    else
                    {
                        strwhere = " where l.EventId = " + eventId + " ";
                    }
                }

                if (period.HasValue)
                {
                    var str = MakePeriodLogic(period.Value);
                    if (!string.IsNullOrEmpty(str))
                    {
                        if (!string.IsNullOrEmpty(strwhere))
                        {
                            strwhere += " AND " + str + "";
                        }
                        else
                        {
                            strwhere = " where " + str + "";
                        }
                    }
                }
                else
                {
                    if (fromdate.HasValue && todate.HasValue)
                    {
                        if (!string.IsNullOrEmpty(strwhere))
                        {
                            strwhere += " AND CONVERT(DATE,l.CreatedOn) between CONVERT(DATE,'" + fromdate.Value.Date.ToString("yyyy-MM-dd") + "') and CONVERT(DATE,'" + todate.Value.Date.ToString("yyyy-MM-dd") + "')";
                        }
                        else
                        {
                            strwhere = " where CONVERT(DATE,l.CreatedOn) between CONVERT(DATE,'" + fromdate.Value.Date.ToString("yyyy-MM-dd") + "') and CONVERT(DATE,'" + todate.Value.Date.ToString("yyyy-MM-dd") + "')";
                        }
                    }
                }

                var logs_Results = await _masterService.GetApplicationLogsAsync(activityCategoryIds,userIds, strwhere);
                res.status = Message.success;
                res.data = logs_Results;
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

        private string MakePeriodLogic(int period)
        {
            string str = string.Empty;
            if (period == (int)LogPeriods.Today)
            {
                str = "  CONVERT(DATE,l.CreatedOn) between CONVERT(DATE,'" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "') and CONVERT(DATE,'" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "')";
            }
            else if (period == (int)LogPeriods.Yesterday)
            {
                str = "  CONVERT(DATE,l.CreatedOn) between CONVERT(DATE,'" + DateTime.Now.AddDays(-1).Date.ToString("yyyy-MM-dd") + "') and CONVERT(DATE,'" + DateTime.Now.AddDays(-1).Date.ToString("yyyy-MM-dd") + "')";
            }
            else if (period == (int)LogPeriods.Last7Days)
            {
                str = "  CONVERT(DATE,l.CreatedOn) between CONVERT(DATE,'" + DateTime.Now.AddDays(-6).Date.ToString("yyyy-MM-dd") + "') and CONVERT(DATE,'" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "')";
            }
            else if (period == (int)LogPeriods.Last30Days)
            {
                str = "  CONVERT(DATE,l.CreatedOn) between CONVERT(DATE,'" + DateTime.Now.AddDays(-29).Date.ToString("yyyy-MM-dd") + "') and CONVERT(DATE,'" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "')";
            }
            else if (period == (int)LogPeriods.ThisMonth)
            {
                string yyyy = DateTime.Now.Year.ToString();
                string mm = DateTime.Now.Month > 9 ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month.ToString();
                var fromDate = $"{yyyy}-{mm}-01";
                str = "  CONVERT(DATE,l.CreatedOn) between '" + fromDate + "' and CONVERT(DATE,'" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "')";
            }
            else if (period == (int)LogPeriods.LastMonth)
            {

                var lastMonth = DateTime.Today.AddMonths(-1);
                DateTime fromDate = new(lastMonth.Year, lastMonth.Month, 1);
                DateTime todate = new(lastMonth.Year, lastMonth.Month, DateTime.DaysInMonth(lastMonth.Year, lastMonth.Month));

                str = "  CONVERT(DATE,l.CreatedOn) between CONVERT(DATE,'" + fromDate.Date.ToString("yyyy-MM-dd") + "') and CONVERT(DATE,'" + todate.Date.ToString("yyyy-MM-dd") + "')";
            }

            return str;
        }

        [HttpPost]
        public async Task<IActionResult> ReadLog([BindRequired, FromQuery] int userId, [BindRequired, FromQuery] int logId, [BindRequired, FromQuery] bool isRead)
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
                var result = await _masterService.ReadLog(userId, logId, isRead);
                if (result)
                {
                    msg.status = Message.success;
                    if (isRead)
                        msg.message = "Log marked as read.";
                    else
                        msg.message = "Log marked as unread.";
                }
                else
                {
                    msg.status = Message.error;
                    msg.message = "Log marked failed.";
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

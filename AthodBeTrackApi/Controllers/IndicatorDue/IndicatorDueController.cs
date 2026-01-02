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
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class IndicatorDueController : ControllerBase
    {
        private readonly IIndicatorDueService _service;
        private readonly ILogger<IndicatorDueController> _logger;
        private readonly IJWTManagerService _jWTManager;
        readonly MessageModel msg = new();
        public IndicatorDueController(IIndicatorDueService service, ILogger<IndicatorDueController> logger, IJWTManagerService jWTManager)
        {
            _service = service;
            _logger = logger;
            _jWTManager = jWTManager;

        }

        [HttpGet]
        public async Task<IActionResult> GetDueReportSummary([BindRequired, FromQuery] int activityCategoryMappingId, [BindRequired, FromQuery] int groupId, [BindRequired, FromQuery] int reportingFrequecyTypeId)
        {
            var res = new ResponseModel<List<DueReportSummaryModel>>();
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

                var questions = await _service.GetDueReportSummary(activityCategoryMappingId, groupId, reportingFrequecyTypeId);
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
        public async Task<IActionResult> GetIndicatorDueSummary([BindRequired, FromQuery] int userId, [BindRequired, FromQuery] int activityCategoryMappingId,
           int? stateId, int? districtId, int? blockId, int? villageId, [BindRequired, FromQuery] int? groupId, int? reportingFrequecyTypeId, string status)
        {
            var res = new ResponseModel<List<IndicatorDueSummary_Result>>();
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

                var questions = await _service.GetIndicatorDueSummary(userId, activityCategoryMappingId, stateId, districtId, blockId, villageId, groupId, reportingFrequecyTypeId, status);
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
        [Route("GetIndicatorDueDetails")]
        public async Task<DataTablesResponseIndicatorDue> GetIndicatorDueDetails([FromBody] DataTablesRootObjectIndicatorDue dataTablesRootObject)
        {
            DataTablesResponseIndicatorDue r = new();
            string searchTest = "";

            if (dataTablesRootObject.search != null)
            {
                searchTest = dataTablesRootObject.search.value;
            }
            #region single sort code

            string sortInformAction = "";
            if (dataTablesRootObject.order != null && dataTablesRootObject.order.Count > 0)
            {
                if (dataTablesRootObject.columns != null && dataTablesRootObject.columns.Count > 0)
                {
                    if (dataTablesRootObject.order[0].column > 0)
                        sortInformAction = "ORDER BY " + dataTablesRootObject.order[0].column + " " + dataTablesRootObject.order[0].dir;

                }
            }
            if (string.IsNullOrEmpty(sortInformAction))
            {
                sortInformAction = "ORDER BY 1";
            }

            #endregion
            string error = "";
            var oListAll = await _service.GetIndicatorDueDetailsDynamic(dataTablesRootObject.userId, dataTablesRootObject.stateId, dataTablesRootObject.districtId, dataTablesRootObject.blockId, dataTablesRootObject.villageId, dataTablesRootObject.activityCategoryMappingId, dataTablesRootObject.activityQuestionId, dataTablesRootObject.groupId, dataTablesRootObject.status, dataTablesRootObject.start, dataTablesRootObject.length, sortInformAction, searchTest);
            r.data = oListAll;
            r.draw = dataTablesRootObject.draw;
            r.error = error;
            if (oListAll != null && oListAll.Count > 0)
            {
                var recordsFiltered = await _service.GetIndicatorDueDetailsCountDynamic(dataTablesRootObject.userId, dataTablesRootObject.stateId, dataTablesRootObject.districtId, dataTablesRootObject.blockId, dataTablesRootObject.villageId, dataTablesRootObject.activityCategoryMappingId, dataTablesRootObject.activityQuestionId, dataTablesRootObject.groupId, dataTablesRootObject.status, searchTest);
                r.recordsTotal = recordsFiltered;
                r.recordsFiltered = recordsFiltered;
            }
            return r;
        }


        [HttpGet]
        [Route("GetIndicatorDueDetailsExport")]
        public async Task<IActionResult> GetIndicatorDueDetailsExport(int userId, int? stateId, int? districtId, int? blockId, int? villageId, int activityCategoryMappingId, int activityQuestionId, int groupId, string status)
        {
            try
            {
                var res = new ResponseModel<List<DueReportDetailDynamic_Result>>();
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

                var dueReportDetails = await _service.GetIndicatorDueDetailsExport(userId, stateId, districtId, blockId, villageId, activityCategoryMappingId, activityQuestionId, groupId, status);
                res.status = Message.success;
                res.data = dueReportDetails;
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
        [Route("RefreshDueIndicator")]
        public async Task<IActionResult> RefreshDueIndicator()
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

                await _service.RefreshDueIndicator();
                msg.status = Message.success;
                msg.message = "Indicator due report updated.";
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
        [Route("GetLastUpdateDueIndicator")]
        public async Task<IActionResult> GetLastUpdateDueIndicator()
        {
            try
            {
                var res = new ResponseModel<DateTime>();
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

                var lastUpdate = await _service.GetLastUpdateDueIndicator();
                res.status = Message.success;
                res.data = lastUpdate ?? DateTime.Now.AddDays(-1);
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
        [Route("CheckDueIndicatorReportGenerate")]
        public async Task<IActionResult> CheckDueIndicatorReportGenerate()
        {
            try
            {
                var res = new ResponseModel<bool>();
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

                var gnerated = await _service.CheckDueIndicatorReportGenerated();
                res.status = Message.success;
                res.data = gnerated;
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

    }
}

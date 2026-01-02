using AthodBeTrackApi.Data;
using AthodBeTrackApi.Helpers;
using AthodBeTrackApi.Models;
using AthodBeTrackApi.Services;
using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Controllers.Report
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;
        private readonly ILogger<ReportController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IJWTManagerService _jWTManager;
        private readonly IDropdownService _dropdownService;
        private readonly IConfiguration _iconfiguration;
        readonly MessageModel msg = new();
        public ReportController(IReportService service, ILogger<ReportController> logger, IWebHostEnvironment webHostEnvironment, IJWTManagerService jWTManager, IDropdownService dropdownService, IConfiguration iconfiguration)
        {
            _service = service;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _jWTManager = jWTManager;
            _dropdownService = dropdownService;
            _iconfiguration = iconfiguration;
        }
        [HttpGet]
        public async Task<IActionResult> GetReport(int? reportId, int? activityCategoryMappingId, bool? isActive, int? userId, int? reportStatus)
        {
            var res = new ResponseModel<List<GetReports_Result>>();
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


                var results = await _service.GetReports(reportId, activityCategoryMappingId, isActive, userId, reportStatus);
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
        public async Task<IActionResult> GetAllReport(int? activityCategoryMappingId, bool? isActive, int? userId, int? reportStatus)
        {
            var res = new ResponseModel<List<GetAllReports_Result>>();
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


                var results = await _service.GetAllReports(activityCategoryMappingId, isActive, userId, reportStatus);
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
        public async Task<IActionResult> AddReport([FromBody] ReportModel request)
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
                request.ReportNo = await _service.GetCode("Reports");
                var result = await _service.AddReport(request);
                if (result > 0)
                {
                    msg.status = Message.success;
                    msg.message = "Report created successfully.";
                    msg.id = result;
                    msg.name = request.ReportNo;
                }
                else
                {
                    msg.status = Message.error;
                    msg.message = "Report creating failed.";
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
        public async Task<IActionResult> AddReportWithoutGenerateSummary([FromBody] ReportModel request)
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
                request.ReportNo = await _service.GetCode("Reports");

                if (string.IsNullOrEmpty(request.DistrictId))
                {
                    var disLists = await _dropdownService.RPT_GetAssignUserLocationDropdown(request.CreatedBy, "D", request.StateId);
                    request.DistrictId = string.Join(",", disLists.Select(x => x.id));
                }

                if (string.IsNullOrEmpty(request.BlockId))
                {
                    var blockLists = await _dropdownService.RPT_GetAssignUserLocationDropdown(request.CreatedBy, "B", request.DistrictId);
                    request.BlockId = string.Join(",", blockLists.Select(x => x.id));
                }

                if (string.IsNullOrEmpty(request.VillageId))
                {
                    var villageLists = await _dropdownService.RPT_GetAssignUserLocationDropdown(request.CreatedBy, "V", request.BlockId);
                    request.VillageId = string.Join(",", villageLists.Select(x => x.id));
                }

                if (string.IsNullOrEmpty(request.ReportingGroupIds))
                {
                    var groupLists = await _dropdownService.RPT_GetQuestionGroupAsync(request.CreatedBy);
                    request.ReportingGroupIds = string.Join(",", groupLists.Select(x => x.id));
                }
                if (string.IsNullOrEmpty(request.ReportingTagIds))
                {
                    var tagsLists = await _dropdownService.RPT_GetTagsdll(request.ReportingTagIds);
                    request.ReportingTagIds = string.Join(",", tagsLists.Select(x => x.id));
                }

                if (string.IsNullOrEmpty(request.ReportQuestionIds))
                {
                    string strwhere = string.Empty;
                    //if (request.ReportingFrequnecy.HasValue)
                    //{
                    //    if (request.ReportingFrequnecy.Value != 0)
                    //    {
                    //        var str = MakePeriodLogic(request.ReportingFrequnecy.Value);
                    //        if (!string.IsNullOrEmpty(str))
                    //        {
                    //            strwhere += " AND " + str + "";
                    //        }
                    //    }

                    //}
                    //else
                    //{
                    //    if (request.FromDate.HasValue && request.ToDate.HasValue)
                    //    {
                    //        strwhere += " AND CONVERT(DATE,v.UpdatedOn) between CONVERT(DATE,'" + request.FromDate.Value.Date.ToString("yyyy-MM-dd") + "') and CONVERT(DATE,'" + request.ToDate.Value.Date.ToString("yyyy-MM-dd") + "')";
                    //    }
                    //}
                    var questionsLists = await _dropdownService.RPT_GetQuestiondll(request.ReportingTagIds, request.ActivityCategoryMappingId ?? 1, strwhere);
                    request.ReportQuestionIds = string.Join(",", questionsLists.Select(x => x.id));
                }

                var result = await _service.AddReportWithoutGenerateSummary(request);
                if (result > 0)
                {
                    msg.status = Message.success;
                    msg.message = "Generating you report. Please wait a moment.";
                    msg.id = result;
                    msg.name = request.ReportNo;
                }
                else
                {
                    msg.status = Message.error;
                    msg.message = "Report creating failed.";
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

        [HttpPut]
        public async Task<IActionResult> UpdateReport([FromBody] ReportModel request)
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
                if (request.ReportId > 0 || request.ReportNo != "")
                {
                    var result = await _service.UpdateReport(request);
                    if (result)
                    {
                        await _service.RefreshReportAsync(request.ReportId);
                        await _service.DeleteReportTemplate(request.ReportId);
                        msg.status = Message.success;
                        msg.message = "Report updated successfully.";
                    }
                    else
                    {
                        msg.status = Message.error;
                        msg.message = "Report updation failed.";
                    }
                }
                else
                {
                    msg.status = Message.error;
                    msg.message = "Invalid request.";
                    return Ok(msg);
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
        [HttpDelete]
        public async Task<IActionResult> DeleteReport([BindRequired, FromQuery] int reportId, [BindRequired, FromQuery] int userId, [BindRequired, FromQuery] int roleId)
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

                await _service.DeleteReport(reportId, userId, roleId);
                msg.status = Message.success;
                msg.message = $"Report deleted.";
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


        [HttpDelete]
        public async Task<IActionResult> DeleteReportPermanent([BindRequired, FromQuery] int reportId)
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

                await _service.DeleteReport(reportId);
                msg.status = Message.success;
                msg.message = $"Report permanently deleted.";
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
        public async Task<IActionResult> ActiveReport([BindRequired, FromQuery] int reportId, [BindRequired, FromQuery] int userId)
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

                await _service.ActiveReport(reportId, userId);
                msg.status = Message.success;
                msg.message = $"Report active successfully.";
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
        public async Task<IActionResult> GetGraphData([BindRequired, FromQuery] int reportId)
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

            var res = new ResponseModel<List<ReportSummaryModel>>();
            try
            {
                var results = await _service.GetGraphData(reportId);
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
        public async Task<IActionResult> GetGraphDataWithPagination([BindRequired, FromQuery] int reportId, [BindRequired, FromQuery] int pageNumber, [BindRequired, FromQuery] int rowsOfPage)
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

            var res = new ResponseModel<ReportSummaryViewModel>();
            try
            {
                var results = await _service.GetGraphDataWithPagination(reportId, pageNumber, rowsOfPage);
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
        public async Task<IActionResult> SaveReportTemplateAsync(ChartTemplate chartTemplate)
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

                var flag = await _service.SaveReportTemplateAsync(chartTemplate);
                if (flag)
                {
                    msg.status = Message.success;
                    msg.message = "Template saved successfully.";
                }

                else
                {
                    msg.status = Message.error;
                    msg.message = "Template saving failed.";
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
        public async Task<IActionResult> ResetReportTemplate([BindRequired, FromQuery] int reportId)
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

                var flag = await _service.ResetReportTemplate(reportId);
                if (flag)
                {
                    msg.status = Message.success;
                    msg.message = "Report reset successfully.";
                }

                else
                {
                    msg.status = Message.error;
                    msg.message = "Report reset failed.";
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
        public async Task<IActionResult> RefreshReport([BindRequired, FromQuery] int reportId)
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

                await _service.RefreshReportAsync(reportId);
                msg.status = Message.success;
                msg.message = $"Report updated successfully.";
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
        public async Task<IActionResult> GetQuestionDetails([BindRequired, FromQuery] int reportId, [BindRequired, FromQuery] int activityQuestionId)
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

            var res = new ResponseModel<List<RPT_GetQuestionDetails_Result>>();
            try
            {
                var results = await _service.GetQuestionDetails(reportId, activityQuestionId);
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
        public async Task<IActionResult> GetReportLocation([BindRequired, FromQuery] int reportId)
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

            var res = new ResponseModel<RPT_GetReportLocation_Result>();
            try
            {
                var results = await _service.GetReportLocation(reportId);
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
        public async Task<IActionResult> ShareReport([FromBody] ShareReport request)
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

                if (request.SharedUsers.Count == 0)
                {
                    msg.status = Message.error;
                    msg.message = "Please select user for sharing report.";
                }

                var result = await _service.ShareReport(request);
                if (result)
                {
                    msg.status = Message.success;
                    msg.message = "Report sharing status updated with selected users.";
                }

                else
                {
                    msg.status = Message.error;
                    msg.message = "Report sharing status failed with selected users.";
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
        public async Task<IActionResult> CloneReport([BindRequired, FromQuery] int reportId, int userId)
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

                var results = await _service.CloneReport(reportId, userId);
                if (results)
                {
                    msg.status = Message.success;
                    msg.message = "Report cloned successfully.";
                    return Ok(msg);
                }
                else
                {
                    msg.status = Message.error;
                    msg.message = "Report cloning failed.";
                    return Ok(msg);
                }
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
        public async Task<IActionResult> GetUsersForShareReport([BindRequired, FromQuery] string ReportNo, [BindRequired, FromQuery] int UserId)
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

            var res = new ResponseModel<List<RPT_GetUsersForShareReport_Result>>();
            try
            {
                var results = await _service.GetUsersForShareReport(ReportNo, UserId);
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
        public async Task<IActionResult> RPT_GetUpdatedHousehold([BindRequired, FromQuery] int reportId)
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

            var res = new ResponseModel<List<RPT_GetUpdatedHousehold_Result>>();
            try
            {
                var results = await _service.RPT_GetUpdatedHousehold(reportId);
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
        public async Task<IActionResult> MakeFevoriteReport([BindRequired, FromQuery] int reportId, int userId)
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

                var results = await _service.MakeFevoriteReport(reportId, userId);
                if (results)
                {
                    msg.status = Message.success;
                    msg.message = "Report set as favourite.";
                    return Ok(msg);
                }
                else
                {
                    msg.status = Message.error;
                    msg.message = "failed !!.";
                    return Ok(msg);
                }
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
        public async Task<IActionResult> GetFevoriteReport([BindRequired, FromQuery] int UserId)
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

            var res = new ResponseModel<ReportModel>();
            try
            {
                var results = await _service.GetFevoriteReport(UserId);
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
        public async Task<IActionResult> GenerateReport( [BindRequired, FromQuery] int reportId, [BindRequired, FromQuery] int userId)
        {
            try
            {
                #region 🔒 Token Validation
                if (!Request.Headers.TryGetValue("Authorization", out StringValues authorization))
                {
                    msg.status = Message.error;
                    msg.message = "Unauthorized";
                    return Unauthorized(msg);
                }

                string token = authorization.ToString().Replace("Bearer", "").Trim();

                if (!await _jWTManager.ValidateToken(token))
                {
                    msg.status = Message.error;
                    msg.message = "Unauthorized";
                    return Unauthorized(msg);
                }
                #endregion

                var dt = await _service.GenerateReportAsync(reportId, userId);
                if (dt == null || dt.Rows.Count == 0)
                {
                    msg.status = Message.error;
                    msg.message = "The data compilation has failed. Please try again.!";
                    return Ok(msg);
                }

                string fileName = Convert.ToString(dt.Rows[0]["FileName"]);
                string deletedFileName = Convert.ToString(dt.Rows[0]["DeletedFileName"]);

                // ✅ Define parameters
                var serverName = _iconfiguration.GetValue<string>("CsvExport:ServerName");
                var databaseName = _iconfiguration.GetValue<string>("CsvExport:DbName");

                //string serverName = "MW-ANIL\\MSSQLSERVER2022";
                //string databaseName = "AthodBeTrack";
                string storedProc = "dbo.usp_ExportPivotedReport_File";

                // ✅ Ensure export folder exists
                string exportFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Documents", "Reports");
                Directory.CreateDirectory(exportFolder);

                string outputFilePath = Path.Combine(exportFolder, fileName);

                // ✅ Construct the BCP command
                string bcpCommand =
                    $"bcp \"EXEC {databaseName}.{storedProc} {reportId}\" queryout \"{outputFilePath}\" -S \"{serverName}\" -T -c -t, -r \\n";

                var processInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {bcpCommand}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // ✅ Execute the process safely
                using (var process = new System.Diagnostics.Process { StartInfo = processInfo })
                {
                    process.Start();

                    string output = await process.StandardOutput.ReadToEndAsync();
                    string error = await process.StandardError.ReadToEndAsync();

                    await process.WaitForExitAsync();

                    if (process.ExitCode != 0 || !string.IsNullOrEmpty(error))
                    {
                        _logger.LogError($"BCP failed: {error}");
                        _logger.LogInformation($"BCP Output: BCP export failed!");
                        msg.status = Message.error;
                        msg.message = "The data compilation has failed. Please try again.";
                        return Ok(msg);
                    }

                    _logger.LogInformation($"BCP Output: {output}");
                }

                // ✅ Delete old file if exists
                if (!string.IsNullOrEmpty(deletedFileName))
                {
                    string deletedFilePath = Path.Combine(exportFolder, deletedFileName);
                    ExtensionMethods.DeleteFile(deletedFilePath);
                }

                msg.status = Message.success;
                msg.message = "Raw data generated successfully.";
                return Ok(msg);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message: {ex.Message} | StackTrace: {ex.StackTrace} | Timestamp: {DateTime.Now}");
                msg.status = Message.error;
                msg.message = "An error occurred while generating the report.";
                return Ok(msg);
            }
        }


        //[HttpGet]
        //public async Task<IActionResult> GenerateReport([BindRequired, FromQuery] int reportId, [BindRequired, FromQuery] int userId)
        //{
        //    try
        //    {
        //        #region Token Validation
        //        if (Request.Headers.TryGetValue("Authorization", out StringValues authorization))
        //        {
        //            string token = Convert.ToString(authorization).Replace("Bearer", "").Trim();

        //            if (!await _jWTManager.ValidateToken(token))
        //            {
        //                msg.status = Message.error;
        //                msg.message = "Unauthorized";
        //                return Unauthorized(msg);
        //            }
        //        }
        //        else
        //        {
        //            msg.status = Message.error;
        //            msg.message = "Unauthorized";
        //            return Unauthorized(msg);
        //        }
        //        #endregion

        //        var dataSet = await _service.GenerateReport(reportId, userId);
        //        if (dataSet != null && dataSet.Tables.Count > 1)
        //        {
        //            DataTable dataDt = new();
        //            DataTable fileDt = new();
        //            string fileName = string.Empty;
        //            string deletedFileName = string.Empty;
        //            dataDt = dataSet.Tables[0];
        //            fileDt = dataSet.Tables[1];
        //            fileName = Convert.ToString(fileDt.Rows[0]["FileName"]);
        //            deletedFileName = Convert.ToString(fileDt.Rows[0]["DeletedFileName"]);


        //            List<dynamic> dynamicDt = dataDt.ToDynamic();
        //            WriteCSV(dynamicDt, fileName);

        //            if (!string.IsNullOrEmpty(deletedFileName))
        //            {
        //                var filePath = @$"Documents\Reports\{deletedFileName}";
        //                var deletedFilePath = Path.Combine(_webHostEnvironment.WebRootPath, filePath);
        //                ExtensionMethods.DeleteFile(deletedFilePath);
        //            }

        //            msg.status = Message.success;
        //            msg.message = "Raw data generated.";
        //            return Ok(msg);
        //        }
        //        else
        //        {
        //            msg.status = Message.error;
        //            msg.message = "Raw data generation failed!";
        //            return Ok(msg);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
        //        msg.status = Message.error;
        //        msg.message = ex.Message;
        //        return Ok(msg);
        //    }
        //}


        [HttpGet]
        public async Task<IActionResult> GetGeneratedReports([BindRequired, FromQuery] int reportId)
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

            var res = new ResponseModel<List<GetGenerateReportDetails_Result>>();
            try
            {
                var results = await _service.GetGenerateReportDetailsAsync(reportId);
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

        private void WriteCSV<T>(List<T> records, string fileName)
        {
            var filePath = @$"Documents\Reports\{fileName}";
            var folderPath = @$"Documents\Reports";
            if (!Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, folderPath)))
                Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, folderPath));

            var uploadedFilePath = Path.Combine(_webHostEnvironment.WebRootPath, filePath);

            using var writer = new StreamWriter(uploadedFilePath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(records);
        }


        [HttpGet]
        public HttpResponseMessage GetFile(string fileName)
        {
            var filePath = @$"Documents\Reports\{fileName}";
            var localFilePath = Path.Combine(_webHostEnvironment.WebRootPath, filePath);
            // Check if file exists
            if (!System.IO.File.Exists(localFilePath))
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            HttpResponseMessage response = new(HttpStatusCode.OK)
            {
                Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read))
            };
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");

            return response;
        }

        [HttpGet]
        public async Task<IActionResult> DownloadFile(string FileName)
        {
            var result = await GetDownloadFile(FileName);
            return File(result.Item1, result.Item2, result.Item2);
        }


        private async Task<(byte[], string, string)> GetDownloadFile(string fileName)
        {
            var filePath = @$"Documents\Reports\{fileName}";
            var _GetFilePath = Path.Combine(_webHostEnvironment.WebRootPath, filePath);
            // Check if file exists

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(_GetFilePath, out var _ContentType))
            {
                _ContentType = "application/octet-stream";
            }
            var _ReadAllBytesAsync = await System.IO.File.ReadAllBytesAsync(_GetFilePath);
            return (_ReadAllBytesAsync, _ContentType, Path.GetFileName(_GetFilePath));
        }


        [HttpPost]
        [Route("GetUpdatedHouseholdList")]
        public async Task<DataTablesResponseUpdatedHH> GetUpdatedHouseholdList([FromBody] DataTablesRootObjectUpdatedHH dataTablesRootObject)
        {

            DataTablesResponseUpdatedHH r = new();
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
                    //sortInformAction = "ORDER BY 1";
                }
            }
            if (string.IsNullOrEmpty(sortInformAction))
            {
                //sortInformAction = "ORDER BY " + dataTablesRootObject.columns[0].data + " asc";
                sortInformAction = "ORDER BY 1";
            }

            #endregion
            string error = "";
            var oListAll = await _service.RPT_GetUpdatedHouseholdList(dataTablesRootObject.reportId, dataTablesRootObject.start, dataTablesRootObject.length, sortInformAction, searchTest);
            r.data = oListAll;
            r.draw = dataTablesRootObject.draw;
            r.error = error;
            if (oListAll != null && oListAll.Count > 0)
            {
                var recordsFiltered = await _service.RPT_GetUpdatedHouseholdCount(dataTablesRootObject.reportId, searchTest);
                r.recordsTotal = recordsFiltered;
                r.recordsFiltered = recordsFiltered;
            }
            return r;
        }



        [HttpGet]
        [Route("ExportUpdatedHouseholds")]
        public async Task<IActionResult> ExportUpdatedHouseholds([BindRequired, FromQuery] int reportId)
        {
            try
            {
                var res = new ResponseModel<List<RPT_GetUpdatedHousehold_Result>>();
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

                var result = await _service.RPT_ExportUpdatedHouseholdList(reportId);
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

        [HttpPost]
        [Route("GetQuestionDetails")]
        public async Task<DataTablesResponseQuestionDetails> GetQuestionDetails([FromBody] DataTablesRootObjectQuestionDetails dataTablesRootObject)
        {
            DataTablesResponseQuestionDetails r = new();
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
                    //sortInformAction = "ORDER BY 1";
                }
            }
            if (string.IsNullOrEmpty(sortInformAction))
            {
                //sortInformAction = "ORDER BY " + dataTablesRootObject.columns[0].data + " asc";
                sortInformAction = "ORDER BY 1";
            }

            #endregion
            string error = "";
            var oListAll = await _service.RPT_GetQuestionDetailsDynamic(dataTablesRootObject.reportId, dataTablesRootObject.activityQuestionId, dataTablesRootObject.filter, dataTablesRootObject.isUniqueFilter, dataTablesRootObject.isPrimaryFilter, dataTablesRootObject.start, dataTablesRootObject.length, sortInformAction, searchTest);
            r.data = oListAll;
            r.draw = dataTablesRootObject.draw;
            r.error = error;
            if (oListAll != null && oListAll.Count > 0)
            {
                var recordsFiltered = await _service.RPT_GetQuestionDetailsCount(dataTablesRootObject.reportId, dataTablesRootObject.activityQuestionId, dataTablesRootObject.filter, dataTablesRootObject.isUniqueFilter, dataTablesRootObject.isPrimaryFilter, searchTest);
                r.recordsTotal = recordsFiltered;
                r.recordsFiltered = recordsFiltered;
            }
            return r;
        }


        [HttpGet]
        [Route("RPT_ExportQuestionDetails")]
        public async Task<IActionResult> RPT_ExportQuestionDetails(int reportId, int activityQuestionId, string filter, bool isUniqueFilter, bool isPrimaryFilter)
        {
            try
            {
                var res = new ResponseModel<List<RPT_GetQuestionDetails_Result>>();
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

                var result = await _service.RPT_ExportQuestionDetails(reportId, activityQuestionId, filter, isUniqueFilter, isPrimaryFilter);
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
        public async Task<IActionResult> GetMaximumIndicator([BindRequired, FromQuery] int activityCategoryMappingId)
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
            try
            {
                var results = await _service.GetMaximumIndicator(activityCategoryMappingId);
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

    }
}

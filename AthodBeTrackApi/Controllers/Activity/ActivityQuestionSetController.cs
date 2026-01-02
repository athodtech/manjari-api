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
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class ActivityQuestionSetController : ControllerBase
    {
        private readonly IActivityQuestionSetService _service;
        private readonly ILogger<ActivityQuestionSetController> _logger;
        private readonly IJWTManagerService _jWTManager;
        readonly MessageModel msg = new();
        public ActivityQuestionSetController(IActivityQuestionSetService service, ILogger<ActivityQuestionSetController> logger, IJWTManagerService jWTManager)
        {
            _service = service;
            _logger = logger;
            _jWTManager = jWTManager;

        }
        [HttpGet]
        public async Task<IActionResult> Get(int activityCategoryMappingId, int userId, string days, DateTime? fromDate, DateTime? toDate)
        {
            var res = new ResponseModel<string>();
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
                var jsonResult = _service.GetActivityQuestionSet(activityCategoryMappingId, userId, days, fromDate, toDate);
                res.status = Message.success;
                res.data = jsonResult;
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
        [Route("GetHouseholds")]
        public async Task<IActionResult> GetHouseholds(int activityCategoryMappingId, int userId, string days, DateTime? fromDate, DateTime? toDate, string stateId, string districtId, string blockId, string villageId)
        {
            var res = new ResponseModel<string>();
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
                var jsonResult = _service.GetActivityQuestionSetWithLocationFilter(activityCategoryMappingId, userId, days, fromDate, toDate, stateId, districtId, blockId, villageId);
                res.status = Message.success;
                res.data = jsonResult;
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
        [Route("GetHouseholdSets")]
        public async Task<DataTablesResponsehh> GetHouseholdSets([FromBody] DataTablesRootObjecthh dataTablesRootObject)
        {
            DataTablesResponsehh r = new();
            string searchTest = "";
            string whereLoc = "";
            if (dataTablesRootObject.search != null)
            {
                searchTest = dataTablesRootObject.search.value;
            }
            if (dataTablesRootObject.stateId != null && dataTablesRootObject.stateId > 0)
            {
                whereLoc = " AND qs.StateId = " + dataTablesRootObject.stateId + "";
            }
            if (dataTablesRootObject.districtId != null && dataTablesRootObject.districtId > 0)
            {
                if (string.IsNullOrEmpty(whereLoc))
                    whereLoc = " AND qs.DistrictId = " + dataTablesRootObject.districtId + "";
                else
                    whereLoc += " AND qs.DistrictId = " + dataTablesRootObject.districtId + "";
            }
            if (dataTablesRootObject.blockId != null && dataTablesRootObject.blockId > 0)
            {
                if (string.IsNullOrEmpty(whereLoc))
                    whereLoc = " AND qs.BlockId = " + dataTablesRootObject.blockId + "";
                else
                    whereLoc += " AND qs.BlockId = " + dataTablesRootObject.blockId + "";
            }
            if (dataTablesRootObject.villageId != null && dataTablesRootObject.villageId > 0)
            {
                if (string.IsNullOrEmpty(whereLoc))
                    whereLoc = " AND qs.VillageId = " + dataTablesRootObject.villageId + "";
                else
                    whereLoc += " AND qs.VillageId = " + dataTablesRootObject.villageId + "";
            }
            if (dataTablesRootObject.currentStatus != null)
            {
                if (string.IsNullOrEmpty(whereLoc))
                    whereLoc = " AND qs.[Status] = " + dataTablesRootObject.currentStatus + "";
                else
                    whereLoc += " AND qs.[Status] = " + dataTablesRootObject.currentStatus + "";
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
                sortInformAction = "order by 1";
            }

            #endregion
            string error = "";
            var oListAll = await _service.GetHouseholdSets(dataTablesRootObject.activityCategoryMappingId, dataTablesRootObject.days, dataTablesRootObject.fromDate, dataTablesRootObject.toDate, dataTablesRootObject.start, dataTablesRootObject.length, sortInformAction, searchTest, whereLoc, dataTablesRootObject.userId);

            r.data = oListAll;
            r.draw = dataTablesRootObject.draw;
            r.error = error;
            if (oListAll != null && oListAll.Count > 0)
            {
                var recordsFiltered = await _service.GetHouseholdSetsCount(dataTablesRootObject.activityCategoryMappingId, dataTablesRootObject.days, dataTablesRootObject.fromDate, dataTablesRootObject.toDate, searchTest, whereLoc, dataTablesRootObject.userId);

                r.recordsTotal = recordsFiltered;
                r.recordsFiltered = recordsFiltered;
            }
            return r;
        }


        [HttpPost]
        [Route("GetHouseholdDeletedSets")]
        public async Task<DataTablesResponsehh> GetHouseholdDeletedSets([FromBody] DataTablesRootObjecthh dataTablesRootObject)
        {
            DataTablesResponsehh r = new();
            string searchTest = "";
            string whereLoc = "";
            if (dataTablesRootObject.search != null)
            {
                searchTest = dataTablesRootObject.search.value;
            }
            if (dataTablesRootObject.stateId != null && dataTablesRootObject.stateId > 0)
            {
                whereLoc = " AND qs.StateId = " + dataTablesRootObject.stateId + "";
            }
            if (dataTablesRootObject.districtId != null && dataTablesRootObject.districtId > 0)
            {
                if (string.IsNullOrEmpty(whereLoc))
                    whereLoc = " AND qs.DistrictId = " + dataTablesRootObject.districtId + "";
                else
                    whereLoc += " AND qs.DistrictId = " + dataTablesRootObject.districtId + "";
            }
            if (dataTablesRootObject.blockId != null && dataTablesRootObject.blockId > 0)
            {
                if (string.IsNullOrEmpty(whereLoc))
                    whereLoc = " AND qs.BlockId = " + dataTablesRootObject.blockId + "";
                else
                    whereLoc += " AND qs.BlockId = " + dataTablesRootObject.blockId + "";
            }
            if (dataTablesRootObject.villageId != null && dataTablesRootObject.villageId > 0)
            {
                if (string.IsNullOrEmpty(whereLoc))
                    whereLoc = " AND qs.VillageId = " + dataTablesRootObject.villageId + "";
                else
                    whereLoc += " AND qs.VillageId = " + dataTablesRootObject.villageId + "";
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
                sortInformAction = "order by 1";
            }

            #endregion
            string error = "";
            var oListAll = await _service.GetHouseholdDeletedSets(dataTablesRootObject.activityCategoryMappingId, dataTablesRootObject.days, dataTablesRootObject.fromDate, dataTablesRootObject.toDate, dataTablesRootObject.start, dataTablesRootObject.length, sortInformAction, searchTest, whereLoc, dataTablesRootObject.userId);

            r.data = oListAll;
            r.draw = dataTablesRootObject.draw;
            r.error = error;
            if (oListAll != null && oListAll.Count > 0)
            {
                var recordsFiltered = await _service.GetHouseholdDeletedSetsCount(dataTablesRootObject.activityCategoryMappingId, dataTablesRootObject.days, dataTablesRootObject.fromDate, dataTablesRootObject.toDate, searchTest, whereLoc, dataTablesRootObject.userId);

                r.recordsTotal = recordsFiltered;
                r.recordsFiltered = recordsFiltered;
            }
            return r;
        }

        [HttpPost]
        [Route("ExportHouseholdSets")]
        public async Task<DataTablesResponsehh> ExportHouseholdSets([FromBody] ExportHouseholdFilter dataTablesRootObject)
        {
            DataTablesResponsehh r = new();
            try
            {
                string whereLoc = "";

                if (dataTablesRootObject.stateId != null && dataTablesRootObject.stateId > 0)
                {
                    whereLoc = " AND qs.StateId = " + dataTablesRootObject.stateId + "";
                }
                if (dataTablesRootObject.districtId != null && dataTablesRootObject.districtId > 0)
                {
                    if (string.IsNullOrEmpty(whereLoc))
                        whereLoc = " AND qs.DistrictId = " + dataTablesRootObject.districtId + "";
                    else
                        whereLoc += " AND qs.DistrictId = " + dataTablesRootObject.districtId + "";
                }
                if (dataTablesRootObject.blockId != null && dataTablesRootObject.blockId > 0)
                {
                    if (string.IsNullOrEmpty(whereLoc))
                        whereLoc = " AND qs.BlockId = " + dataTablesRootObject.blockId + "";
                    else
                        whereLoc += " AND qs.BlockId = " + dataTablesRootObject.blockId + "";
                }
                if (dataTablesRootObject.villageId != null && dataTablesRootObject.villageId > 0)
                {
                    if (string.IsNullOrEmpty(whereLoc))
                        whereLoc = " AND qs.VillageId = " + dataTablesRootObject.villageId + "";
                    else
                        whereLoc += " AND qs.VillageId = " + dataTablesRootObject.villageId + "";
                }
                if (dataTablesRootObject.currentStatus != null)
                {
                    if (string.IsNullOrEmpty(whereLoc))
                        whereLoc = " AND qs.[Status] = " + dataTablesRootObject.currentStatus + "";
                    else
                        whereLoc += " AND qs.[Status] = " + dataTablesRootObject.currentStatus + "";
                }

                var result = await _service.ExportHouseholdSetsAsync(dataTablesRootObject.activityCategoryMappingId, dataTablesRootObject.days, dataTablesRootObject.fromDate, dataTablesRootObject.toDate, whereLoc, dataTablesRootObject.userId);

                r.data = result;
                r.draw = 0;
                r.error = "";
                return r;
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                r.data = null;
                r.draw = 0;
                r.error = ex.Message;
                return r;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ActivityQuestionSetUniqueIdentityModel request)
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
                if (!ModelState.IsValid)
                {
                    msg.status = Message.error;
                    msg.message = "Invalid request.";
                    return Ok(msg);
                }

                var activityQuestionSetId = await _service.AddActivityQuestionSetAsync(request);
                if (activityQuestionSetId > 0)
                {
                    res.status = Message.success;
                    res.data = activityQuestionSetId;
                }

                else
                {
                    msg.status = Message.error;
                    msg.message = "Something went wrong please try again";
                    return Ok(msg);
                }
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
        public async Task<IActionResult> Delete(int id, int userId)
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
                var flag = await _service.DeleteAsync(id, userId);
                if (flag)
                {
                    msg.status = Message.success;
                    msg.message = $"Household deleted.";
                }
                else
                {
                    msg.status = Message.error;
                    msg.message = $"Household deletion failed.";
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
        [Route("RestoreHousehold")]
        public async Task<IActionResult> RestoreHousehold(int id, int userId)
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
                var flag = await _service.ActiveAsync(id, userId);
                if (flag)
                {
                    msg.status = Message.success;
                    msg.message = $"Household restored.";
                }
                else
                {
                    msg.status = Message.error;
                    msg.message = $"Household restoring failed.";
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
        [Route("DeleteHousehold")]
        public async Task<IActionResult> DeleteHousehold(int id, int userId)
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
                var flag = await _service.DeleteActivityQuestionSet(id, userId);
                if (flag)
                {
                    msg.status = Message.success;
                    msg.message = $"Household permanently deleted.";
                }
                else
                {
                    msg.status = Message.error;
                    msg.message = $"Household permanently deletion failed.";
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
        [Route("SaveHHFilter")]
        public async Task<IActionResult> SaveHHFilter([FromBody] HouseholdFilterModel request)
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
                var flag = await _service.SaveHHFilter(request);
                if (flag)
                {
                    msg.status = Message.success;
                    msg.message = $"Household filter saved.";
                }

                else
                {
                    msg.status = Message.error;
                    msg.message = "Household filter saving failed.";
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

        [HttpGet]
        [Route("GetHHFilter")]
        public async Task<IActionResult> GetHHFilter(int userId, int activityCategoryMappingId)
        {
            var res = new ResponseModel<HouseholdFilterModel>();
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
                var householdFilter = await _service.GetHHFilter(userId, activityCategoryMappingId);
                res.status = Message.success;
                res.data = householdFilter;
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

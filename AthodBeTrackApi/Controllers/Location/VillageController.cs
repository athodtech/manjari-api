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
    //[Authorize]
    public class VillageController : ControllerBase
    {
        private readonly ILocationService _service;
        private readonly ILogger<VillageController> _logger;
        private readonly IJWTManagerService _jWTManager;
        readonly MessageModel msg = new();
        public VillageController(ILocationService service, ILogger<VillageController> logger, IJWTManagerService jWTManager)
        {
            _service = service;
            _logger = logger;
            _jWTManager = jWTManager;

        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(int? stateId, int? districtId, int? blockId)
        {
            var res = new ResponseModel<List<VillageModel>>();
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
                var villages = await _service.GetVillageAsync(stateId, districtId, blockId);
                res.status = Message.success;
                res.data = villages;
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
            var res = new ResponseModel<VillageModel>();
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

            var village = await _service.GetVillageByIdAsync(id);
            if (village == null)
            {
                msg.status = Message.error;
                msg.message = $"Village with Id: { id} not found.";
                return Ok(msg);
            }
            res.status = Message.success;
            res.data = village;
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VillageModel request)
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
                var result = await _service.AddVillageAsync(request);
                if (result)
                {
                    msg.status = Message.success;
                    msg.message = "Village added successfully";
                }

                else
                {
                    msg.status = Message.error;
                    msg.message = "Village adding failed";
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
        public async Task<IActionResult> Put([FromBody] VillageModel request)
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
                // Check Village is used
                var identity = await _service.CheckVillageIsUsed(request.VillageId);
                if (identity != null)
                {
                    if (identity.Stateid != request.StateId || identity.DistrictId != request.DistrictId || identity.BlockId != request.BlockId)
                    {
                        msg.status = Message.error;
                        msg.message = "Village has been used in report, you do not change parent location.";
                    }
                    else
                    {
                        var result = await _service.UpdateVillageAsync(request);
                        if (result)
                        {
                            msg.status = Message.success;
                            msg.message = "Village updated successfully";
                        }

                        else
                        {
                            msg.status = Message.success;
                            msg.message = "Village updation failed";
                        }
                    }
                }
                else
                {
                    var result = await _service.UpdateVillageAsync(request);
                    if (result)
                    {
                        msg.status = Message.success;
                        msg.message = "Village updated successfully";
                    }

                    else
                    {
                        msg.status = Message.success;
                        msg.message = "Village updation failed";
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

                await _service.DeleteVillageAsync(id);
                msg.status = Message.success;
                msg.message = $"Village with Id: { id} deleted";
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

        [HttpPost]
        [Route("GetVillages")]
        public async Task<DataTablesResponse> GetVillages([FromBody] DataTablesRootObject dataTablesRootObject)
        {

            DataTablesResponse r = new();
            string searchTest = "";
            string whereLoc = "";
            if (dataTablesRootObject.search != null)
            {
                searchTest = dataTablesRootObject.search.value;
            }
            if (dataTablesRootObject.stateId != null && dataTablesRootObject.stateId > 0)
            {
                whereLoc = " s.StateId = " + dataTablesRootObject.stateId + "";
            }
            if (dataTablesRootObject.districtId != null && dataTablesRootObject.districtId > 0)
            {
                if (string.IsNullOrEmpty(whereLoc))
                    whereLoc = " d.DistrictId = " + dataTablesRootObject.districtId + "";
                else
                    whereLoc += " AND d.DistrictId = " + dataTablesRootObject.districtId + "";
            }
            if (dataTablesRootObject.blockId != null && dataTablesRootObject.blockId > 0)
            {
                if (string.IsNullOrEmpty(whereLoc))
                    whereLoc = " b.BlockId = " + dataTablesRootObject.blockId + "";
                else
                    whereLoc += " AND b.BlockId = " + dataTablesRootObject.blockId + "";
            }
            if (dataTablesRootObject.villageId != null && dataTablesRootObject.villageId > 0)
            {
                if (string.IsNullOrEmpty(whereLoc))
                    whereLoc = " v.VillageId = " + dataTablesRootObject.villageId + "";
                else
                    whereLoc += " AND v.VillageId = " + dataTablesRootObject.villageId + "";
            }
            #region single sort code

            string sortInformAction = "";
            if (dataTablesRootObject.order != null && dataTablesRootObject.order.Count > 0)
            {
                if (dataTablesRootObject.columns != null && dataTablesRootObject.columns.Count > 0)
                {
                    if (dataTablesRootObject.order[0].column > 0)
                        sortInformAction = "ORDER BY " + dataTablesRootObject.order[0].column + " " + dataTablesRootObject.order[0].dir;
                    //sortInformAction = "ORDER BY " + dataTablesRootObject.columns[dataTablesRootObject.order[0].column].data + " " + dataTablesRootObject.order[0].dir;
                    //sortInformAction = "ORDER BY VillageId";

                }

            }
            if (string.IsNullOrEmpty(sortInformAction))
            {
                //sortInformAction = "ORDER BY " + dataTablesRootObject.columns[0].data + " asc";
                sortInformAction = "ORDER BY VillageId";
            }

            #endregion
            string error = "";
            var oListAll = await _service.GetVillages(dataTablesRootObject.start, dataTablesRootObject.length, sortInformAction, searchTest, whereLoc);
            var recordsFiltered = await _service.GetFilterVillageCount(searchTest, whereLoc);
            r.data = oListAll;
            r.draw = dataTablesRootObject.draw;
            r.error = error;
            if (oListAll != null && oListAll.Count > 0)
            {
                if (!string.IsNullOrEmpty(searchTest))
                    r.recordsTotal = await _service.TotalVillageCount();
                else
                    r.recordsTotal = recordsFiltered;
                r.recordsFiltered = recordsFiltered;
            }
            return r;
        }

        [HttpPost]
        [Route("GetUserLocation")]
        public async Task<DataTablesResponseUserLocation> GetUserLocation([FromBody] DataTablesRootObjectUserLocation dataTablesRootObject)
        {

            DataTablesResponseUserLocation r = new();
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
            var oListAll = await _service.GetUserLoationDetailsAsync(dataTablesRootObject.userId, dataTablesRootObject.stateId, dataTablesRootObject.districtId, dataTablesRootObject.blockId, dataTablesRootObject.villageId, dataTablesRootObject.start, dataTablesRootObject.length, sortInformAction, searchTest);
            r.data = oListAll;
            r.draw = dataTablesRootObject.draw;
            r.error = error;
            if (oListAll != null && oListAll.Count > 0)
            {
                var recordsFiltered = await _service.GetUserLoationDetailsCountAsync(dataTablesRootObject.userId, dataTablesRootObject.stateId, dataTablesRootObject.districtId, dataTablesRootObject.blockId, dataTablesRootObject.villageId, searchTest);
                r.recordsTotal = recordsFiltered;
                r.recordsFiltered = recordsFiltered;
            }
            return r;
        }
    }
}

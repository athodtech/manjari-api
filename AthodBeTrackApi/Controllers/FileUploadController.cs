using AthodBeTrackApi.Helpers;
using AthodBeTrackApi.Models;
using AthodBeTrackApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUploadService _fileUploadService;
        private readonly IActivityService _activityService;
        public FileUploadController(IFileUploadService fileUploadService, IActivityService activityService)
        {
            _fileUploadService = fileUploadService;
            _activityService = activityService;

        }
        [HttpPost]
        public async Task<IActionResult> SaveFile([BindRequired] IFormFile file, [BindRequired, FromQuery] string uploadFileDir, [BindRequired, FromQuery] string uniqueFName)
        {
            try
            {
                var httpRequest = Request.Form;
                string filePath = @$"Documents\{uploadFileDir}";
                if (file.Length > 0)
                {
                    var result = await _fileUploadService.SaveFile(filePath, file, uniqueFName);
                    return Ok(file.FileName);
                }
                else
                {
                    return BadRequest($"Bad Request file size is zero.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadHouseholdProfileImage([BindRequired] IFormFile file, [BindRequired, FromQuery] string uploadFileDir, [BindRequired, FromQuery] string uniqueFName, [BindRequired, FromQuery] int activityQuestionSetId)
        {
            try
            {
                MessageModel message = new MessageModel();
                string filePath = @$"Documents\{uploadFileDir}";
                if (file.Length > 0)
                {
                    var result = await _fileUploadService.SaveFile(filePath, file, uniqueFName);
                    if (result != "")
                    {
                        var flag = await _activityService.UpdateHouseholdProfileImage(activityQuestionSetId, uniqueFName);
                        if (flag)
                        {
                            message.status = Message.success;
                            message.message = "Profile image updated.";
                            return Ok(message);
                        }
                        else
                        {
                            message.status = Message.error;
                            message.message = "Profile image updation failed.";
                            return Ok(message);
                        }
                    }
                    else
                    {
                        message.status = Message.error;
                        message.message = "Profile image updation failed.";
                        return Ok(message);
                    }
                }
                else
                {
                    message.status = Message.error;
                    message.message = "Bad Request file size is zero.";
                    return Ok(message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }
    }
}

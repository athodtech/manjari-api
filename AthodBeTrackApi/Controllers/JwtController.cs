using AthodBeTrackApi.Models;
using AthodBeTrackApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Controllers
{    
    [Route("[controller]/[action]")]
    [AllowAnonymous]
    [ApiController]
    public class JwtController : ControllerBase
    {
        private readonly IJWTManagerService _jWTManager;
        private readonly ILogger<JwtController> _logger;
        public JwtController(IJWTManagerService jWTManager, ILogger<JwtController> logger)
        {
            _jWTManager = jWTManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AuthToken([FromBody] AuthRequest authRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new AuthResponse { IsSuccess = false, Reason = "UserName and Password must be Provided." });

                var authResponse = await _jWTManager.GetTokenAsync(authRequest);
                if (authResponse == null)
                    return BadRequest(new AuthResponse { IsSuccess = false, Reason = "Wrong Credentials." });

                return Ok(authResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new AuthResponse { IsSuccess = false, Reason = "Token must be Provided." });

            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            var token = _jWTManager.GetJwtToken(request.ExpiredToken);            
            var authResponse = await _jWTManager.ValidateDetails(token, request, ipAddress);
            if (!authResponse.IsSuccess)
                return Ok(authResponse);          

            var userName = token.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.NameId).Value;
            
            var response = await _jWTManager.GetRefreshTokenAsync(ipAddress, Convert.ToInt32(authResponse.Reason), userName, request.ExpiredToken);

            return Ok(response);
        }
        
        [HttpGet]
        public IActionResult ValidateApi()
        {
            return Ok("API Validated");
        }
    }
}

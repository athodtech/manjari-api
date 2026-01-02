using AthodBeTrackApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public interface IJWTManagerService
    {
        JwtSecurityToken GetJwtToken(string expiredToken);
        Task<AuthResponse> GetRefreshTokenAsync(string ipAddress, int userId, string userName, string expiredToken);
        Task<AuthResponse> GetTokenAsync(AuthRequest authRequest, string ipAddress);
        Task<AuthResponse> GetTokenAsync(string userName, int userId, string ipAddress);
        Task<AuthResponse> GetTokenAsync(AuthRequest authRequest);
        Task<AuthResponse> ValidateDetails(JwtSecurityToken token, RefreshTokenRequest request, string ipAddress);
        Task<bool> ValidateToken(string token);
    }
}
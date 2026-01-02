using AthodBeTrackApi.Data;
using AthodBeTrackApi.Helpers;
using AthodBeTrackApi.Models;
using AthodBeTrackApi.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public class JWTManagerService : IJWTManagerService
    {
        private readonly IConfiguration _iconfiguration;
        private readonly IGenericRepository _genericRepository;
        private readonly IAccountRepository _accountRepository;
        public JWTManagerService(IConfiguration iconfiguration, IGenericRepository genericRepository, IAccountRepository accountRepository)
        {
            _iconfiguration = iconfiguration;
            _genericRepository = genericRepository;
            _accountRepository = accountRepository;
        }

        public async Task<AuthResponse> GetTokenAsync(AuthRequest authRequest, string ipAddress)
        {
            string dbPassword = string.Empty;
            bool userExits = false;
            userExits = _accountRepository.VerifyUser(authRequest.UserName, out dbPassword);
            if (!userExits)
            {
                return await Task.FromResult<AuthResponse>(null);
            }
            bool isMatch = Password.VerifyPassword(authRequest.Password, dbPassword, Password.Password_Salt);
            if (isMatch)
            {
                var user = await _genericRepository.GetFirstOrDefaultAsync<User>(x => x.UserName.Equals(authRequest.UserName));

                return await SaveTokenDetails(ipAddress, user.UserId, user.UserName);
            }
            else
                return await Task.FromResult<AuthResponse>(null);
        }

        public async Task<AuthResponse> GetTokenAsync(AuthRequest authRequest)
        {
            string dbPassword = string.Empty;
            bool userExits = false;
            userExits = _accountRepository.VerifyUser(authRequest.UserName, out dbPassword);
            if (!userExits)
            {
                return await Task.FromResult<AuthResponse>(null);
            }
            bool isMatch = Password.VerifyPassword(authRequest.Password, dbPassword, Password.Password_Salt);
            if (isMatch)
            {
                var user = await _genericRepository.GetFirstOrDefaultAsync<User>(x => x.UserName.Equals(authRequest.UserName));
                string tokenString = GenerateToken(user.UserName);
                string refresToken = GenerateRefresToken();
                return new AuthResponse { Token = tokenString, RefreshToken = refresToken, IsSuccess = true, ExpirationDate = DateTime.UtcNow.AddMinutes(10) };
            }
            else
                return await Task.FromResult<AuthResponse>(null);
        }

        public async Task<AuthResponse> GetTokenAsync(string userName, int userId, string ipAddress)
        {

            return await SaveTokenDetails(ipAddress, userId, userName);
        }

        private async Task<AuthResponse> SaveTokenDetails(string ipAddress, int userId, string userName, string sessionId = null)
        {
            try
            {
                var expirHours = _iconfiguration.GetValue<int>("JwtSettings:ExpirHours");// 48 Hours
                string tokenString = GenerateToken(userName);
                string refresToken = GenerateRefresToken();
                var userRefreshToken = new ApiUserRefreshToken
                {
                    CreatedDate = DateTime.Now,
                    ExpirationDate = DateTime.Now.AddHours(expirHours),
                    IpAddress = ipAddress,
                    IsInvalidated = false,
                    RefreshToken = refresToken,
                    Token = tokenString,
                    SessionId = sessionId ?? Convert.ToString(Guid.NewGuid()),
                    UserId = userId
                };
                await _genericRepository.InsertAsync(userRefreshToken);
                return new AuthResponse { Token = tokenString, RefreshToken = refresToken, IsSuccess = true, ExpirationDate = userRefreshToken.ExpirationDate, SessionId = userRefreshToken.SessionId };
            }
            catch (Exception)
            {
                return new AuthResponse();
            }
        }


        private string GenerateRefresToken()
        {
            var byteArray = new byte[64];
            using (var cryptoProvider = new RNGCryptoServiceProvider())
            {
                cryptoProvider.GetBytes(byteArray);
                return Convert.ToBase64String(byteArray);
            }
        }
        private string GenerateToken(string userName)
        {
            var jwtkey = _iconfiguration.GetValue<string>("JwtSettings:Key");
            var expirHours = _iconfiguration.GetValue<int>("JwtSettings:ExpirHours");// 48 Hours
            var keyBytes = Encoding.ASCII.GetBytes(jwtkey);

            var tokenHandler = new JwtSecurityTokenHandler();
            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userName)
                }),
                Expires = DateTime.Now.AddHours(expirHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(descriptor);
            string tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        public JwtSecurityToken GetJwtToken(string expiredToken)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ReadJwtToken(expiredToken);
        }

        public async Task<AuthResponse> GetRefreshTokenAsync(string ipAddress, int userId, string userName, string expiredToken)
        {
            string sessionId = string.Empty;
            var apiUserRefreshToken = await _genericRepository.GetFirstOrDefaultAsync<ApiUserRefreshToken>(x => x.Token == expiredToken);
            if (apiUserRefreshToken != null)
                sessionId = apiUserRefreshToken.SessionId;

            return await SaveTokenDetails(ipAddress, userId, userName, sessionId);
        }

        public async Task<AuthResponse> ValidateDetails(JwtSecurityToken token, RefreshTokenRequest request, string ipAddress)
        {
            var userRefreshToken = await _genericRepository.GetFirstOrDefaultAsync<ApiUserRefreshToken>(x =>
             x.IsInvalidated == false
             && x.Token == request.ExpiredToken
             && x.RefreshToken == request.RefreshToken
             //&& x.IpAddress == ipAddress
             );
            if (userRefreshToken == null)
                return new AuthResponse { IsSuccess = false, Reason = "Invalid Token Details." };
            //if (token.ValidTo > DateTime.UtcNow)
            //    return new AuthResponse { IsSuccess = false, Reason = "Token not expired." };
            if (userRefreshToken.ExpirationDate < DateTime.UtcNow)
                return new AuthResponse { IsSuccess = false, Reason = "Refresh Token expired." };

            else
            {
                userRefreshToken.IsInvalidated = true;
                await _genericRepository.UpdateAsync<ApiUserRefreshToken>(userRefreshToken);

                return new AuthResponse { IsSuccess = true, Reason = userRefreshToken.UserId.ToString() };
            }
        }

        public async Task<bool> ValidateToken(string token)
        {
            var date = DateTime.Now;
            var sessionId = _genericRepository.GetFirstOrDefaultAsync<ApiUserRefreshToken>(x => x.Token == token).GetAwaiter().GetResult().SessionId;
            if (await _genericRepository.ExistsAsync<UserLog>(x => x.SessionId == sessionId && x.LogoutTime != null)) return false;

            var expirHours = _iconfiguration.GetValue<int>("JwtSettings:ExpirHours");// 48 Hours
            return await _genericRepository.ExistsAsync<ApiUserRefreshToken>(x => x.Token == token && x.CreatedDate.AddHours(expirHours) >= date);
        }
    }
}

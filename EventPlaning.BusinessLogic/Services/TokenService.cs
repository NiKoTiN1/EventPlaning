using EventPlanning.BusinessLogic.Interfaces;
using EventPlanning.DataAccess.Interfaces;
using EventPlanning.Domain.Enums;
using EventPlanning.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanning.BusinessLogic.Services
{
    public class TokenService : ITokenService
    {
        public TokenService(
            IRefreshTokenRepository refreshTokenRepository,
            IConfiguration configuration,
            UserManager<Account> userManager,
            ILogger<TokenService> logger)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
        }

        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Account> _userManager;
        private readonly ILogger<TokenService> _logger;

        public async Task<string> GenerateToken(Account user)
        {
            _logger.LogInformation("Method GenerateToken started.");

            var userRole = (await _userManager.IsInRoleAsync(user, Roles.Guest.ToString())) ? Roles.Guest : Roles.Creator;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("Role", userRole.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(Convert.ToInt32(_configuration["Authentication:LIFETIME"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:KEY"])), SecurityAlgorithms.HmacSha256Signature),
                Audience = _configuration["Authentication:AUDIENCE"],
                Issuer = _configuration["Authentication:ISSUER"],
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {

                var token = tokenHandler.CreateToken(tokenDescriptor);

                _logger.LogInformation("Method GenerateToken finished succeed.");

                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return string.Empty;
        }

        public async Task<RefreshToken> GenerateRefreshToken()
        {
            RefreshToken refreshToken = new RefreshToken()
            {
                Id = Guid.NewGuid(),
                Expiration = DateTime.UtcNow.AddMonths(3)
            };

            var randomNumber = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken.Token = Convert.ToBase64String(randomNumber);
            }

            await _refreshTokenRepository.Add(refreshToken);

            return refreshToken;
        }

        public string GetUserIdFromAccessToken(string accessToken)
        {
            var tokenValidationParamters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _configuration["Authentication:ISSUER"],
                ValidateAudience = true,
                ValidAudience = _configuration["Authentication:AUDIENCE"],
                ValidateLifetime = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:KEY"])),
                ValidateIssuerSigningKey = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParamters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token!");
            }

            var userId = principal.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new SecurityTokenException("Missing claim: UserId!");
            }

            return userId;
        }

        public bool ValidateRefreshToken(Account user, string refreshToken)
        {
            if (user == null || user.RefreshToken.Token != refreshToken)
            {
                return false;
            }

            if (DateTime.UtcNow > user.RefreshToken.Expiration)
            {
                return false;
            }

            return true;
        }

        public bool RemoveToken(string refreshToken)
        {
            var tokenFromDb = _refreshTokenRepository.Get(token => token.Token == refreshToken).SingleOrDefault();

            if (tokenFromDb == null)
            {
                return false;
            }

            _refreshTokenRepository.Remove(tokenFromDb);
            return true;
        }
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChatBlockchain.Core.Options;
using ChatBlockchain.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ChatBlockchain.Api.Services
{
    public class JwtService(IOptions<JwtSettings> jwtSettings, ILogger<JwtService> logger): IJwtService
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;
        private readonly ILogger<JwtService> _logger = logger;

        public string GenerateToken(string address)
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, address),
                    new Claim("address", address)
                };

                var token = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while generating JWT token.");
                throw;
            }
        }

        public bool ValidateToken(string token, out string address)
        {
            address = string.Empty;
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                address = jwtToken.Claims.First(x => x.Type == "address").Value;

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex, "Invalid JWT token.");
                return false;
            }
        }
    }
}
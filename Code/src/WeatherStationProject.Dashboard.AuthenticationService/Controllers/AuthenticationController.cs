using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WeatherStationProject.Dashboard.Core.Configuration;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WeatherStationProject.Dashboard.AuthenticationService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route(template: "api/v{version:apiVersion}/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAppConfiguration _configuration;

        public AuthenticationController(IAppConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet(template: "{secret}")]
        public IActionResult Get(string secret)
        {
            if (GetHashedSecret(secret:secret) != _configuration.HashedAuthenticationSecret) return NotFound();

            var now = DateTime.UtcNow;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "client"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.Audience.Secret));

            var jwt = new JwtSecurityToken(
                issuer: _configuration.Audience.Issuer,
                audience: _configuration.Audience.ValidAudience,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(key: signingKey, algorithm: SecurityAlgorithms.HmacSha512)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var responseJson = new
            {
                access_token = encodedJwt,
                expires_in = (int)TimeSpan.FromMinutes(2).TotalSeconds
            };

            return Ok(responseJson);
        }

        private string GetHashedSecret(string secret)
        {
            var sb = new StringBuilder();

            using var hash = SHA256.Create();
            var result = hash.ComputeHash(Encoding.UTF8.GetBytes(secret));
            foreach (var b in result)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}

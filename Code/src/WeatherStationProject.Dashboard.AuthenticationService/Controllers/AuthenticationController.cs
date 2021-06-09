using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        private const int SecretMinimumLength = 16;
        private readonly IAppConfiguration _configuration;

        public AuthenticationController(IAppConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet(template: "{secret}")]
        public IActionResult Get(string secret)
        {
            if (secret != _configuration.AuthenticationSecret) return StatusCode(403);

            var now = DateTime.UtcNow;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "client"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.Audience.Secret.PadLeft(totalWidth: SecretMinimumLength, paddingChar: '0')));

            var jwt = new JwtSecurityToken(
                issuer: _configuration.Audience.Issuer,
                audience: _configuration.Audience.ValidAudience,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(key: signingKey, algorithm: SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var responseJson = new
            {
                access_token = encodedJwt,
                expires_in = (int)TimeSpan.FromMinutes(2).TotalSeconds
            };

            return Ok(responseJson);
        }
    }
}

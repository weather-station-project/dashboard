using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WeatherStationProject.Dashboard.Core.Configuration;
using WeatherStationProject.Dashboard.Core.Security;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WeatherStationProject.Dashboard.AuthenticationService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/authentication")]
    public class AuthenticationController : ControllerBase
    {
        [HttpGet("{secret}")]
        public ObjectResult Index(string secret)
        {
            if (secret != AppConfiguration.AuthenticationSecret) return StatusCode(403, "Secret invalid!");

            var now = DateTime.UtcNow;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "client"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(CultureInfo.InvariantCulture),
                    ClaimValueTypes.Integer64)
            };

            var jwt = new JwtSecurityToken(
                Audience.Issuer,
                Audience.ValidAudience,
                claims,
                now,
                now.Add(TimeSpan.FromMinutes(2)),
                new SigningCredentials(JwtAuthenticationConfiguration.SigningKey, SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var responseJson = new
            {
                accessToken = encodedJwt,
                expiresIn = (int) TimeSpan.FromMinutes(2).TotalSeconds
            };

            return Ok(responseJson);
        }
    }
}
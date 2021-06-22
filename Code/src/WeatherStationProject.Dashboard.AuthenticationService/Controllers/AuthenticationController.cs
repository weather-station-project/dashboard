using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WeatherStationProject.Dashboard.Core.Configuration;
using WeatherStationProject.Dashboard.Core.Security;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WeatherStationProject.Dashboard.AuthenticationService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route(template: "api/v{version:apiVersion}/authentication")]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost(template: "{secret}")]
        public IActionResult Post(string secret)
        {
            if (secret != AppConfiguration.AuthenticationSecret) return StatusCode(403);

            var now = DateTime.UtcNow;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "client"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            var jwt = new JwtSecurityToken(
                issuer: Audience.Issuer,
                audience: Audience.ValidAudience,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(key: JwtAuthenticationConfiguration.SigningKey, algorithm: SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var responseJson = new
            {
                accessToken = encodedJwt,
                expiresIn = (int)TimeSpan.FromMinutes(2).TotalSeconds
            };

            return Ok(responseJson);
        }
    }
}

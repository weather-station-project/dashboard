using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using WeatherStationProject.Dashboard.Core.Configuration;

namespace WeatherStationProject.Dashboard.Core.Security
{
    public static class JwtAuthenticationConfiguration
    {
        private const int SecretMinimumLength = 16;

        public const string SwaggerDescriptionText = @"JWT Authorization header using the Bearer scheme.
            Enter 'Bearer' [space] and then your token in the text input below.
            Example: 'Bearer 12345abcdef'";

        public static SymmetricSecurityKey SigningKey =>
            new(Encoding.ASCII.GetBytes(Audience.Secret.PadLeft(totalWidth: SecretMinimumLength, paddingChar: '0')));

        public static TokenValidationParameters GetTokenValidationParameters()
        {
            return new()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SigningKey,
                ValidateIssuer = true,
                ValidIssuer = Audience.Issuer,
                ValidateAudience = true,
                ValidAudience = Audience.ValidAudience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
            };
        }
    }
}

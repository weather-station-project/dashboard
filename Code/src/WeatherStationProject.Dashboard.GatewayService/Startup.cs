using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using WeatherStationProject.Dashboard.Core.Security;

namespace WeatherStationProject.Dashboard.GatewayService
{
    public class Startup
    {
        private readonly bool _isDevelopment;
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment env)
        {
            _isDevelopment = env.IsDevelopment();

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile(path: $"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddOcelot(folder: "Configuration", env: env);

            _configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.TokenValidationParameters = JwtAuthenticationConfiguration.GetTokenValidationParameters();
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeatherStationProject - Dashboard - GatewayService", Version = "v1" });
            });

            services.AddOcelot(configuration: _configuration)
                .AddCacheManager(x =>
                {
                    x.WithDictionaryHandle();
                });

            if (_isDevelopment)
            {
                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder.AllowAnyOrigin();
                            builder.AllowAnyHeader();
                        });
                });
            }
        }

        public async void Configure(IApplicationBuilder app)
        {
            if (_isDevelopment)
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(url: "/air-parameters/v1/swagger.json", name: "AirParametersService v1");
                    c.SwaggerEndpoint(url: "/ambient-temperatures/v1/swagger.json", name: "AmbientTemperatureService v1");
                    c.SwaggerEndpoint(url: "/ground-temperatures/v1/swagger.json", name: "GroundTemperatureService v1");
                    c.SwaggerEndpoint(url: "/rainfall/v1/swagger.json", name: "RainfallService v1");
                    c.SwaggerEndpoint(url: "/wind-measurements/v1/swagger.json", name: "WindMeasurementsService v1");
                });

                app.UseCors();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            await app.UseOcelot();
        }
    }
}

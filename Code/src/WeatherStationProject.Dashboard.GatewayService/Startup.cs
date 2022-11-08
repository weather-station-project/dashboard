using System.Net.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using WeatherStationProject.Dashboard.Core.Handlers;
using WeatherStationProject.Dashboard.Core.Security;

namespace WeatherStationProject.Dashboard.GatewayService;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly bool _isDevelopment;

    public Startup(IWebHostEnvironment env)
    {
        _isDevelopment = env.IsDevelopment();

        var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
            .AddOcelot(_isDevelopment ? "Configuration.Development" : $"Configuration.{env.EnvironmentName}", null);

        _configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHealthChecks().AddCheck<HealthCheck.HealthCheck>("health-check");

        services.AddScoped<HttpMessageHandler, SslIgnoreClientHandler>();

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

        services.AddMvcCore().AddApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo {Title = "WeatherStationProject - Dashboard - GatewayService", Version = "v1"});
        });

        services.AddOcelot(_configuration)
            .AddCacheManager(x => { x.WithDictionaryHandle(); });

        if (_isDevelopment)
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

    public void Configure(IApplicationBuilder app)
    {
        if (_isDevelopment)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/air-parameters/v1/swagger.json", "AirParametersService v1");
                c.SwaggerEndpoint("/ambient-temperatures/v1/swagger.json", "AmbientTemperatureService v1");
                c.SwaggerEndpoint("/ground-temperatures/v1/swagger.json", "GroundTemperatureService v1");
                c.SwaggerEndpoint("/rainfall/v1/swagger.json", "RainfallService v1");
                c.SwaggerEndpoint("/wind-measurements/v1/swagger.json", "WindMeasurementsService v1");
            });

            app.UseCors();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapHealthChecks("/api/health-check"); });
        app.UseOcelot();
    }
}
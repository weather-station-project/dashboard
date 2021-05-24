using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WeatherStationProject.Dashboard.Core.Configuration;
using WeatherStationProject.Dashboard.Data;
using WeatherStationProject.Dashboard.GroundTemperatureService.Data;
using WeatherStationProject.Dashboard.GroundTemperatureService.Services;

namespace WeatherStationProject.Dashboard.GroundTemperatureService
{
    public class Startup
    {
        private readonly bool _isDevelopment;

        public Startup(IWebHostEnvironment env)
        {
            _isDevelopment = env.IsDevelopment();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAppConfiguration, AppConfiguration>();

            services.AddDbContext<GroundTemperatureDbContext>();

            services.AddScoped<IRepository<GroundTemperature>, GroundTemperatureRepository>();

            services.AddScoped<IGroundTemperatureService, Services.GroundTemperatureService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(majorVersion: 1, minorVersion: 0);

                config.AssumeDefaultVersionWhenUnspecified = true;

                config.ReportApiVersions = true;

                config.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader(headerNames: "X-version"),
                                                                   new QueryStringApiVersionReader(parameterNames: "api-version"));
            });

            if (_isDevelopment)
            {
                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder.WithOrigins(GlobalConstants.AppLocalhostUrl);
                        });
                });
            }

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Weather Station Project - Dashboard - GroundTemperatureService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_isDevelopment)
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherStationProject.Dashboard.GroundTemperatureService v1"));

                app.UseCors();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
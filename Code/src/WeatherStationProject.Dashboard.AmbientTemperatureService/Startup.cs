using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Data;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Services;
using WeatherStationProject.Dashboard.Core.Configuration;
using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.AmbientTemperatureService
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

            services.AddDbContext<AmbientTemperatureDbContext>();

            services.AddScoped<IRepository<AmbientTemperature>, AmbientTemperatureRepository>();

            services.AddScoped<IAmbientTemperatureService, Services.AmbientTemperatureService>();

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
                            builder.AllowAnyOrigin();
                        });
                });
            }

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Weather Station Project - Dashboard - AmbientTemperatureService", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            if (_isDevelopment)
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AmbientTemperatureService v1"));

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

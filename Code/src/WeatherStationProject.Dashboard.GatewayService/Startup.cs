using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace WeatherStationProject.Dashboard.GatewayService
{
    public class Startup
    {
        private readonly bool _isDevelopment;

        public Startup(IWebHostEnvironment env)
        {
            _isDevelopment = env.IsDevelopment();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeatherStationProject - Dashboard - GatewayService", Version = "v1" });
            });
            services.AddOcelot()
                .AddCacheManager(x =>
                {
                    x.WithDictionaryHandle();
                });
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
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            await app.UseOcelot();
        }
    }
}

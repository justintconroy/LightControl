using LightControlServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace LightControlServer
{
    public class Startup
    {
        readonly string DevFrontendAllowCors = "_devFrontendAllowCors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddLogging();

            services.AddCors(options =>
            {
                options.AddPolicy(name: DevFrontendAllowCors,
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:3000")
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            #region Seed Data
            services.AddDbContext<LightControlModel>(options =>
                options.UseInMemoryDatabase("LightsDb"));

            #endregion Seed Data
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var scope =
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            using (var ctx = scope.ServiceProvider.GetService<LightControlModel>())
            {
                AddSeedData(ctx);
            }

                app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(DevFrontendAllowCors);

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddSeedData(LightControlModel ctx)
        {
            var strand1 = new Strand
            {
                Id = 1,
                Lights = new List<Light>
                { 
                    new Light { Id = 1, Color = "#FF0000" },
                    new Light { Id = 2, Color = "#00FF00"},
                    new Light { Id = 3, Color = "#0000FF"},
                    new Light { Id = 4, Color = "#FF0000"},
                    new Light { Id = 5, Color = "#00FF00"},
                    new Light { Id = 6, Color = "#0000FF"},
                    new Light { Id = 7, Color = "#FF0000"},
                    new Light { Id = 8, Color = "#00FF00"},
                    new Light { Id = 9, Color = "#0000FF"},
                }
            };
            ctx.Strands.Add(strand1);
            ctx.SaveChanges();
        }
    }
}

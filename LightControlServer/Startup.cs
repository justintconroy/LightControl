using LightControlServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;
using System;
using System.Collections.Generic;
using System.Text;

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

            services.AddDbContext<LightControlModel>(options =>
                options.UseInMemoryDatabase("LightsDb"));

            #region MQTT
            services.AddSingleton(serviceProvider =>
            {
                var options = new ManagedMqttClientOptionsBuilder()
                    .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                    .WithClientOptions(new MqttClientOptionsBuilder()
                        .WithClientId("Client1")
                        .WithTcpServer("localhost")
                        .Build())
                    .Build();

                var client = new MqttFactory().CreateManagedMqttClient();
                client.SubscribeAsync(new TopicFilterBuilder()
                    .WithTopic("hi/world").Build()).GetAwaiter().GetResult();

                client.UseApplicationMessageReceivedHandler(e =>
                {
                    Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
                    Console.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
                    Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
                    Console.WriteLine($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
                    Console.WriteLine($"+ Retain = {e.ApplicationMessage.Retain}");
                    Console.WriteLine();

                    client.PublishAsync("hello/world", "hi").GetAwaiter().GetResult();
                });

                client.StartAsync(options).GetAwaiter().GetResult();

                return client;
            });
            #endregion MQTT
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

            app.ApplicationServices.GetService<IManagedMqttClient>();
        }

        private static void AddSeedData(LightControlModel ctx)
        {
            var lights = new List<Light>
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
            };
            ctx.Lights.AddRange(lights);

            var strand1 = new Strand
            {
                Id = 1,
                Lights = lights
            };
            ctx.Strands.Add(strand1);
            ctx.SaveChanges();
        }
    }
}

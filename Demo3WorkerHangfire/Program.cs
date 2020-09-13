using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo3WorkerHangfire
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("Server =.; Database = HangfireTest; Integrated Security = SSPI;");

            var hostBuilder = new HostBuilder()
                // Add configuration, logging, ...
                .ConfigureServices((hostContext, services) =>
                {
                    // Add your services with depedency injection.
                });

            using (var server = new BackgroundJobServer(new BackgroundJobServerOptions()
            {
                WorkerCount = 1
            }))
            { 
                await hostBuilder.RunConsoleAsync();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
                 .ConfigureServices((hostContext, services) =>
                 {
                     //services.AddHostedService<Worker>();
                     services.AddSingleton(provider => new BackgroundJobServerOptions
                     {
                         Queues = new[] { "default", "misc" },
                         WorkerCount = 1,
                     });
                     services.AddHangfire(configuration => configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                      .UseSqlServerStorage("Server =.; Database = HangfireTest; Integrated Security = SSPI; ", new SqlServerStorageOptions
                      {
                          CommandBatchMaxTimeout = System.TimeSpan.FromMinutes(5),
                          SlidingInvisibilityTimeout = System.TimeSpan.FromMinutes(5),
                          QueuePollInterval = System.TimeSpan.Zero,
                          UseRecommendedIsolationLevel = true,
                          DisableGlobalLocks = true // Migration to Schema 7 is required
                      }));

                     services.AddHangfireServer();
                 });
    }
}

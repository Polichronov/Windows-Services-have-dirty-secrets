using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hangfire;
using Hangfire.SqlServer;


namespace Demo4WorkerHangfire
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch
            {
                var po = 1;
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

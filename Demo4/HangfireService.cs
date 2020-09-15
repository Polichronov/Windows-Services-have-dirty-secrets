using Autofac;
using Hangfire;
using Hangfire.Logging;
using Hangfire.SqlServer;
using System;
using Topshelf;

namespace Demo4
{
    internal class HangfireService
    {
        private BackgroundJobServer _server;
        private BackgroundJobServerOptions _options;

        public HangfireService()
        {
            var builder = new ContainerBuilder().Configure();

            // Recommended in: https://docs.hangfire.io/en/latest/configuration/using-sql-server.html
            var config = GlobalConfiguration.Configuration
                .UseAutofacActivator(builder.Build())
                .UseSqlServerStorage("Server =.; Database = HangfireTest; Integrated Security = SSPI;", new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),//algo to get meessages without trasactions
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true
                });
            //.UseLog4NetLogProvider();

            if (Environment.UserInteractive)
                config.UseColouredConsoleLogProvider(LogLevel.Debug);

            //GlobalJobFilters.Filters.Add(new CustomRetryFilter
            //{
            //    Attempts = 15,
            //    DelaysInSeconds = new[] { 60 },
            //    OnAttemptsExceeded = AttemptsExceededAction.Fail,
            //    LogEvents = true
            //});

            _options = new BackgroundJobServerOptions
            {
                WorkerCount = 1,
            };

        }

        public bool Start(HostControl hostControl)
        {

            _server = new BackgroundJobServer();
            return true;
        }

        public void Start()
        {
            _server = new BackgroundJobServer(_options);

        }

        public void Stop()
        {
            _server.Dispose();
        }
    }
}

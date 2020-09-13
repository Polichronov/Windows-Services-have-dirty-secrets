using Demo4.Hangfire.Common.JobInterfaces;
using Hangfire.Logging;
using System;
using System.Threading;

namespace Demo4.Jobs
{
    class PublisherJobs : IPublisherJob
    {
        protected static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

        public void Execute()
        {
            var currentDomain = AppDomain.CurrentDomain;
            var assems = currentDomain.GetAssemblies();
            Logger.Debug($"Publish messages {Thread.CurrentThread.Name}");
        }
    }
}

using Demo4.Hangfire.Common.JobInterfaces;
using Hangfire.Logging;
using System.Threading;

namespace Demo4.Jobs
{
    public class PublisherJob : IPublisherJob
    {
        protected static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

        public void Execute()
        {
            Logger.Debug($"Publish messages {Thread.CurrentThread.Name}");
        }
    }
}

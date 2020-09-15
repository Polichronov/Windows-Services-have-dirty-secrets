using Hangfire;

namespace Demo4.Hangfire.Common.JobInterfaces
{
    //[DisableConcurrentExecution(timeoutInSeconds: 10 * 60)]
    public interface IPublisherJob : IBackgroundJob
    {

    }
}

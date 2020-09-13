using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Demo3Worker
{
    public class NewBackgroundWorker : BackgroundService
    {

        public NewBackgroundWorker()
        {
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Do some work
            await Task.Delay(1000, stoppingToken);
        }
    }
}

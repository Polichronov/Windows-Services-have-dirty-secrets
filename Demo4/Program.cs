using System;
using System.Threading;
using Topshelf;

namespace Demo4
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (Environment.UserInteractive)
                {
                    var HangfireService = new HangfireService();
                    HangfireService.Start();
                    Console.WriteLine("BackgroundJobsRunner service started. Press any key to exit...");

                    while (true)
                    {
                        Thread.Sleep(10000);
                    }
                }
                else
                {
                    HostFactory.Run(windowsService =>
                        {
                            windowsService.Service<HangfireService>(s =>
                            {
                                s.ConstructUsing(service => new HangfireService());
                                s.WhenStarted(service => service.Start());
                                s.WhenStopped(service => service.Stop());
                            });

                            windowsService.RunAsLocalSystem();
                            windowsService.StartAutomatically();

                            windowsService.SetDescription("ServiceHangfire");
                            windowsService.SetDisplayName("ServiceHangfire");
                            windowsService.SetServiceName("ServiceHangfire");
                        });
                }
            }
            catch(Exception ex)
            {
                var p = ex;
            }

        }
    }
}

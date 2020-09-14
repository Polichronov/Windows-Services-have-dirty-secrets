using System;
using System.ServiceProcess;
using System.Threading;

namespace Demo1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            var servicesToRun = new ServiceBase[]
            {
                new Demo1()
            };

            ServiceBase.Run(servicesToRun);

            //InteractiveStart(args, servicesToRun);

        }

        private static void InteractiveStart(string[] args, ServiceBase[] servicesToRun)
        {
            // Start the service
            if (Environment.UserInteractive)
            {
                var service = new Demo1();
                service.StartDemo(args);

                Console.WriteLine("BackgroundJobsRunner service started. Press any key to exit...");
                while (true)
                {
                    Thread.Sleep(10000);
                }
            }
            else
            {
                ServiceBase.Run(servicesToRun);
            }
        }
    }
}

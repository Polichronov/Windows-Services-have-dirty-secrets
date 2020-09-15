using Demo2;
using System;
using Topshelf;

namespace Demo2Topshelf
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<ServiceControlService>();
                x.EnableServiceRecovery(r => r.RestartService(TimeSpan.FromSeconds(10)));
                x.SetServiceName("ServiceTopshell");
                x.StartAutomatically();
                x.RunAsLocalSystem();
            });


            //HostFactory.Run(serviceConfig =>
            //{
            //    serviceConfig.Service<CustomService>(s =>
            //    {
            //        s.ConstructUsing(
            //            () => new CustomService());

            //        s.WhenStarted(
            //            execute => execute.Start());

            //        s.WhenStopped(
            //            execute => execute.Stop());
            //    });

            //    serviceConfig.EnableServiceRecovery(recoveryOption =>
            //    {
            //        recoveryOption.RestartService(1);
            //        recoveryOption.RestartComputer(60, "PS Demo");
            //        recoveryOption.RunProgram(5,
            //            @"c:\someprogram.exe");
            //    });

            //    serviceConfig.SetServiceName("ServiceTopshell");
            //    serviceConfig.SetDisplayName("ServiceTopshell");
            //    serviceConfig.SetDescription("Dev bg demo service");

            //    serviceConfig.StartAutomatically();
            //});
        }
    }
}

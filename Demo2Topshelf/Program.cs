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
        }
    }   

    public class ServiceControlService : ServiceControl
    {
        private const string _logFileLocation = @"C:\temp\servicelog.txt";

        private void Log(string logMessage)
        {
            //Directory.CreateDirectory(Path.GetDirectoryName(_logFileLocation));
            //File.AppendAllText(_logFileLocation, DateTime.UtcNow.ToString() + " : " + logMessage + Environment.NewLine);
        }

        public bool Start(HostControl hostControl)
        {
            Log("Starting");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            Log("Stopping");
            return true;
        }
    }
}

using Autofac;
using Demo4.Hangfire.Common.JobInterfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Demo4
{
    public static class ContainerConfig
    {
        /// <summary>
        /// Documentation how to define scope per Job: https://github.com/HangfireIO/Hangfire.Autofac#deterministic-disposal
        /// </summary>
        public static ContainerBuilder Configure(this ContainerBuilder builder)
        {
            // Setup global DI
            var assemblies = new[]
            {
                Assembly.GetExecutingAssembly(),              // The current assembly
                typeof(IBackgroundJob).Assembly,              // Setup jobs from Inventory.BackgroundJobsRunner.Common
            };

            builder.RegisterAssemblyTypes(assemblies)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            return builder;
        }
    }
}

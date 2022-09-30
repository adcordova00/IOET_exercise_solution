using Horarios.Interfaces;
using Horarios.Repos;
using ConsoleApp2.Controller;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace ConsoleApp2
{
    class Program
    {
        public static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var attendanceController = serviceProvider.GetService<Controller1>();
            attendanceController.Index();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole())
                .AddTransient<Controller1>();

            services.AddSingleton<IAssistance, Repositories>();
        }
    }
}


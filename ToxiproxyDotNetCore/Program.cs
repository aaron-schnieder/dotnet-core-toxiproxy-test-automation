using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Newtonsoft.Json;
using ToxiproxyDotNetCore.Interfaces;

namespace ToxiproxyDotNetCore
{
    class Program
    {
        public static void Main(string[] args) 
        {
            // Configure services
            var services = ConfigureServices();

            // Build a service provider 
            var serviceProvider = services.BuildServiceProvider();

            // Call the Run method in our console application to start things off
            serviceProvider.GetService<ConsoleApplication>().Run().GetAwaiter().GetResult();
        }
        

        public static IServiceCollection ConfigureServices()
        {
            // Create a ServicesCollection
            IServiceCollection services = new ServiceCollection();

            // TODO: replace with logging instance
            //services.AddSingleton(typeof(ILogger<T>), typeof(Logger<T>));

            // Register an HttpClient for ApiClient            
            // No need to register the ApiClient class again as this takes care of both
            services.AddHttpClient<IApiClient, ApiClient>();

            // Register our dependencies
            services.AddTransient<IModule, Module>();
            services.AddTransient<ConsoleApplication>();

            return services;
        }
    }
}

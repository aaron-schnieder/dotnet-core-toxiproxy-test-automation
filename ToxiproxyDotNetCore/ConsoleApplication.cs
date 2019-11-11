using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ToxiproxyDotNetCore.Interfaces;

namespace ToxiproxyDotNetCore
{
    class ConsoleApplication
    {
        private readonly IModule _module;

        public ConsoleApplication(IModule module)
        {
            _module = module;
        }

        public async Task Run()
        {
            var stopWatch = new Stopwatch();

            stopWatch.Start();
            PostmanEcho result = await _module.GetDataAsync();
            stopWatch.Stop();

            Console.WriteLine($"Execution time: {stopWatch.ElapsedMilliseconds}ms");
            Console.WriteLine(JsonConvert.SerializeObject(result));
        }
    }
}
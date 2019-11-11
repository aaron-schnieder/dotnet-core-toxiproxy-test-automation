using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ToxiproxyDotNetCore.Interfaces;

namespace ToxiproxyDotNetCore
{
    public class Module : IModule
    {
        private readonly ILogger<Module> _logger;

        private readonly IApiClient _apiClient;

        public Module(IApiClient apiClient, ILogger<Module> logger) {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public async Task<PostmanEcho> GetDataAsync() {
            return await _apiClient.GetEchoAsync();
        }

        public PostmanEcho PostData() {
            var model = new PostmanEcho();

            return model;
        }
    }
}
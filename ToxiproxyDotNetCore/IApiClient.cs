using System;
using System.Threading.Tasks;

namespace ToxiproxyDotNetCore
{
    public interface IApiClient    {
        Task<PostmanEcho> GetEchoAsync();
    }
}
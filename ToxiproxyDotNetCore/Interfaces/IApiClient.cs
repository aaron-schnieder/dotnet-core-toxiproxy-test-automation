using System;
using System.Threading.Tasks;

namespace ToxiproxyDotNetCore.Interfaces
{
    public interface IApiClient    {
        Task<PostmanEcho> GetEchoAsync();
    }
}
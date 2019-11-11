using System;
using System.Threading.Tasks;

namespace ToxiproxyDotNetCore
{
    public interface IModule
    {
        Task<PostmanEcho> GetDataAsync();
        PostmanEcho PostData();
    }
}
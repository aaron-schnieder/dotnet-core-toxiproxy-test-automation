using System;
using System.Threading.Tasks;

namespace ToxiproxyDotNetCore.Interfaces
{
    public interface IModule
    {
        Task<PostmanEcho> GetDataAsync();
        PostmanEcho PostData();
    }
}
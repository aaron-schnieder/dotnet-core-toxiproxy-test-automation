using System;
using System.Net.Http;
using Microsoft.Extensions.Http;

namespace Toxiproxy
{
    public interface IToxiproxyClient 
    {
        void Reset();
    }
}
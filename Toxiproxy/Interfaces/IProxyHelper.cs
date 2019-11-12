using System;
using System.Collections.Generic;

namespace Toxiproxy
{
    public interface IProxyHelper
    {
        IEnumerable<Proxy> List();
        Proxy Get(string proxyName);
        Proxy Add(Proxy proxy);
        Proxy Update(string proxyNameToUpdate, Proxy proxy);
        void Delete(string proxyName);
    }
}
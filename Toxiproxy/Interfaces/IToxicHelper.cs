using System;
using System.Collections.Generic;

namespace Toxiproxy
{
    public interface IToxicHelper
    {
        IEnumerable<Toxic> List(string proxyName);
        Toxic Get(string toxicName, string proxyName);
        Toxic Add(Toxic toxic, string proxyName);
        Toxic Update(Toxic toxic, string toxicNameToUpdate, string proxyName);
        void Delete(string toxicName, string proxyName);
    }
}
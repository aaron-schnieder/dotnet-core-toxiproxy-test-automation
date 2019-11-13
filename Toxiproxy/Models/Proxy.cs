using System.Collections.Generic;
using Newtonsoft.Json;

namespace Toxiproxy
{
    public class Proxy
    {
        [JsonProperty("name")]
        public string Name {get;set;}

        [JsonProperty("listen")]
        public string Listen {get;set;}

        [JsonProperty("upstream")]
        public string Upstream {get;set;}

        [JsonProperty("enabled")]
        public bool Enabled {get;set;}

        [JsonProperty("toxics")]
        public IEnumerable<Toxic> Toxics {get;set;}
    }
}
using System;
using Newtonsoft.Json;

namespace Toxiproxy
{
    public abstract class Toxic
    {
        [JsonProperty("name")]
        public string Name {get;set;}

        [JsonProperty("type")]
        public string Type {get;set;}

        [JsonProperty("stream")]
        public string Stream {get;set;}
        
        [JsonProperty("toxicity")]
        public float Toxicity {get;set;}
    }
}
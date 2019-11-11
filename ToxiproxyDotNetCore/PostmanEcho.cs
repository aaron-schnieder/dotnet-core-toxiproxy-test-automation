namespace ToxiproxyDotNetCore
{
    using System;
    using Newtonsoft.Json;

    public partial class PostmanEcho
    {
        [JsonProperty("args")]
        public Args Args { get; set; }

        [JsonProperty("headers")]
        public Headers Headers { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public partial class Args
    {
        [JsonProperty("foo1")]
        public string Foo1 { get; set; }

        [JsonProperty("foo2")]
        public string Foo2 { get; set; }
    }

    public partial class Headers
    {
        [JsonProperty("x-forwarded-proto")]
        public string XForwardedProto { get; set; }

        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("accept")]
        public string Accept { get; set; }

        [JsonProperty("accept-encoding")]
        public string AcceptEncoding { get; set; }

        [JsonProperty("cache-control")]
        public string CacheControl { get; set; }

        [JsonProperty("postman-token")]
        public Guid PostmanToken { get; set; }

        [JsonProperty("user-agent")]
        public string UserAgent { get; set; }

        [JsonProperty("x-forwarded-port")]
        public long XForwardedPort { get; set; }
    }
}

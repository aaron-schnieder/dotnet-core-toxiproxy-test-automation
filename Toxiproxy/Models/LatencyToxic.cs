using System;
using Newtonsoft.Json;

namespace Toxiproxy
{
    public class LatencyToxic : Toxic
    {
        /// <summary>
        /// Attributes for the toxic
        /// </summary>
        public class ToxicAttributes
        {
            public int Latency { get; set; }
            public int Jitter { get; set; }
        }

        public ToxicAttributes Attributes { get; set; }

        public static string ToxicType = "latency";

        /// <summary>
        /// Initializes a new instance of the <see cref="LatencyToxic"/> class.
        /// </summary>
        public LatencyToxic()
        {
            Attributes = new ToxicAttributes();
        }
    }
}
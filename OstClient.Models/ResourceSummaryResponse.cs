using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OstClient.Models
{
    public class ResourceSummaryResponse
    {
        [JsonProperty("error")]
        public bool Error { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("summary")]
        public IEnumerable<ResourceSummary> Summary { get; set; }
    }
}

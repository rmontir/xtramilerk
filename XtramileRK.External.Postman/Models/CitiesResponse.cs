using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace XtramileRK.External.Postman.Models
{
    public class CitiesResponse
    {
        [JsonPropertyName("error")]
        public bool IsError { get; set; }

        [JsonPropertyName("msg")]
        public string Message { get; set; }

        [JsonPropertyName("data")]
        public ImmutableHashSet<string> Cities { get; set; }
    }
}

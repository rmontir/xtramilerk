using System.Text.Json.Serialization;

namespace XtramileRK.External.OpenWeather.Models
{
    public class CloudInfo
    {
        [JsonPropertyName("all")]
        public int All { get; set; }

        public CloudInfo() { }
        
        public CloudInfo(
            int all
        )
        {
            All = all;
        }
    }
}

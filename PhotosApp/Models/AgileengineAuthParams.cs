using Newtonsoft.Json;

namespace PhotosApp.Models
{
    public class AgileengineAuthParams
    {
        [JsonProperty("apiKey")]
        public string ApiKey { get; set; }
    }
}

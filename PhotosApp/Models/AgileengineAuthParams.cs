using Newtonsoft.Json;

namespace PhotosApp.Models
{
    internal class AgileengineAuthParams
    {
        [JsonProperty("apiKey")]
        public string ApiKey { get; set; }
    }
}

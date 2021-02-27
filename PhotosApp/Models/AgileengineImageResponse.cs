using System;
using Newtonsoft.Json;

namespace PhotosApp.Models
{
    internal class AgileengineImageResponse
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public string Camera { get; set; }
        public string Tags { get; set; }
        [JsonProperty("cropped_picture")]
        public Uri CroppedPicture { get; set; }
        [JsonProperty("full_picture")]
        public Uri FullPicture { get; set; }
    }
}

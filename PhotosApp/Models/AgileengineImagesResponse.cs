using System.Collections.Generic;
using Newtonsoft.Json;

namespace PhotosApp.Models
{
    internal class AgileengineImagesResponse
    {
        public IEnumerable<Picture> Pictures { get; set; }
        public int Page { get; set; }
        public int PageCount { get; set; }
        public bool HasMore { get; set; }
    }

    internal class Picture
    {
        public string Id { get; set; }
        [JsonProperty("cropped_picture")]
        public string CroppedPicture { get; set; }
    }
}

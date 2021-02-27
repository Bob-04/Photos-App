using System.Collections.Generic;

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
        public string cropped_picture { get; set; }
    }
}

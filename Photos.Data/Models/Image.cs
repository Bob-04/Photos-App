using System;
using System.ComponentModel.DataAnnotations;

namespace Photos.Data.Models
{
    public class Image
    {
        [Key]
        public string Id { get; set; }
        public string Author { get; set; }
        public string Camera { get; set; }
        public string Tags { get; set; }
        public Uri CroppedPicture { get; set; }
        public Uri FullPicture { get; set; }
    }
}

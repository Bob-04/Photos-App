using System.Threading.Tasks;

namespace PhotosApp.Services
{
    internal interface IPhotosService
    {
        public Task FillPhotosAsync();
    }
}

using System.Threading.Tasks;

namespace PhotosApp.Services
{
    public interface IPhotosService
    {
        public Task<string> FillPhotosAsync();
    }
}

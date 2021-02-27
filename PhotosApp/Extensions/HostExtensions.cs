using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhotosApp.Services;

namespace PhotosApp.Extensions
{
    public static class HostExtensions
    {
        public static IHost FillDatabase(this IHost webHost)
        {
            var serviceScopeFactory = (IServiceScopeFactory) webHost.Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = serviceScopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;
                var photosService = services.GetRequiredService<IPhotosService>();
                photosService.FillPhotosAsync().GetAwaiter().GetResult();
            }

            return webHost;
        }
    }
}

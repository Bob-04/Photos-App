using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Photos.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection serviceCollection,
            string connectionString)
        {
            serviceCollection.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString));

            return serviceCollection;
        }
    }
}

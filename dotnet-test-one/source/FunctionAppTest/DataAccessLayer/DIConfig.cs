using DataAccessLayer.DBContext;
using DataAccessLayer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DataAccessLayer
{
    public static class DataLayerDIConfig
    {
        public static IOptions<ConnectionStrings> ConnectionString;

        public static IServiceCollection AddAPIDataService(this IServiceCollection services)
        {
            services.AddDbContext<ProductCatalogueContext>(options => options.UseSqlServer(ConnectionString.Value.FunctionAppTest));
            services.AddScoped<IProductCatalogDataService, ProductCatalogDataService>();
            return services;
        }
    }
}

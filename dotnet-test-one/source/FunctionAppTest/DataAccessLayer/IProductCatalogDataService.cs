using DataAccessLayer.Models;

namespace DataAccessLayer.Services
{
    public interface IProductCatalogDataService : IDisposable
    {
        Task AddProduct(string productName);

        Task UpdateProduct(string productName, int quantity);

        Task<Product?> GetProduct(string productName);

        Task RemoveProduct(string productName);
    }
}

using DataAccessLayer.DBContext;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DataAccessLayer.Services
{
    public class ProductCatalogDataService : IProductCatalogDataService
    {
        private readonly ProductCatalogueContext _dbContext;
        public ProductCatalogDataService(IOptions<ConnectionStrings> connectionString, ProductCatalogueContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ProductCatalogDataService()
        {

        }

        public async Task AddProduct(string productName)
        {
            _dbContext.Products.Add(new Models.Product { ProductName = productName, CreatedBy = "dummy user", CreatedOn = DateTime.Now, Item = new Item() { CreatedBy = "dummy user", CreatedOn = DateTime.Now, Quantity = 1 } });

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProduct(string productName, int quantity)
        {
            var product = await _dbContext.Products.Where(x => x.ProductName == productName).Include(x => x.Item).SingleOrDefaultAsync();

            if (product is not null)
            {
                product.Item.Quantity = quantity;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Product?> GetProduct(string productName)
        {
            var product = await _dbContext.Products.Where(x => x.ProductName == productName).Include(x => x.Item).SingleOrDefaultAsync();
            return product;
        }

        public async Task RemoveProduct(string productName)
        {
            var product = await _dbContext.Products.Where(x => x.ProductName == productName).SingleOrDefaultAsync();

            _dbContext.Items.Remove(new Item() { ProductId = product.Id });

            _dbContext.Products.Remove(product);

            await _dbContext.SaveChangesAsync();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MyGPhCPortalDataService() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }


        #endregion
    }
}

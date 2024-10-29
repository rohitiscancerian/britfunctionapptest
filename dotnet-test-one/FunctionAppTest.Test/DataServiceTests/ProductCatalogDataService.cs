using DataAccessLayer;
using DataAccessLayer.DBContext;
using DataAccessLayer.Models;
using DataAccessLayer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Moq.EntityFrameworkCore;
using XUnit = Xunit;
namespace FunctionAppTest.Test.DataServiceTests
{

    public class ProductCatalogDataServiceTests
    {
        [XUnit.Fact]
        public async Task AddProduct_ShouldAddNewProductAndItem()
        {
            // Arrange
            var mockDbContext = new Mock<ProductCatalogueContext>();

            var mockDbSetProducts = GetQueryableMockDbSet(new List<Product>());
            var mockDbSetItems = GetQueryableMockDbSet(new List<Item>());


            mockDbContext.Setup(c => c.Set<Product>()).Returns(mockDbSetProducts.Object);

            mockDbContext.Setup(x => x.Products).Returns(mockDbSetProducts.Object);
            mockDbContext.Setup(x => x.Items).Returns(mockDbSetItems.Object);



            mockDbContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);
            var connectionStrings = Options.Create(new ConnectionStrings());
            var service = new ProductCatalogDataService(connectionStrings, mockDbContext.Object);

            string productName = "Test Product";

            // Act
            await service.AddProduct(productName);

            // Assert
            mockDbSetProducts.Verify(x => x.Add(It.IsAny<Product>()), Times.Once);
            mockDbContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [XUnit.Fact]
        public async Task UpdateProduct_ShouldUpdateQuantityOfExistingProductAndItem()
        {
            // Arrange
            var mockDbSetProducts = new Mock<DbSet<Product>>();
            var mockDbSetItems = new Mock<DbSet<Item>>();

            var products = new List<Product>
            {
                new Product { Id = 1, ProductName = "Product A", Item = new Item() { Id = 1, ProductId = 1, Quantity = 5 }},
                new Product { Id = 2, ProductName = "Product B", Item = new Item() { Id = 2, ProductId = 2, Quantity = 5 }},
                new Product { Id = 3, ProductName = "Product C" , Item = new Item() { Id = 2, ProductId = 3, Quantity = 5 }}
            };

            var mockDbContext = new Mock<ProductCatalogueContext>();

            mockDbContext.Setup(x => x.Products).ReturnsDbSet(products);

            mockDbContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);
            var connectionStrings = Options.Create(new ConnectionStrings());
            var service = new ProductCatalogDataService(connectionStrings, mockDbContext.Object);

            string productName = "Product A";

            // Act
            await service.UpdateProduct(productName, 4);

            // Assert            
            mockDbContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [XUnit.Fact]
        public async Task RemoveProduct_ShouldRemoveExistingProduct()
        {
            // Arrange
            var mockDbSetProducts = new Mock<DbSet<Product>>();
            var mockDbSetItems = new Mock<DbSet<Item>>();

            var items = new List<Item>()
            {
                new Item() { Id = 1, ProductId = 1, Quantity = 5 },
                new Item() { Id = 2, ProductId = 2, Quantity = 5 },
                new Item() { Id = 2, ProductId = 3, Quantity = 5 }
            };

            var products = new List<Product>
            {
                new Product { Id = 1, ProductName = "Product A", Item = new Item() { Id = 1, ProductId = 1, Quantity = 5 }},
                new Product { Id = 2, ProductName = "Product B", Item = new Item() { Id = 2, ProductId = 2, Quantity = 5 }},
                new Product { Id = 3, ProductName = "Product C" , Item = new Item() { Id = 2, ProductId = 3, Quantity = 5 }}
            };

            var mockDbContext = new Mock<ProductCatalogueContext>();

            mockDbContext.Setup(x => x.Products).ReturnsDbSet(products);

            mockDbContext.Setup(x => x.Items).ReturnsDbSet(items);

            mockDbContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);
            var connectionStrings = Options.Create(new ConnectionStrings());
            var service = new ProductCatalogDataService(connectionStrings, mockDbContext.Object);

            string productName = "Product A";

            // Act
            await service.RemoveProduct(productName);

            // Assert            
            mockDbContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [XUnit.Fact]
        public async Task GetProduct_ShouldReturnExistingProduct()
        {
            // Arrange
            var mockDbSetProducts = new Mock<DbSet<Product>>();
            var mockDbSetItems = new Mock<DbSet<Item>>();

            var items = new List<Item>()
            {
                new Item() { Id = 1, ProductId = 1, Quantity = 5 },
                new Item() { Id = 2, ProductId = 2, Quantity = 5 },
                new Item() { Id = 2, ProductId = 3, Quantity = 5 }
            };

            var products = new List<Product>
            {
                new Product { Id = 1, ProductName = "Product A", Item = new Item() { Id = 1, ProductId = 1, Quantity = 5 }},
                new Product { Id = 2, ProductName = "Product B", Item = new Item() { Id = 2, ProductId = 2, Quantity = 5 }},
                new Product { Id = 3, ProductName = "Product C" , Item = new Item() { Id = 2, ProductId = 3, Quantity = 5 }}
            };

            var mockDbContext = new Mock<ProductCatalogueContext>();

            mockDbContext.Setup(x => x.Products).ReturnsDbSet(products);

            mockDbContext.Setup(x => x.Items).ReturnsDbSet(items);

            mockDbContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);
            var connectionStrings = Options.Create(new ConnectionStrings());
            var service = new ProductCatalogDataService(connectionStrings, mockDbContext.Object);

            string productName = "Product A";

            // Act
            var returnedProduct = await service.GetProduct(productName);

            // Assert            
            XUnit.Assert.Equal(returnedProduct?.ProductName, productName);
        }

        private static Mock<DbSet<T>> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));
            return dbSet;
        }
    }
}
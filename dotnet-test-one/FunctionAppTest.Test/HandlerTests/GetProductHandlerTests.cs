using DataAccessLayer.Models;
using DataAccessLayer.Services;
using FunctionAppTest.ApiModels.Queries;
using FunctionAppTest.Handlers;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace FunctionAppTest.Test.HandlerTests
{
    public class GetProductHandlerTests
    {
        [Fact]
        public async Task GetProduct_ExistingProduct_ReturnsProductResponse()
        {
            // Arrange
            var mockProductCatalogDataService = new Mock<IProductCatalogDataService>();
            var mockProduct = new Product { Id = 1, ProductName = "Test Product", Item = new Item { Quantity = 10 } };
            mockProductCatalogDataService.Setup(s => s.GetProduct(It.IsAny<string>()))
                .ReturnsAsync(mockProduct);

            var handler = new GetProductHandler(mockProductCatalogDataService.Object);
            var request = new GetProductRequest { ProductName = "Test Product" };

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(mockProduct.Id, response.ProductId);
            Assert.Equal(mockProduct.ProductName, response.ProductName);
            Assert.Equal(mockProduct.Item.Quantity, response.Quantity);
        }

        [Fact]
        public async Task GetProduct_NonexistentProduct_ReturnsNullResponse()
        {
            // Arrange
            var mockProductCatalogDataService = new Mock<IProductCatalogDataService>();
            mockProductCatalogDataService.Setup(s => s.GetProduct(It.IsAny<string>()))
                .ReturnsAsync((Product)null);

            var handler = new GetProductHandler(mockProductCatalogDataService.Object);
            var request = new GetProductRequest { ProductName = "Nonexistent Product" };

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public async Task GetProduct_DataServiceThrowsException_ThrowsException()
        {
            // Arrange
            var mockProductCatalogDataService = new Mock<IProductCatalogDataService>();
            mockProductCatalogDataService.Setup(s => s.GetProduct(It.IsAny<string>()))
                .ThrowsAsync(new Exception("Data service error"));

            var handler = new GetProductHandler(mockProductCatalogDataService.Object);
            var request = new GetProductRequest { ProductName = "Any Product" };

            // Act & Assert
            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() =>
            {
                return handler.Handle(request, CancellationToken.None);
            });

            Assert.Equal("Data service error", exception.Message);
        }
    }
}

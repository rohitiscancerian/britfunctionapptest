using DataAccessLayer.Services;
using FunctionAppTest.ApiModels.Commands;
using FunctionAppTest.Handlers;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace FunctionAppTest.Test.HandlerTests
{
    public class UpdateProductHandlerTests
    {
        [Fact]
        public async Task UpdateProduct_SuccessfulUpdate_ReturnsSuccessResponse()
        {
            // Arrange
            var mockProductCatalogDataService = new Mock<IProductCatalogDataService>();
            mockProductCatalogDataService.Setup(s => s.UpdateProduct(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            var handler = new UpdateProductHandler(mockProductCatalogDataService.Object);
            var request = new UpdateProductRequest { ProductName = "Test Product", Quantity = 20 };

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Success);
            mockProductCatalogDataService.Verify(s => s.UpdateProduct(request.ProductName, request.Quantity), Times.Once);
        }

        [Fact]
        public async Task UpdateProduct_DataServiceThrowsException_ThrowsException()
        {
            // Arrange
            var mockProductCatalogDataService = new Mock<IProductCatalogDataService>();
            mockProductCatalogDataService.Setup(s => s.UpdateProduct(It.IsAny<string>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("Data service error"));

            var handler = new UpdateProductHandler(mockProductCatalogDataService.Object);
            var request = new UpdateProductRequest { ProductName = "Test Product", Quantity = 20 };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(request, CancellationToken.None));
        }
    }
}

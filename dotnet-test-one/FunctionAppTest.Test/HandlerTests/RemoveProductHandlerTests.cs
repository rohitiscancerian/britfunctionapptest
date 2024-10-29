using DataAccessLayer.Services;
using FunctionAppTest.ApiModels.Commands;
using FunctionAppTest.Handlers;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace FunctionAppTest.Test.HandlerTests
{
    public class RemoveProductHandlerTests
    {
        private readonly Mock<IProductCatalogDataService> _mockProductCatalogDataService;
        private readonly RemoveProductHandler _handler;

        public RemoveProductHandlerTests()
        {
            _mockProductCatalogDataService = new Mock<IProductCatalogDataService>();
            _handler = new RemoveProductHandler(_mockProductCatalogDataService.Object);
        }

        [Fact]
        public async Task Handle_ShouldCallRemoveProduct_WhenRequestIsValid()
        {
            // Arrange
            var request = new RemoveProductRequest { ProductName = "TestProduct" };

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            _mockProductCatalogDataService.Verify(x => x.RemoveProduct(request.ProductName), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenProductRemovedSuccessfully()
        {
            // Arrange
            var request = new RemoveProductRequest { ProductName = "TestProduct" };

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(response.Success);
        }

        [Fact]
        public async Task
     Handle_ShouldReturnFailure_WhenProductRemovalFails()
        {
            // Arrange
            var request = new RemoveProductRequest { ProductName = "TestProduct" };
            _mockProductCatalogDataService.Setup(x => x.RemoveProduct(request.ProductName)).ThrowsAsync(new Exception("Error removing product"));

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(request, CancellationToken.None));
        }
    }
}

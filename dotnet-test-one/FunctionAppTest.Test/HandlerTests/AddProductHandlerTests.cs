using DataAccessLayer.Services;
using FunctionAppTest.ApiModels.Commands;
using FunctionAppTest.Handlers;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace FunctionAppTest.Test.HandlerTests
{
    public class AddProductHandlerTests
    {
        private readonly Mock<IProductCatalogDataService> _mockProductCatalogDataService;
        private readonly AddProductHandler _handler;

        public AddProductHandlerTests()
        {
            _mockProductCatalogDataService = new Mock<IProductCatalogDataService>();
            _handler = new AddProductHandler(_mockProductCatalogDataService.Object);
        }

        [Fact]
        public async Task Handle_ShouldCallAddProduct_WhenRequestIsValid()
        {
            // Arrange
            var request = new AddProductRequest { ProductName = "Test Product" };

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            _mockProductCatalogDataService.Verify(x => x.AddProduct(request.ProductName), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResponse_WhenAddProductSucceeds()
        {
            // Arrange
            var request = new AddProductRequest { ProductName = "Test Product" };

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(response.Success);
        }

        [Fact]
        public async Task
     Handle_ShouldReturnFailureResponse_WhenAddProductFails()
        {
            // Arrange
            var request = new AddProductRequest { ProductName = "Test Product" };
            _mockProductCatalogDataService.Setup(x => x.AddProduct(It.IsAny<string>())).ThrowsAsync(new Exception("Add product failed"));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() =>
            {
                return _handler.Handle(request, CancellationToken.None);
            });

            // Assert          
            Assert.Equal("Add product failed", exception.Message);
        }
    }
}

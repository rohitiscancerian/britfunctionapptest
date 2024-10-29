using DataAccessLayer.Services;
using FunctionAppTest.ApiModels.Commands;

using MediatR;

namespace FunctionAppTest.Handlers
{
    public class AddProductHandler : IRequestHandler<AddProductRequest, AddProductResponse>
    {
        private readonly IProductCatalogDataService _productCatalogDataService;

        public AddProductHandler(IProductCatalogDataService productCatalogDataService)
        {
            _productCatalogDataService = productCatalogDataService;
        }

        public async Task<AddProductResponse> Handle(AddProductRequest request, CancellationToken cancellationToken)
        {
            await _productCatalogDataService.AddProduct(request.ProductName);

            return await Task.FromResult(new AddProductResponse() { Success = true });
        }
    }
}

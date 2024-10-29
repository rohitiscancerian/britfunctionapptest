using DataAccessLayer.Services;
using FunctionAppTest.ApiModels.Commands;

using MediatR;

namespace FunctionAppTest.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, UpdateProductResponse>
    {
        private readonly IProductCatalogDataService _productCatalogDataService;

        public UpdateProductHandler(IProductCatalogDataService productCatalogDataService)
        {
            _productCatalogDataService = productCatalogDataService;
        }

        public async Task<UpdateProductResponse> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            await _productCatalogDataService.UpdateProduct(request.ProductName, request.Quantity);

            return await Task.FromResult(new UpdateProductResponse() { Success = true });
        }
    }
}

using DataAccessLayer.Services;
using FunctionAppTest.ApiModels.Commands;

using MediatR;

namespace FunctionAppTest.Handlers
{
    public class RemoveProductHandler : IRequestHandler<RemoveProductRequest, RemoveProductResponse>
    {
        private readonly IProductCatalogDataService _productCatalogDataService;

        public RemoveProductHandler(IProductCatalogDataService productCatalogDataService)
        {
            _productCatalogDataService = productCatalogDataService;
        }

        public async Task<RemoveProductResponse> Handle(RemoveProductRequest request, CancellationToken cancellationToken)
        {
            await _productCatalogDataService.RemoveProduct(request.ProductName);

            return await Task.FromResult(new RemoveProductResponse() { Success = true });
        }
    }
}

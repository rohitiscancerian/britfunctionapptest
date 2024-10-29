using DataAccessLayer.Services;
using FunctionAppTest.ApiModels.Queries;

using MediatR;

namespace FunctionAppTest.Handlers
{
    public class GetProductHandler : IRequestHandler<GetProductRequest, GetProductResponse?>
    {
        private readonly IProductCatalogDataService _productCatalogDataService;

        public GetProductHandler(IProductCatalogDataService productCatalogDataService)
        {
            _productCatalogDataService = productCatalogDataService;
        }

        public async Task<GetProductResponse?> Handle(GetProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _productCatalogDataService.GetProduct(request.ProductName);
            if (product is not null)
            {
                return await Task.FromResult(new GetProductResponse() { ProductId = product.Id, ProductName = product.ProductName, Quantity = product.Item.Quantity });
            }
            else
            {
                return await Task.FromResult((GetProductResponse)null);
            }
        }
    }
}

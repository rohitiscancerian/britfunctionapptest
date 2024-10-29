using MediatR;

namespace FunctionAppTest.ApiModels.Queries
{
    public class GetProductRequest : IRequest<GetProductResponse>
    {
        public string ProductName { get; set; }

    }
}

using MediatR;

namespace FunctionAppTest.ApiModels.Commands
{
    public class RemoveProductRequest : IRequest<RemoveProductResponse>
    {
        public string ProductName { get; set; }
    }
}

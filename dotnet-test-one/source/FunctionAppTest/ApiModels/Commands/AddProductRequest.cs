using MediatR;

namespace FunctionAppTest.ApiModels.Commands
{
    public class AddProductRequest : IRequest<AddProductResponse>
    {
        public string ProductName { get; set; }
    }
}

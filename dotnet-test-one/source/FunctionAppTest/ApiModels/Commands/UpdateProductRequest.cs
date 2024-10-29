using MediatR;

namespace FunctionAppTest.ApiModels.Commands
{
    public class UpdateProductRequest : IRequest<UpdateProductResponse>
    {
        public string ProductName { get; set; }

        public int Quantity { get; set; }
    }
}

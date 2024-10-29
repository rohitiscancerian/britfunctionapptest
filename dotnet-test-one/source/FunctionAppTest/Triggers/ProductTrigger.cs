using FunctionAppTest.ApiModels.Commands;
using FunctionAppTest.ApiModels.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

namespace FunctionAppTest.Triggers
{
    public class ProductTrigger
    {
        private readonly ILogger<ProductTrigger> _logger;
        private readonly IMediator _mediator;
        public ProductTrigger(ILogger<ProductTrigger> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Function("GetProduct")]
        public async Task<IActionResult> GetProduct([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            var productName = req.Query["productName"];
            if (!string.IsNullOrEmpty(productName))
            {
                var getProductRequest = new GetProductRequest() { ProductName = productName };
                return new OkObjectResult(await _mediator.Send(getProductRequest));
            }
            else
            {
                return await Task.FromResult(new BadRequestResult());
            }
        }

        [Function("AddProduct")]
        public async Task<IActionResult> AddProduct([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req, [FromBody] AddProductRequest addProductRequest)
        {

            var response = await _mediator.Send(addProductRequest);
            return new OkObjectResult(response);
        }

        [Function("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequest req, [FromBody] UpdateProductRequest updateProductRequest)
        {
            var response = await _mediator.Send(updateProductRequest);
            return new OkObjectResult(response);
        }

        [Function("RemoveProduct")]
        public async Task<IActionResult> RemoveProduct([HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequest req, [FromBody] RemoveProductRequest removeProductRequest)
        {
            var response = await _mediator.Send(removeProductRequest);
            return new OkObjectResult(response);
        }
    }
}

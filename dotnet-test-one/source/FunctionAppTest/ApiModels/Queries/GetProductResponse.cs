﻿namespace FunctionAppTest.ApiModels.Queries
{
    public class GetProductResponse : ApiResponse
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Abstractions.Hubs;
using ECommerceAPI.Application.Repositories.Product_App;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductWriteRespository _productWriteRespository;
        private readonly IProductHubService _productHubService;

		public CreateProductCommandHandler(IProductWriteRespository productWriteRespository, IProductHubService productHubService)
		{
			_productWriteRespository = productWriteRespository;
			_productHubService = productHubService;
		}

		public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriteRespository.AddAsync(new()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
            });
            await _productWriteRespository.SaveAsync();

            await _productHubService.ProductAddedMessageAsync($"Product that id is {request.Name} added ");

            return new();
        }
    }
}

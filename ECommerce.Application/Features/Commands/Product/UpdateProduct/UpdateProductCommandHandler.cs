﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Repositories.Product_App;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Application.Features.Commands.Product.UpdateProduct
{
	public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
	{
		private readonly IProductWriteRespository _productWriteRespository;
		private readonly IProductReadRepository _productReadRespository;
		private readonly ILogger<UpdateProductCommandHandler> _logger;

		public UpdateProductCommandHandler(IProductWriteRespository productWriteRespository, IProductReadRepository productReadRespository)
		{
			_productWriteRespository = productWriteRespository;
			_productReadRespository = productReadRespository;
		}

		public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
		{

			var product = await _productReadRespository.GetByIdAsync(request.Id);
			product.Stock = request.Stock;
			product.Name = request.Name;
			product.Price = request.Price;
			await _productWriteRespository.SaveAsync();
			_logger.LogInformation("Product updated");

			return new();
		}
	}
}

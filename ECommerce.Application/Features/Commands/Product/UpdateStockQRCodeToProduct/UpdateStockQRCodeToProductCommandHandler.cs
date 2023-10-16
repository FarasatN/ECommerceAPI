using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Product.UpdateStockQRCodeToProduct
{
	public class UpdateStockQRCodeToProductCommandHandler : IRequestHandler<UpdateStockQRCodeToProductCommandRequest, UpdateStockQRCodeToProductCommandResponse>
	{
		private readonly IProductService _productService;

		public UpdateStockQRCodeToProductCommandHandler(IProductService productService)
		{
			_productService = productService;
		}

		public async Task<UpdateStockQRCodeToProductCommandResponse> Handle(UpdateStockQRCodeToProductCommandRequest request, CancellationToken cancellationToken)
		{
			await _productService.StockUpdateToProductAsync(request.ProductId, request.Stock);
			return new();
		}
	}
}

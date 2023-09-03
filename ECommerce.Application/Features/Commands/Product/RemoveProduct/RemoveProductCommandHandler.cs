using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Repositories.Product_App;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Product.RemoveProduct
{
	public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequest, RemoveProductCommandResponse>
	{
		private readonly IProductWriteRespository _productWriteRespository;

		public RemoveProductCommandHandler(IProductWriteRespository productWriteRespository)
		{
			_productWriteRespository = productWriteRespository;
		}

		public async Task<RemoveProductCommandResponse> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
		{
			await _productWriteRespository.RemoveAsync(request.Id);
			await _productWriteRespository.SaveAsync();

			return new();
		}
	}
}

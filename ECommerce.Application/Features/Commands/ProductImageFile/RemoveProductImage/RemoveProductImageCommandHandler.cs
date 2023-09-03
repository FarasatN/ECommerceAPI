using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Repositories.Product_App;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace ECommerceAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage
{
	public class RemoveProductImageCommandHandler : IRequestHandler<RemoveProductImageCommandRequest, RemoveProductImageCommandResponse>
	{
		private readonly IProductReadRepository _productReadRespository;
		private readonly IProductWriteRespository _productWriteRespository;


		public RemoveProductImageCommandHandler(IProductReadRepository productReadRespository, IProductWriteRespository productWriteRespository)
		{
			_productReadRespository = productReadRespository;
			_productWriteRespository = productWriteRespository;
		}

		public async Task<RemoveProductImageCommandResponse> Handle(RemoveProductImageCommandRequest request, CancellationToken cancellationToken)
		{
			var product = await _productReadRespository.Table.Include(p => p.ProductImageFiles)
				.FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
			var productImageFile = product?.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));
			if (productImageFile != null)
			{
				product?.ProductImageFiles.Remove(productImageFile);
			}
			await _productWriteRespository.SaveAsync();

			return new();
		}
	}
}

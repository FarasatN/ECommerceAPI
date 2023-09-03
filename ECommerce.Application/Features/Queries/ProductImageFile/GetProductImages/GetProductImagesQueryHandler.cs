using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Repositories.Product_App;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerceAPI.Application.Features.Queries.ProductImageFile.GetProductImages
{
	public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, List<GetProductImagesQueryResponse>>
	{
		private readonly IProductReadRepository _productReadRespository;
		private readonly IConfiguration _configuration;


		public GetProductImagesQueryHandler(IProductReadRepository productReadRespository, IConfiguration configuration)
		{
			_productReadRespository = productReadRespository;
			_configuration = configuration;
		}

		public async Task<List<GetProductImagesQueryResponse>> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
		{
			var product = await _productReadRespository.Table.Include(p => p.ProductImageFiles)
				.FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
			
			if (product != null)
			{
				return product.ProductImageFiles.Select(p => new GetProductImagesQueryResponse
				{
					Path = $"{_configuration["BaseStorageUrl:Local"]}/{p.Path}",
					FileName = p.FileName,
					Id = p.Id
				}).ToList();
			}
			else
			{
				return null;
			}

		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Repositories.Product_App;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Product.GetByIdProduct
{
	public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
	{
		private readonly IProductReadRepository _productReadRespository;
		public GetByIdProductQueryHandler(IProductReadRepository productReadRespository)
		{
			_productReadRespository = productReadRespository;
		}

		public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
		{
			var product = await _productReadRespository.GetByIdAsync(request.Id, false);

			return new()
			{
				Name = product.Name,
				Price = product.Price,
				Stock = product.Stock,
			};
		}
	}
}

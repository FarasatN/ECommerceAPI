using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Features.Commands.Product.UpdateProduct;
using ECommerceAPI.Application.Repositories.Product_App;
using ECommerceAPI.Application.RequestParameters;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Application.Features.Queries.Product.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, GetAllProductsQueryResponse>
    {
        private readonly IProductReadRepository _productReadRespository;
		private readonly ILogger<GetAllProductsQueryHandler> _logger;


		public GetAllProductsQueryHandler(IProductReadRepository productReadRespository, ILogger<GetAllProductsQueryHandler> logger)
		{
			_productReadRespository = productReadRespository;
			_logger = logger;
		}

		public async Task<GetAllProductsQueryResponse> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            
            var totalCount = _productReadRespository.GetAll(false).Count();
            var products = _productReadRespository.GetAll(false)
                .Skip(request.Size).Take(request.Page * request.Size)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Stock,
                    p.Price,
                    p.CreatedDate,
                    p.UpdatedDate
                }).ToList();


            return new()
            {
                Products = products,
                TotalCount = totalCount
            };
        }
    }
}

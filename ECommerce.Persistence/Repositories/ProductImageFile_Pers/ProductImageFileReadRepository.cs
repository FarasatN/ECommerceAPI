using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Repositories.ProductImageFile_App;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.ProductImageFile_Pers
{
	public class ProductImageFileReadRepository : ReadRepository<ProductImageFile>, IProductImageFileReadRepository
	{
		public ProductImageFileReadRepository(ECommerceAPIDbContext context) : base(context)
		{
		}
	}
}

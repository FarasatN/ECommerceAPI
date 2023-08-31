using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.Product_App;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Product_Pers
{
	public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
	{
		public ProductReadRepository(ECommerceAPIDbContext context) : base(context)
		{
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.Customer_App;
using ECommerceAPI.Application.Repositories.Order_App;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Order_Pers
{
	public class OrderReadRepository : ReadRepository<Order>, IOrderReadRepository
	{
		public OrderReadRepository(ECommerceAPIDbContext context) : base(context)
		{
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Repositories.Basket_Item_App;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.BasketItem_Pers
{
	public class BasketItemReadRepository : ReadRepository<BasketItem>, IBasketItemReadRepository
	{
		public BasketItemReadRepository(ECommerceAPIDbContext context) : base(context)
		{
		}
	}
}

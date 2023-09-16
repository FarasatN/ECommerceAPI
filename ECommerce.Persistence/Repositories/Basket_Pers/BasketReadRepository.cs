﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Repositories.Basket_App;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Basket_Pers
{
	public class BasketReadRepository : ReadRepository<Basket>, IBasketReadRepository
	{
		public BasketReadRepository(ECommerceAPIDbContext context) : base(context)
		{
		}
	}
}

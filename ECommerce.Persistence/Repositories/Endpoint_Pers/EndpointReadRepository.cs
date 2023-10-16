﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Repositories.Endpoint_App;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Endpoint_Pers
{
	public class EndpointReadRepository : ReadRepository<Endpoint>, IEndpointReadRepository
	{
		public EndpointReadRepository(ECommerceAPIDbContext context) : base(context)
		{
		}
	}
}

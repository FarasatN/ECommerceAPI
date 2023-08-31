using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.Customer_App;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Customer_Pers
{
	public class CustomerWriteRepository : WriteRepository<Customer>, ICustomerWriteRepository
	{
		public CustomerWriteRepository(ECommerceAPIDbContext context) : base(context)
		{
		}


	}
}

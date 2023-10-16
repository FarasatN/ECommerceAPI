using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Repositories.Menu_App;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.Menu_Pers
{
	public class MenuReadRepository : ReadRepository<Menu>, IMenuReadRepository
	{
		public MenuReadRepository(ECommerceAPIDbContext context) : base(context)
		{
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Repositories.File_App;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.File_Pers
{
	public class FileReadRepository : ReadRepository<ECommerceAPI.Domain.Entities.File>, IFileReadRepository
	{
		public FileReadRepository(ECommerceAPIDbContext context) : base(context)
		{
		}
	}
}

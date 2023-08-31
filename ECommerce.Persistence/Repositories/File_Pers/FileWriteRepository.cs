using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Repositories.File_App;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.File_Pers
{
	public class FileWriteRepository : WriteRepository<ECommerceAPI.Domain.Entities.File>, IFileWriteRepository
	{
		public FileWriteRepository(ECommerceAPIDbContext context) : base(context)
		{
		}
	}
}

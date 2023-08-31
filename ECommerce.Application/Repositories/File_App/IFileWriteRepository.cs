using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Application.Repositories.File_App
{
	public interface IFileWriteRepository: IWriteRepository<ECommerceAPI.Domain.Entities.File>
	{
	}
}

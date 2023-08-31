using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Repositories.InvoiceFile_App;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories.InvoiceFile_Pers
{
	public class InvoiceFileWriteRepository : WriteRepository<InvoiceFile>, IInvoiceFileWriteRepository
	{
		public InvoiceFileWriteRepository(ECommerceAPIDbContext context) : base(context)
		{
		}
	}
}

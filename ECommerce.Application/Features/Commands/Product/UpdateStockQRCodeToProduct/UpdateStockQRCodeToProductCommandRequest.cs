using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Product.UpdateStockQRCodeToProduct
{
	public class UpdateStockQRCodeToProductCommandRequest: IRequest<UpdateStockQRCodeToProductCommandResponse>
	{
        public string ProductId { get; set; }
        public int Stock { get; set; }
	}
}

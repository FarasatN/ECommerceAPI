using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.DTOs.Order;

namespace ECommerceAPI.Application.Abstractions.Services
{
	public interface IOrderService
	{
		Task<bool> CreateOrderAsync(CreateOrder createOrder);
		Task<ListOrder> GetAllOrdersAsync(int page, int size);
		Task<SingleOrder> GetOrderByIdAsync(string id);
		Task CompleteOrderAsynC(string id);
	}
}

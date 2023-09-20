using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Abstractions.Hubs;
using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Order.CreateOrder
{
	public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
	{
		private readonly IOrderService _orderService;
		private readonly IBasketService _basketService;
		private readonly IOrderHubService _orderHubService;

		public CreateOrderCommandHandler(IOrderService orderService, IBasketService basketService, IOrderHubService orderHubService)
		{
			_orderService = orderService;
			_basketService = basketService;
			_orderHubService = orderHubService;
		}

		public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
		{
			var response = await _orderService.CreateOrderAsync(new()
			{
				Address = request.Address,
				Description = request.Description,
				BasketId = (await  _basketService.GetUserActiveBasket())?.Id.ToString()
			});

			if (response)
			{
				await _orderHubService.OrderAddedMessageAsync("New order added");
			}
			

			return new()
			{
				Succeeded = response
			};
		}
	}
}

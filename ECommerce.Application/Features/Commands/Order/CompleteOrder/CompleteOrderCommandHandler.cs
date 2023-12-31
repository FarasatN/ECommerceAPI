﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.Order;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Order.CompleteOrder
{
	public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommandRequest, CompleteOrderCommandResponse>
	{
		private readonly IOrderService _orderService;
		private readonly IMailService _mailService;

		public CompleteOrderCommandHandler(IOrderService orderService, IMailService mailService)
		{
			_orderService = orderService;
			_mailService = mailService;
		}

		public async Task<CompleteOrderCommandResponse> Handle(CompleteOrderCommandRequest request, CancellationToken cancellationToken)
		{
			(bool succeeded,CompletedOrderDTO dto) = await _orderService.CompleteOrderAsynC(request.Id);
			if(succeeded)
			{
				await _mailService.SendCompletedOrderMailAsync(dto.Email, dto.OrderCode, dto.OrderDate, dto.UserName);
			}
			return new();
		}
	}
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Order.CompleteOrder
{
	public class CompleteOrderCommandRequest: IRequest<CompleteOrderCommandResponse>
	{
		public string Id { get; set; }
	}
}

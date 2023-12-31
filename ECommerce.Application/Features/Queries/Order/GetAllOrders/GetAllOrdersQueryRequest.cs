﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Order.GetAllOrders
{
    public class GetAllOrdersQueryRequest : IRequest<GetAllOrdersQueryResponse>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 5;
    }
}

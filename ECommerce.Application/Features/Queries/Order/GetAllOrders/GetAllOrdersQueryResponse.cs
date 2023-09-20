﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.Order.GetAllOrders
{
    public class GetAllOrdersQueryResponse
    {
        //public string OrderCode { get; set; }
        //public string UserName { get; set; }
        //public float TotalPrice { get; set; }
        //public DateTime CreatedDate { get; set; }

        public int TotalOrderCount { get; set; }
        public object Orders { get; set; }
    }
}

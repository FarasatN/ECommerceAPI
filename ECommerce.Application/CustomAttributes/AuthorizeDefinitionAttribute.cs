﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Enums;

namespace ECommerceAPI.Application.CustomAttributes
{
	public class AuthorizeDefinitionAttribute: Attribute
	{
		public string Menu { get; set; }
		public string Definition { get; set; }
		public ActionType ActionType { get; set; }
	}
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Role.CreateRole
{
	public class CreateRoleCommandRequest: IRequest<CreateRoleCommandResponse>
	{
        public string Name { get; set; }
    }
}

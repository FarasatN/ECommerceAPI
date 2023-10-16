using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Role.DeleteRole
{
	public class DeleteRoleCommandRequest: IRequest<DeleteRoleCommandResponse>
	{
        public string Id { get; set; }
    }
}

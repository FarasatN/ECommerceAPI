using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Role.GetRoles
{
	public class GetRolesQueryHandler : IRequestHandler<GetRolesQueryRequest, GetRolesQueryResponse>
	{
		private readonly IRoleService _roleService;

		public GetRolesQueryHandler(IRoleService roleService)
		{
			_roleService = roleService;
		}

		public async Task<GetRolesQueryResponse> Handle(GetRolesQueryRequest request, CancellationToken cancellationToken)
		{
			var (datas,count) = _roleService.GetAllRoles(request.Page,request.Size);
			return new()
			{
				Datas = datas,
				TotalRoleCount = count,
			};
		}
	}
}

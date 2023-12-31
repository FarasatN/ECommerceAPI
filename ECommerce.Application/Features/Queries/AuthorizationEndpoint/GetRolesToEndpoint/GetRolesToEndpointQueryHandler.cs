﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.AuthorizationEndpoint.GetRolesToEndpoints
{
	public class GetRolesToEndpointQueryHandler : IRequestHandler<GetRolesToEndpointQueryRequest, GetRolesToEndpointQueryResponse>
	{
		private readonly IAuthorizationEndpointService _authorizationEndpointService;

		public GetRolesToEndpointQueryHandler(IAuthorizationEndpointService authorizationEndpointService)
		{
			_authorizationEndpointService = authorizationEndpointService;
		}

		public async Task<GetRolesToEndpointQueryResponse> Handle(GetRolesToEndpointQueryRequest request, CancellationToken cancellationToken)
		{
			var datas = await _authorizationEndpointService.GetRolesToEndpointAsync(request.Code,request.Menu);
			return new()
			{
				Roles = datas
			};
		}
	}
}

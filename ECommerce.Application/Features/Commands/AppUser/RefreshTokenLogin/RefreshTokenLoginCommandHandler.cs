using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Services.Authentications;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.AppUser.RefreshTokenLogin
{
	public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
	{
		private IAuthService _authService;

		public RefreshTokenLoginCommandHandler(IAuthService authService)
		{
			_authService = authService;
		}

		public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
		{
			var response = await _authService.RefreshTokenLoginAsync(request.RefreshToken);
			return new()
			{
				Message = response.Message,
				Succeeded = response.Succeeded,
				Token = response.Token
			};
		}
	}
}

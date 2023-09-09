using ECommerceAPI.Application.Abstractions.Services.Authentications;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.AppUser.LoginUser
{
	public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
	{
		private readonly IInternalAuthService _internalAuthService;

		public LoginUserCommandHandler(IInternalAuthService internalAuthService)
		{
			_internalAuthService = internalAuthService;
		}

		public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
		{
			var response = await _internalAuthService.LoginAsync(request.UsernameOrEmail, request.Password, 15);
			return new()
			{
				Message = response.Message,
				Succeeded = response.Succeeded,
				Token = response.Token
			};
		}
	}
}

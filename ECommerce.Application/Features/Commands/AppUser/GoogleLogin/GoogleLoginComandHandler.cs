using ECommerceAPI.Application.Abstractions.Services.Authentications;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.AppUser.GoogleLogin
{

	public class GoogleLoginComandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
	{

		private readonly IExternalAuthService _externalAuthService;

		public GoogleLoginComandHandler(IExternalAuthService externalAuthService)
		{
			_externalAuthService = externalAuthService;
		}


		public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
		{

			var response = await _externalAuthService.GoogleLoginAsync(request.IdToken, 900);

			return new()
			{
				Succeeded = true,
				Message = response.Message,
				Token = response.Token,
			};

		}
	}
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Features.Commands.AppUser.LoginUser;
using ECommerceAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerceAPI.Persistence.Services
{
	public class AuthService : IAuthService
	{
		private readonly IConfiguration _configuration;
		private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
		private readonly ITokenHandler _tokenHandler;
		private readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
		private readonly IUserService _userService;


		public AuthService(IConfiguration configuration, UserManager<Domain.Entities.Identity.AppUser> userManager, ITokenHandler tokenHandler, SignInManager<Domain.Entities.Identity.AppUser> signInManager, IUserService userService)
		{
			_configuration = configuration;
			_userManager = userManager;
			_tokenHandler = tokenHandler;
			_signInManager = signInManager;
			_userService = userService;
		}

		public async Task<LoginUserResponse> GoogleLoginAsync(string IdToken, int accessTokenLifeTime)
		{
			var settings = new GoogleJsonWebSignature.ValidationSettings()
			{
				Audience = new List<string> { _configuration["ExternalLoginSettings:Google:Client_ID"] }
			};

			var payload = await GoogleJsonWebSignature.ValidateAsync(IdToken, settings);
			var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
			Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
			bool result = user != null;
			if (user == null)
			{
				user = await _userManager.FindByEmailAsync(payload.Email);
				if (user == null)
				{
					user = new Domain.Entities.Identity.AppUser()
					{
						Id = Guid.NewGuid().ToString(),
						Email = payload.Email,
						UserName = payload.Email,
						NameSurname = payload.Name,
					};
					var identityResult = await _userManager.CreateAsync(user);
					result = identityResult.Succeeded;
				}
			}

			if (result)
			{
				await _userManager.AddLoginAsync(user, info);
			}
			else
			{
				throw new Exception("Invalid external authentication");
			}

			Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
			await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 10);

			return new()
			{
				Succeeded = true,
				Message = "Google login is success",
				Token = token,
			};
		}

		public async Task<LoginUserResponse> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
		{
			Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
			if (user == null)
			{
				user = await _userManager.FindByEmailAsync(usernameOrEmail);
			}

			if (user == null)
			{
				throw new UserNotFoundException();
			}

			SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);


			LoginUserResponse response = new() { Succeeded = result.Succeeded };

			if (result.Succeeded)
			{
				//authentication successfull ..
				//authorization ...
				Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
				await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 10);

				response.Token = token;
				response.Message = "User has successfuly logon";

				return response;
			}
			//else
			//{
			//response.Message = "Error";
			throw new AuthenticationErrorException();
			//}


			//return response;
		}

		public async Task<LoginUserResponse> RefreshTokenLoginAsync(string refreshToken)
		{
			AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
			if(user == null && user?.RefreshTokenEndDate > DateTime.UtcNow)
			{
				Token token = _tokenHandler.CreateAccessToken(15,user);
				await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 15);
				return new()
				{
					Token = token,
					Message = "Token has successfuly refreshed",
					Succeeded = true
				};
			}
			else
			{
				throw new UserNotFoundException();
			}

		}

	}
}

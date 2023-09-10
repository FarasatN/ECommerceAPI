﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Features.Commands.AppUser.CreateUser;
using ECommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Persistence.Services
{
	public class UserService : IUserService
	{

		private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
		public UserService(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<CreateUserResponse> CreateAsync(CreateUserRequest model)
		{
			

			IdentityResult result = await _userManager.CreateAsync(new()
			{
				Id = Guid.NewGuid().ToString(),
				UserName = model.Username,
				Email = model.Email,
				NameSurname = model.NameSurname,
			}, model.Password);

			CreateUserResponse response = new() { Succeeded = result.Succeeded };

			if (result.Succeeded)
			{
				response.Message = "User has successfuly created";
			}
			else
			{
				foreach (var error in result.Errors)
				{
					response.Message += $"{error.Code} - {error.Description}<br>";
				}
			}
			//result.Errors.First()
			//throw new UserCreateFailedException();

			return response;
		}

		public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
		{
			if (user != null)
			{
				user.RefreshToken = refreshToken;
				user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
				await _userManager.UpdateAsync(user);
			}
			else
			{
				throw new UserNotFoundException();
			}
		}
	}
}
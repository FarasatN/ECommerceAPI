using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Features.Commands.AppUser.CreateUser;
using ECommerceAPI.Application.Helpers;
using ECommerceAPI.Application.Repositories.Endpoint_App;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistence.Services
{
	public class UserService : IUserService
	{

		private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
		private readonly IEndpointReadRepository _endpointReadRepository;

		public UserService(UserManager<AppUser> userManager, IEndpointReadRepository endpointReadRepository)
		{
			_userManager = userManager;
			_endpointReadRepository = endpointReadRepository;
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


		public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
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




		public async Task UpdatePasswordAsync(string userId,string resetToken, string newPassword)
		{
			AppUser user = await _userManager.FindByIdAsync(userId);
			if (user != null)
			{
				resetToken = resetToken.UrlDecode();
				IdentityResult result = await _userManager.ResetPasswordAsync(user,resetToken,newPassword);
				if (result.Succeeded)
					await _userManager.UpdateSecurityStampAsync(user);
				else
					throw new PasswordChangeFailedException();
			}
		}

		public async Task<List<ListUser>> GetAllUsersAsync(int page,int size)
		{
			var users = await _userManager.Users
				.Skip(page*size)
				.Take(size).ToListAsync();
			return users.Select(user => new ListUser
			{
				Id = user.Id,
				Email = user.Email,
				NameSurname = user.NameSurname,
				TwoFactorEnabled = user.TwoFactorEnabled,
				UserName = user.UserName 
			}).ToList();
		}


		public int TotalUserCount => _userManager.Users.Count();

		public async Task AssignRoleToUserAsync(string userId, string[] roles)
		{
			AppUser user = await _userManager.FindByIdAsync(userId);
			if (user != null)
			{
				var userRoles = await _userManager.GetRolesAsync(user);
				await _userManager.RemoveFromRolesAsync(user, userRoles);
				await _userManager.AddToRolesAsync(user, roles);
			}
		}

		public async Task<string[]> GetRolesToUserAsync(string userIdOrName)
		{
			AppUser user = await _userManager.FindByIdAsync(userIdOrName);
			if (user == null)
			{
				await _userManager.FindByNameAsync(userIdOrName);
			}
			if (user != null)
			{
				var usersRoles =  await _userManager.GetRolesAsync(user);
				return usersRoles.ToArray();
			}
			return new string[] { };
		}

		public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
		{
			var userRoles = await GetRolesToUserAsync(name);
			if(!userRoles.Any())
			{
				return false;
			}

			Endpoint? endpoint = await _endpointReadRepository.Table
				.Include(e => e.Roles)
				.FirstOrDefaultAsync(e=>e.Code==code);
			if(endpoint == null)
			{
				return false;
			}

			var hasRole = false;
			var endpointRoles = endpoint.Roles.Select(r=>r.Name);

			//foreach (var userRole in userRoles)
			//{
			//	if (!hasRole)
			//	{
			//		foreach (var endpointRole in endpointRoles)
			//		{
			//			if (userRole == endpointRole)
			//			{
			//				hasRole = true;
			//				break;
			//			}
			//		}
			//	}
			//	else
			//	{
			//		break;
			//	}
			//}
			//return hasRole;

			foreach (var userRole in userRoles)
			{
				foreach (var endpointRole in endpointRoles)
					if (userRole == endpointRole)
						return true;
			}
			return false;
		}
	}
}

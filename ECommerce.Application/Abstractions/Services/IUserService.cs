using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Application.Abstractions.Services
{
	public interface IUserService
	{
		Task<CreateUserResponse> CreateAsync(CreateUserRequest model);
		Task UpdateRefreshToken(string refreshToken,AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);

	}
}

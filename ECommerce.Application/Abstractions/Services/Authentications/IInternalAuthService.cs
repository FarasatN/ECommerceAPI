using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.DTOs.User;

namespace ECommerceAPI.Application.Abstractions.Services.Authentications
{
	public interface IInternalAuthService
	{
		Task<LoginUserResponse> LoginAsync(string usernameOrEmail,string password,int accessTokenLifeTime);
		Task<LoginUserResponse> RefreshTokenLoginAsync(string refreshToken);

	}
}

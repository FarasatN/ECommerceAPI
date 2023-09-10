using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Application.Abstractions.Token
{
	public interface ITokenHandler
	{
		DTOs.Token CreateAccessToken(int second, AppUser user);
		string CreateRefreshToken();
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Abstractions.Services.Authentications;
using ECommerceAPI.Application.DTOs.User;

namespace ECommerceAPI.Application.Abstractions.Services
{
	public interface IAuthService: IInternalAuthService, IExternalAuthService
	{
		
	}
}

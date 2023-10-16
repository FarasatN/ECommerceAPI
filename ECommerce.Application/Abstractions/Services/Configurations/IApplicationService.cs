using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.DTOs.Configuration;

namespace ECommerceAPI.Application.Abstractions.Services.Configurations
{
	public interface IApplicationService
	{
		List<Menu> GetAuthorizeDefinotionEndpoints(Type type);
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Abstractions.Hubs;
using ECommerceAPI.SignalR.HubServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.SignalR
{
	public static class ServiceRegistration
	{
		public static void AddSignalRServices(this IServiceCollection services)
		{
			services.AddTransient<IProductHubService, ProductHubService>();
			services.AddSignalR();

		}
	}
}

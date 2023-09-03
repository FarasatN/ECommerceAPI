using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Application
{
	public static class ServiceRegistration
	{
		public static void AddpplicationService(this IServiceCollection servicesCollection)
		{
			//servicesCollection.AddMediatR(typeof(ServiceRegistration));
			servicesCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ServiceRegistration).GetTypeInfo().Assembly));
		}
	}
}

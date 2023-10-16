using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Abstractions.Services.Configurations;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.DTOs.Configuration;
using ECommerceAPI.Application.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace ECommerceAPI.Infrastructure.Services.Configurations
{
	public class ApplicationService : IApplicationService
	{
		public List<Menu> GetAuthorizeDefinotionEndpoints(Type type)
		{
			Assembly assembly = Assembly.GetAssembly(type);
			var controllers = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));
			List<Menu> menus = new();

			foreach(var controller in controllers)
			{
				var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));
				if (actions != null)
				{
					foreach (var action in actions)
					{
						Menu? menu = null;
						var attributes = action.GetCustomAttributes(true);
						if(attributes != null)
						{
							AuthorizeDefinitionAttribute? authorizeDefinitionAttribute = attributes.FirstOrDefault(a=>a.GetType()==typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
							if (!menus.Any(m=>m.MenuName == authorizeDefinitionAttribute.Menu))
							{
								menu = new()
								{
									MenuName = authorizeDefinitionAttribute.Menu,
								};
								menus.Add(menu);
							}
							else
							{
								menu = menus.FirstOrDefault(m => m.MenuName == authorizeDefinitionAttribute.Menu);
							}

							Application.DTOs.Configuration.Action _action = new()
							{
								ActionType = Enum.GetName(typeof(ActionType),authorizeDefinitionAttribute.ActionType),
								Definition = authorizeDefinitionAttribute.Definition,
							};

							var httpAttribute = attributes.FirstOrDefault(a=>a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;
							if (httpAttribute != null)
								_action.HttpType = httpAttribute.HttpMethods.First();
							else
								_action.HttpType = HttpMethods.Get;

							_action.Code = $"{_action.HttpType}.{_action.ActionType}.{_action.Definition.Replace(" ", "")}";
							menu.Actions.Add(_action);
						}
					}
				}
			}
			return menus;
		}
	}
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Persistence.Services
{
	public class RoleService : IRoleService
	{
		private readonly RoleManager<AppRole> _roleManager;

		public RoleService(RoleManager<AppRole> roleManager)
		{
			_roleManager = roleManager;
		}

		public async Task<bool> CreateRole(string name)
		{
			IdentityResult result = await _roleManager.CreateAsync(new()
			{
				Name = name
			});
			return result.Succeeded;
		}

		public async Task<bool> DeleteRole(string id)
		{
			AppRole appRole = await _roleManager.FindByIdAsync(id);
			IdentityResult result = await _roleManager.DeleteAsync(appRole);
			return result.Succeeded;
		}

		public async Task<bool> UpdateRole(string id,string name)
		{
			AppRole appRole = await _roleManager.FindByIdAsync(id);
			IdentityResult result = await _roleManager.UpdateAsync(appRole);

			return result.Succeeded;
		}

		public (object,int) GetAllRoles(int page,int size)
		{
			var query = _roleManager.Roles;
			IQueryable<AppRole> rolesQuery = null;

			if (page != -1 && size != -1)
				rolesQuery = query.Skip(page * size).Take(size);
			else
				rolesQuery = query;

			return (rolesQuery.Select(r => new { r.Id, r.Name }), query.Count());
				//ToDictionary(role=>role.Id,role=>role.Name);
		}

		public async Task<(string id, string name)> GetRoleById(string id)
		{
			string role = await _roleManager.GetRoleIdAsync(new() { Id = id });
			return (id, role);
		}
	}
}

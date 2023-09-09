using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.DTOs;

namespace ECommerceAPI.Application.Features.Commands.AppUser.RefreshTokenLogin
{
	public class RefreshTokenLoginCommandResponse
	{
		public bool Succeeded { get; set; }
		public string Message { get; set; }
		public Token Token { get; set; }
	}
}

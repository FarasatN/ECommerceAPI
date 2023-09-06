using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.DTOs;

namespace ECommerceAPI.Application.Features.Commands.AppUser.LoginUser
{
	public class LoginUserCommandResponse
	{
		public bool Succeeded { get; set; }
		public string Message { get; set; }
        public Token Token { get; set; }
    }
}

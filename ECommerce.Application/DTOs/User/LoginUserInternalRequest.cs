using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.DTOs.User
{
	public class LoginUserInternalRequest
	{
		public string UsernameOrEmail { get; set; }
		public string Password { get; set; }

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Operations
{
	public static class NameOperation
	{
		public static string CharacterRegulatory(string name)
			=> name.Replace("\"", "")
					.Replace("!", "")
					.Replace("^", "")
					.Replace("+", "");

	}
}

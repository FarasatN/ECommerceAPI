using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Domain.Entities
{
	public class Basket: BaseEntity
	{
		public string UserId { get; set; }
		public AppUser User { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
		public Order Order { get; set; }
    }
}

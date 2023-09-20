using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Services.Authentications;
using ECommerceAPI.Application.Repositories.Basket_App;
using ECommerceAPI.Application.Repositories.Basket_Item_App;
using ECommerceAPI.Application.Repositories.Customer_App;
using ECommerceAPI.Application.Repositories.File_App;
using ECommerceAPI.Application.Repositories.InvoiceFile_App;
using ECommerceAPI.Application.Repositories.Order_App;
using ECommerceAPI.Application.Repositories.Product_App;
using ECommerceAPI.Application.Repositories.ProductImageFile_App;
using ECommerceAPI.Domain.Entities.Identity;
using ECommerceAPI.Persistence.Contexts;
using ECommerceAPI.Persistence.Repositories.Basket_Pers;
using ECommerceAPI.Persistence.Repositories.BasketItem_Pers;
using ECommerceAPI.Persistence.Repositories.Customer_Pers;
using ECommerceAPI.Persistence.Repositories.File_Pers;
using ECommerceAPI.Persistence.Repositories.InvoiceFile_Pers;
using ECommerceAPI.Persistence.Repositories.Order_Pers;
using ECommerceAPI.Persistence.Repositories.Product_Pers;
using ECommerceAPI.Persistence.Repositories.ProductImageFile_Pers;
using ECommerceAPI.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Persistence
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
		{

			//services.AddDbContext<ECommerceAPIDbContext>(options => options.UseNpgsql("Username=postgres;Password=123456;Host=localhost;Port=5432;Database=ECommerceAPIDb;"), ServiceLifetime.Singleton);

			services.AddDbContext<ECommerceAPIDbContext>(options => options.UseNpgsql(Configuration.ConnectionString)
				//,ServiceLifetime.Singleton
				);
			services.AddIdentity<AppUser, AppRole>(options =>
				{
					options.Password.RequiredLength = 8;
					options.Password.RequireNonAlphanumeric = false;
					options.Password.RequireDigit = false;
					options.Password.RequireLowercase = false;
					options.Password.RequireUppercase = false;


				}

			).AddEntityFrameworkStores<ECommerceAPIDbContext>();


			//services.AddSingleton<IProductReadRepository, ProductReadRepository>();
			//services.AddSingleton<IProductWriteRespository, ProductWriteRepository>();
			//services.AddSingleton<ICustomerReadRepository, CustomerReadRepository>();
			//services.AddSingleton<ICustomerWriteRepository, CustomerWriteRepository>();
			//services.AddSingleton<IOrderReadRepository, OrderReadRepository>();
			//services.AddSingleton<IOrderWriteRepository, OrderWriteRepository>();

			services.AddScoped<IProductReadRepository, ProductReadRepository>();
			services.AddScoped<IProductWriteRespository, ProductWriteRepository>();
			services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
			services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
			services.AddScoped<IOrderReadRepository, OrderReadRepository>();
			services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

			services.AddScoped<IFileReadRepository, FileReadRepository>();
			services.AddScoped<IFileWriteRepository, FileWriteRepository>();
			services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
			services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
			services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
			services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();

			services.AddScoped<IBasketReadRepository, BasketReadRepository>();
			services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();
			services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
			services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();

			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IInternalAuthService, AuthService>();
			services.AddScoped<IExternalAuthService, AuthService>();
			services.AddScoped<IBasketService, BasketService>();
			services.AddScoped<IOrderService, OrderService>();



			return services;
		}
	}
}

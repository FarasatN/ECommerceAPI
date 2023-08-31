﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.Customer_App;
using ECommerceAPI.Application.Repositories.File_App;
using ECommerceAPI.Application.Repositories.InvoiceFile_App;
using ECommerceAPI.Application.Repositories.Order_App;
using ECommerceAPI.Application.Repositories.Product_App;
using ECommerceAPI.Application.Repositories.ProductImageFile_App;
using ECommerceAPI.Persistence.Contexts;
using ECommerceAPI.Persistence.Repositories;
using ECommerceAPI.Persistence.Repositories.Customer_Pers;
using ECommerceAPI.Persistence.Repositories.File_Pers;
using ECommerceAPI.Persistence.Repositories.InvoiceFile_Pers;
using ECommerceAPI.Persistence.Repositories.Order_Pers;
using ECommerceAPI.Persistence.Repositories.Product_Pers;
using ECommerceAPI.Persistence.Repositories.ProductImageFile_Pers;
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


			return services;
		}
	}
}
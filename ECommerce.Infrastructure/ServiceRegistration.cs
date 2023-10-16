﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Services.Configurations;
using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Infrastructure.Services;
using ECommerceAPI.Infrastructure.Services.Configurations;
using ECommerceAPI.Infrastructure.Services.Storage;
using ECommerceAPI.Infrastructure.Services.Storage.Local;
using ECommerceAPI.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Infrastructure
{
	public static class ServiceRegistration
	{
		public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
		{
			//serviceCollection.AddScoped<IFileService, FileService>();	

			serviceCollection.AddScoped<IStorageService,StorageService>();
			serviceCollection.AddScoped<ITokenHandler,TokenHandler>();
			serviceCollection.AddScoped<IMailService,MailService>();
			serviceCollection.AddScoped<IApplicationService, ApplicationService>();
			serviceCollection.AddScoped<IQRCodeService, QRCodeService>();
		}

		public static void AddStorage<T>(this IServiceCollection serviceCollection) where T: class,IStorage
		{
			serviceCollection.AddScoped<IStorage, T>();
		}

		//public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType) 
		//{
		//	switch(storageType) {
		//		case StorageType.Local:
		//			serviceCollection.AddScoped<IStorage, LocalStorage>();
		//			break;
		//		case StorageType.Azure:
		//			//serviceCollection.AddScoped<IStorage, AzureStorage>();
		//			break;
		//		case StorageType.AWS:
		//			//serviceCollection.AddScoped<IStorage, AWSStorage>();
		//			break;
		//	}
		//}


		//public enum StorageType
		//{
		//	Local,
		//	Azure,
		//	AWS
		//}

	}
}

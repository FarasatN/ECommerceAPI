﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Repositories.Product_App;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Repositories.Product_Pers;

namespace ECommerceAPI.Persistence.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductReadRepository _productReadRepository;
		private readonly IProductWriteRespository _productWriteRepository;
		private readonly IQRCodeService _qrCodeService;

		public ProductService(IProductReadRepository productReadRepository, IQRCodeService qrCodeService, IProductWriteRespository productWriteRepository)
		{
			_productReadRepository = productReadRepository;
			_qrCodeService = qrCodeService;
			_productWriteRepository = productWriteRepository;
		}

		public async Task<byte[]> QRCodeToProductAsync(string productId)
		{
			Product product = await _productReadRepository.GetByIdAsync(productId);
			if(product == null)
			{
				throw new Exception("Product not found");
			}

			var plainObject = new
			{
				product.Id,
				product.Name,
				product.Price,
				product.Stock,
				product.CreatedDate
			};
			string plainText = JsonSerializer.Serialize(plainObject);

			return _qrCodeService.GenerateQRCode(plainText);
		}

		public async Task StockUpdateToProductAsync(string productId, int stock)
		{
			Product product = await _productReadRepository.GetByIdAsync(productId);
			if( product == null )
			{
				throw new Exception("Product not found");
			}
			product.Stock = stock;
			await _productWriteRepository.SaveAsync();

		}
	}
}

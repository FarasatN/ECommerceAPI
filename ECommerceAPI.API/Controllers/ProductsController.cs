using System.Net;
using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Features.Commands.Product.CreateProduct;
using ECommerceAPI.Application.Features.Commands.Product.RemoveProduct;
using ECommerceAPI.Application.Features.Commands.Product.UpdateProduct;
using ECommerceAPI.Application.Features.Commands.ProductImageFile.ChangeShowcaseImage;
using ECommerceAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage;
using ECommerceAPI.Application.Features.Commands.ProductImageFile.UploadProductImage;
using ECommerceAPI.Application.Features.Queries.Product.GetAllProducts;
using ECommerceAPI.Application.Features.Queries.Product.GetByIdProduct;
using ECommerceAPI.Application.Features.Queries.ProductImageFile.GetProductImages;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.Customer_App;
using ECommerceAPI.Application.Repositories.File_App;
using ECommerceAPI.Application.Repositories.InvoiceFile_App;
using ECommerceAPI.Application.Repositories.Order_App;
using ECommerceAPI.Application.Repositories.Product_App;
using ECommerceAPI.Application.Repositories.ProductImageFile_App;
using ECommerceAPI.Application.RequestParameters;
using ECommerceAPI.Application.ViewModels.Products;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;
using ECommerceAPI.Persistence.Repositories.Product_Pers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ProductsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAll(
			//[FromQuery]Pagination pagination
			[FromQuery]GetAllProductsQueryRequest getAllProductsQueryRequest
			)
		{
			//var totalCount = _productReadRespository.GetAll(false).Count();
			//var products = _productReadRespository.GetAll(false).Select(p => new
			//{
			//	p.Id,
			//	p.Name,
			//	p.Stock,
			//	p.Price,
			//	p.CreatedDate,
			//	p.UpdatedDate
			//})
			//.Skip(pagination.Size).Take(pagination.Page *pagination.Size);

			//return Ok(new
			//{
			//	totalCount, products 
			//});
			//-----------------------------------

			 GetAllProductsQueryResponse products = await _mediator.Send(getAllProductsQueryRequest);
			return Ok(products);
		}

		[HttpGet("GetById/{Id}")]
		public async Task<IActionResult> Get([FromRoute]GetByIdProductQueryRequest getByIdProductQueryRequest)
		{
			GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
			return Ok(response);
		}

		[HttpPost("Add")]
		[Authorize(AuthenticationSchemes = "Admin")]
		public async Task<IActionResult> Post(
			//VM_Create_Product model,
			CreateProductCommandRequest createProductCommandRequest
			)
		{

			//await _productWriteRespository.AddAsync(new()
			//{
			//	Name = model.Name,
			//	Price = model.Price,
			//	Stock = model.Stock,
			//});
			//await _productWriteRespository.SaveAsync();
			//return Ok();

			CreateProductCommandResponse response =  await _mediator.Send(createProductCommandRequest);
			return StatusCode((int)HttpStatusCode.Created);
		}

		[HttpPut("Update")]
		[Authorize(AuthenticationSchemes = "Admin")]
		public async Task<IActionResult> Put([FromBody]UpdateProductCommandRequest updateProductCommandRequest)
		{
			var response =  await _mediator.Send(updateProductCommandRequest);
			return Ok();
		}

		[HttpDelete("Delete/{Id}")]
		[Authorize(AuthenticationSchemes = "Admin")]
		public async Task<IActionResult> Delete([FromRoute]RemoveProductCommandRequest removeProductCommandRequest)
		{
			var response = await _mediator.Send(removeProductCommandRequest);
			return Ok();
		}

		[HttpPost("Upload")]
		[Authorize(AuthenticationSchemes = "Admin")]
		public async Task<IActionResult> Upload([FromQuery]UploadProductImageCommandRequest uploadProductImageCommandRequest)
		{
			//-------------------------
			//var datas = await _fileService.UploadAsync("resources/product-images", Request.Form.Files);
			//var result = await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d=> new ProductImageFile()
			//{ 
			//	FileName = d.fileName,
			//	Path = d.path,

			//}).ToList());
			//await _productImageFileWriteRepository.SaveAsync();

			//--------------------
			//var datas = await _fileService.UploadAsync("resources/invoices", Request.Form.Files);
			//var result = await _invoiceFileWriteRepository.AddRangeAsync(datas.Select(d => new InvoiceFile()
			//{
			//	FileName = d.fileName,
			//	Path = d.path,
			//	Price = new Random().Next(3000)
			//}).ToList()); 
			//await _invoiceFileWriteRepository.SaveAsync();

			//var datas = await _fileService.UploadAsync("resources/files", Request.Form.Files);
			//var result = await _fileWriteRepository.AddRangeAsync(datas.Select(d => new ECommerceAPI.Domain.Entities.File()
			//{
			//	FileName = d.fileName,
			//	Path = d.path,
			//}).ToList());
			//await _fileWriteRepository.SaveAsync();

			//var d1 = _fileReadRepository.GetAll(false);
			//var d2 = _invoiceFileReadRepository.GetAll(false);
			//var d4 = _productImageFileReadRepository.GetAll(false);

			//-----------------------------

			//var datas = await _storageService.UploadAsync("resources/files", Request.Form.Files);
			//var result = await _fileWriteRepository.AddRangeAsync(datas.Select(d => new ECommerceAPI.Domain.Entities.File()
			//{
			//	FileName = d.fileName,
			//	Path = d.pathOrContainerName,
			//	Storage = _storageService.StorageName
			//}).ToList());
			//await _fileWriteRepository.SaveAsync();
			//-------------------------------------------------------------------


			uploadProductImageCommandRequest.Files = Request.Form.Files;
			var response = await _mediator.Send(uploadProductImageCommandRequest);
			return Ok();
		}

		[HttpGet("GetProductImages/{Id}")]
		public async Task<IActionResult> GetProductImages([FromRoute]GetProductImagesQueryRequest getProductImagesQueryRequest)
		{
			var response = await _mediator.Send(getProductImagesQueryRequest);
			return Ok(response);
		}


		[HttpDelete("DeleteProductImage/{Id}")]
		[Authorize(AuthenticationSchemes = "Admin")]
		public async Task<IActionResult> DeleteProductImage([FromRoute]RemoveProductImageCommandRequest removeProductImageCommandRequest, [FromQuery]string ImageId)
		{
			removeProductImageCommandRequest.ImageId = ImageId;
			var response = await _mediator.Send(removeProductImageCommandRequest);
			return Ok(); 
		}



		[HttpGet("[action]")]
		[Authorize(AuthenticationSchemes = "Admin")]
		public async Task<IActionResult> ChangeShowcaseImage([FromQuery]ChangeShowcaseImageCommandRequest changeShowcaseImageCommandRequest)
		{
			ChangeShowcaseImageCommandResponse response = await _mediator.Send(changeShowcaseImageCommandRequest);
			return Ok(response);
		}







		//-------------------------------------------------
		//-------------------------------------------------
		//[HttpGet]
		//public IActionResult Get()
		//{
		//_ = _productWriteRespository.AddRangeAsync(new()
		//{
		//	new(){Id = Guid.NewGuid(), Name = "Product 1", Price = 100, CreatedDate = DateTime.UtcNow, Stock = 10},
		//	new(){Id = Guid.NewGuid(), Name = "Product 2", Price = 200, CreatedDate = DateTime.UtcNow, Stock = 10},
		//	new(){Id = Guid.NewGuid(), Name = "Product 3", Price = 300, CreatedDate = DateTime.UtcNow, Stock = 10},
		//});
		//var count = await _productWriteRespository.SaveAsync();
		//Console.WriteLine(count);

		//var data = await _productReadRespository.GetByIdAsync("d399dba4-d62c-49f6-8d1d-160283ac4399",false);
		//Console.WriteLine(data.Name);
		//data.Name = "Notebook2";
		//await _productWriteRespository.SaveAsync();
		//Console.WriteLine(data.Name);

		//await _productWriteRespository.AddAsync(new() {Name = "C Product", Price = 1.500F,Stock = 10, CreatedDate = DateTime.UtcNow });
		//await _productWriteRespository.SaveAsync();


		//---------------------------------------------------

		//var customerId = Guid.NewGuid();
		//await _customerWriteRepository.AddAsync(new() { Id = customerId, Name = "Mardan" });

		//await _orderWriteRepository.AddAsync(new() { Description = "bla bla", Address = "Baku", CustomerId = customerId });
		//await _orderWriteRepository.AddAsync(new() { Description = "bla bla bla", Address = "Shamakhy", CustomerId = customerId });
		//await _orderWriteRepository.SaveAsync();


		//var order = await _orderReadRepository.GetByIdAsync("9798d0ad-2542-4a85-ac54-dfb25b2d7733");
		//order.Address = "Sumqayit";
		//await _orderWriteRepository.SaveAsync();


		//var products = _productReadRespository.GetAll();
		//return Ok(products);
		//}


		//[HttpGet("{id}")]
		//public async Task<IActionResult> Get(string id)
		//{
		//	var product = await _productReadRespository.GetByIdAsync(id);
		//	return Ok(product);
		//}
		//-----------------------------------------
	}
}

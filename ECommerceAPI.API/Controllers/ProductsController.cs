using System.Net;
using ECommerceAPI.Application.Abstractions.Storage;
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
		private readonly IProductWriteRespository _productWriteRespository;
		private readonly IProductReadRepository _productReadRespository;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IFileWriteRepository _fileWriteRepository;
		private readonly IFileReadRepository _fileReadRepository;
		private readonly IProductImageFileReadRepository _productImageFileReadRepository;
		private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
		private readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
		private readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;
		private readonly IStorageService _storageService;
		private readonly IConfiguration _configuration;


		public ProductsController(IProductWriteRespository productWriteRespository, IProductReadRepository productReadRespository, IWebHostEnvironment webHostEnvironment, IFileWriteRepository fileWriteRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository, IFileReadRepository fileReadRepository, IStorageService storageService, IConfiguration configuration)
		{
			_productWriteRespository = productWriteRespository;
			_productReadRespository = productReadRespository;
			_webHostEnvironment = webHostEnvironment;
			_fileWriteRepository = fileWriteRepository;
			_productImageFileReadRepository = productImageFileReadRepository;
			_productImageFileWriteRepository = productImageFileWriteRepository;
			_invoiceFileReadRepository = invoiceFileReadRepository;
			_invoiceFileWriteRepository = invoiceFileWriteRepository;
			_fileReadRepository = fileReadRepository;
			_storageService = storageService;
			_configuration = configuration;
		}

		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAll([FromQuery]Pagination pagination)
		{
			var totalCount = _productReadRespository.GetAll(false).Count();
			var products = _productReadRespository.GetAll(false).Select(p => new
			{
				p.Id,
				p.Name,
				p.Stock,
				p.Price,
				p.CreatedDate,
				p.UpdatedDate
			})
			.Skip(pagination.Size).Take(pagination.Page *pagination.Size);

			return Ok(new
			{
				totalCount, products 
			});
		}

		[HttpGet("Get/{id}")]
		public async Task<IActionResult> Get(string id)
		{
			return Ok(await _productReadRespository.GetByIdAsync(id, false));
		}

		[HttpPost("Add")]
		public async Task<IActionResult> Post(VM_Create_Product model)
		{

			await _productWriteRespository.AddAsync(new()
			{
				Name = model.Name,
				Price = model.Price,
				Stock = model.Stock,
			});
			await _productWriteRespository.SaveAsync();
			//return Ok();
			return StatusCode((int)HttpStatusCode.Created);
		}

		[HttpPut("Update")]
		public async Task<IActionResult> Put(VM_Update_Product model)
		{
			var product = await _productReadRespository.GetByIdAsync(model.Id);
			product.Stock = model.Stock;
			product.Name = model.Name;
			product.Price = model.Price;
			await _productWriteRespository.SaveAsync();
			return Ok();
		}

		[HttpDelete("Delete/{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			await _productWriteRespository.RemoveAsync(id);
			await _productWriteRespository.SaveAsync();
			return Ok();
		}

		[HttpPost("Upload")]
		public async Task<IActionResult> Upload(string id)
		{
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


			var result = await _storageService.UploadAsync("resources/photo-images", Request.Form.Files);
			var product = await _productReadRespository.GetByIdAsync(id);

			//for ile butun sekilleri bir bir doldurmaq da olar
			//meselen:
			//foreach(var r in result)
			//{
			//	product.ProductImageFiles.Add(new()
			//	{
			//		FileName = r.fileName,
			//		Path = r.pathOrContainerName,
			//		Storage = _storageService.StorageName,
			//		Products = new List<Product>() { product }
			//	});
			//}
			//await _productImageFileWriteRepository.SaveAsync();

			await _productImageFileWriteRepository.AddRangeAsync(result.Select(r => new ProductImageFile
			{
				FileName = r.fileName,
				Path = r.pathOrContainerName,
				Storage = _storageService.StorageName,
				Products = new List<Product>() { product }
			}).ToList());
			await _productImageFileWriteRepository.SaveAsync();

			return Ok();
		}

		[HttpGet("GetProductImages/{id}")]
		public async Task<IActionResult> GetProductImages(string id)
		{
			Product? product = await _productReadRespository.Table.Include(p=>p.ProductImageFiles)
				.FirstOrDefaultAsync(p=>p.Id == Guid.Parse(id));
			return Ok(product.ProductImageFiles.Select(p=> new
			{
				Path = $"{_configuration["BaseStorageUrl:Local"]}/{p.Path}",
				Filename = p.FileName,
				Id = p.Id
			}));
		}


		[HttpDelete("DeleteProductImage/{id}")]
		public async Task<IActionResult> DeleteProductImage(string id, string imageId)
		{
			Product? product = await _productReadRespository.Table.Include(p => p.ProductImageFiles)
				.FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
			ProductImageFile productImageFile = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(imageId));
			product.ProductImageFiles.Remove(productImageFile); 
			await _productWriteRespository.SaveAsync();
			return Ok(); 
		}



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

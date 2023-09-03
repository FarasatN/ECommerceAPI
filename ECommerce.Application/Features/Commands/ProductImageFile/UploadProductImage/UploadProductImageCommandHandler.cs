using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Repositories.InvoiceFile_App;
using ECommerceAPI.Application.Repositories.Product_App;
using ECommerceAPI.Application.Repositories.ProductImageFile_App;
using ECommerceAPI.Domain.Entities;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
	{

		private readonly IProductReadRepository _productReadRespository;
		private readonly IStorageService _storageService;
		private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;



		public UploadProductImageCommandHandler(IProductReadRepository productReadRespository, IStorageService storageService, IProductImageFileWriteRepository productImageFileWriteRepository)
		{
			_productReadRespository = productReadRespository;
			_storageService = storageService;
			_productImageFileWriteRepository = productImageFileWriteRepository;
		}


		public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
			var result = await _storageService.UploadAsync("resources/photo-images", request.Files);
			var product = await _productReadRespository.GetByIdAsync(request.Id);

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

			await _productImageFileWriteRepository.AddRangeAsync(result.Select(r => new Domain.Entities.ProductImageFile
			{
				FileName = r.fileName,
				Path = r.pathOrContainerName,
				Storage = _storageService.StorageName,
				Products = new List<Domain.Entities.Product>() { product }
			}).ToList());
			await _productImageFileWriteRepository.SaveAsync();

			return new();
		}
    }
}

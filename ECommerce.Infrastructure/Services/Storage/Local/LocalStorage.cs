using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Infrastructure.Services.Storage.Local
{
	public class LocalStorage : ILocalStorage
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		public LocalStorage(IWebHostEnvironment webHostEnvironment)
		{
			_webHostEnvironment = webHostEnvironment;
		}

		public async Task DeleteAsync(string path, string fileName)
			=>  File.Delete($"{path}\\{fileName}");

		public List<string> GetFiles(string path)
		{
			DirectoryInfo dir = new(path);
			return dir.GetFiles().Select(f => f.Name).ToList();
		}

		public bool HasFile(string path, string fileName)
			=> File.Exists($"{path}\\{fileName}");



		private async Task<bool> CopyFileAsync(string path, IFormFile file)
		{
			try
			{
				await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
				await file.CopyToAsync(fileStream);
				await fileStream.FlushAsync();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
				return false;
			}
		}


		public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
		{
			List<(string filename, string path)> datas = new();

			try
			{
				var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath.ToString(), path);
				if (!Directory.Exists(uploadPath))
				{
					Directory.CreateDirectory(uploadPath);
				}

				foreach (IFormFile file in files)
				{
					await CopyFileAsync($"{uploadPath}\\{file.Name}", file);
					datas.Add((file.Name, $"{path}\\{file.Name}"));
				}
				return datas;

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

			}

			return datas;
		}
	}
}

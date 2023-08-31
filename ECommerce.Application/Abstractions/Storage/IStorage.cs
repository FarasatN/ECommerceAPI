﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Application.Abstractions.Storage
{
	public interface IStorage
	{
		Task<List<(string fileName,string pathOrContainerName)>> UploadAsync(string pathOrContainer, IFormFileCollection files);
		Task DeleteAsync(string pathOrContainerName, string fileName);
		List<string> GetFiles(string pathOrContainerName);
		bool HasFile(string pathOrContainerName, string fileName);
	}
}

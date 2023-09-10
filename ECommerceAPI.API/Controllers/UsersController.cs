﻿using ECommerceAPI.Application.Features.Commands.AppUser.CreateUser;
using ECommerceAPI.Application.Features.Commands.AppUser.GoogleLogin;
using ECommerceAPI.Application.Features.Commands.AppUser.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IMediator _mediator;

		public UsersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task <IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
		{
			
			var response = await _mediator.Send(createUserCommandRequest);
			return Ok(response);
		}

		[HttpGet("Test")]
		public IActionResult Test()
		{

			return Ok("Test User");
		}

	}
}

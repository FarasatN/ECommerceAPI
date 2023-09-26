using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Features.Commands.AppUser.CreateUser;
using ECommerceAPI.Application.Features.Commands.AppUser.GoogleLogin;
using ECommerceAPI.Application.Features.Commands.AppUser.LoginUser;
using ECommerceAPI.Application.Features.Commands.AppUser.UpdatePassword;
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
		private readonly IMailService _mailService;

		public UsersController(IMediator mediator, IMailService mailService)
		{
			_mediator = mediator;
			_mailService = mailService;
		}

		[HttpPost]
		public async Task <IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
		{
			
			var response = await _mediator.Send(createUserCommandRequest);
			return Ok(response);
		}

		[HttpPost("update-password")]
		public async Task<IActionResult> UpdatePassword(UpdatePasswordCommandRequest updatePasswordCommandRequest)
		{
			var response = await _mediator.Send(updatePasswordCommandRequest);
			return Ok(response);
		}

		//[HttpGet("Test")]
		//public async Task<IActionResult> ExampleMailTest()
		//{
		//	await _mailService.SendMailAsync("farasatnovruzov@gmail.com", "dfdfdfg","dfdf");
		//	return Ok();
		//}

	}
}

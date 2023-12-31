﻿using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Features.Commands.AppUser.AssignRoleToUser;
using ECommerceAPI.Application.Features.Commands.AppUser.CreateUser;
using ECommerceAPI.Application.Features.Commands.AppUser.GoogleLogin;
using ECommerceAPI.Application.Features.Commands.AppUser.LoginUser;
using ECommerceAPI.Application.Features.Commands.AppUser.UpdatePassword;
using ECommerceAPI.Application.Features.Queries.AppUser;
using ECommerceAPI.Application.Features.Queries.AppUser.GetAllUsers;
using ECommerceAPI.Application.Features.Queries.AppUser.GetRolesToUser;
using ECommerceAPI.Application.Features.Queries.Order.GetAllOrders;
using ECommerceAPI.Application.Features.Queries.Product.GetAllProducts;
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

		[HttpGet]
		[Authorize(AuthenticationSchemes = "Admin")]
		[AuthorizeDefinition(ActionType = Application.Enums.ActionType.Reading, Definition = "Get All Users", Menu = "Users")]
		public async Task<IActionResult> GetAllUsers([FromQuery]GetAllUsersQueryRequest getAllUsersQueryRequest)
		{
			GetAllUsersQueryResponse response = await _mediator.Send(getAllUsersQueryRequest);
			return Ok(response);
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

		[HttpPost("assign-role-to-user")]
		[Authorize(AuthenticationSchemes = "Admin")]
		[AuthorizeDefinition(ActionType = Application.Enums.ActionType.Writing, Definition = "Assign Role To User", Menu = "Users")]
		public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommandRequest assignRoleToUserCommandRequest)
		{
			var response = await _mediator.Send(assignRoleToUserCommandRequest);
			return Ok(response);
		}

		[HttpGet("get-roles-to-user/{UserId}")]
		[Authorize(AuthenticationSchemes = "Admin")]
		[AuthorizeDefinition(ActionType = Application.Enums.ActionType.Reading, Definition = "Get Roles To User", Menu = "Users")]
		public async Task<IActionResult> GetRolesToUser([FromRoute] GetRolesToUserQueryRequest getRolesToUserQueryRequest)
		{
			GetRolesToUserQueryResponse response = await _mediator.Send(getRolesToUserQueryRequest);
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

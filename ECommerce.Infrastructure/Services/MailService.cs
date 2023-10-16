using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;

namespace ECommerceAPI.Infrastructure.Services
{
	public class MailService : IMailService
	{
		private readonly IConfiguration _configuration;

		public MailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}


		public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
		{

			await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
		}

		public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
		{
			MailMessage message = new MailMessage();
			message.IsBodyHtml = isBodyHtml;
            foreach (var to in tos)
            {
				message.To.Add(to);
            }
			message.Subject = subject;
			message.Body = body;
			message.From = new(_configuration["Mail:Username"],"NG E-Commerce",System.Text.Encoding.UTF8);
			SmtpClient smtp = new();
			smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Username"]);
			smtp.Port = int.Parse(_configuration["Mail:Port"]);
			smtp.EnableSsl = true;
			smtp.Host = _configuration["Mail:Host"];
			await smtp.SendMailAsync(message);

		}

		public async Task SendPasswordResetMailAsync(string to,string userId,string token)
		{
			StringBuilder mail = new();
			mail.AppendLine("Hi<br>If you demand to obtain new password then you can update password from below link.<br><strong><a target=\"_blank\"  href=\"");
			mail.AppendLine(_configuration["AngularClientUrl"]);
			mail.AppendLine("/update-password/");
			mail.AppendLine(userId + "/" + token + "\">For new password click it</a></strong><br><br><span style=\"font-size:12px;\" >*NOTE: If this request has not been made by you, please do not take this email seriously.</span><br>Best Regards..<br><br><br>NG - ECommerce");

			await SendMailAsync(to, "Request for Password Updating", mail.ToString());
		}


		public async Task SendCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate, string userName)
		{
			string mail = $"Sir {userName}, Hi<br>" +
				$"{orderDate} tarihinde vermis oldugunuz {orderCode} kodlu siparisiniz tamamlanmis ve karqo firmasina verilmisdir";
			await SendMailAsync(to, $"{orderCode} No. order completed", mail);

		}

	}
}

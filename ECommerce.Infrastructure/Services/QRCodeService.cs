using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Abstractions.Services;
using QRCoder;

namespace ECommerceAPI.Infrastructure.Services
{
	public class QRCodeService: IQRCodeService
	{

		public byte[] GenerateQRCode(string text)
		{
			QRCodeGenerator generator = new();
			QRCodeData data = generator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
			PngByteQRCode qRCode = new(data);
			byte[] byteGraghic = qRCode.GetGraphic(10, new byte[] { 84, 99, 71 }, new byte[] { 240, 240, 240 });

			return byteGraghic;
		}
	}
}

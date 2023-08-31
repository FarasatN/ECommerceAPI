using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.ViewModels.Products;
using FluentValidation;

namespace ECommerceAPI.Application.Validators.Products
{
	public class CreateProductValidator: AbstractValidator<VM_Create_Product>
	{
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(30)
                    .WithMessage("Please, enter a valid name");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Please, enter a valid stock")
                .Must(s => s >= 0)
                    .WithMessage("Stock value cannot be negative");

			RuleFor(p => p.Price)
				.NotEmpty()
				.NotNull()
					.WithMessage("Please, enter a valid price")
				.Must(s => s >= 0)
					.WithMessage("Price value cannot be negative");




		}

	}
}

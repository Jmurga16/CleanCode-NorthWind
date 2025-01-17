﻿using FluentValidation;
using NorthWind.UseCasesDTOs.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.UseCases.Common.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderParams>
    {
        public CreateOrderValidator()
        {
            RuleFor(c => c.CustomerId).NotEmpty()
               .WithMessage("Debe proporcionar el Identificador del Cliente.");

            RuleFor(c => c.ShipAddress).NotEmpty()
                    .WithMessage("Debe proporcionar la dirección de envío.");

            RuleFor(c => c.ShipCity).NotEmpty().MinimumLength(3)
                    .WithMessage("Debe proporcionar al menos 3 caracteres del nombre de la ciudad.");

            RuleFor(c => c.ShipCountry).NotEmpty().MinimumLength(3)
                   .WithMessage("Debe proporcionar al menos 3 caracteres del nombre del País.");

            RuleFor(c => c.OrderDetails)
                    .Must(d => d != null && d.Any())
                    .WithMessage("Deben especificarse los productos de la orden.");
        }

    }
}

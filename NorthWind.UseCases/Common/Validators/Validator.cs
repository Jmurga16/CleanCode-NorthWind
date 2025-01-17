﻿using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.UseCases.Common.Validators
{
    public static class Validator<Model>
    {
        public static Task<List<ValidationFailure>> Validate(Model model, IEnumerable<IValidator<Model>> validators, bool causesException = true)
        {
            var Failures = validators
               .Select(validator => validator.Validate(model))
               .SelectMany(r => r.Errors)
               .Where(f => f != null)
               .ToList();

            if (Failures.Any() && causesException)
            {
                throw new ValidationException(Failures);
            }

            return Task.FromResult(Failures);

        }



    }
}

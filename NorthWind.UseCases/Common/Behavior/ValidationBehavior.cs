using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthWind.UseCases.Common.Behavior
{
    public class ValidationBehavior<TRequest, TResponse>:
        IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
    {
        readonly IEnumerable<IValidator<TRequest>> Validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => Validators = validators;

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var Failures = Validators
                .Select(validator => validator.Validate(request))
                .SelectMany(r=>r.Errors)
                .Where(f => f != null)
                .ToList();

            if (Failures.Any())
            {
                throw new ValidationException(Failures);
            }
            return next();
        }
    }
}

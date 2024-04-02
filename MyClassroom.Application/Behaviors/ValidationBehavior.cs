using FluentValidation;
using MediatR;
using MyClassroom.Application.Common;

namespace MyClassroom.Application.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
               .Select(v => v.Validate(context))
               .SelectMany(result => result.Errors)
               .Where(error => error != null)
               .ToList();

            if (failures.Any())
            {
                throw new ValidationException("Validation exception", failures);
            }

            var response = await next();
            return response;
        }
    }
}

using System.Text;
using Common.Infrastructure.Core.Exceptions;
using FluentValidation;
using MediatR;

namespace Common.Infrastructure.Core.Behavior.Vaildation;

public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var errors = _validators.Select(x => x.Validate(request, options => options.IncludeAllRuleSets()))
            .SelectMany(x => x.Errors)
            .Where(error => error != null)
            .ToList();
        if (errors.Any())
        {
            var messageBuilder = new StringBuilder("Invalid arguments:");
            foreach (var error in errors)
            {
                messageBuilder.AppendLine(error.ErrorMessage);
            }
            throw new InvalidCommandException(400, messageBuilder.ToString());
        }
        return next();
    }
}
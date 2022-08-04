using Common.Infrastructure.Core.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Common.Infrastructure.Core.Behavior.Exceptions;

public class ExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ExceptionBehaviour<TRequest, TResponse>> _logger;

    public ExceptionBehaviour(ILogger<ExceptionBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 异常中间件捕捉
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (InvalidCommandException exception)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation(exception, "InvalidCommandException");
            }

            throw;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "ERROR Handling for {CommandName} ({@Command})", request.GetType(), request);
            throw;
        }
    }
}
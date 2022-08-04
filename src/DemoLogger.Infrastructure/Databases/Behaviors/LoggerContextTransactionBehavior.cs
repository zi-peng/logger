using Common.Infrastructure.Core.Behavior;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DemoLogger.Infrastructure.Databases.Behaviors;

public class LoggerContextTransactionBehavior<TRequest,TResponse>:TransactionBehavior<LoggerContext,TRequest,TResponse> where TRequest : IRequest<TResponse>
{
    public LoggerContextTransactionBehavior(LoggerContext dbContext, ILoggerFactory loggerFactory) : base(dbContext, loggerFactory)
    {
        
    }
}
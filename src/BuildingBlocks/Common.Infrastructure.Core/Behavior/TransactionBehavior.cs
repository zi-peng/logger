using Common.Infrastructure.Core.Extensions;
using Common.Infrastructure.Core.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Common.Infrastructure.Core.Behavior;

public class TransactionBehavior<TDbContext, TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TDbContext : EFContext where TRequest : IRequest<TResponse>
{
    private readonly TDbContext _dbContext;
    private readonly ILogger _logger;

    public TransactionBehavior(TDbContext dbContext, ILoggerFactory loggerFactory)
    {
        _dbContext = dbContext;
        _logger = loggerFactory.CreateLogger<TransactionBehavior<TDbContext, TRequest, TResponse>>();
    }

    //处理的Handler
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var response = default(TResponse);
        var typeName = request.GetGenericTypeName();

        try
        {
            //1.判断事务是否开启
            if (_dbContext.HasActiveTransaction || IsQuery())
            {
                return await next();
            }

            //新建一个方案
            var strategy = _dbContext.Database.CreateExecutionStrategy();


            //执行策略(注意ExecuteAsync的命名空间)
            await strategy.ExecuteAsync(async () =>
            {
                //事务
                await using var transaction = await _dbContext.BeginTransactionAsync()!;
                using (_logger.BeginScope("TransactionContext:{TransactionId}", transaction!.TransactionId))
                {
                    _logger.LogInformation("==========开始事务{TransactionId}|Type:{Type}|Request: {@Command}=============",
                        transaction!.TransactionId, typeName, request);

                    response = await next();

                    _logger.LogInformation("==========提交事务{TransactionId}=============",
                        transaction!.TransactionId);

                    await _dbContext.CommitTransactionAsync(transaction, cancellationToken);
                }
            });
            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "处理事务出错 {CommandName} ({@Command})", typeName, request);
            throw;
        }
    }

    /// <summary>
    /// 查询你则不需要开启事务
    /// </summary>
    /// <returns></returns>
    private static bool IsQuery()
    {
        var type = typeof(TRequest);

        return type.GetInterfaces().Where(x => x.IsGenericType)
            .Select(x => x.GetGenericTypeDefinition())
            .Any(x => x == typeof(IQuery<>));
    }
}
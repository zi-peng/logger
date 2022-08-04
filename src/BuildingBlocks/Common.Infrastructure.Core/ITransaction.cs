using Microsoft.EntityFrameworkCore.Storage;

namespace Common.Infrastructure.Core;

public interface ITransaction
{
    /// <summary>
    /// 获取事务
    /// </summary>
    /// <returns></returns>
    IDbContextTransaction? GetCurrentTransaction();

    /// <summary>
    /// 是否已经有事务
    /// </summary>
    bool HasActiveTransaction { get; }

    /// <summary>
    /// 开启事务
    /// </summary>
    /// <returns></returns>
    Task<IDbContextTransaction?>? BeginTransactionAsync();

    /// <summary>
    /// 提交事务
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task CommitTransactionAsync(IDbContextTransaction transaction,CancellationToken cancellationToken = default);

    /// <summary>
    /// 事务回滚
    /// </summary>
    void RollBackTransaction();
}
using Common.Infrastructure.Core.Extensions;
using DotNetCore.CAP;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Common.Infrastructure.Core;

public class EFContext : DbContext, IUnitOfWork, ITransaction
{
    private readonly IMediator _mediator;
    private readonly ICapPublisher _capPublisher;

    public EFContext(DbContextOptions options, IMediator mediator,ICapPublisher capPublisher): base(options)
    {
        _mediator = mediator;
        _capPublisher = capPublisher;
    }

    #region IUnitWork

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);
         await _mediator.DispatchDomainEventsAsync(this);
        return true;
    }

    #endregion


    #region ITransaction

    private IDbContextTransaction? _dbContextTransaction;


    public IDbContextTransaction? GetCurrentTransaction() => _dbContextTransaction;


    public bool HasActiveTransaction => _dbContextTransaction != null;

    public async Task<IDbContextTransaction?>? BeginTransactionAsync()
    {
        if (_dbContextTransaction != null) return null;
        _dbContextTransaction = await Database.BeginTransactionAsync();
        return _dbContextTransaction;
    }


    /// <summary>
    /// 提交事务
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task CommitTransactionAsync(IDbContextTransaction transaction,
        CancellationToken cancellationToken = default)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _dbContextTransaction)
            throw new InvalidOperationException($"Transaction{transaction} is not _dbContextTransaction ");

        try
        {
            await SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            await _mediator.DispatchDomainEventsAsync(this);
        }
        catch (Exception e)
        {
            RollBackTransaction();
            throw;
        }
        finally
        {
            Dispose();
        }
    }


    /// <summary>
    /// 事务回滚
    /// </summary>
    public void RollBackTransaction()
    {
        try
        {
            _dbContextTransaction?.Rollback();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            Dispose();
        }
    }

    /// <summary>
    /// 这个Dispose 只在这里面使用
    /// </summary>
    private new void Dispose()
    {
        if (_dbContextTransaction == null) return;
        _dbContextTransaction.Dispose();
        _dbContextTransaction = null;
    }

    #endregion
}
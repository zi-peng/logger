using Common.Domain.Abstractions;

namespace Common.Infrastructure.Core;

public abstract class Repository<TEntity, TDbContext> : IRepository<TEntity>
    where TEntity : Entity, IAggregateRoot where TDbContext : EFContext
{
    protected virtual TDbContext Context { get; set; }

    protected Repository(TDbContext context)
    {
        Context = context;
    }

    public virtual IUnitOfWork UnitOfWork => Context;

    public virtual TEntity Add(TEntity entity)
    {
        return Context.Add(entity).Entity;
    }

    public virtual Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Add(entity));
    }

    public virtual TEntity Update(TEntity entity)
    {
        return Context.Update(entity).Entity;
    }

    public virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Update(entity));
    }

    public virtual bool Remove(Entity entity)
    {
        Context.Remove(entity);
        return true;
    }

    public virtual Task<bool> RemoveAsync(Entity entity)
    {
        return Task.FromResult(Remove(entity));
    }
}

public abstract class Repository<TEntity, TKey, TDbContext> : Repository<TEntity, TDbContext>, IRepository<TEntity,TKey>
    where TEntity : Entity<TKey>, IAggregateRoot where TDbContext : EFContext
{
    protected Repository(TDbContext context) : base(context)
    {
    }

    public virtual bool Delete(TKey id)
    {
        var entity = Context.Find<TEntity>(id);
        if (entity == null)
        {
            return false;
        }
        Context.Remove(entity);
        return true;
    }

    public virtual async Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = await Context.FindAsync<TEntity>(id, cancellationToken);
        if (entity == null)
        {
            return false;
        }
        Context.Remove(entity);
        return true;
    }

    public virtual TEntity? Get(TKey id)
    {
        return Context.Find<TEntity>(id);
    }

    public virtual async Task<TEntity?> GetAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return await Context.FindAsync<TEntity>(id, cancellationToken);
    }
}
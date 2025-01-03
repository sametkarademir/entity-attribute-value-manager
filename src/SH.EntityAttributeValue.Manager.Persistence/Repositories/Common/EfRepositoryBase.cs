using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SH.EntityAttributeValue.Manager.Application.Dtos.Dynamic;
using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Repositories.Common;
using SH.EntityAttributeValue.Manager.Domain.AggrigateRoots;
using SH.EntityAttributeValue.Manager.Persistence.Extensions;

namespace SH.EntityAttributeValue.Manager.Persistence.Repositories.Common;

public class EfRepositoryBase<TEntity, TContext> :
    IRepository<TEntity> where TEntity : AggregateRoot
    where TContext : DbContext
{
    protected readonly TContext Context;

    public EfRepositoryBase(TContext context)
    {
        Context = context;
    }

    public IQueryable<TEntity> Query()
    {
        return Context.Set<TEntity>();
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        var queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);
        
        return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<Paginate<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, int index = 0,
        int size = 10, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        var queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);
        if (predicate != null)
            queryable = queryable.Where(predicate);
        if (orderBy != null)
            queryable = orderBy(queryable);
        
        return await queryable.ToPaginateAsync(index, size, cancellationToken);
    }

    public async Task<Paginate<TEntity>> GetListByDynamicAsync(DynamicQuery dynamic,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0,
        int size = 10, bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        var queryable = Query().ToDynamic(dynamic);
        if (predicate != null)
            queryable = queryable.Where(predicate);
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);

        return await queryable.ToPaginateAsync(index, size, cancellationToken);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null,
        bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        var queryable = Query();
        if (predicate != null)
            queryable = queryable.Where(predicate);
        if (!enableTracking)
            queryable = queryable.AsNoTracking();

        return await queryable.AnyAsync(cancellationToken);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null,
        bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        var queryable = Query();
        if (predicate != null)
            queryable = queryable.Where(predicate);
        if (!enableTracking)
            queryable = queryable.AsNoTracking();

        return await queryable.CountAsync(cancellationToken);
    }
    
        public async Task<TEntity> AddAsync(TEntity entity, bool saveImmediately = false)
    {
        await Context.AddAsync(entity);

        if (saveImmediately)
        {
            await this.SaveChangesAsync();
        }

        return entity;
    }

    public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities, bool saveImmediately = false)
    {
        await Context.AddRangeAsync(entities);

        if (saveImmediately)
        {
            await this.SaveChangesAsync();
        }

        return entities;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, bool saveImmediately = false)
    {
        Context.Update(entity);

        if (saveImmediately)
        {
            await this.SaveChangesAsync();
        }

        return entity;
    }

    public async Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities,
        bool saveImmediately = false)
    {
        Context.UpdateRange(entities);

        if (saveImmediately)
        {
            await this.SaveChangesAsync();
        }

        return entities;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity, bool saveImmediately = false)
    {
        Context.Remove(entity);

        if (saveImmediately)
        {
            await this.SaveChangesAsync();
        }

        return entity;
    }

    public async Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities,
        bool saveImmediately = false)
    {
        Context.RemoveRange(entities);

        if (saveImmediately)
        {
            await this.SaveChangesAsync();
        }

        return entities;
    }
    

    public async Task SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
    }
}
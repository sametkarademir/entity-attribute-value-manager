using SH.EntityAttributeValue.Manager.Domain.AggrigateRoots;

namespace SH.EntityAttributeValue.Manager.Application.Repositories.Common;

public interface IWriteRepository<TEntity> where TEntity : AggregateRoot
{
    Task<TEntity> AddAsync(TEntity entity, bool saveImmediately = false);
    Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities, bool saveImmediately = false);

    Task<TEntity> UpdateAsync(TEntity entity, bool saveImmediately = false);
    Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities, bool saveImmediately = false);

    Task<TEntity> DeleteAsync(TEntity entity, bool saveImmediately = false);
    Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities, bool saveImmediately = false);

    Task SaveChangesAsync();
}
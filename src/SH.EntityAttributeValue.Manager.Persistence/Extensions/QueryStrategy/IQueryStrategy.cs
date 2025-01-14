using System.Linq.Expressions;
using SH.EntityAttributeValue.Manager.Domain.Entities;

namespace SH.EntityAttributeValue.Manager.Persistence.Extensions.QueryStrategy;

public interface IQueryStrategy
{
    IQueryable<Product> Apply(IQueryable<Product> queryable, AttributeFilter filter);
    Expression<Func<Product, bool>> Apply(AttributeFilter filter);
}
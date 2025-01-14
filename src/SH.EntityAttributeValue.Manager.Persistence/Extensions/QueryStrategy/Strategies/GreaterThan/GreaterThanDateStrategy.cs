using System.Linq.Expressions;
using SH.EntityAttributeValue.Manager.Domain.Entities;

namespace SH.EntityAttributeValue.Manager.Persistence.Extensions.QueryStrategy.Strategies.GreaterThan;

public class GreaterThanDateStrategy : IQueryStrategy
{
    public IQueryable<Product> Apply(IQueryable<Product> queryable, AttributeFilter filter)
    {
        return queryable.Where(
            x => x.Values.Any(
                v => 
                    v.AttributeId == filter.AttributeId && 
                    v.AsDate > DateTime.Parse(filter.Content).ToUniversalTime()
            )
        );
    }

    public Expression<Func<Product, bool>> Apply(AttributeFilter filter)
    {
        return x => x.Values.Any(
            v => 
                v.AttributeId == filter.AttributeId && 
                v.AsDate > DateTime.Parse(filter.Content).ToUniversalTime()
        );
    }
}
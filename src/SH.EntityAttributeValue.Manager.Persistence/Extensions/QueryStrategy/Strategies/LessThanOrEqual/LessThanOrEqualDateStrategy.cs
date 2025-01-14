using System.Linq.Expressions;
using SH.EntityAttributeValue.Manager.Domain.Entities;

namespace SH.EntityAttributeValue.Manager.Persistence.Extensions.QueryStrategy.Strategies.LessThanOrEqual;

public class LessThanOrEqualDateStrategy : IQueryStrategy
{
    public IQueryable<Product> Apply(IQueryable<Product> queryable, AttributeFilter filter)
    {
        var date = DateTime.Parse(filter.Content).ToUniversalTime();
        return queryable.Where(
            x => x.Values.Any(
                v => 
                    v.AttributeId == filter.AttributeId && 
                    v.AsDate <= date
            )
        );
    }

    public Expression<Func<Product, bool>> Apply(AttributeFilter filter)
    {
        return x => x.Values.Any(
            v => 
                v.AttributeId == filter.AttributeId && 
                v.AsDate <= DateTime.Parse(filter.Content).ToUniversalTime()
        );
    }
}
using System.Linq.Expressions;
using SH.EntityAttributeValue.Manager.Domain.Entities;

namespace SH.EntityAttributeValue.Manager.Persistence.Extensions.QueryStrategy.Strategies.GreaterThanOrEqual;

public class GreaterThanOrEqualIntegerStrategy : IQueryStrategy
{
    public IQueryable<Product> Apply(IQueryable<Product> queryable, AttributeFilter filter)
    {
        return queryable.Where(
            x => x.Values.Any(
                v => 
                    v.AttributeId == filter.AttributeId && 
                    v.AsInteger >= int.Parse(filter.Content)
            )
        );
    }

    public Expression<Func<Product, bool>> Apply(AttributeFilter filter)
    {
        return x => x.Values.Any(
            v => 
                v.AttributeId == filter.AttributeId && 
                v.AsInteger >= int.Parse(filter.Content)
        );
    }
}
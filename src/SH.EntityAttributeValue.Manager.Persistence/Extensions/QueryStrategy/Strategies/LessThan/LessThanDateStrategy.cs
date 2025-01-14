using System.Linq.Expressions;
using SH.EntityAttributeValue.Manager.Domain.Entities;

namespace SH.EntityAttributeValue.Manager.Persistence.Extensions.QueryStrategy.Strategies.LessThan;

public class LessThanDateStrategy : IQueryStrategy
{
    public IQueryable<Product> Apply(IQueryable<Product> queryable, AttributeFilter filter)
    {
        var date = DateTime.SpecifyKind(
            DateTime.Parse(filter.Content), 
            DateTimeKind.Utc
        ).ToUniversalTime();
        return queryable.Where(
            x => x.Values.Any(
                v => 
                    v.AttributeId == filter.AttributeId && 
                    v.AsDate < date
            )
        );
    }

    public Expression<Func<Product, bool>> Apply(AttributeFilter filter)
    { ;
        return x => x.Values.Any(
            v => 
                v.AttributeId == filter.AttributeId && 
                v.AsDate < DateTime.SpecifyKind(DateTime.Parse(filter.Content), DateTimeKind.Utc).ToUniversalTime()
        );
    }
}
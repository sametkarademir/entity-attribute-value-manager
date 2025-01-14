using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SH.EntityAttributeValue.Manager.Application.Enums;
using SH.EntityAttributeValue.Manager.Domain.Entities;
using SH.EntityAttributeValue.Manager.Domain.Enums;
using SH.EntityAttributeValue.Manager.Persistence.Extensions.QueryStrategy.Strategies.Contains;
using SH.EntityAttributeValue.Manager.Persistence.Extensions.QueryStrategy.Strategies.Equal;
using SH.EntityAttributeValue.Manager.Persistence.Extensions.QueryStrategy.Strategies.GreaterThan;
using SH.EntityAttributeValue.Manager.Persistence.Extensions.QueryStrategy.Strategies.GreaterThanOrEqual;
using SH.EntityAttributeValue.Manager.Persistence.Extensions.QueryStrategy.Strategies.LessThan;
using SH.EntityAttributeValue.Manager.Persistence.Extensions.QueryStrategy.Strategies.LessThanOrEqual;
using SH.EntityAttributeValue.Manager.Persistence.Extensions.QueryStrategy.Strategies.NotEqual;

namespace SH.EntityAttributeValue.Manager.Persistence.Extensions.QueryStrategy;

public class QueryBuilder
{
    private readonly Dictionary<(OperatorTypes, DataTypes), IQueryStrategy> _strategies;
    
    public QueryBuilder()
    {
        _strategies = new Dictionary<(OperatorTypes, DataTypes), IQueryStrategy>
        {
            { (OperatorTypes.Eq, DataTypes.String), new EqualStringStrategy() },
            { (OperatorTypes.Eq, DataTypes.Boolean), new EqualBooleanStrategy() },
            { (OperatorTypes.Eq, DataTypes.Date), new EqualDateStrategy() },
            { (OperatorTypes.Eq, DataTypes.Integer), new EqualIntegerStrategy() },
            { (OperatorTypes.Eq, DataTypes.Decimal), new EqualDecimalStrategy() },
            
            { (OperatorTypes.Neq, DataTypes.String), new NotEqualStringStrategy() },
            { (OperatorTypes.Neq, DataTypes.Boolean), new NotEqualBooleanStrategy() },
            { (OperatorTypes.Neq, DataTypes.Date), new NotEqualDateStrategy() },
            { (OperatorTypes.Neq, DataTypes.Integer), new NotEqualIntegerStrategy() },
            { (OperatorTypes.Neq, DataTypes.Decimal), new NotEqualDecimalStrategy() },
            
            { (OperatorTypes.Lt, DataTypes.Date), new LessThanDateStrategy() },
            { (OperatorTypes.Lt, DataTypes.Integer), new LessThanIntegerStrategy() },
            { (OperatorTypes.Lt, DataTypes.Decimal), new LessThanDecimalStrategy() },
            
            { (OperatorTypes.Lte, DataTypes.Date), new LessThanOrEqualDateStrategy() },
            { (OperatorTypes.Lte, DataTypes.Integer), new LessThanOrEqualIntegerStrategy() },
            { (OperatorTypes.Lte, DataTypes.Decimal), new LessThanOrEqualDecimalStrategy() },
            
            { (OperatorTypes.Gt, DataTypes.Date), new GreaterThanDateStrategy() },
            { (OperatorTypes.Gt, DataTypes.Integer), new GreaterThanIntegerStrategy() },
            { (OperatorTypes.Gt, DataTypes.Decimal), new GreaterThanDecimalStrategy() },
            
            { (OperatorTypes.Gte, DataTypes.Date), new GreaterThanOrEqualDateStrategy() },
            { (OperatorTypes.Gte, DataTypes.Integer), new GreaterThanOrEqualIntegerStrategy() },
            { (OperatorTypes.Gte, DataTypes.Decimal), new GreaterThanOrEqualDecimalStrategy() },
            
            { (OperatorTypes.Contains, DataTypes.String), new ContainsStringStrategy() },
        };
    }
    
    public IQueryable<Product> ApplyFilter(IQueryable<Product> queryable, AttributeFilter filter)
    {
        var key = (filter.OperatorType, filter.DataType);
        if (_strategies.TryGetValue(key, out var strategy))
        {
            return strategy.Apply(queryable, filter);
        }

        throw new NotSupportedException($"Combination of '{filter.OperatorType}' and '{filter.DataType}' is not supported.");
    }
    
    public Expression<Func<Product, bool>> BuildExpression(AttributeFilter filter)
    {
        var key = (filter.OperatorType, filter.DataType);
        if (_strategies.TryGetValue(key, out var strategy))
        {
            return strategy.Apply(filter);
        }

        throw new NotSupportedException($"Combination of '{filter.OperatorType}' and '{filter.DataType}' is not supported.");
    }
}
using System.Linq.Expressions;
using SH.EntityAttributeValue.Manager.Application.Enums;
using SH.EntityAttributeValue.Manager.Domain.Entities;
using SH.EntityAttributeValue.Manager.Persistence.Extensions.QueryStrategy;

namespace SH.EntityAttributeValue.Manager.Persistence.Extensions;

public static class QueryableApplyDynamicFilterExtensions
{
    public static IQueryable<Product> ApplyEavFilter(this IQueryable<Product> queryable, List<AttributeFilter> attributes)
    {
        QueryBuilder queryBuilder = new();
        
        Expression<Func<Product, bool>>? combinedExpression = null;

        attributes = attributes.OrderBy(item => item.LogicType).ToList();
        
        for (int i = 0; i < attributes.Count; i++)
        {
            var attributeFilter = attributes[i];
            var filterExpression = queryBuilder.BuildExpression(attributeFilter);

            if (i == 0)
            {
                combinedExpression = filterExpression;
            }
            else
            {
                combinedExpression = attributes[i-1].LogicType == LogicTypes.Or
                    ? combinedExpression.Or(filterExpression)
                    : combinedExpression.And(filterExpression);
            }
        }
        
        return combinedExpression != null
            ? queryable.Where(combinedExpression)
            : queryable;
    }
}

public static class ExpressionHelper
{
    public static Expression<Func<T, bool>> Or<T>(
        this Expression<Func<T, bool>> first,
        Expression<Func<T, bool>> second)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var body = Expression.OrElse(
            Expression.Invoke(first, parameter),
            Expression.Invoke(second, parameter));
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> first,
        Expression<Func<T, bool>> second)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var body = Expression.AndAlso(
            Expression.Invoke(first, parameter),
            Expression.Invoke(second, parameter));
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }
}
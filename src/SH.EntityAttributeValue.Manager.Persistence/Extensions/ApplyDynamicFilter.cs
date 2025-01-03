using System.Globalization;
using System.Linq.Expressions;
using SH.EntityAttributeValue.Manager.Application.Dtos.Products;
using SH.EntityAttributeValue.Manager.Application.Enums;
using SH.EntityAttributeValue.Manager.Domain.Entities;

namespace SH.EntityAttributeValue.Manager.Persistence.Extensions;

public static class QueryableApplyDynamicFilter
{
    public static IQueryable<Product> ApplyDynamicFilter(this IQueryable<Product> queryable,
        List<FilterAttributeDto> attributes)
    {
        if (!attributes.Any())
            return queryable;

        Expression<Func<Product, bool>> combinedExpression = p => true;

        foreach (var attribute in attributes)
        {
            var expression = GetDynamicExpression(attribute);
            
            combinedExpression = CombineExpressions(combinedExpression, expression, attribute);
        }
        
        return queryable.Where(combinedExpression);
    }

    private static Expression<Func<Value, bool>> GetDynamicExpression(FilterAttributeDto attribute)
    {
        Expression<Func<Value, bool>> expression = v => true;

        switch (attribute.OperatorType)
        {
            case OperatorTypes.Eq:
                expression = v => v.AttributeId == attribute.AttributeId && v.Content == attribute.Content;
                break;
            case OperatorTypes.Neq:
                expression = v => v.AttributeId == attribute.AttributeId && v.Content != attribute.Content;
                break;
            case OperatorTypes.Lt:
                expression = v =>
                    v.AttributeId == attribute.AttributeId && string.Compare(v.Content, attribute.Content) < 0;
                break;
            case OperatorTypes.Lte:
                expression = v =>
                    v.AttributeId == attribute.AttributeId && string.Compare(v.Content, attribute.Content) <= 0;
                break;
            case OperatorTypes.Gt:
                expression = v =>
                    v.AttributeId == attribute.AttributeId && string.Compare(v.Content, attribute.Content) > 0;
                break;
            case OperatorTypes.Gte:
                expression = v =>
                    v.AttributeId == attribute.AttributeId && string.Compare(v.Content, attribute.Content) >= 0;
                break;
            case OperatorTypes.IsNull:
                expression = v => v.AttributeId == attribute.AttributeId && v.Content == null;
                break;
            case OperatorTypes.IsNotNull:
                expression = v => v.AttributeId == attribute.AttributeId && v.Content != null;
                break;
            case OperatorTypes.StartsWith:
                expression = v =>
                    v.AttributeId == attribute.AttributeId && v.Content != null &&
                    v.Content.StartsWith(attribute.Content);
                break;
            case OperatorTypes.EndsWith:
                expression = v =>
                    v.AttributeId == attribute.AttributeId && v.Content != null &&
                    v.Content.EndsWith(attribute.Content);
                break;
            case OperatorTypes.Contains:
                expression = v =>
                    v.AttributeId == attribute.AttributeId && v.Content != null &&
                    v.Content.Contains(attribute.Content);
                break;
            case OperatorTypes.DoesNotContain:
                expression = v =>
                    v.AttributeId == attribute.AttributeId && v.Content != null &&
                    !v.Content.Contains(attribute.Content);
                break;
            default:
                throw new NotSupportedException($"Operator type {attribute.OperatorType} is not supported.");
        }

        return expression;
    }

    private static Expression<Func<Product, bool>> CombineExpressions(
        Expression<Func<Product, bool>> productExpression,
        Expression<Func<Value, bool>> valueExpression,
        FilterAttributeDto attribute)
    {
        var parameter = Expression.Parameter(typeof(Product), "p");
        
        var anyCall = Expression.Call(
            typeof(Enumerable),
            nameof(Enumerable.Any),
            new[] { typeof(Value) },
            Expression.Property(parameter, nameof(Product.Values)),
            valueExpression
        );
        
        var productBody =
            new ReplaceParameterVisitor(productExpression.Parameters[0], parameter).Visit(productExpression.Body);
        
        Expression combined = attribute.LogicType switch
        {
            LogicTypes.Or => Expression.OrElse(productBody, anyCall),
            _ => Expression.AndAlso(productBody, anyCall) // LogicTypes.And
        };

        return Expression.Lambda<Func<Product, bool>>(combined, parameter);
    }
    
    private class ReplaceParameterVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _oldParameter;
        private readonly ParameterExpression _newParameter;

        public ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
        {
            _oldParameter = oldParameter;
            _newParameter = newParameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == _oldParameter ? _newParameter : base.VisitParameter(node);
        }
    }
}
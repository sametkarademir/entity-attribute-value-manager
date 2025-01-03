using System.Text;
using System.Linq.Dynamic.Core;
using SH.EntityAttributeValue.Manager.Application.Dtos.Dynamic;
using SH.EntityAttributeValue.Manager.Application.Enums;

namespace SH.EntityAttributeValue.Manager.Persistence.Extensions;

public static class QueryableDynamicFilterExtensions
{
    private static readonly IDictionary<OperatorTypes, string> Operators = new Dictionary<OperatorTypes, string>
    {
        { OperatorTypes.Eq, "=" },
        { OperatorTypes.Neq, "!=" },
        { OperatorTypes.Lt, "<" },
        { OperatorTypes.Lte, "<=" },
        { OperatorTypes.Gt, ">" },
        { OperatorTypes.Gte, ">=" },
        { OperatorTypes.IsNull, "== null" },
        { OperatorTypes.IsNotNull, "!= null" },
        { OperatorTypes.StartsWith, "StartsWith" },
        { OperatorTypes.EndsWith, "EndsWith" },
        { OperatorTypes.Contains, "Contains" },
        { OperatorTypes.DoesNotContain, "Contains" }
    };

    public static IQueryable<T> ToDynamic<T>(this IQueryable<T> query, DynamicQuery dynamicQuery)
    {
        if (dynamicQuery.Filter is not null)
            query = ToFilter(query, dynamicQuery.Filter);
        if (dynamicQuery.Sort is not null && dynamicQuery.Sort.Any())
            query = ToSort(query, dynamicQuery.Sort);
        return query;
    }

    public static IQueryable<T> ToFilter<T>(IQueryable<T> queryable, Filter filter)
    {
        var filters = GetAllFilters(filter);
        var values = filters.Select(f => f.Value).ToArray();
        var where = Transform(filter, filters);
        if (!string.IsNullOrEmpty(where) && values != null)
            queryable = queryable.Where(where, values);

        return queryable;
    }

    public static IQueryable<T> ToSort<T>(IQueryable<T> queryable, IEnumerable<Sort> sort)
    {
        foreach (var item in sort)
        {
            if (string.IsNullOrEmpty(item.Field))
                throw new InvalidOperationException("Invalid Field");
        }

        if (sort.Any())
        {
            var ordering = string.Join(",", sort.Select(s => $"{s.Field} {s.Dir.ToString()}"));
            return queryable.OrderBy(ordering);
        }

        return queryable;
    }

    public static IList<Filter> GetAllFilters(Filter filter)
    {
        List<Filter> filters = new();
        GetFilters(filter, filters);
        return filters;
    }

    private static void GetFilters(Filter filter, IList<Filter> filters)
    {
        filters.Add(filter);
        if (filter.Filters is not null && filter.Filters.Any())
            foreach (var item in filter.Filters)
                GetFilters(item, filters);
    }

    public static string Transform(Filter filter, IList<Filter> filters)
    {
        if (string.IsNullOrEmpty(filter.Field))
            throw new InvalidOperationException("Invalid Field");
        if (!Operators.ContainsKey(filter.Operator))
            throw new InvalidOperationException("Invalid Operator");

        var index = filters.IndexOf(filter);
        var comparison = Operators[filter.Operator];
        StringBuilder where = new();

        if (filter.Field.Contains(".Any"))
        {
            var fieldParts = filter.Field.Split(new[] { ".Any(" }, StringSplitOptions.None);
            var rootCollection = fieldParts[0]; // e.g., "Values"
            var subField = fieldParts[1].TrimEnd(')'); // e.g., "AttributeId"

            if (filter.Operator == OperatorTypes.DoesNotContain)
            {
                where.Append($"NOT EXISTS (SELECT 1 FROM np({rootCollection}) WHERE np({rootCollection}).{subField} {comparison} @{index})");
            }
            else
            {
                where.Append($"EXISTS (SELECT 1 FROM np({rootCollection}) WHERE np({rootCollection}).{subField} {comparison} @{index})");
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(filter.Value))
            {
                if (filter.Operator == OperatorTypes.DoesNotContain)
                    where.Append($"(!np({filter.Field}).{comparison}(@{index.ToString()}))");
                else if (comparison is "StartsWith" or "EndsWith" or "Contains")
                    where.Append($"(np({filter.Field}).{comparison}(@{index.ToString()}))");
                else
                    where.Append($"np({filter.Field}) {comparison} @{index.ToString()}");
            }
            else if (filter.Operator is OperatorTypes.IsNull or OperatorTypes.IsNotNull)
            {
                where.Append($"np({filter.Field}) {comparison}");
            }
        }

        if (filter.Logic is not null && filter.Filters is not null && filter.Filters.Any())
        {
            return
                $"{where} {filter.Logic.ToString()} ({string.Join($" {filter.Logic.ToString()} ", filter.Filters.Select(f => Transform(f, filters)))})";
        }

        return where.ToString();
    }
}
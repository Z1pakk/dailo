using System.Linq.Dynamic.Core;

namespace DevHabit.Api.Services.Sorting;

internal static class QueryableExtensions
{
    public static IQueryable<T> ApplySort<T>(
        this IQueryable<T> query,
        string? sort,
        SortMapping[] mappings,
        string defaultOrderBy = "Id"
    )
    {
        if (string.IsNullOrEmpty(sort))
        {
            return query.OrderBy(defaultOrderBy);
        }

        IEnumerable<string> orderByParts = sort.Split(',')
            .Select(s => s.Trim())
            .Where(s => !string.IsNullOrEmpty(s))
            .Select(s =>
            {
                var (sortField, isDescending) = ParseSortField(s);
                SortMapping? mapping = mappings.FirstOrDefault(m =>
                    m.SortField.Equals(sortField, StringComparison.OrdinalIgnoreCase)
                );

                if (mapping is null)
                {
                    throw new InvalidOperationException(
                        $"No mapping found for sort field '{sortField}'"
                    );
                }

                string propertyName = mapping.PropertyName;
                if (mapping.IsReverse)
                {
                    isDescending = !isDescending;
                }

                return isDescending ? $"{propertyName} DESC" : propertyName;
            });

        string orderByClause = string.Join(", ", orderByParts);
        return query.OrderBy(orderByClause);
    }

    private static (string SortField, bool IsDescending) ParseSortField(string field)
    {
        string[] parts = field.Split(' ');
        string sortField = parts[0];
        bool isDescending =
            parts.Length > 1 && string.Equals(parts[1], "desc", StringComparison.OrdinalIgnoreCase);

        return (sortField, isDescending);
    }
}

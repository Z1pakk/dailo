namespace DevHabit.Api.Services.Sorting;

public sealed class SortMappingProvider(IEnumerable<ISortMappingDefinition> definitions)
{
    public SortMapping[] GetMappings<TSource, TDestination>()
    {
        SortMappingDefinition<TSource, TDestination> sortMappingDefinition = definitions
            .OfType<SortMappingDefinition<TSource, TDestination>>()
            .FirstOrDefault();

        if (sortMappingDefinition is null)
        {
            throw new InvalidOperationException(
                $"No sort mapping definition found for {typeof(TSource).Name} to {typeof(TDestination).Name}"
            );
        }

        return sortMappingDefinition.Mappings;
    }

    public bool ValidateMappings<TSource, TDestination>(string? sortFields)
    {
        if (string.IsNullOrEmpty(sortFields))
        {
            return true; // No sorting specified, so no validation needed
        }

        SortMapping[] mappings = GetMappings<TSource, TDestination>();
        IEnumerable<string> sortFieldsList = sortFields
            .Split(',')
            .Select(s => s.Trim())
            .Where(s => !string.IsNullOrEmpty(s))
            .Select(s => s.Split(' ')[0]); // Get only the field part, ignoring direction

        return sortFieldsList.All(sortField =>
            mappings.Any(mapping =>
                mapping.SortField.Equals(sortField, StringComparison.OrdinalIgnoreCase)
            )
        );
    }
}

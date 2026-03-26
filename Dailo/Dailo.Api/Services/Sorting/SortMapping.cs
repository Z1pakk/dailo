namespace Dailo.Api.Services.Sorting;

/// <summary>
/// Sort Mapping to defined how dto's property should be sorted in the internal contract.
/// </summary>
/// <param name="SortField"></param>
/// <param name="PropertyName"></param>
/// <param name="IsReverse">When sorting should be reversed. e.g the field 'Age ASC' must sort by 'DateOfBirth DESC'</param>
public sealed record SortMapping(string SortField, string PropertyName, bool IsReverse = false);

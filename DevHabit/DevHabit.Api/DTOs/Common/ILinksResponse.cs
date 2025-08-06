namespace DevHabit.Api.DTOs.Common;

public interface ILinksResponse
{
    IEnumerable<LinkDto> Links { get; set; }
}

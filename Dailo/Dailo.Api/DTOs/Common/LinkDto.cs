namespace Dailo.Api.DTOs.Common;

public sealed record LinkDto
{
    public required string Href { get; set; }

    public required string Rel { get; set; }

    public required string Method { get; set; }
}

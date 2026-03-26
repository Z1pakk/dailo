using Dailo.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Dailo.Api.DTOs.Common;

public record AcceptHeaderDto
{
    [FromHeader(Name = "Accept")]
    public required string AcceptHeader { get; set; }

    public bool IsLinksIncluded
    {
        get
        {
            if (!MediaTypeHeaderValue.TryParse(AcceptHeader, out MediaTypeHeaderValue? mediaType))
            {
                return false;
            }

            return mediaType.Type == CustomMediaTypeNames.Application.HateoasJsonMediaType.Type
                && mediaType.SubTypeWithoutSuffix
                    == CustomMediaTypeNames.Application.HateoasJsonMediaType.SubTypeWithoutSuffix;
        }
    }
}

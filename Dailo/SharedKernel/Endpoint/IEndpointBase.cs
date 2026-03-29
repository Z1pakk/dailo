using Microsoft.AspNetCore.Routing;

namespace SharedKernel.Endpoint;

public interface IEndpointBase
{
    void MapEndpoint(IEndpointRouteBuilder app);
}

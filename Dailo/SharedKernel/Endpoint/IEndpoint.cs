using Microsoft.AspNetCore.Http;

namespace SharedKernel.Endpoint;

public interface IEndpoint<in TRequest, TResponse> : IEndpointBase
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Dailo.Infrastructure.ProblemDetails;

public sealed class GlobalExceptionHandler(IProblemDetailsService problemDetailsService)
    : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        return problemDetailsService.TryWriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails()
                {
                    Title = "Internal Server Error",
                    Detail =
                        "An unexpected error occurred while processing your request. Please try again later.",
                    Status = StatusCodes.Status500InternalServerError,
                },
            }
        );
    }
}

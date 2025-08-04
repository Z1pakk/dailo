using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DevHabit.Api.Middleware;

public sealed class ValidationExceptionHandler(IProblemDetailsService problemDetailsService)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        if (exception is not ValidationException validationException)
        {
            return false;
        }

        var errors = validationException
            .Errors.GroupBy(e => e.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        var context = new ProblemDetailsContext()
        {
            HttpContext = httpContext,
            Exception = validationException,
            ProblemDetails = new ProblemDetails()
            {
                Title = "Validation Error",
                Detail = "One or more validation errors occurred.",
                Status = StatusCodes.Status400BadRequest,
                Extensions = new Dictionary<string, object?> { ["errors"] = errors },
            },
        };

        return await problemDetailsService.TryWriteAsync(context);
    }
}

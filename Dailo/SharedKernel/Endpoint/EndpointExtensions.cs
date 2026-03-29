using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace SharedKernel.Endpoint;

public static class EndpointExtensions
{
    public static IServiceCollection AddEndpoints(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Scoped,
        params Assembly[] assemblies
    )
    {
        services.Scan(scan =>
            scan.FromAssemblies(assemblies)
                .AddClasses(classes => classes.AssignableTo<IEndpointBase>())
                .UsingRegistrationStrategy(RegistrationStrategy.Append)
                .As<IEndpointBase>()
                .WithLifetime(lifetime)
        );

        return services;
    }

    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
    {
        using var scope = builder.ServiceProvider.CreateScope();

        var endpoints = scope.ServiceProvider.GetRequiredService<IEnumerable<IEndpointBase>>();

        IEndpointRouteBuilder apiBuilder = builder.MapGroup("/api");

        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(apiBuilder);
        }

        return builder;
    }
}

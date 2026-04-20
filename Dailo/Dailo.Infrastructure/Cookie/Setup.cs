using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Cookie;

namespace Dailo.Infrastructure.Cookie;

public static class Setup
{
    public static IServiceCollection AddCookieServices(this IServiceCollection services)
    {
        services.AddScoped<ICookieService, CookieService>();

        return services;
    }
}

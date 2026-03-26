using Microsoft.Extensions.DependencyInjection;
using SharedKernel.User;

namespace Dailo.Infrastructure.User;

public static class UserSetup
{
    public static IServiceCollection AddUserServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}

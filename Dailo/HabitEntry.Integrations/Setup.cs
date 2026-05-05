using Microsoft.Extensions.DependencyInjection;

namespace HabitEntry.Integrations;

public static class Setup
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddHabitEntryIntegrations()
        {
            return services;
        }
    }
}

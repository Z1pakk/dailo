using Microsoft.EntityFrameworkCore;
using SharedKernel.Persistence.Conventions;
using SharedKernel.Persistence.Interceptors;
using SharedKernel.User;

namespace SharedKernel.Persistence;

public abstract class AppDbContextBase(
    DbContextOptions options,
    ICurrentUserService? currentUserService = null,
    TimeProvider? timeProvider = null
) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new VersionInterceptor());

        // Only add audit interceptor if dependencies are available (runtime scenario)
        if (currentUserService is not null && timeProvider is not null)
        {
            optionsBuilder.AddInterceptors(new AuditInterceptor(currentUserService, timeProvider));
        }

        base.OnConfiguring(optionsBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        // Order matters: Pluralization must run before SnakeCaseNaming
        configurationBuilder.Conventions.Add(_ => new TablePluralizationConvention());
        configurationBuilder.Conventions.Add(_ => new DefaultStringLengthConvention());
        configurationBuilder.Conventions.Add(_ => new SoftDeleteConvention());
    }
}

using Identity.Application.Persistence;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Persistence.Conventions;
using SharedKernel.Persistence.Extensions;
using SharedKernel.Persistence.Interceptors;
using SharedKernel.User;

namespace Identity.Infrastructure.Database;

public sealed class IdentityDbContext(
    DbContextOptions<IdentityDbContext> options,
    ICurrentUserService? currentUserService = null,
    TimeProvider? timeProvider = null
)
    : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<
        User,
        Role,
        Guid,
        UserClaim,
        UserRole,
        UserLogin,
        RoleClaim,
        UserToken
    >(options),
        IIdentityDbContext
{
    public string Schema => IdentitySchema.NAME;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(Schema);

        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
        builder.ToSnakeCaseTables();
    }

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

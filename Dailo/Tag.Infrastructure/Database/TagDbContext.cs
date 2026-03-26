using Microsoft.EntityFrameworkCore;
using SharedKernel.Persistence;
using SharedKernel.User;
using Tag.Application.Persistence;

namespace Tag.Infrastructure.Database;

public sealed class TagDbContext(
    DbContextOptions options,
    ICurrentUserService? currentUserService = null,
    TimeProvider? timeProvider = null
) : AppDbContextBase(options, currentUserService, timeProvider), ITagDbContext
{
    public string Schema => TagSchema.NAME;

    public DbSet<Domain.Entities.Tag> Tags => Set<Domain.Entities.Tag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

        base.OnModelCreating(modelBuilder);
    }
}

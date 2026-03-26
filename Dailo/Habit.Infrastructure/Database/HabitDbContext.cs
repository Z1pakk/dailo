using Habit.Application.Persistence;
using Habit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Persistence;
using SharedKernel.User;

namespace Habit.Infrastructure.Database;

public sealed class HabitDbDbContext(
    DbContextOptions<HabitDbDbContext> options,
    ICurrentUserService? currentUserService = null,
    TimeProvider? timeProvider = null
) : AppDbContextBase(options, currentUserService, timeProvider), IHabitDbContext
{
    public string Schema => HabitSchema.NAME;

    public DbSet<Domain.Entities.Habit> Habits => Set<Domain.Entities.Habit>();

    public DbSet<HabitTag> HabitTags => Set<HabitTag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

        base.OnModelCreating(modelBuilder);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Persistence;

namespace Habit.Infrastructure.Database.Configurations;

internal sealed class HabitConfiguration : BaseEntityTypedConfiguration<Domain.Entities.Habit>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Domain.Entities.Habit> builder)
    {
        builder.Property(h => h.Name).HasMaxLength(100);
        builder.Property(h => h.Description).HasMaxLength(500);
        builder.Property(h => h.IsArchived).HasDefaultValue(false);

        builder.OwnsOne(h => h.Frequency);

        builder.OwnsOne(
            h => h.Target,
            targetBuilder =>
            {
                targetBuilder.Property(t => t.Unit).HasMaxLength(100);
            }
        );
        builder.OwnsOne(h => h.Milestone);
    }
}

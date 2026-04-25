using Habit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Persistence;

namespace Habit.Infrastructure.Database.Configurations;

internal sealed class HabitConfiguration : BaseEntityTypedConfiguration<HabitEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<HabitEntity> builder)
    {
        builder.ToTable("habits");

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

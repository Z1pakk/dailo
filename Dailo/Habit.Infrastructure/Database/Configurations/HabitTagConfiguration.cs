using Habit.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Persistence;

namespace Habit.Infrastructure.Database.Configurations;

internal sealed class HabitTagConfiguration : BaseEntityConfiguration<HabitTag>
{
    protected override void ConfigureEntity(EntityTypeBuilder<HabitTag> builder)
    {
        builder
            .HasIndex(b => new
            {
                b.HabitId,
                b.TagId,
                b.UserId,
            })
            .IsUnique();
    }
}

using Habit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Persistence;

namespace Habit.Application.Persistence;

public interface IHabitDbContext : IAppDbContextBase
{
    DbSet<Domain.Entities.Habit> Habits { get; }

    DbSet<HabitTag> HabitTags { get; }
}

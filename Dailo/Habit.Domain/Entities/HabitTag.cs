using StrictId;

namespace Habit.Domain.Entities;

public class HabitTag : BaseEntity<Id<HabitTag>>
{
    public required Guid HabitId { get; set; }

    public required Guid TagId { get; set; }

    public required Guid UserId { get; set; }
}

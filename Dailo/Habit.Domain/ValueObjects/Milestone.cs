namespace Habit.Domain.ValueObjects;

public sealed class Milestone
{
    public required int Target { get; set; }
    public required int Current { get; set; }
}

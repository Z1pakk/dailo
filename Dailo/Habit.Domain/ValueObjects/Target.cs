namespace Habit.Domain.ValueObjects;

public sealed class Target
{
    public required int Value { get; set; }

    public required string Unit { get; set; }
}

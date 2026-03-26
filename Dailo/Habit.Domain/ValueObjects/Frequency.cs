using Habit.Domain.Enums;

namespace Habit.Domain.ValueObjects;

public sealed class Frequency
{
    public required FrequencyType Type { get; set; }

    public required int TimesPerPeriod { get; set; }
}

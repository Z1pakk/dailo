using DevHabit.Api.Entities;

namespace DevHabit.Api.DTOs.Habits;

internal static class HabitMappings
{
    public static HabitDto ToDto(this Habit habit)
    {
        return new HabitDto
        {
            Id = habit.Id,
            Name = habit.Name,
            Description = habit.Description,
            Type = habit.Type,
            Frequency = new FrequencyDto
            {
                Type = habit.Frequency.Type,
                TimesPerPeriod = habit.Frequency.TimesPerPeriod,
            },
            Target = new TargetDto { Value = habit.Target.Value, Unit = habit.Target.Unit },
            Status = habit.Status,
            IsArchived = habit.IsArchived,
            EndDAte = habit.EndDAte,
            Milestone =
                habit.Milestone != null
                    ? new MilestoneDto
                    {
                        Target = habit.Milestone.Target,
                        Current = habit.Milestone.Current,
                    }
                    : null,
            CreatedAtUtc = habit.CreatedAtUtc,
            UpdatedAtUtc = habit.UpdatedAtUtc,
            LastCompletedAtUtc = habit.LastCompletedAtUtc,
        };
    }

    public static Habit ToEntity(this CreateHabitDto createHabitDto)
    {
        return new Habit
        {
            Id = $"h_{Guid.CreateVersion7()}",
            Name = createHabitDto.Name,
            Description = createHabitDto.Description,
            Type = createHabitDto.Type,
            Frequency = new Frequency
            {
                Type = createHabitDto.Frequency.Type,
                TimesPerPeriod = createHabitDto.Frequency.TimesPerPeriod,
            },
            Target = new Target
            {
                Value = createHabitDto.Target.Value,
                Unit = createHabitDto.Target.Unit,
            },
            Status = HabitStatus.Ongoing,
            IsArchived = false,
            EndDAte = createHabitDto.EndDAte,
            Milestone =
                createHabitDto.Milestone != null
                    ? new Milestone
                    {
                        Target = createHabitDto.Milestone.Target,
                        Current = createHabitDto.Milestone.Current,
                    }
                    : null,
            CreatedAtUtc = DateTime.UtcNow,
        };
    }

    public static void UpdateFromDto(this Habit habit, UpdateHabitDto updateHabitDto)
    {
        habit.Name = updateHabitDto.Name;
        habit.Description = updateHabitDto.Description;
        habit.Type = updateHabitDto.Type;
        habit.Frequency = new Frequency
        {
            Type = updateHabitDto.Frequency.Type,
            TimesPerPeriod = updateHabitDto.Frequency.TimesPerPeriod,
        };
        habit.Target = new Target
        {
            Value = updateHabitDto.Target.Value,
            Unit = updateHabitDto.Target.Unit,
        };
        habit.EndDAte = updateHabitDto.EndDAte;

        if (updateHabitDto.Milestone is not null)
        {
            habit.Milestone ??= new Milestone() { Target = 0, Current = 0 };
            habit.Milestone.Target = updateHabitDto.Milestone.Target;
        }

        habit.UpdatedAtUtc = DateTime.UtcNow;
    }
}

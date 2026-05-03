using Habit.Application.IntegratedServices;
using Habit.Application.Models;
using Habit.Application.Persistence;
using Habit.Domain.Aggregates;
using Habit.Domain.Entities;
using Habit.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using SharedKernel.CQRS;
using SharedKernel.ResultPattern;
using SharedKernel.User;
using StrictId;

namespace Habit.Application.Features.UpdateHabit;

public sealed record UpdateHabitCommand(
    Id<HabitModel> HabitId,
    string Name,
    string? Description,
    HabitType Type,
    FrequencyModel Frequency,
    TargetModel Target,
    DateOnly? EndDate,
    MilestoneModel? Milestone,
    IEnumerable<Id<TagModel>> TagIds
) : ICommand<Result>;

public sealed class UpdateHabitCommandHandler(
    IHabitDbContext dbContext,
    ICurrentUserService currentUserService,
    ITagService tagService
) : ICommandHandler<UpdateHabitCommand, Result>
{
    public async ValueTask<Result> Handle(
        UpdateHabitCommand request,
        CancellationToken cancellationToken
    )
    {
        var habitId = new Id<HabitEntity>(request.HabitId.Value);

        var entity = await dbContext.Habits.FirstOrDefaultAsync(
            h => h.Id == habitId && h.UserId == currentUserService.UserId,
            cancellationToken
        );

        if (entity is null)
        {
            return Result.NotFound("Habit not found.");
        }

        var requestedTagIds = request.TagIds.ToHashSet();
        var tags = await tagService.GetByIdsAsync(requestedTagIds, cancellationToken);
        var existingTagIds = tags.Keys.Select(k => k.ToId()).ToHashSet();

        var habitResult = HabitAggregate.Create(
            new Id<HabitAggregate>(entity.Id.Value),
            entity.UserId,
            request.Name,
            request.Description,
            request.Type,
            request.Frequency.Type,
            request.Frequency.TimesPerPeriod,
            request.Target.Value,
            request.Target.Unit,
            request.EndDate,
            request.Milestone?.Target,
            request.Milestone?.Current,
            requestedTagIds.Select(id => id.ToId()).ToHashSet(),
            existingTagIds,
            entity.LastCompletedAtUtc
        );

        if (habitResult.IsFailure)
        {
            return habitResult.ToPlainResult();
        }

        var validated = habitResult.Value.ToEntity();

        entity.Name = validated.Name;
        entity.Description = validated.Description;
        entity.Type = validated.Type;
        entity.Frequency = validated.Frequency;
        entity.Target = validated.Target;
        entity.EndDate = validated.EndDate;
        entity.Milestone = validated.Milestone;
        entity.LastCompletedAtUtc = validated.LastCompletedAtUtc;

        var oldTags = await dbContext
            .HabitTags.Where(t => t.HabitId == habitId)
            .ToListAsync(cancellationToken);

        dbContext.HabitTags.RemoveRange(oldTags);
        dbContext.HabitTags.AddRange(validated.Tags);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

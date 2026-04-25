using Habit.Application.Models;
using Habit.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using SharedKernel.CQRS;
using SharedKernel.ResultPattern;

namespace Habit.Application.Features.GetHabits;

public sealed class GetHabitsQuery : IQuery<Result<GetHabitsQueryResponse>> { }

public sealed record GetHabitsQueryResponse(IEnumerable<HabitModel> Habits);

public sealed class GetHabitsQueryHandler(IHabitDbContext habitDbContext)
    : IQueryHandler<GetHabitsQuery, Result<GetHabitsQueryResponse>>
{
    public async ValueTask<Result<GetHabitsQueryResponse>> Handle(
        GetHabitsQuery request,
        CancellationToken cancellationToken
    )
    {
        var habits = await habitDbContext
            .Habits.Select(h => new HabitModel()
            {
                Id = h.Id.ToGuid(),
                Name = h.Name,
                Description = h.Description,
                CreatedAtUtc = h.CreatedAtUtc,
                LastModifiedAtUtc = h.LastModifiedAtUtc,
            })
            .ToListAsync(cancellationToken);

        return Result<GetHabitsQueryResponse>.Success(new GetHabitsQueryResponse(habits));
    }
}

using Mediator;
using SharedKernel.ResultPattern;

namespace Habit.Application.Features.CreateHabit;

public sealed record CreateHabitCommand(string Name, string Description)
    : IRequest<Result<CreateHabitCommandResponse>> { }

public sealed record CreateHabitCommandResponse(Guid Id);

public sealed class CreateHabitCommandHandler
    : IRequestHandler<CreateHabitCommand, Result<CreateHabitCommandResponse>>
{
    public async ValueTask<Result<CreateHabitCommandResponse>> Handle(
        CreateHabitCommand request,
        CancellationToken cancellationToken
    )
    {
        var habit = new CreateHabitCommandResponse(Guid.NewGuid());

        return Result<CreateHabitCommandResponse>.Success(habit);
    }
}

using SharedKernel.CQRS;
using SharedKernel.ResultPattern;

namespace Habit.Application.Features.CreateHabit;

public sealed record CreateHabitCommand(string Name, string Description)
    : ICommand<Result<CreateHabitCommandResponse>> { }

public sealed record CreateHabitCommandResponse(Guid Id);

public sealed class CreateHabitCommandHandler()
    : ICommandHandler<CreateHabitCommand, Result<CreateHabitCommandResponse>>
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

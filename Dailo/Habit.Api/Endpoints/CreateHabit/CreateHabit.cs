using Habit.Application.Features.CreateHabit;
using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharedKernel.Endpoint;

namespace Habit.Api.Endpoints.CreateHabit;

public sealed record CreateHabitRequest(string Name, string Description);

public sealed record CreateHabitResponse(Guid Id);

public sealed class CreateHabit(ISender sender) : IEndpoint<CreateHabitRequest, IResult>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/habits", async (CreateHabitRequest payload) => await HandleAsync(payload))
            .Produces<CreateHabitResponse>(StatusCodes.Status201Created)
            .WithTags(nameof(Habit))
            .WithName("Create Basket");
    }

    public async Task<IResult> HandleAsync(
        CreateHabitRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var command = new CreateHabitCommand(request.Name, request.Description);

        var commandResult = await sender.Send(command, cancellationToken);
        if (commandResult.IsFailure)
        {
            return commandResult.ToTypedHttpResult();
        }

        var response = new CreateHabitResponse(commandResult.Value!.Id);

        return TypedResults.Created($"{EndpointConfig.BaseApiPath}/habits/{response.Id}", response);
    }
}

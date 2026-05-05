using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharedKernel.Endpoint;

namespace HabieEntry.Api;

public sealed class HabitEntryGroup : IEndpointGroup
{
    public void MapGroupEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGroup("/habit-entries").WithTags("HabitEntries");
    }
}

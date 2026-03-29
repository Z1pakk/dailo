using Dailo.Infrastructure.User;
using Habit.Infrastructure;
using Identity.Infrastructure;
using Scalar.AspNetCore;
using SharedKernel.Endpoint;
using Tag.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddUserServices();
builder.Services.AddHttpClient();

builder
    .Services.AddHabitModule(builder.Configuration)
    .AddTagModule(builder.Configuration)
    .AddIdentityModule(builder.Configuration);

builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(
        "/scalar",
        opt =>
        {
            opt.WithTitle("Dailo Requests Documentation");
        }
    );
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();

await app.RunAsync();

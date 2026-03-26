using Dailo.Infrastructure.User;
using Habit.Infrastructure;
using Identity.Infrastructure;
using Tag.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddUserServices();
builder.Services.AddHttpClient();

builder
    .Services.AddHabitModule(builder.Configuration)
    .AddTagModule(builder.Configuration)
    .AddIdentityModule(builder.Configuration);

builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

await app.RunAsync();

using Habit.Api;
using Habit.Application;
using Habit.Application.Features.CreateHabit;
using Habit.Application.Persistence;
using Habit.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Endpoint;

namespace Habit.Infrastructure;

public static class Setup
{
    public const string HabitDbConnectionString = "HabitPostgresConnectionString";

    public static IServiceCollection AddHabitModule(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString(HabitDbConnectionString);

        services.AddDbContext<IHabitDbContext, HabitDbDbContext>(opt =>
            opt.UseNpgsql(
                    connectionString,
                    b =>
                        b.MigrationsAssembly(AssemblyReference.Assembly)
                            .MigrationsHistoryTable(
                                HistoryRepository.DefaultTableName,
                                HabitSchema.NAME
                            )
                )
                .UseSnakeCaseNamingConvention()
        );

        services.AddEndpoints(assemblies: HabitApiRoot.Assembly);

        services.AddMediator(opt =>
        {
            opt.Assemblies = [typeof(IHabitApplicationRoot).Assembly];
        });

        return services;
    }
}

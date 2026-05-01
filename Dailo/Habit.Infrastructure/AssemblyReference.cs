using System.Reflection;
using Habit.Infrastructure.Database;

namespace Habit.Infrastructure;

internal static class AssemblyReference
{
    internal static Assembly Assembly => typeof(HabitDbContext).Assembly;
}

using System.Reflection;
using HabitEntry.Infrastructure.Database;

namespace HabitEntry.Infrastructure;

internal static class AssemblyReference
{
    internal static Assembly Assembly => typeof(HabitEntryDbContext).Assembly;
}

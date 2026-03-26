using System.Reflection;
using Tag.Infrastructure.Database;

namespace Tag.Infrastructure;

internal static class AssemblyReference
{
    internal static Assembly Assembly => typeof(TagDbContext).Assembly;
}

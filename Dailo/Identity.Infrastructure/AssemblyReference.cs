using System.Reflection;
using Identity.Infrastructure.Database;

namespace Identity.Infrastructure;

internal static class AssemblyReference
{
    internal static Assembly Assembly => typeof(IdentityDbContext).Assembly;
}

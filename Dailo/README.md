### NuGet Packages

```bash
# Install dotnet-outdated tool
dotnet tool install -g dotnet-outdated-tool

# Update all outdated NuGet packages
dotnet outdated -u
```

### Database

Commands are run directly from the `Habit.Infrastructure` project. The `HabitDbContextDesignFactory` is used automatically for design-time context creation.

```bash
# Create migration (run from Habit.Infrastructure)
cd .\Habit.Infrastructure\
dotnet ef migrations add <MigrationName>
```

### Identity Database

Commands are run directly from the `Identity.Infrastructure` project. The `IdentityDbContextDesignFactory` is used automatically for design-time context creation.

```bash
# Create migration (run from Identity.Infrastructure)
cd .\Identity.Infrastructure\
dotnet ef migrations add <MigrationName>
```

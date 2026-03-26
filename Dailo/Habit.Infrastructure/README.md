### Database

```bash
# Create migration
dotnet ef migrations add InitialCreate --context HabitDbDbContext -o Database/Migrations


# Update database
dotnet ef database update Dailo.Api --context HabitDbDbContext

# Remove last migration
dotnet ef migrations remove Dailo.Api --context HabitDbDbContext
```
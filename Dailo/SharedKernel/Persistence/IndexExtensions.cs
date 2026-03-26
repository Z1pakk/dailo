using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SharedKernel.Persistence;

public static class IndexExtensions
{
    private const string PostgreSqlProvider = "Npgsql.EntityFrameworkCore.PostgreSQL";
    private const string SqlServerProvider = "Microsoft.EntityFrameworkCore.SqlServer";

    /// <summary>
    /// Adds a database-independent filtered index for soft delete (IsDeleted = false).
    /// Automatically detects the database provider and applies the correct syntax.
    /// </summary>
    public static IndexBuilder<TEntity> HasSoftDeleteFilter<TEntity>(
        this IndexBuilder<TEntity> indexBuilder
    )
    {
        var providerName = GetDatabaseProvider(indexBuilder);

        var filter = providerName switch
        {
            PostgreSqlProvider => "\"is_deleted\" = false",
            SqlServerProvider => "[IsDeleted] = 0",
            _ => "\"is_deleted\" = false", // Default to PostgreSQL
        };

        return indexBuilder.HasFilter(filter);
    }

    private static string? GetDatabaseProvider<TEntity>(IndexBuilder<TEntity> indexBuilder)
    {
        // Get the model from the index metadata
        var model = indexBuilder.Metadata.DeclaringEntityType.Model;

        // Look for provider-specific annotations
        var providerAnnotation = model
            .GetAnnotations()
            .FirstOrDefault(a => a.Name.Contains("Npgsql") || a.Name.Contains("SqlServer"));

        if (providerAnnotation != null && providerAnnotation.Name.Contains("Npgsql"))
        {
            return PostgreSqlProvider;
        }

        if (providerAnnotation != null && providerAnnotation.Name.Contains("SqlServer"))
        {
            return SqlServerProvider;
        }

        // Check for snake_case naming convention as a heuristic for PostgreSQL
        var tableName = indexBuilder.Metadata.DeclaringEntityType.GetTableName();
        if (!string.IsNullOrEmpty(tableName) && tableName.Contains('_'))
        {
            return PostgreSqlProvider;
        }

        return null;
    }
}

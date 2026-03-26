using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace SharedKernel.Persistence.Conventions;

public sealed class TablePluralizationConvention : IModelFinalizingConvention
{
    public void ProcessModelFinalizing(
        IConventionModelBuilder modelBuilder,
        IConventionContext<IConventionModelBuilder> context
    )
    {
        foreach (var entityType in modelBuilder.Metadata.GetEntityTypes())
        {
            // Skip if table name is explicitly configured
            if (
                entityType.GetTableName() != null
                || entityType.GetTableNameConfigurationSource() == ConfigurationSource.Explicit
                || entityType.GetTableNameConfigurationSource()
                    == ConfigurationSource.DataAnnotation
            )
            {
                continue;
            }

            // Get the entity name and pluralize it using Humanizer
            var entityName = entityType.ClrType.Name;
            var pluralizedName = entityName.Pluralize(inputIsKnownToBeSingular: false);

            entityType.SetTableName(pluralizedName);
        }
    }
}

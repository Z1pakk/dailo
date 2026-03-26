using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Persistence;

namespace Tag.Infrastructure.Database.Configurations;

internal sealed class TagConfiguration : BaseEntityConfiguration<Domain.Entities.Tag>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Domain.Entities.Tag> builder)
    {
        builder.Property(t => t.Name).HasMaxLength(100);
        builder.Property(t => t.Description).HasMaxLength(2000);
    }
}

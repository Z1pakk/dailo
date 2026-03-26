using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Entity;

namespace SharedKernel.Persistence;

public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IEntity, IAuditableEntity, ISoftDeletableEntity, IEntityVersion
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id).ValueGeneratedOnAdd();

        builder.Property(b => b.IsDeleted).HasDefaultValue(false);

        builder.Property(b => b.Version).IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();

        builder.HasIndex(b => b.CreatedAtUtc);

        builder.HasIndex(b => b.IsDeleted).HasSoftDeleteFilter();

        ConfigureEntity(builder);
    }

    protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
}

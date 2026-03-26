using Humanizer;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Persistence;

namespace Identity.Infrastructure.Database.Configurations;

internal sealed class UserRoleConfiguration : BaseEntityConfiguration<UserRole>
{
    protected override void ConfigureEntity(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable(nameof(UserRole).Pluralize(false));
    }
}

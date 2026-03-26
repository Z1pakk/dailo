using Microsoft.AspNetCore.Identity;
using SharedKernel.Entity;

namespace Identity.Domain.Entities;

public class Role
    : IdentityRole<Guid>,
        IEntity,
        IEntityVersion,
        IAuditableEntity,
        ISoftDeletableEntity
{
    public DateTime CreatedAtUtc { get; set; }
    public Guid? CreatedByUserId { get; set; }
    public DateTime? LastModifiedAtUtc { get; set; }
    public Guid? LastModifiedByUserId { get; set; }

    public bool IsDeleted { get; set; }

    public Guid Version { get; set; }
}

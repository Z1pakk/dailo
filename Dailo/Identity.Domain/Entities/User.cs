using Microsoft.AspNetCore.Identity;
using SharedKernel.Entity;

namespace Identity.Domain.Entities;

public class User
    : IdentityUser<Guid>,
        IEntity,
        IEntityVersion,
        IAuditableEntity,
        ISoftDeletableEntity
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }

    public DateTime CreatedAtUtc { get; set; }
    public Guid? CreatedByUserId { get; set; }
    public DateTime? LastModifiedAtUtc { get; set; }
    public Guid? LastModifiedByUserId { get; set; }

    // Soft-deleting
    public bool IsDeleted { get; set; }

    // Concurrency check
    public Guid Version { get; set; }
}

namespace SharedKernel.Entity;

public abstract class BaseEntity : IEntity, IEntityVersion, IAuditableEntity, ISoftDeletableEntity
{
    public required Guid Id { get; set; }

    // Auditing
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public Guid? CreatedByUserId { get; set; }

    public DateTime? LastModifiedAtUtc { get; set; }

    public Guid? LastModifiedByUserId { get; set; }

    // Soft deleting
    public bool IsDeleted { get; set; }

    // Concurrency check
    public Guid Version { get; set; } = Guid.NewGuid();
}

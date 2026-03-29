namespace SharedKernel.Entity;

public abstract class BaseEntity<T>
    : IEntity<T>,
        IEntityVersion,
        IAuditableEntity,
        ISoftDeletableEntity
{
    public required T Id { get; set; }

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

using SharedKernel.Entity;

namespace Tag.Domain.Entities;

public sealed class Tag : BaseEntity
{
    public Guid UserId { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }
}

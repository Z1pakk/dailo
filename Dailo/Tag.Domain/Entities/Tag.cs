using StrictId;

namespace Tag.Domain.Entities;

public sealed class Tag : BaseEntity<Id<Tag>>
{
    public Guid UserId { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }
}

using Microsoft.EntityFrameworkCore;
using SharedKernel.Persistence;

namespace Tag.Application.Persistence;

public interface ITagDbContext : IAppDbContextBase
{
    DbSet<Domain.Entities.Tag> Tags { get; }
}

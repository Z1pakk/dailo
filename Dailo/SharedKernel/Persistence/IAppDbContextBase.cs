namespace SharedKernel.Persistence;

public interface IAppDbContextBase
{
    string Schema { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

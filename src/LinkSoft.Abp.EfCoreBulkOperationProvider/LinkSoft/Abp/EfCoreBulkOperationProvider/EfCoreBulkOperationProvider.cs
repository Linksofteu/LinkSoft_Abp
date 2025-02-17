using EFCore.BulkExtensions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace LinkSoft.Abp.EfCoreBulkOperationProvider;

public class EfCoreBulkOperationProvider : IEfCoreBulkOperationProvider, ITransientDependency
{
    public async Task InsertManyAsync<TDbContext, TEntity>(IEfCoreRepository<TEntity> repository,
        IEnumerable<TEntity> entities, bool autoSave,
        CancellationToken cancellationToken) where TDbContext : IEfCoreDbContext where TEntity : class, IEntity
    {
        await (await repository.GetDbContextAsync()).BulkInsertAsync(entities, cancellationToken: cancellationToken);
    }

    public async Task UpdateManyAsync<TDbContext, TEntity>(IEfCoreRepository<TEntity> repository,
        IEnumerable<TEntity> entities, bool autoSave,
        CancellationToken cancellationToken) where TDbContext : IEfCoreDbContext where TEntity : class, IEntity
    {
        await (await repository.GetDbContextAsync()).BulkUpdateAsync(entities, cancellationToken: cancellationToken);
    }

    public async Task DeleteManyAsync<TDbContext, TEntity>(IEfCoreRepository<TEntity> repository,
        IEnumerable<TEntity> entities, bool autoSave,
        CancellationToken cancellationToken) where TDbContext : IEfCoreDbContext where TEntity : class, IEntity
    {
        await (await repository.GetDbContextAsync()).BulkDeleteAsync(entities, cancellationToken: cancellationToken);
    }
}
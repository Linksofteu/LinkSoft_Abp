using EFCore.BulkExtensions;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace CAS.Combime.Framework.EntityFrameworkCore.Providers;

public class EfCoreBulkOperationExtendedProvider : EfCoreBulkOperationProvider, IEfCoreBulkOperationExtendedProvider
{
    public async Task InsertOrUpdateManyAsync<TDbContext, TEntity>(IEfCoreRepository<TEntity> repository,
        IEnumerable<TEntity> entities, bool autoSave,
        CancellationToken cancellationToken) where TDbContext : IEfCoreDbContext where TEntity : class, IEntity
    {
        await (await repository.GetDbContextAsync()).BulkInsertOrUpdateAsync(entities,
            cancellationToken: cancellationToken);
    }

    public async Task InsertOrUpdateOrDeleteManyAsync<TDbContext, TEntity>(IEfCoreRepository<TEntity> repository,
        IEnumerable<TEntity> entities, bool autoSave,
        CancellationToken cancellationToken) where TDbContext : IEfCoreDbContext where TEntity : class, IEntity
    {
        await (await repository.GetDbContextAsync()).BulkInsertOrUpdateOrDeleteAsync(entities,
            cancellationToken: cancellationToken);
    }   
}
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace LinkSoft.Abp.EfCoreBulkOperationProvider.Abstractions;

public interface IEfCoreBulkOperationExtendedProvider
{
    Task InsertOrUpdateManyAsync<TDbContext, TEntity>(IEfCoreRepository<TEntity> repository,
        IEnumerable<TEntity> entities, bool autoSave,
        CancellationToken cancellationToken) where TDbContext : IEfCoreDbContext where TEntity : class, IEntity;

    Task InsertOrUpdateOrDeleteManyAsync<TDbContext, TEntity>(IEfCoreRepository<TEntity> repository,
        IEnumerable<TEntity> entities, bool autoSave,
        CancellationToken cancellationToken) where TDbContext : IEfCoreDbContext where TEntity : class, IEntity;
}
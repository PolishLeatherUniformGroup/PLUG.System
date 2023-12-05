using System.Linq.Expressions;

namespace ONPA.Common.Application;

public interface IReadOnlyRepository<TEntity> where TEntity:class
{
    Task<TEntity?> ReadSingleById(Guid id, CancellationToken cancellationToken);
    
    Task<IEnumerable<TEntity>> ReadMany(int page = 0, int pageSize = 20,
        CancellationToken cancellationToken = new());

    Task<IEnumerable<TEntity>> ManyByFilter(Expression<Func<TEntity, bool>> filter, int page = 0, int pageSize = 20,
        CancellationToken cancellationToken = new());
}
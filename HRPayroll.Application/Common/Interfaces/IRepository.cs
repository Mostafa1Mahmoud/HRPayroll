using HRPayroll.Domain.Common;
using System.Linq.Expressions;

namespace HRPayroll.Application.Common.Interfaces
{
    public interface IRepository<T> where T : AuditableEntity
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        T? GetById(Guid id, CancellationToken cancellationToken = default);
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
        List<T> GetAll(CancellationToken cancellationToken = default);
        Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        List<T> Get(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        bool Exists(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        void Add(T entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<T> entity, CancellationToken cancellationToken = default);
        void AddRange(IEnumerable<T> entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity);
        void Update(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entities);
        void UpdateRange(IEnumerable<T> entities);
        Task RemoveAsync(T entity);
        void Remove(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
        void RemoveRange(IEnumerable<T> entities);
    }
}

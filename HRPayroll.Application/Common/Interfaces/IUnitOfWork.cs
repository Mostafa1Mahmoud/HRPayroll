using HRPayroll.Domain.Common;

namespace HRPayroll.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges(CancellationToken cancellationToken = default);
        Task<IRepository<TRepo>> GetRepositoryAsync<TRepo>() where TRepo : AuditableEntity;
        IRepository<TRepo> GetRepository<TRepo>() where TRepo : AuditableEntity;
    }
}

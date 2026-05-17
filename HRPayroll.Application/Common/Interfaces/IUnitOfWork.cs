namespace HRPayroll.Application.Common.Interfaces
{
    public interface IUnitOfWork<T> where T : class
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

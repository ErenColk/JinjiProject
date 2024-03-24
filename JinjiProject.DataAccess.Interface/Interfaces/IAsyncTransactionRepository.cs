using Microsoft.EntityFrameworkCore.Storage;

namespace JinjiProject.DataAccess.Interface.Interfaces
{
    public interface IAsyncTransactionRepository
    {
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task<IExecutionStrategy> CreateExecutionStrategy();
    }
}

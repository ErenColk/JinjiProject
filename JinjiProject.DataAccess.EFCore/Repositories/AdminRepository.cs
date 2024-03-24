using JinjiProject.Core.Entities.Concrete;
using JinjiProject.DataAccess.Interface.Repositories;
using JinjiProject.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.DataAccess.EFCore.Repositories
{
    public class AdminRepository : BaseRepository<Admin> , IAdminRepository
    {
        private readonly AppDbContext appDbContext;

        public AdminRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return appDbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public Task<IExecutionStrategy> CreateExecutionStrategy()
        {
            return Task.FromResult(appDbContext.Database.CreateExecutionStrategy());
        }

        public async Task<Admin?> GetByIdentityIdAsync(string identityId)
        {
            return await appDbContext.Admins.Where(x => x.AppUserId == identityId).FirstOrDefaultAsync();
        }
    }
}

using JinjiProject.Core.Entities.Concrete;
using JinjiProject.DataAccess.Interface.Repositories;
using JinjiProject.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
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
        protected DbSet<Admin> _adminTable;

        public AdminRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Admin?> GetByIdentityIdAsync(string identityId)
        {
            return await _adminTable.Where(x => x.AppUserId == identityId).FirstOrDefaultAsync();
        }
    }
}

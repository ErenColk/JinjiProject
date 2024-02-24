using JinjiProject.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.DataAccess.Interface.Repositories
{
    public interface IAdminRepository : IBaseRepository<Admin>
    {
        Task<Admin?> GetByIdentityIdAsync(string identityId);
    }
}

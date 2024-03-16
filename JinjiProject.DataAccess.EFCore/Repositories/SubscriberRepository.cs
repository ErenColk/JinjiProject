using JinjiProject.Core.Entities.Concrete;
using JinjiProject.DataAccess.Interface.Repositories;
using JinjiProject.DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.DataAccess.EFCore.Repositories
{
    public class SubscriberRepository : BaseRepository<Subscriber>,ISubscriberRepository
    {
        public SubscriberRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
